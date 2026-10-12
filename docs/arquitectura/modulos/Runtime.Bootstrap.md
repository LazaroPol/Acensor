<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Bootstrap`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Bootstrap`
Tests: (sin carpeta propia)

**Depende de:** [Core.Composition](Core.Composition.md)
**Lo usan:** [Runtime.Atico](Runtime.Atico.md), [Runtime.Hubs](Runtime.Hubs.md), [Runtime.Incursion](Runtime.Incursion.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `GameBootstrap.cs` | class `GameBootstrap` | Punto de entrada único: antes de cargar cualquier escena crea el contenedor de dependencias y lo deja accesible a las escenas. |
| `UnitySceneLoader.cs` | class `UnitySceneLoader` | Adaptador de sobre SceneManager (el nombre de la escena es el del enum). |
