# 17.1 Oficios, XP y calidad

## Nivel y XP por oficio (OF-04)
- Cada personaje lleva un `ProfessionSkill` (nivel + XP) **por cada `ProfessionId`**. Las profesiones
  *aprendidas* (`LearnedProfessions`, máx. 2) siguen mandando sobre qué se puede fabricar; el nivel
  existe para todas para no perder el progreso si se cambia de oficio.
- Se gana XP **al terminar** cada fabricación (la XP la fija la receta). Recetas muy por debajo del
  nivel del jugador dan menos XP (gris = 10 %, verde = 50 %, amarillo/naranja = 100 %), como el color
  de dificultad que ya usa `LevelDifficulty`.
- **Curva configurable:** `ProfessionCurve` es una tabla `nivel → XP necesaria para el siguiente`
  más el nivel máximo (por defecto 20). Viene de datos (`ProfessionCurveAsset`); el valor por defecto
  del núcleo se genera con `base · nivel^exponente` (40 · n^1,5) solo como semilla del asset.
- **Hitos de oficio** (`ProfessionMilestones`, también datos): qué abre cada nivel.

| Nivel | Desbloquea | Bonus acumulado |
|---|---|---|
| 1 | Materiales tier 1, recetas básicas | — |
| 5 | Materiales tier 2 | −10 % tiempo de fabricación |
| 10 | Materiales tier 3, puede leer tratados | −20 % tiempo, +1 calidad máx. |
| 15 | Materiales tier 4 | −25 % tiempo, +2 calidad máx. |
| 20 | Recetas maestras (libros completos) | −30 % tiempo, +3 calidad máx. |

Además, cada nivel suma +1 a la **puntuación de calidad** (ver abajo).

## Calidad del objeto (OF-05)
Seis grados, de peor a mejor. La calidad la tiene cada `ItemInstance` fabricada o hallada (la que no
tiene calidad, como los materiales, cuenta como «sin calidad»).

| Grado | Nombre | Huecos de pieza | Multiplicador de stats | Multiplicador de durabilidad |
|---|---|---|---|---|
| 1 | Tosca | 1 | ×0,85 | ×0,8 |
| 2 | Corriente | 1 | ×1,00 | ×1,0 |
| 3 | Buena | 2 | ×1,10 | ×1,15 |
| 4 | Fina | 2 | ×1,20 | ×1,3 |
| 5 | Magistral | 3 | ×1,35 | ×1,5 |
| 6 | Legendaria | 4 | ×1,50 | ×1,8 |

**Cálculo** (`QualityRoller`, estrategia inyectable, constantes en `QualityRules` = datos):

```
puntos = nivelOficio × 1
       + tierMedioMateriales × 3          (media ponderada por cantidad de los ingredientes)
       + nivelEstación × 2
       + bonusPiezas                       (p. ej. yunque de acero +3, tijeras de maestro +2)
       + aleatorio[-2, +3]                 (IRandom con semilla)
grado = umbral(puntos)   → 0–7 Tosca · 8–13 Corriente · 14–20 Buena · 21–28 Fina · 29–36 Magistral · 37+ Legendaria
grado = min(grado, calidadMáxima)        calidadMáxima = 3 + bonus de hitos (+1/+2/+3) + bonus de pieza
```

Así un herrero de nivel 1 con hierro en una fragua básica saca casi siempre Tosca/Corriente, y uno de
nivel 18 con acero templado, fragua nivel 3 y yunque de acero roza la Legendaria. El jugador **nota**
el progreso en tres palancas: subir el oficio, conseguir mejores materiales y mejorar la estación.

## Reglas y casos límite
- Las recetas de consumibles (comida, pociones) también tienen calidad: multiplica la potencia del
  efecto (`ConsumableEffect`) en lugar de dar huecos.
- Los materiales intermedios (lingotes, cuero curtido) **no** tienen calidad: su tier es fijo.
- Reparar (`Core.Repair`) no cambia la calidad. Desmontar devuelve los mods instalados (OF-10).
- Bajar de nivel no existe; cambiar de oficio aprendido conserva el nivel del anterior.
