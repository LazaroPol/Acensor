# Backlog — SPECIMEN

Una fase = una sesión. Orden lógico de dependencias: cada fase se apoya en las anteriores.
Leyenda: ✅ hecha · 🔄 en curso · ⬜ pendiente

## F0 — Fundaciones del proyecto (Sesión 1)

_Repositorio, arquitectura por capas, tests y flujo de PRs_

- ✅ **F0-01** Estructura del proyecto Unity y puerta de merge — PR-001
- ✅ **F0-02** Arnés de tests C# headless (NUnit) — PR-002
- ✅ **F0-03** GDD fusionado — PR-003
- ✅ **F0-04** Backlog y sistema de progreso — PR-004
- ⬜ **F0-05** Integración continua (GitHub Actions)
- ⬜ **F0-06** Núcleo: abstracciones de tiempo y aleatoriedad (IClock, IRandom)
- ⬜ **F0-07** Núcleo: bus de eventos tipado
- ⬜ **F0-08** Núcleo: máquina de estados genérica
- ⬜ **F0-09** Historial de sesiones e informe de la Sesión 1

## F1 — Bucle de iteración (Sesión 2)

_Armería → Calma → Incursión → Ascenso → Asedio → Extracción_

- ⬜ **F1-01** Modelo de configuración de iteración (pisos, duraciones)
- ⬜ **F1-02** Temporizador de fase con avisos (60 s / 15 s)
- ⬜ **F1-03** Máquina de estados de la iteración
- ⬜ **F1-04** Sistema de 'Listos' y bonus Prisa
- ⬜ **F1-05** Voto de cierre anticipado de puertas
- ⬜ **F1-06** Estado Atrapado y Hueco de Emergencia
- ⬜ **F1-07** Fin de iteración (victoria, todos caídos, abandono)

## F2 — Personaje y recursos (Sesión 3)

_Stats, recursos, kits de clase y la Armería_

- ⬜ **F2-01** Atributos y modificadores (aditivos / multiplicativos)
- ⬜ **F2-02** Recursos: vida, recurso de clase, stamina
- ⬜ **F2-03** Fatiga entre pisos
- ⬜ **F2-04** Definición de los 6 kits de clase
- ⬜ **F2-05** Armería con stock limitado por tipo
- ⬜ **F2-06** Selección de profesión
- ⬜ **F2-07** Muerte, Espectro y revivir

## F3 — Inventario, loot y crafting (Sesión 4)

_Objetos, rareza, durabilidad y profesiones_

- ⬜ **F3-01** Modelo de objetos, rareza y durabilidad
- ⬜ **F3-02** Inventario con capacidad y apilado
- ⬜ **F3-03** Tablas de loot ponderadas por anillo de profundidad
- ⬜ **F3-04** Recetas y crafting por profesión
- ⬜ **F3-05** Recetas de sinergia entre profesiones
- ⬜ **F3-06** Reparación de equipo

## F4 — Ascensor-Refugio (Sesión 5)

_Base móvil con módulos construibles_

- ⬜ **F4-01** Ranuras de módulo y construcción con materiales
- ⬜ **F4-02** Mejora de módulos por niveles
- ⬜ **F4-03** Hoguera: regeneración y recuperación de fatiga
- ⬜ **F4-04** Cocina: buffs de comida temporales
- ⬜ **F4-05** Litera: revivir Espectros durante la Calma

## F5 — Generación de pisos (Sesión 6)

_Pisos procedurales con semilla, tipos y presagios_

- ⬜ **F5-01** Generador procedural de pasillos por anillos (con semilla)
- ⬜ **F5-02** Tipos de piso (patrón Strategy)
- ⬜ **F5-03** Presagio y modificadores de piso
- ⬜ **F5-04** Distribución de loot y enemigos por anillo
- ⬜ **F5-05** Caminos alternativos según profesión
- ⬜ **F5-06** Restos de clones anteriores y Fragmentos de memoria

## F6 — Combate e IA (Sesión 7)

_Daño, enemigos, percepción y jefe del Acto I_

- ⬜ **F6-01** Modelo de daño, resistencias y críticos
- ⬜ **F6-02** Enemigos con escalado por profundidad
- ⬜ **F6-03** Percepción de IA (visión/oído) y modificadores de Atrapado
- ⬜ **F6-04** Habilidades de clase y rasgos de movilidad
- ⬜ **F6-05** Jefe del Acto I con fases

## F7 — Ecos y decisiones (Sesión 8)

_Votaciones y consecuencias persistentes_

- ⬜ **F7-01** Sistema de votación (mayoría y desempate)
- ⬜ **F7-02** Registro de Ecos
- ⬜ **F7-03** Consecuencias que alteran pisos futuros
- ⬜ **F7-04** Decisiones secretas individuales

## F8 — Asedio y extracción (Sesión 9)

_Clímax en la azotea y puntuación_

- ⬜ **F8-01** Director de oleadas del asedio
- ⬜ **F8-02** Fortificaciones y torretas
- ⬜ **F8-03** Puntuación y multiplicador de supervivencia colectiva
- ⬜ **F8-04** Extracción en Osprey y conversión a puntos de ADN

## F9 — Meta-progresión: Laboratorio (Sesión 10)

_Árbol de ADN y persistencia entre clones_

- ⬜ **F9-01** Árbol de ADN con ramas y Tier Lock
- ⬜ **F9-02** Aplicación de mutaciones al nuevo clon
- ⬜ **F9-03** Fragmentos de memoria y desbloqueo de lore
- ⬜ **F9-04** Guardado y carga del perfil

## F10 — Presentación en Unity (Sesión 11)

_Escenas, control, HUD y estilo visual_

- ⬜ **F10-01** Bootstrap de escenas e inyección de dependencias
- ⬜ **F10-02** Controlador del jugador y cámara
- ⬜ **F10-03** HUD: temporizador, recursos, avisos
- ⬜ **F10-04** UI del Laboratorio Genético
- ⬜ **F10-05** Dualidad cromática: Volumes URP y shader toon con contornos
- ⬜ **F10-06** Escena del Ascensor y Armería jugables

## F11 — Multijugador cooperativo (Sesión 12)

_Netcode for GameObjects, 2-4 jugadores_

- ⬜ **F11-01** Integración de Netcode for GameObjects
- ⬜ **F11-02** Sincronización del estado de iteración (servidor autoritativo)
- ⬜ **F11-03** Lobby y conexión
- ⬜ **F11-04** Replicación de jugadores, combate y loot

## F12 — Vertical Slice — Acto I (Sesión 13)

_Contenido jugable completo y pulido_

- ⬜ **F12-01** Cinco pisos del Acto I + jefe
- ⬜ **F12-02** Tutorial con la mentora Ilse
- ⬜ **F12-03** Audio y feedback
- ⬜ **F12-04** Balanceo con telemetría
- ⬜ **F12-05** Build jugable
