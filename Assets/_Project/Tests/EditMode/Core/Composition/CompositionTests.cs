using System;
using NUnit.Framework;
using Specimen.Core.Composition;
using Specimen.Core.Events;

namespace Specimen.Tests.Core.Composition
{
    public class CompositionTests
    {
        private sealed class FakeLoader : ISceneLoader
        {
            public SceneId? Last;
            public void Load(SceneId scene) => Last = scene;
        }

        private sealed class A { }
        private sealed class B { public B(A a) { Dep = a; } public A Dep { get; } }

        [Test]
        public void Resolve_ReturnsRegisteredInstance()
        {
            var c = new ServiceContainer();
            var a = new A();
            c.Register(a);
            Assert.That(c.Resolve<A>(), Is.SameAs(a));
        }

        [Test]
        public void Factory_IsLazyAndSingleInstanceAndCanUseOtherServices()
        {
            var c = new ServiceContainer();
            int built = 0;
            c.RegisterFactory(r => { built++; return new B(r.Resolve<A>()); });
            c.Register(new A());
            Assert.That(built, Is.Zero);
            var first = c.Resolve<B>();
            Assert.That(c.Resolve<B>(), Is.SameAs(first));
            Assert.That(built, Is.EqualTo(1));
            Assert.That(first.Dep, Is.SameAs(c.Resolve<A>()));
        }

        [Test]
        public void Resolve_Unknown_ThrowsAndTryResolveReturnsFalse()
        {
            var c = new ServiceContainer();
            Assert.Throws<InvalidOperationException>(() => c.Resolve<A>());
            Assert.That(c.TryResolve<A>(out var a), Is.False);
            Assert.That(a, Is.Null);
        }

        [Test]
        public void Register_Twice_Throws()
        {
            var c = new ServiceContainer();
            c.Register(new A());
            Assert.Throws<InvalidOperationException>(() => c.Register(new A()));
            Assert.Throws<InvalidOperationException>(() => c.RegisterFactory(_ => new A()));
        }

        [Test]
        public void Factory_WithCircularDependency_Throws()
        {
            var c = new ServiceContainer();
            c.RegisterFactory(r => { r.Resolve<B>(); return new A(); });
            c.RegisterFactory(r => new B(r.Resolve<A>()));
            Assert.Throws<InvalidOperationException>(() => c.Resolve<A>());
        }

        [Test]
        public void Build_RegistersBusLoaderAndSceneFlow()
        {
            var loader = new FakeLoader();
            var c = GameComposition.Build(loader);
            Assert.That(c.Resolve<IEventBus>(), Is.Not.Null);
            Assert.That(c.Resolve<ISceneLoader>(), Is.SameAs(loader));
            Assert.That(c.Resolve<SceneFlow>().Current, Is.EqualTo(SceneId.Boot));
        }

        [Test]
        public void SceneFlow_ValidTransition_LoadsAndPublishes()
        {
            var loader = new FakeLoader();
            var c = GameComposition.Build(loader);
            SceneChanged? seen = null;
            c.Resolve<IEventBus>().Subscribe<SceneChanged>(e => seen = e);
            var flow = c.Resolve<SceneFlow>();

            Assert.That(flow.GoTo(SceneId.Ascensor), Is.True);

            Assert.That(loader.Last, Is.EqualTo(SceneId.Ascensor));
            Assert.That(flow.Current, Is.EqualTo(SceneId.Ascensor));
            Assert.That(seen.Value.From, Is.EqualTo(SceneId.Boot));
            Assert.That(seen.Value.To, Is.EqualTo(SceneId.Ascensor));
        }

        [Test]
        public void SceneFlow_InvalidTransition_IsRejectedWithoutEffects()
        {
            var loader = new FakeLoader();
            var flow = GameComposition.Build(loader).Resolve<SceneFlow>();
            Assert.That(flow.GoTo(SceneId.Incursion), Is.False);
            Assert.That(flow.Current, Is.EqualTo(SceneId.Boot));
            Assert.That(loader.Last, Is.Null);
        }

        [Test]
        public void SceneFlow_ElevatorIsTheHub()
        {
            var flow = GameComposition.Build(new FakeLoader()).Resolve<SceneFlow>();
            flow.GoTo(SceneId.Ascensor);
            Assert.That(flow.GoTo(SceneId.Armeria), Is.True);
            Assert.That(flow.GoTo(SceneId.Laboratorio), Is.False);
            Assert.That(flow.GoTo(SceneId.Ascensor), Is.True);
            Assert.That(flow.GoTo(SceneId.Incursion), Is.True);
        }
    }
}
