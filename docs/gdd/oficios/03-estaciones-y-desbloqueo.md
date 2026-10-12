# 17.3 Estaciones, cola y desbloqueos

## Estaciones de trabajo (OF-08)
`Workstation` (núcleo) = tipo + nivel + ranuras + cola. Cada oficio tiene su estación principal (§17) y
algunas recetas usan la de otro oficio (p. ej. el Carpintero fabrica mangos en su banco que luego usa el
Herrero). El `BenchStation` actual del Campo de pruebas pasa a envolver un `Workstation`.

| Ranura | Para qué | Notas |
|---|---|---|
| Entrada (6) | materiales que la estación consume | si faltan, también tira de la bolsa del jugador |
| Salida (6) | resultados terminados | si se llena, la cola se detiene (no se pierde nada) |
| Combustible (1–2) | leña, carbón, hulla… | solo Fragua, Hogar de cocina y Mesa de alquimia |
| Piezas (3) | crisol, fuelle, olla de hierro… | `StationPart` físico: se coloca, se retira, se puede perder |

**Nivel de estación** (1–3), mejorable con materiales (`StationUpgrade`, datos):

| Nivel | Coste orientativo (Fragua) | Efecto |
|---|---|---|
| 1 | — (se construye con piedra y barro) | recetas de estación nivel 1, cola de 2 |
| 2 | 20 piedra labrada + 10 lingotes de hierro | recetas nivel 2, cola de 4, +2 puntos de calidad |
| 3 | 15 bloques de granito + 10 lingotes de acero + 1 plano | recetas nivel 3, cola de 6, +4 puntos de calidad |

**Qué recetas ofrece una estación** (`RecipeAvailability`, puro y testeable). Una receta aparece si:
1. su `Station` es este tipo, y `MinStationLevel ≤ nivel`;
2. el jugador la **conoce** (`UnlockCondition` cumplida en su `KnownRecipes`);
3. todas sus `RequiredParts` están colocadas en la estación.

Las que fallan solo por nivel de oficio o por materiales **se muestran en gris** con el motivo
(«Requiere Herrero 10», «Falta: 2× cualquier metal tier 3»). Las no conocidas no se muestran (sorpresa
al leer el plano). Cada motivo es un valor de `RecipeBlockReason` para que la UI lo traduzca.

## Cola de fabricación y combustible (OF-09)
- `CraftQueue` con temporizador (`ITickable`, `IClock` de `Core.Time`): al encolar se **reservan y
  retiran** los materiales; al cancelar un trabajo pendiente se devuelven íntegros.
- Tiempo real = `CraftSeconds × (1 − bonusOficio) × (1 − bonusPiezas)`, mínimo 1 s.
- Estaciones con combustible: cada unidad tiene `BurnSeconds` (leña 30 s, carbón 90 s, hulla 180 s).
  Sin combustible la cola **se pausa**, no se cancela. El fuelle reduce el consumo un 25 %.
- Al terminar un trabajo: tirada de calidad (OF-05) con los datos **del momento de encolar**, salida a
  las ranuras de salida y XP al oficio de quien lo encoló (guardado en el trabajo).
- La cola sigue avanzando aunque el jugador se aleje (como un horno), pero solo dentro de la partida.

## Desbloqueo por objetos hallados (OF-07 y OF-08)

| Vía | Objeto | Comportamiento |
|---|---|---|
| Conocimiento | **Pergamino** (común) | enseña 1 receta; se consume al leerlo |
| Conocimiento | **Plano** (poco común) | enseña una receta de equipo o de mejora de estación |
| Conocimiento | **Tratado** (raro) | enseña la **variante mejorada** de una receta o un grupo de 2–3 recetas |
| Conocimiento | **Libro de oficio** (por volúmenes, 5) | cada volumen da un bonus pequeño; los 5 enseñan una receta maestra |
| Equipamiento | **Pieza de estación** | colocada en la estación habilita o mejora recetas mientras esté puesta |

- `KnowledgeDefinition`: id, tipo, rareza, oficio, **nivel mínimo para entenderlo**, recetas que enseña.
- Leer: `KnowledgeReader.Read(item, skill, known)` → `Learned`, `AlreadyKnown` (no se consume),
  `TooLowLevel` (no se consume; tooltip «Necesitas Herrero 10 para entender este tratado»),
  `WrongProfession` (se puede leer igual si su oficio no está aprendido: queda apuntado para después).
- Lo aprendido vive en `KnownRecipes` del progreso del jugador y se guarda (OF-13).
- **Piezas de estación** (`StationPartDefinition`): ítem físico con estación compatible, recetas que
  habilita, bonus (tiempo, calidad, combustible) y durabilidad. Se gastan con el uso (1 punto por
  fabricación que las necesite); rotas dejan de contar hasta repararlas. Si la estación se destruye en
  un asedio, las piezas caen como botín al suelo (se pueden perder).

Ejemplos de la petición: «Tratado de forja de acero» → enseña `herrero.espada_acero`; un **crisol** en
la fragua habilita fundir lingotes de acero y aleaciones; una **olla de hierro** en el hogar mejora los
guisos (habilita los guisos tier 2 y +2 de calidad a las sopas).
