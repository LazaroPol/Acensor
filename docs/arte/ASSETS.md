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

## Cómo probar (prototipo greybox)
Requisitos: **Unity 6000.0.23f1** (Unity Hub) y el repo clonado.
1. Unity Hub › *Add project from disk* › carpeta raíz del repo. La primera importación tarda unos minutos.
2. Si no existe `Assets/_Project/Scenes/Prototipo/Prototipo.unity`: menú **SPECIMEN › Crear escena
   Prototipo** (crea la escena, el `CatalogoVisual` vacío y la añade a *Build Settings*).
3. Abre la escena `Prototipo` y pulsa **Play**. La consola no debe mostrar errores rojos.

Qué debe verse:
- Una **sala de suelo gris** en cuadrícula de 1 m, rodeada de **muros grises** de 3 m y una
  **puerta marrón** en el centro del muro sur.
- El **jugador**: cápsula azul de 1,8 m en el centro de la sala; la cámara lo sigue desde arriba
  en diagonal (tercera persona cenital-oblicua).
- Arriba a la izquierda, el **HUD de texto**: Vida, recurso de clase, Aguante, estado vital,
  `Fase <nombre> <mm:ss>` y la ayuda `F1 dañar · F2 curar · F3 avanzar tiempo`.

Controles y resultado esperado:
| Entrada | Teclado | Mando | Debe pasar |
|---|---|---|---|
| Mover | WASD / flechas | Stick izquierdo | Se mueve relativo a la cámara (~4 m/s) |
| Sprint | Shift izq. | Pulsar stick izq. | Más rápido (×1,6) y baja el Aguante; al soltar se recupera |
| Dañar | F1 | — | Baja la Vida; a 0 cambia el estado vital |
| Curar | F2 | — | Sube la Vida sin pasar del máximo |
| Avanzar tiempo | F3 | — | El temporizador de fase baja; al llegar a 0 indica que ha expirado |

Si algo no coincide, apúntalo (qué hiciste, qué viste, captura de la consola) y pásamelo.

## Checklist de importación (cuando entregues assets)
Por cada asset, antes de asignarlo:
- [ ] Está en su carpeta (`Art/Models/<Categoria>/`…) y con nombre `Categoria_Nombre_Variante`.
- [ ] Origen y licencia anotados en `Assets/_Project/Art/CREDITOS.md`.
- [ ] Import Settings del modelo: *Scale Factor* 1 y *Convert Units* activado; tras arrastrarlo a la
      escena mide lo esperado (personaje ≈ 1,8 m, muro 1 m de ancho o múltiplo).
- [ ] Pivote en pies/base (personajes, objetos) o esquina inferior (muros); +Z hacia delante.
- [ ] Rotación y escala del transform raíz a 0/1 (si no, corregir en el exportador, no en Unity).
- [ ] Personajes: *Rig › Animation Type* = **Humanoid** y el avatar se configura sin errores.
- [ ] Materiales en URP (sin rosa/magenta); texturas con *sRGB* solo en color, normal maps como *Normal map*.
- [ ] Prefab creado en `Art/Prefabs/<Categoria>/` sin scripts de juego (solo malla, materiales, animator).
- [ ] Asignado en `Data/Visuals/CatalogoVisual` al `VisualId` correcto (`Jugador`, `Suelo`, `Muro`,
      `Puerta`, `Ascensor`, `Loot`, `Enemigo`).
- [ ] Play en `Prototipo`: sustituye al greybox, alineado con la cuadrícula y sin flotar ni hundirse.
