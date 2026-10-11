# Assets: cómo entregarlos y cómo se usan

El juego no referencia modelos directamente: cada cosa visible tiene un **ID lógico** (`VisualId`) y el
`CatalogoVisual` (ScriptableObject en `Assets/_Project/Data/Visuals/`) dice qué prefab usar. Si un ID
no tiene prefab, se dibuja un cubo/cápsula greybox. Detalle técnico: [GDD 16](../gdd/16-prototipo-greybox.md).

## Dónde van
| Tipo | Carpeta |
|---|---|
| Modelos (`.fbx`, `.glb`) | `Assets/_Project/Art/Models/<Categoria>/` |
| Texturas | `Assets/_Project/Art/Textures/<Categoria>/` |
| Materiales | `Assets/_Project/Art/Materials/` |
| Animaciones | `Assets/_Project/Art/Animations/<Categoria>/` |
| Prefabs listos para el catálogo | `Assets/_Project/Art/Prefabs/<Categoria>/` |
| Audio | `Assets/_Project/Art/Audio/<Categoria>/` |

Categorías: `Personajes`, `Entorno`, `Ascensor`, `Objetos`, `Enemigos`, `UI`.

## Convenciones
- Escala: **1 unidad = 1 metro**. Personaje ≈ 1,8 m. Muros modulares de 1 × 1 m o múltiplos (2, 4).
- Pivote: personajes y objetos en los **pies/base**; muros en la esquina inferior.
- Eje: +Z hacia delante, Y arriba (al exportar desde Blender: "Forward -Z, Up Y" o FBX "Apply Transform").
- Nombres: `Categoria_Nombre_Variante` (ej. `Entorno_Muro_Roto01`). Sin espacios ni tildes.
- Personajes humanoides: rig compatible con **Humanoid** de Unity.
- Materiales en URP (Lit o el shader toon de F10). Estilo en [GDD 13](../gdd/13-direccion-artistica.md).
- Licencia: indica el origen y la licencia de cada pack en `Assets/_Project/Art/CREDITOS.md`.

## Cómo pasármelos
1. Súbelos al repo en las carpetas de arriba (o adjúntalos en el chat / en una carpeta conectada).
2. Dime qué ID lógico es cada uno (ej. "este es el `Jugador`, estos tres son `Muro`").
3. Crearé el prefab si hace falta y lo asignaré en `CatalogoVisual`; el código no cambia.
