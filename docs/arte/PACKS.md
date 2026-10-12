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
