# 17.2 Materiales, etiquetas y recetas

## Materiales e ítems (OF-02)
`ItemDefinition` se amplía **sin romper** los constructores actuales (parámetros opcionales):

| Campo | Tipo | Notas |
|---|---|---|
| `Id`, `Name`, `Category`, `Rarity`, `BaseDurability`, `MaxStack` | ya existen | — |
| `Tier` | `int` 0–4 | 0 = sin tier (equipo, consumibles); 1–4 para materiales |
| `Weight` | `float` ≥ 0 (kg) | para la carga del inventario y para los mods de peso |
| `Tags` | `MaterialTags` (flags) | `Madera, Metal, Piedra, Cuero, Tela, Hierba, Comida, Hueso, Mineral, Combustible, Vidrio` |
| `IconKey` | `string` | clave del icono (lo resuelve Runtime, como `VisualId`) |

- `MaterialTags` es un enum `[Flags]` con **valores explícitos y estables** (se serializan).
- Un ítem puede tener varias etiquetas (el carbón es `Mineral | Combustible`).
- En Unity: `ItemDefinitionAsset : ScriptableObject` con esos campos + `Sprite` del icono (OF-14).

## Ingredientes por objeto o por etiqueta (OF-03)
`IngredientSpec` sustituye internamente a `RecipeIngredient` (que se conserva como atajo «por objeto»):

```
IngredientSpec.ForItem("lingote_hierro", 2)
IngredientSpec.ForTag(MaterialTags.Madera, 3, minTier: 2)   // «3 de cualquier madera de tier ≥ 2»
```

- **Consumo por etiqueta** (`TagConsumptionPlanner`): primero calcula un plan sin tocar el inventario
  (qué pilas y cuántas unidades), luego `CraftTransaction` lo aplica de forma atómica como hoy.
- **Orden del plan:** gasta antes lo de **menor tier** que cumpla y, a igual tier, la pila con menos
  unidades (no quema el roble si hay pino). Se puede **fijar** un ítem concreto para una etiqueta desde
  la UI (`IngredientChoice`), útil para subir la calidad a propósito.
- El tier medio que alimenta la calidad se calcula **con el plan real** (lo que de verdad se gasta).
- Dos specs no pueden pedir lo mismo: un `ForItem` y un `ForTag` que lo incluya se resuelven primero
  el concreto y luego la etiqueta con lo que queda.

## Definición de receta (OF-06)
`RecipeDefinition` (núcleo, inmutable) — en Unity `RecipeDefinitionAsset`:

| Campo | Ejemplo |
|---|---|
| `Id` | `herrero.espada_acero` |
| `Inputs` | `IngredientSpec[]` (objeto o etiqueta + cantidad + tier mínimo) |
| `Outputs` | `RecipeOutput[]` (ítem + cantidad; el principal recibe la calidad) |
| `Station` / `MinStationLevel` | `StationType.Fragua` / 2 |
| `RequiredParts` | `["pieza.crisol"]` (piezas que deben estar colocadas en la estación) |
| `Profession` / `MinProfessionLevel` | `Herrero` / 10 |
| `CraftSeconds` | 45 (antes de bonus de oficio y pieza) |
| `Xp` | 30 |
| `Unlock` | `UnlockCondition` (ver abajo) |
| `QualityCap` | opcional: algunas recetas toscas nunca pasan de Corriente |

`UnlockCondition` (estrategias pequeñas, combinables con «Y»):
- `AlwaysKnown` — receta de partida del oficio.
- `KnowledgeRequired(knowledgeId)` — hay que haber leído ese pergamino/plano/tratado (OF-07).
- `CollectionComplete(libroId)` — todos los volúmenes del libro de oficio leídos.
- `StationPartRequired` no es condición de *conocer* sino de *poder fabricar aquí*: va en `RequiredParts`.

**Variantes mejoradas:** una receta puede declarar `Improves = "herrero.espada_hierro"`. Si se conoce la
variante, la estación muestra la mejorada en su lugar (misma salida con más tier/calidad máxima o menos
coste), y la básica queda en «otras recetas».

## Compatibilidad con lo que ya existe
- `Recipe` y `DefaultRecipes` actuales se convierten con `RecipeDefinition.FromLegacy(recipe)`:
  estación = la principal del oficio, nivel 1, `AlwaysKnown`, tiempo 0 (instantánea). El `Crafter`
  actual pasa a ser un caso particular (fabricación instantánea) y sus tests siguen en verde.
- `SynergyRecipe` gana el mismo trato: sigue exigiendo las dos profesiones en el grupo.
