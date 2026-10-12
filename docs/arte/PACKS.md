# Packs de arte de terceros (fase AR)

Los packs **no se suben a git** (repo público + licencias que prohíben redistribuirlos, y ~450 MB).
Cada copia local los rellena una vez; todo lo que se genera a partir de ellos también es local.

| Carpeta local (ignorada) | Qué hay |
|---|---|
| `Assets/_Project/Art/ThirdParty/` | FBX y texturas copiados de los packs |
| `ThirdPartyPackages/` | `.unitypackage` de Kevin Iglesias (opcionales) |
| `Assets/Kevin Iglesias/` | Lo que instalan esos paquetes |
| `Assets/_Project/Art/Generado/` | Materiales, AnimatorControllers y prefabs generados |
| `Assets/_Project/Data/Arte/` | `CatalogoArte` (kit → prefab, enemigo → prefab, profesión → banco) |

Licencias y autores: [`Assets/_Project/Art/CREDITOS.md`](../../Assets/_Project/Art/CREDITOS.md).

## 1. Copiar los packs
```powershell
powershell -ExecutionPolicy Bypass -File tools/assets/copiar_assets.ps1 -Origen "C:\ruta\Assets_pack"
```
Solo copia lo que usa Unity (FBX para Unity, texturas y normales OpenGL); ignora Godot, OBJ y glTF.

## 2. Importar en Unity
Al abrir el proyecto, `ThirdPartyImportRules` aplica las reglas automáticamente:
- Personajes, atuendos, bestiario y librerías de animación → **Humanoid** (comparten el esqueleto de Quaternius).
- Clips `*_Loop` en bucle; todos sin desplazamiento de raíz (el movimiento lo hace el código).
- `_Normal` como mapa normal; `_ORM` y `_Roughness` lineales.

Opcional: **SPECIMEN › Arte › 1 · Importar paquetes de animación (Kevin Iglesias)**.

## 3. Generar
**SPECIMEN › Arte › 2 · Generar prefabs y catálogo** (fases AR-02…AR-07) crea materiales URP con las
texturas del pack, los AnimatorControllers y los prefabs, y rellena `CatalogoArte` y `CatalogoVisual`.

## 4. Probar: Campo de pruebas
**SPECIMEN › ▶ Probar Campo de pruebas** genera el arte si falta, crea `Scenes/CampoPruebas` y entra en Play.
(Para rehacer solo la escena: **SPECIMEN › Crear escena Campo de pruebas**.)

| Qué probar | Cómo | Debe pasar |
|---|---|---|
| Movimiento y animación | WASD, Shift, Espacio, rueda | Andar/trotar/esprintar con blend; voltereta al esquivar |
| Kits de clase | 1-6 o Tab | Cambia modelo, arma, stats, recurso y ataque |
| Melé | Clic izq. / J con Bastión, Vigía o Duelista | Animación de espada; daña a los enemigos del arco delantero |
| Hechizos | Clic izq. / J con Oráculo, Druida o Arcanista | Proyectil del color de su elemento; gasta recurso |
| Firma | Q | Escudo, impulso, curación, área, torbellino u orbe según el kit |
| Bestiario | Acércate al norte | Persiguen, amagan y golpean; barra de vida sobre la cabeza |
| Botín | Mata criaturas | Cae botín; al pisarlo va a «Botín» en el HUD |
| Profesiones | E junto a un banco (muros este/oeste) | Elige profesión y muestra sus sinergias |
| Muerte | F1 varias veces | Animación de muerte; R reaparece y reinicia, F2 cura/revive |

Si algo no se ve: consola de Unity (errores en rojo) y los avisos `[SPECIMEN]` del generador.
Sin arte generado todo funciona igual en greybox (cápsulas y cajas).

## Ajustes a mano habituales
- **Arma torcida**: abre `Art/Generado/Personajes/Personaje_<Kit>.prefab` y rota/mueve el hijo `Arma`
  (está bajo el hueso `hand_r`). Se pierde al regenerar: si vale para todos, dímelo y lo fijo en `GripSolver`.
- **Clipping del atuendo Campesino**: es la versión gratuita del pack (sin cuerpo recortado).
- **Más animaciones de ataque**: importa los paquetes de Kevin Iglesias (menú 1) y vuelve a generar.
