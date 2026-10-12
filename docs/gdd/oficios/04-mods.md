# 17.4 Modificación de equipo (OF-10)

## Modelo
- `ItemInstance` gana `Quality` (opcional) y `ModSlots`: el número de huecos sale de la calidad
  (§17.1). Un objeto sin calidad (hallado antiguo, material) no admite piezas.
- `ModDefinition` (núcleo) — en Unity `ModDefinitionAsset`:

| Campo | Ejemplo |
|---|---|
| `Id` | `mod.esquirlas_metal` |
| `SlotType` | `Filo`, `Cabeza`, `Empuñadura`, `Refuerzo`, `Forro`, `Accesorio` |
| `CompatibleTags` | tags del **objeto base**: `Arma`, `ArmaContundente`, `Herramienta`, `Armadura`, `Madera`, `Metal`… |
| `Modifiers` | lista de `StatModifier` (plano o %) sobre los stats de equipo |
| `Requirements` | nivel de oficio para instalarlo, calidad mínima del objeto base |
| `Removable` | si al desinstalar vuelve a la bolsa (por defecto sí) o se destruye (p. ej. resinas) |

- Stats de equipo afectados (`GearStat`): `DañoCortante`, `DañoContundente`, `DañoPerforante`,
  `Durabilidad`, `Velocidad`, `Peso`, `Armadura`. Los tres daños se suman al daño del arma de
  `WeaponSpec`; el combate puede usarlos después para debilidades del bestiario.

## Reglas de instalación (`ModInstaller`)
1. el objeto tiene un hueco libre (huecos = calidad);
2. el `SlotType` no está repetido salvo que el objeto lo permita (límite **1 por tipo** por defecto;
   `Accesorio` admite 2);
3. alguna `CompatibleTags` del mod coincide con las del objeto base;
4. se cumplen los `Requirements`.

Resultado: `ModInstallResult` (`Installed`, `NoFreeSlot`, `SlotTypeTaken`, `Incompatible`,
`RequirementsNotMet`, `ItemBroken`). Desinstalar devuelve el mod a la bolsa si cabe (si no, falla sin
cambios: atómico como `CraftTransaction`).

## Stats efectivos (`ModdedStats`)
```
efectivo = (base × multiplicadorCalidad + Σ planos de mods) × (1 + Σ % de mods)
durabilidadMáx = baseRareza × multDurabilidadCalidad × (1 + Σ % durabilidad)   // nunca < 1
```
La UI del objeto muestra base → efectivo con el mismo verde/rojo que ya usa `GearDelta`.

## Ejemplos (los de la petición y algunos más)

| Base | Mod | Hueco | Efecto |
|---|---|---|---|
| Palo de madera | Esquirlas de metal | Cabeza | +4 cortante, −20 % durabilidad |
| Palo de madera | Piedra atada | Cabeza | +5 contundente, +1,5 kg peso, −5 % velocidad |
| Palo de madera | Clavo | Cabeza | +3 perforante |
| Cualquier arma | Tiras de cuero | Empuñadura | +8 % velocidad, +10 % durabilidad |
| Arma de metal | Muela (afilado fino) | Filo | +15 % cortante |
| Arma contundente | Contrapeso de plomo | Cabeza | +20 % contundente, −10 % velocidad |
| Lanza / flecha | Punta de hueso | Filo | +4 perforante |
| Armadura | Placas cosidas | Refuerzo | +6 armadura, +2 kg |
| Armadura | Forro acolchado | Forro | +3 armadura, +10 % durabilidad |
| Herramienta | Mango de fresno | Empuñadura | +15 % velocidad (también al recolectar) |

## Cómo se consiguen
- **Botín:** las tablas de `Core.Loot` por anillo de profundidad incluyen mods (más raros más abajo).
- **Fabricación:** cada oficio tiene recetas de mods (§17.6): el Herrero hace esquirlas y clavos, el
  Sastre tiras y forros, el Cantero piedras talladas, el Carpintero mangos, el Alquimista resinas.
