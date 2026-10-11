<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Repair`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Repair`
Tests: `Assets/_Project/Tests/EditMode/Core/Repair`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `IRepairCatalog.cs` | interface `IRepairCatalog` | Consulta de reglas de reparación por categoría de objeto. |
| `RepairCatalog.cs` | class `RepairCatalog` | Catálogo en memoria con una regla por categoría. |
| `RepairReport.cs` | struct `RepairReport` | Qué pasó al reparar: resultado, material gastado y durabilidad recuperada. |
| `RepairResult.cs` | enum `RepairResult` | Resultado de un intento de reparación. |
| `RepairRule.cs` | class `RepairRule` | Cómo se repara una categoría de equipo: qué material consume y cuánta durabilidad devuelve cada unidad. |
| `RepairService.cs` | class `RepairService` | Repara equipo gastando materiales del inventario. |
