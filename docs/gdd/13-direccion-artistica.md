# 13. Dirección artística — "Dualidad cromática"

- **Estilo:** semirrealismo estilizado, contornos entintados.
- **Ascensor / Armería (seguro):** cómic vibrante y cálido (amarillos, ocres, madera). Hogar.
- **Pisos (peligro):** pastel desaturado y frío (azules pálidos, grises, violetas). Puntos focales bioluminiscentes y loot brillante. Niebla baja.
- **Atrapado:** pastel hiperbrillante, deslumbrante, expuesto.
- **Laboratorio (menú):** limpio, clínico, distópico, probetas y secuenciación en 3D.

### Implementación técnica en Unity (sustituye a UE5)
| UE5 (original) | Unity 6 |
|---|---|
| Lumen | URP + Adaptive Probe Volumes + luz direccional animada |
| Nanite | GPU Resident Drawer + LODs + modularidad de muros |
| Post-proceso adaptativo | **Volumes** con blend por zona (Ascensor / Piso / Atrapado) |
| Cel-shading custom | Shader Graph toon + **Renderer Feature** de contornos (normales/profundidad) |

### Estado de la implementación (F10-05)
- `Core.Art`: `MoodProfiles` (valores por ambiente), `MoodSelector` (ubicación/escena → ambiente) y `MoodTransition` (fundido).
- `Runtime.Art.MoodVolumeDriver` aplica el ambiente a un Volume global de URP, la niebla y el contorno.
- `Assets/_Project/Art/Shaders/SpecimenToon.shader` (`Specimen/Toon`): luz en escalones + contorno por casco invertido.
