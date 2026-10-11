# 3. Bucle de juego

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
