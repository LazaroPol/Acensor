<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Mods`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Mods`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Mods`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `DefaultGearTags.cs` | class `DefaultGearTags` | Etiquetas derivadas de la categoría (Arma, Armadura, Herramienta) y del material (Madera, Metal), más las extra que se declaren por id (Arma |
| `DefaultMods.cs` | class `DefaultMods` | Mods de ejemplo de §17.4 (Palo de madera y comunes). |
| `GearModifier.cs` | enum `GearModifierKind` | Cómo se aplica un modificador: suma plana o porcentaje sobre el total. |
| `GearModifier.cs` | struct `GearModifier` | Modificador de un mod sobre un stat de equipo. |
| `GearStat.cs` | enum `GearStat` | Stats de equipo que los mods modifican (GDD §17.4). |
| `GearStatSheet.cs` | class `GearStatSheet` | Valores de los de un objeto. |
| `GearTags.cs` | enum `GearTags` | Etiquetas del objeto base con las que un mod declara compatibilidad (combinables). |
| `IGearTagProvider.cs` | interface `IGearTagProvider` | Resuelve las etiquetas de equipo de un objeto base para comprobar compatibilidad de mods. |
| `ModDefinition.cs` | class `ModDefinition` | Pieza añadible a un objeto (GDD §17.4). |
| `ModInstallResult.cs` | enum `ModInstallResult` | Resultado de instalar un mod (GDD §17.4). |
| `ModInstallResult.cs` | enum `ModUninstallResult` | Resultado de retirar un mod. |
| `ModInstallValidator.cs` | class `ModInstallValidator` | Comprueba, en orden, las reglas de instalación de §17.4 sin cambiar nada. |
| `ModInstaller.cs` | class `ModInstaller` | Instala y retira mods moviéndolos entre la bolsa y el objeto. |
| `ModRequirements.cs` | class `ModRequirements` | Requisitos para instalar un mod: calidad mínima del objeto y, opcionalmente, nivel de oficio. |
| `ModSlotRules.cs` | class `ModSlotRules` | Límite de mods por tipo de hueco: 1 por defecto; Accesorio admite 2 (GDD §17.4). |
| `ModSlotType.cs` | enum `ModSlotType` | Tipo de hueco que ocupa un mod. |
| `ModdedItem.cs` | class `ModdedItem` | Instancia de equipo con sus mods instalados. |
| `ModdedStats.cs` | class `ModdedStats` | Stats efectivos (GDD §17.4): efectivo = (base × multCalidad + Σ planos) × (1 + Σ %), nunca negativo. |
| `ModdedStatsView.cs` | class `ModdedStatsView` | Stats base y efectivos de un objeto con mods, para la UI (base → efectivo). |
