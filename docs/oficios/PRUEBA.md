# Guía de prueba manual — Fase OF (oficios) en el Campo de pruebas

Recorre en Unity los cinco criterios de aceptación de la fase ([GDD §17](../gdd/17-oficios.md)).
Tiempo estimado: 10 minutos.

## Preparación
1. Abre la escena `CampoPruebas` y pulsa **Play**.
2. Si quieres empezar de cero, borra el guardado de oficios: en la consola de Unity,
   `PlayerPrefs.DeleteKey("specimen.oficios.v1")` (o *Edit › Clear All PlayerPrefs*).
3. Pulsa **F4**: lote de materiales (cobre, leña, madera…), un crisol, un **pergamino** y un **plano de
   Herrero**, una **piedra atada** (mod) y un **palo de madera de calidad Buena**. F4 también sube
   Herrero al nivel de los planos (10) para poder leerlos.

## 1. Craftear consumiendo materiales por etiqueta
1. Ve a la Fragua del poblado y pulsa **E**.
2. Echa leña al combustible (ranuras de la derecha).
3. Elige *Hacha de mano* (verde) y pulsa **Fabricar**: la madera sale de cualquier tipo de madera.
4. ✅ La barra de la cola avanza, al terminar aparece en la salida y el HUD muestra la XP de Herrero en verde.

## 2. Una receta bloqueada aparece tras leer su plano o tratado
1. En la ventana de la Fragua, localiza en gris la receta que enseña el plano (motivo: *falta el plano*).
2. Abre la bolsa (**B**) y haz **clic derecho** sobre el *Plano de Herrero*.
3. ✅ El registro dice «Aprendido: …», el plano desaparece de la bolsa y la receta ya no sale en gris
   por conocimiento. Repite con el pergamino.
4. ✅ Leer otra vez algo ya aprendido avisa «Ya conoces…» y no consume el objeto.

## 3. Una pieza colocada habilita recetas nuevas
1. En la Fragua, pulsa **Colocar** en el *Crisol* (columna derecha).
2. ✅ Las recetas que piden crisol pasan a estar disponibles; al retirarlo vuelven a gris.

## 4. Añadir y quitar mods en un arma con efecto en sus stats
1. En la bolsa, **clic izquierdo** sobre el *Palo de madera* (Buena, 2 huecos).
2. ✅ La ventana *Mods del objeto* muestra los stats base y los huecos 0/2.
3. Pulsa **Poner: Piedra atada**. ✅ Daño contundente sube (verde), peso y velocidad empeoran (rojo),
   huecos 1/2 y la piedra sale de la bolsa.
4. Pulsa **Quitar: Piedra atada**. ✅ Los stats vuelven a la base y la piedra vuelve a la bolsa.

## 5. Todo se guarda y se carga sin perder progreso
1. Deja: Herrero con XP, una receta aprendida, la Fragua mejorada (**Mejorar**), el crisol colocado,
   un trabajo **a medias** en la cola y el palo con la piedra puesta.
2. Cierra la ventana de estación (guarda) y sal del Play.
3. Vuelve a entrar en Play. ✅ El registro dice «Progreso de oficios cargado.» y se conservan nivel y XP
   de Herrero, recetas aprendidas, nivel de la Fragua, crisol con su desgaste, combustible, el trabajo
   con su tiempo restante (no se puede cancelar: ya se pagó) y el palo con su calidad, durabilidad y mod.

## Botín de oficio
- Las criaturas del poblado sueltan a veces (30 %) pergaminos, piezas de estación o mods; el jefe,
  en el anillo 3, también planos, tratados y volúmenes de libro.

## Si algo falla
- Guardado dañado: el registro avisa y queda una copia en `specimen.oficios.v1.bak`.
- Limitaciones conocidas: solo se guarda el inventario de la bolsa (no lo equipado en el personaje) y
  los ítems apilados guardan cantidad, no calidad.
