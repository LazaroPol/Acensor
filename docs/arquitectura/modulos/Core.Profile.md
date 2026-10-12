<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Profile`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Profile`
Tests: `Assets/_Project/Tests/EditMode/Core/Profile`

**Depende de:** [Core.Dna](Core.Dna.md), [Core.Lore](Core.Lore.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `IProfileStore.cs` | interface `IProfileStore` | Almacén de texto del perfil (fichero, nube…). |
| `InMemoryProfileStore.cs` | class `InMemoryProfileStore` | Almacén en memoria, para tests y para el prototipo. |
| `ProfileData.cs` | class `ProfileData` | Estado persistente del perfil entre iteraciones: puntos de ADN, mutaciones y fragmentos de memoria. |
| `ProfileFormatException.cs` | class `ProfileFormatException` | El texto guardado no es un perfil válido. |
| `ProfileSerializer.cs` | class `ProfileSerializer` | Formato de texto versionado, una clave por línea: v1 , points=N , node=Id , fragment=Id . |
| `ProfileService.cs` | class `ProfileService` | Guarda y carga el perfil (ADN + fragmentos) en un . |
