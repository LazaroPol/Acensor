# 17. Oficios: artesanía, conocimiento y modificación de equipo (fase OF)

Objetivo: que fabricar sea una **vía de progreso** que se nota partida a partida. Se sube de nivel en
un oficio fabricando, se encuentran planos y piezas de taller que abren recetas mejores, la calidad de
lo fabricado crece y el equipo se personaliza con piezas añadibles. Mecánica inspirada en los survival
de fabricación por estaciones (referencia: 7 Days to Die), **con estética medieval y contenido propio**:
no se copian nombres, textos, valores ni assets del juego de referencia.

Se construye **sobre lo que ya existe**: las 7 profesiones de `Core.Professions` (§7), `ItemDefinition`,
`ItemInstance`, `Inventory`, `Recipe`/`Crafter` de `Core.Crafting`, las sinergias de grupo y el botín por
anillos de `Core.Loot`. Nada de esto se tira: se amplía y lo antiguo sigue funcionando.

## Equivalencias de diseño

| Mecánica de referencia | Adaptación medieval de SPECIMEN |
|---|---|
| Esquemas y revistas | Pergaminos, planos, tratados y libros de oficio (por volúmenes) |
| Herramientas/piezas de estación | Crisol, fuelle, yunque, muela de afilar, olla de hierro, alambique, mortero… |
| Mods de armas y herramientas | Piezas añadibles: esquirlas de metal, piedra atada, clavos, tiras de cuero… |
| Tiers de calidad | Calidad (Tosca → Legendaria) que fija los huecos de pieza disponibles |
| Estaciones de trabajo | Fragua, hogar de cocina, mesa de alquimia, taller de sastre, banco de cantero… |

## Nombre visible de cada oficio (el `ProfessionId` no cambia)

| `ProfessionId` | Oficio visible | Estación principal | Combustible |
|---|---|---|---|
| `Herrero` | Herrero | Fragua | Sí |
| `Sastre` | Sastre y curtidor | Taller de sastre | No |
| `Cocinero` | Cocinero | Hogar de cocina | Sí |
| `Quimico` | Alquimista | Mesa de alquimia | Sí (alambique) |
| `Ingeniero` | Maestro de ingenios | Taller de ingenios | No |
| `Manitas` | Carpintero | Banco de carpintero | No |
| `Escultor` | Cantero | Banco de cantero | No |

## Principios técnicos
- **Núcleo puro** (`Core/Crafting`, `Core/Items`, `Core/Professions` y módulos nuevos `Core/Crafting.*`):
  sin `UnityEngine`, testeado con NUnit, ficheros ≤ 120 líneas.
- **Datos, no código:** curvas de XP, umbrales de calidad, recetas, materiales, piezas y mods son datos.
  En Unity se editan como **ScriptableObjects** (`Runtime/Crafting/Authoring`) que se convierten a las
  definiciones del núcleo; el catálogo por defecto en C# sirve para tests y para generar los assets.
- **Ids estables** (texto, `ItemIds`-style): son lo que se guarda en partidas; nunca se renombran.
- **Determinismo:** la calidad usa `IRandom` inyectado (semilla) para poder testearla.

## Páginas de detalle
1. [Oficios, XP y calidad](oficios/01-progreso-y-calidad.md)
2. [Materiales, etiquetas y recetas](oficios/02-materiales-y-recetas.md)
3. [Estaciones, cola y desbloqueos](oficios/03-estaciones-y-desbloqueo.md)
4. [Modificación de equipo](oficios/04-mods.md)
5. [Contenido: materiales, piezas y mods](oficios/05-contenido-materiales.md)
6. [Contenido: recetario por oficio y conocimientos](oficios/06-contenido-recetas.md)
7. [Datos en Unity, guardado e integración](oficios/07-datos-guardado-unity.md)

## Criterios de aceptación de la fase → tareas

| Criterio | Se cumple en |
|---|---|
| Craftear una receta en una estación consumiendo materiales por etiqueta | OF-03, OF-08, OF-09, OF-15 |
| Una receta bloqueada aparece disponible tras usar su plano o tratado | OF-07, OF-08, OF-16 |
| Una pieza colocada en una estación habilita recetas nuevas | OF-08, OF-15 |
| Añadir y quitar mods en un arma, con efecto visible en sus stats | OF-10, OF-16 |
| Todo se guarda y se carga sin perder progreso | OF-13, OF-16 |

La guía de prueba manual en Unity (OF-16) recorre los cinco criterios paso a paso en el Campo de pruebas.
