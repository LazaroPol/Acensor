using System.Collections.Generic;
using Specimen.Core.Life;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Life
{
    internal sealed class FakeReviveCost : IReviveCost
    {
        public bool CanPay = true;
        public readonly List<ReviveMethod> Paid = new List<ReviveMethod>();

        public bool TryPay(ReviveMethod method)
        {
            if (!CanPay) return false;
            Paid.Add(method);
            return true;
        }
    }

    internal static class LifeTestKit
    {
        public static CharacterVitals Vitals()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 80f);
            return new CharacterVitals(sheet);
        }

        public static CharacterLife Life(CharacterVitals vitals, IReviveCost cost)
            => new CharacterLife(vitals, ReviveRules.Default, cost);

        public static CharacterLife Life() => Life(Vitals(), new FakeReviveCost());
    }
}
