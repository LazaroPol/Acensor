# Sesión 1 — Fase F0: Fundaciones del proyecto

- **Fecha:** 2026-10-11
- **Estado:** ✅ Cerrada (9/9 tareas, todas mergeadas con verificación en verde)
- **Progreso global al cierre:** 12,5 % (9/72 tareas)

## Objetivo
Dejar un repositorio Unity listo para crecer con seguridad: diseño unificado, arquitectura por capas, tests automáticos que bloquean el merge, backlog con progreso medible y CI.

## Decisiones tomadas
| Decisión | Motivo |
|---|---|
| **Fusión → SPECIMEN**: el pueblo, el ascensor y la antesala se convierten en el *Ascensor-Refugio*; día/noche pasa a Calma/Incursión por piso | Une los tres conceptos en un único bucle sin duplicar refugios |
| La muerte convierte al jugador en **Espectro**; solo termina su iteración si el ascensor parte sin revivirlo | Concilia la muerte dura (concepto 1) con el espíritu cooperativo (concepto 3) |
| Unity 6 + URP en lugar de UE5 (Lumen/Nanite → APV, Volumes, Renderer Feature de contornos) | Petición expresa; equivalencias documentadas en el GDD §13 |
| **Núcleo de dominio puro** (`Specimen.Core`, `noEngineReferences`) y MonoBehaviours finos | Testeable sin editor (Humble Object, DIP) |
| Tests ejecutables dentro de Unity (Test Runner) **y** con `dotnet` en CI | Mismas fuentes; CI sin licencia de Unity |
| NUnit vendorizado + `nuget.config` sin fuentes | Build reproducible y sin red |
| PCG32 propio en vez de `System.Random` | Misma secuencia por semilla en Mono, IL2CPP y .NET (multijugador y generación procedural) |
| PRs locales con `tools/pr.sh` | GitHub no estaba accesible en esta sesión; la puerta replica el flujo: verifica la rama, registra la PR, mergea `--no-ff`, re-verifica `main` y revierte si falla |

## Pull requests
| PR | Tarea | Resultado |
|---|---|---|
| [PR-001](../pull-requests/PR-001.md) | F0-01 Estructura Unity y puerta de merge | ✅ |
| [PR-002](../pull-requests/PR-002.md) | F0-02 Arnés de tests C# headless | ✅ |
| [PR-003](../pull-requests/PR-003.md) | F0-03 GDD fusionado | ✅ |
| [PR-004](../pull-requests/PR-004.md) | F0-04 Backlog y progreso | ✅ |
| [PR-005](../pull-requests/PR-005.md) | F0-05 CI GitHub Actions | ✅ |
| [PR-006](../pull-requests/PR-006.md) | F0-06 IClock / IRandom (PCG32) | ✅ |
| [PR-007](../pull-requests/PR-007.md) | F0-07 Bus de eventos | ✅ |
| [PR-008](../pull-requests/PR-008.md) | F0-08 Máquina de estados | ✅ |
| [PR-009](../pull-requests/PR-009.md) | F0-09 Historial de sesiones | ✅ |

## Calidad
- **40 tests C# (NUnit)** sobre el núcleo + **27 tests de arquitectura y herramientas (Python)**.
- Compilación con *warnings as errors* (detectó un campo sin uso en F0-07 antes del merge).
- Comprobado a propósito que la puerta **bloquea**: un `using UnityEngine` en el núcleo y un test C# en rojo impiden el merge.

## Incidencias
- `api.nuget.org` bloqueado en el entorno → resuelto vendorizando NUnit desde la release oficial de GitHub.
- Token de GitHub inválido → repositorio y PRs locales; pendiente de publicar en GitHub.

## Pendiente para la próxima sesión (Sesión 2 — F1 Bucle de iteración)
1. F1-01 Configuración de iteración · 2. F1-02 Temporizador con avisos · 3. F1-03 Máquina de estados de la iteración · 4. F1-04 Listos y bonus Prisa · 5. F1-05 Voto de cierre anticipado · 6. F1-06 Atrapado y Hueco de Emergencia · 7. F1-07 Fin de iteración.
