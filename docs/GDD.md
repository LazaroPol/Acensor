# SPECIMEN — Documento de Diseño (GDD fusionado)

> Nombre provisional. Fusión de tres conceptos: **Fans of Labyrinth's** (supervivencia día/noche, asedio, clones y ADN), **El Ascensor** (subida continua con temporizador, armería inicial con stock limitado, hogueras) y **La Torre** (pisos temáticos, antesala, profesiones, Ecos, actos).

**Género:** Roguelite cooperativo (1-4 jugadores) · Extraction ARPG · Defensa de base
**Motor:** Unity 6 (URP) · C# · Netcode for GameObjects (fase posterior)
**Perspectiva:** Tercera persona, cámara cenital-oblicua (estilo Risk of Rain 2 / V Rising)

---

## 1. Concepto en una frase

Sois clones despertados en el sótano de una **torre-laberinto**. Un **ascensor** es vuestro único refugio: sube piso a piso con un temporizador implacable. En cada piso, las puertas se abren y salís a saquear un laberinto cada vez más profundo y peligroso; si no volvéis antes de que el ascensor parta, os quedáis atrapados. En la cima os espera el **asedio final** y la **extracción**… y la verdad sobre lo que sois.

## 2. Cómo se fusionan los tres conceptos

| Elemento | Fans of Labyrinth's | El Ascensor | La Torre | **Resultado en SPECIMEN** |
|---|---|---|---|---|
| Refugio | Pueblo seguro | Ascensor | Antesala | **El Ascensor-Refugio**: base móvil que se mejora y donde se craftea, cocina y vota |
| Ciclo | Día prepara / noche explora | Timer → sube | Antesala 5 min → Prueba | **Calma** (puertas cerradas, preparación) → **Incursión** (puertas abiertas, timer) → **Ascenso** |
| Riesgo | Profundidad del laberinto | Pasillos por nivel | Tipos de piso | Cada piso tiene **anillos de profundidad**: más lejos = mejor loot y peores enemigos |
| Castigo | Atrapado de día | Te deja el ascensor | — | **Atrapado**: enemigos con +velocidad y +visión; solo queda buscar el **Hueco de Emergencia** |
| Inicio | Despiertas en cama | Apareces desnudo en la armería | Base de la torre | **Piso 0 – La Armería**: despiertas en una cápsula y eliges equipo con **stock limitado** |
| Clases | Árbol ADN libre | Clase = equipo elegido | 6 clases con movilidad | **Kit inicial** (6 arquetipos) + **ADN híbrido** permanente |
| Final | Asedio noche 4 + Osprey | — | 5 actos, jefe por acto | **Asedio en la Azotea** + extracción en Osprey + muestra de sangre |
| Lore | Experimento con clones | — | Diarios con tu letra, Observadores | Los diarios son de **clones anteriores de ti**; los Observadores son el laboratorio |
| Decisiones | Voto de puertas | — | Ecos | **Ecos**: decisiones votadas que alteran pisos futuros y el final |

## 3. Bucle de juego

### 3.1 Bucle de partida (una "Iteración")
```
[Laboratorio (menú)] → [Piso 0: Armería] → { [Calma] → [Incursión] → [Ascenso] } × N pisos → [Asedio en la Azotea] → [Extracción] → [Laboratorio]
```
- **Duración objetivo:** 45-60 min por iteración. Un **Acto** = 5 pisos + jefe (vertical slice: Acto I).
- **Calma (Antesala):** 3 min. Ascensor cerrado, zona segura. Craftear, cocinar, reparar, repartir, leer el **Presagio** del piso, votar Ecos. Si todos marcan *Listos* antes, se gana el bonus **Prisa** (+% loot del piso proporcional al tiempo ahorrado).
- **Incursión:** 6-8 min. Las puertas se abren a un piso con pasillos generados proceduralmente en **anillos de profundidad** (1-3). Un aviso a los 60 s y a los 15 s.
- **Ascenso:** las puertas se cierran. Quien esté dentro sube. Quien no, queda **Atrapado**.
- **Voto de cierre anticipado:** con mayoría, el grupo puede cerrar las puertas antes (huida de una amenaza).

### 3.2 Estado *Atrapado*
- Enemigos: ×1,5 velocidad, ×2 radio de visión. Paleta hiperbrillante y expuesta.
- Objetivo: alcanzar el **Hueco de Emergencia** del piso (aparece en el anillo más profundo) y trepar hasta el ascensor en el siguiente piso. Si se consigue, se reincorpora con fatiga extra.

### 3.3 Muerte
- El jugador caído pasa a **Espectro**: puede activar mecanismos y marcar enemigos.
- Se le revive en el ascensor durante la Calma (coste de recursos) o en campo con un objeto.
- Si el ascensor parte con un Espectro fuera, ese clon **termina su iteración** (vuelve al Laboratorio con lo acumulado).
- Si todos caen, fin de la iteración.

### 3.4 Asedio en la Azotea (clímax)
- Tras el último piso, el ascensor llega a la azotea. Las criaturas "rompen su instinto" y suben en oleadas.
- Defensa estilo 7 Days to Die: fortificar con lo crafteado, torretas del ingeniero, tótems del escultor.
- Sobrevivir X oleadas → llega el **Osprey**. Al subir: felicitación fría y **muestra de sangre** → sueño → Laboratorio.

### 3.5 Puntuación de la iteración
```
Puntos = (Bajas × valor) + Σ(calidad del loot extraído) + bonus de pisos completados
Multiplicador de Supervivencia Colectiva = 1 + 0,1 × (jugadores vivos al final de cada piso, acumulado)
Puntos de ADN = floor(Puntos × Multiplicador / constante)
```
Morir no anula los puntos: se calculan hasta el momento de la muerte (sin multiplicador final).

## 4. Piso 0 — La Armería

- Despertar en cápsulas, sin equipo. Mentor NPC (**Ilse**, "superviviente" veterana) explica las reglas en la primera partida.
- Armarios, mesas y estanterías con **stock limitado por tipo** (p. ej. 10 escudos pesados por servidor/iteración en multijugador; en coop de 4, máx. 1 kit de clase duplicado).
- Lo que coges define tu **arquetipo inicial**:

| Rol | Kit | Recurso | Rasgo de exploración |
|---|---|---|---|
| Tanque | **Bastión** (escudo pesado) | Furia | Empuja bloques, aguanta trampas |
| Tanque | **Vigía** (ágil, esquivas) | Concentración | Trepar |
| Apoyo | **Oráculo** (sanación directa) | Maná | Ve trampas y runas ocultas |
| Apoyo | **Druida de Ceniza** (área, invocaciones) | Esencia | Planear en caídas |
| DPS | **Duelista** (cuerpo a cuerpo) | Energía | Doble salto y dash |
| DPS | **Arcanista** (distancia) | Maná | Teletransporte corto, activa mecanismos a distancia |

- Además, cada jugador elige **una profesión** de un banco de trabajo de la Armería (ver §7).

## 5. Recursos del jugador

- **Vida**, **Recurso de clase**, **Stamina** (correr, trepar, esquivar, nadar).
- **Fatiga**: se acumula por piso (más si quedas atrapado). Reduce la stamina máxima. Se recupera con la **Hoguera** del ascensor y comidas especiales.

## 6. El Ascensor-Refugio

- Plataforma con **ranuras de módulo** que se construyen con materiales: Hoguera (regeneración + buffs), Forja, Telar, Cocina, Alambique, Banco de ingeniero, Peana de escultor, Litera (revivir).
- Los módulos persisten durante la iteración; algunos pueden **subir de nivel**.
- Visualmente es el "hogar": paleta cálida de cómic.

## 7. Profesiones

Cada profesión tiene tres usos: **Preparación**, **Combate**, **Exploración/puzzles**.

| Profesión | Preparación | Combate | Exploración |
|---|---|---|---|
| Herrero | Armas, armaduras, reparar | Afilar (buff daño) | Forjar llaves |
| Sastre | Armadura ligera, resistencias | Vendas de emergencia | Capas de planeo, bolsas |
| Cocinero | Comidas de buff largo | Raciones de stamina | Cebos, platos para NPCs |
| Químico | Pociones, antídotos | Bombas, ácido, humo | Disolver obstáculos |
| Ingeniero | Gadgets, munición | Torretas, trampas | Reactivar mecanismos |
| Manitas | Reparar equipo encontrado | Sabotear máquinas | Forzar cerraduras, atajos |
| Escultor | Tótems con auras | Señuelos de piedra | Restaurar estatuas/murales |

**Sinergias** (receta solo disponible si el grupo tiene ambas): Químico+Cocinero → Festín alquímico · Herrero+Ingeniero → Armas mecánicas · Sastre+Químico → Telas ignífugas · Escultor+Ingeniero → Gólem · Manitas+Ingeniero → Hackear torretas · Escultor+Químico → Estatuas explosivas · Herrero+Sastre → Armadura mixta.

**Caminos alternativos:** una puerta sellada se abre con Manitas (forzar), Químico (disolver) o Escultor (restaurar estatua). Ninguna profesión es obligatoria.

**Durabilidad:** todo el equipo se desgasta → Herrero, Sastre y Manitas siempre útiles.

## 8. Pisos

**Presagio:** al llegar, inscripción/sonido/acertijo que anuncia tipo y **modificadores** (oscuridad, gravedad baja, enemigos ígneos…).

| Tipo | Qué pone a prueba |
|---|---|
| Saqueo (base) | Exploración, riesgo/recompensa por anillos |
| Jefe | Roles, coordinación |
| Oleadas | Control, posicionamiento, torretas |
| Laberinto de trampas | Movilidad, stamina |
| Puzzle | Comunicación, profesiones |
| Decisión | Ecos, moral |
| Santuario | Mercader, forja especial, recuperar fatiga |
| Profesión | Variedad de kit |
| Híbrido | Todo a la vez |

**Pisos destacados:** El Salón de los Espejos · La Biblioteca Inundada · El Banquete · La Forja Dormida · El Juicio.

**Generación procedural:** cada piso se genera con semilla (reproducible). Muros y trampas cambian entre iteraciones. Los **materiales** varían por piso (planificación).

## 9. Ecos (decisiones con consecuencias)

- Decisiones importantes se votan en la Calma (mayoría; empate → decide el que tenga más Fragmentos de Memoria).
- Algunas son **secretas e individuales** y se revelan pisos después.
- Ejemplos: liberar al prisionero o quedarse el tesoro · romper un sello · sacrificar la profesión de un compañero · elegir entre dos escaleras (rutas).
- Los Ecos alteran pisos, diálogos y el **final**.

## 10. Estructura por actos

| Acto | Pisos | Tema | Revelación |
|---|---|---|---|
| I. El Despertar | 1-5 | Mecánicas básicas | No sois los primeros |
| II. Los Ecos | 6-10 | Decisiones | Los diarios tienen vuestra letra |
| III. La Fractura | 11-15 | La torre cambia | Quiénes son los Observadores |
| IV. El Precio | 16-20 | Sacrificios, rutas | Qué quiere el laboratorio |
| V. La Cima | 21-25 + Azotea | Todo mezclado | Final según Ecos |

Una iteración recorre **un acto** (en la primera, el Acto I). Completar un acto desbloquea empezar la siguiente iteración en el acto siguiente.

**Pisos ocultos:** solo con ciertas combinaciones de profesiones o Ecos; dan Fragmentos extra.

## 11. Lore — "El Proyecto Specimen"

- **Despertar:** amnesia. Ilse, la mentora, explica las reglas y da esperanza.
- **Restos de grupos anteriores:** equipo roto, diarios, cadáveres… Los diarios están escritos **con tu letra**: son de clones anteriores de ti.
- **Los Observadores:** el personal del laboratorio, que evalúa lealtad, sacrificio, ingenio. Nunca se dejan ver del todo.
- **Revelación tras la 1ª iteración:** eres un clon. El menú principal es el **Laboratorio Genético**.
- **Fragmentos de memoria:** coleccionables que reconstruyen la historia.
- **Giro final (según Ecos):** la torre es un filtro que busca a quien la gobierne / sustituya al Director; o Ilse es la primera Specimen.

## 12. Meta-progresión — Árbol de ADN híbrido

- Puntos de ADN por iteración (ver §3.5).
- **Ramas:** Resistencia (tanque), Agilidad, Vitalidad (apoyo), Potencia (daño), Cognición (profesiones/puzzles).
- **Sin restricciones de clase:** se combinan mutaciones de cualquier rama.
- **Tier Lock:** los nodos de nivel N requieren haber invertido ≥ umbral de puntos en la rama (p. ej. Tier 2: 5 pts, Tier 3: 12, Tier 4: 20, Tier 5: 30).
- **Reliquias:** objetos únicos con ventaja + desventaja, ligados a Ecos; algunas se pueden "secuenciar" en el laboratorio.

## 13. Dirección artística — "Dualidad cromática"

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

## 14. Arquitectura técnica

- **Núcleo de dominio puro** (`Specimen.Core`, sin referencias a `UnityEngine`): reglas, estados, cálculos. 100 % testeable con NUnit, tanto en Unity Test Runner como con `dotnet test` en CI sin licencia de Unity.
- **Capa de presentación** (`Specimen.Runtime`): MonoBehaviours finos que adaptan el núcleo (patrón Humble Object).
- **Principios SOLID**: interfaces pequeñas (`IClock`, `IRandom`, `IEventBus`), inyección por constructor, estrategias para tipos de piso, datos en ScriptableObjects mapeados a modelos del núcleo.
- **Determinismo**: aleatoriedad inyectada con semilla para generación procedural reproducible y tests.
- **Multijugador** (fase tardía): Netcode for GameObjects, servidor autoritativo; el núcleo puro facilita la simulación en host.

## 15. Alcance del Vertical Slice (objetivo)

Acto I completo (5 pisos + jefe), 6 kits, 4 profesiones (Herrero, Cocinero, Químico, Ingeniero), Ascensor con 4 módulos, Asedio de 5 oleadas, extracción, árbol de ADN con 2 ramas, coop local/online de 2-4 jugadores.
