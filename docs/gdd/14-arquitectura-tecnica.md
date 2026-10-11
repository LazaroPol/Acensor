# 14. Arquitectura técnica

- **Núcleo de dominio puro** (`Specimen.Core`, sin referencias a `UnityEngine`): reglas, estados, cálculos. 100 % testeable con NUnit, tanto en Unity Test Runner como con `dotnet test` en CI sin licencia de Unity.
- **Capa de presentación** (`Specimen.Runtime`): MonoBehaviours finos que adaptan el núcleo (patrón Humble Object).
- **Principios SOLID**: interfaces pequeñas (`IClock`, `IRandom`, `IEventBus`), inyección por constructor, estrategias para tipos de piso, datos en ScriptableObjects mapeados a modelos del núcleo.
- **Determinismo**: aleatoriedad inyectada con semilla para generación procedural reproducible y tests.
- **Multijugador** (fase tardía): Netcode for GameObjects, servidor autoritativo; el núcleo puro facilita la simulación en host.
