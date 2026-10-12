# 17.7 Datos en Unity, guardado e integración

## Authoring con ScriptableObjects (OF-14)
Carpeta `Assets/_Project/Scripts/Runtime/Crafting/Authoring` (+ assets en `Assets/_Project/Data/Crafting`):

| Asset | Convierte a | Notas |
|---|---|---|
| `ItemDefinitionAsset` | `ItemDefinition` | id, nombre, `Sprite` icono, peso, stack máx., tier, tags, rareza, durabilidad |
| `RecipeDefinitionAsset` | `RecipeDefinition` | entradas/salidas (ítem o tag), estación, niveles, piezas, tiempo, XP, desbloqueo |
| `KnowledgeAsset` | `KnowledgeDefinition` | tipo, rareza, nivel mínimo, recetas que enseña |
| `StationPartAsset` | `StationPartDefinition` | estación, bonus, recetas que habilita |
| `ModDefinitionAsset` | `ModDefinition` | hueco, tags compatibles, modificadores, requisitos |
| `ProfessionCurveAsset` | `ProfessionCurve` + `ProfessionMilestones` | tabla de XP por nivel e hitos |
| `QualityRulesAsset` | `QualityRules` | pesos de la fórmula, umbrales y huecos por grado |
| `CraftingDatabase` | `ICraftingCatalog` | lista de todos los anteriores; un solo punto de entrada |

- La conversión vive en conversores pequeños (`*AssetMapper`), testeables en EditMode.
- **Validación** (menú *SPECIMEN › Oficios › Validar datos*): ids duplicados o vacíos, recetas que
  piden ítems inexistentes, conocimientos que enseñan recetas inexistentes, curvas no crecientes.
- **Generación inicial** (*SPECIMEN › Oficios › Generar assets por defecto*): crea los assets desde el
  catálogo C# de OF-11/OF-12, para no teclear 70 recetas a mano. Después, los datos mandan.
- El núcleo sigue sin conocer Unity: los tests usan el catálogo C#; el juego, la `CraftingDatabase`.

## Guardado y carga (OF-13)
`CraftingSave` = formato de texto versionado, una clave por línea (mismo estilo que `ProfileSerializer`):

```
oficios v1
skill=Herrero:7:340            # oficio:nivel:xp
known=herrero.espada_acero     # receta conocida
read=libro_forjador.vol2       # volumen leído / conocimiento consumido
station=fragua_aldea:2         # estación:nivel
part=fragua_aldea:0:pieza.crisol:37      # estación:ranura:ítem:durabilidad
slot=fragua_aldea:fuel:0:carbon_vegetal:4
job=fragua_aldea:herrero.espada_acero:12.5:Herrero:7:...   # trabajo en cola y segundos restantes
item=ab12:espada_hierro:4:120:mod.tiras_cuero,mod.clavo     # instancia:id:calidad:durabilidad:mods
inv=bolsa:3:ab12               # inventario:ranura:instancia | inv=bolsa:4:madera_pino:12 (pila)
```

- `ICraftingSaveStore` (texto) con implementación en memoria (tests) y en fichero/`PlayerPrefs` (Runtime).
- Round-trip probado: guardar → cargar → mismo estado (niveles, XP, recetas, estaciones, piezas,
  cola con su tiempo restante, inventario con calidad, durabilidad y mods de cada instancia).
- Versiones: un `v2` futuro migra desde `v1`; un fichero corrupto lanza `CraftingSaveFormatException`
  y el juego conserva una copia `.bak` en lugar de perder el progreso.
- El progreso de oficios es **del personaje** (persiste entre iteraciones como el perfil); el
  contenido de las estaciones del Ascensor-Refugio se guarda con el refugio.

## Integración en el Campo de pruebas (OF-15, OF-16)
- Cada `BenchStation` del poblado pasa a ser una estación completa: ventana con ranuras de entrada,
  salida, combustible y piezas, lista de recetas (disponibles / en gris con motivo), cola con barra
  de progreso y botón de mejorar estación.
- Clic derecho sobre un pergamino/plano/tratado en la bolsa → lo lee (mensaje en el HUD).
- Ventana de objeto: huecos de mod arrastrables, stats base → efectivos en verde/rojo.
- XP de oficio en el HUD con el color de profesión que ya existe (`XpSource`).
- Botín del poblado y de pisos: conocimientos, piezas y mods en las tablas por anillo (`Core.Loot`).
- Guardado automático al salir y al cerrar una ventana de estación; carga al entrar al Campo.
- `docs/arte/` gana la lista de iconos necesarios (un `IconKey` por ítem) para los packs del usuario.
