# 17.5 Contenido: materiales, combustibles, piezas y mods (OF-11)

Batería inicial pensada para que cada tier se note. Ids en `snake_case`, estables. Los que ya existen
en `ItemCatalog` se reutilizan y se les añaden tier, peso y etiquetas.

## Materiales por etiqueta y tier

| Etiqueta | Tier 1 | Tier 2 | Tier 3 | Tier 4 |
|---|---|---|---|---|
| Madera | rama seca, madera de pino | tablón de roble, madera de fresno | madera de tejo | madera de corazón negro |
| Metal | mineral de cobre, chatarra oxidada | lingote de cobre, mineral de hierro, lingote de hierro | lingote de acero, lingote de plata | acero de crisol |
| Piedra | piedra suelta, sílex | piedra labrada, bloque de granito | mármol | obsidiana |
| Cuero | piel cruda | cuero curtido | cuero endurecido | piel de bestia mayor |
| Tela | fibra vegetal | lino, lana | paño encerado | seda |
| Hierba | hierba común, raíz amarga | flor de saúco, musgo de cueva | belladona | mandrágora |
| Comida | carne cruda, pescado, trigo, manzana | harina, sal, miel, manteca | especias del sur | trufa negra |
| Hueso | hueso, astilla de hueso | colmillo | asta de ciervo blanco | hueso de bestia mayor |
| Mineral | arcilla, barro | azufre, salitre | cristal de cuarzo | polvo de estrella (meteorito) |
| Vidrio | — | arena fina | vidrio soplado | vidrio de cuarzo |

Intermedios sin calidad (los fabrica un oficio para otro): **cuerda** (Sastre), **correa de cuero**
(Sastre), **clavos** y **remaches** (Herrero), **mango de madera** (Carpintero), **resina** y
**aceite de afilar** (Alquimista), **mortero de cal** (Cantero), **engranaje de madera** (Ingenios).

## Combustibles

| Id | Etiquetas | Arde | Notas |
|---|---|---|---|
| `lena` | Madera, Combustible | 30 s | cualquier madera tier 1 también arde (15 s) |
| `carbon_vegetal` | Mineral, Combustible | 90 s | el Carpintero lo hace en su banco con leña |
| `hulla` | Mineral, Combustible | 180 s | solo en pisos profundos |
| `aceite_lampara` | Combustible | 60 s | solo Mesa de alquimia |

## Piezas de estación (ítems físicos colocables)

| Estación | Pieza | Habilita | Bonus |
|---|---|---|---|
| Fragua | Crisol | fundir lingotes de acero, plata y aleaciones | — |
| Fragua | Fuelle de cuero | recetas tier 3+ de la fragua | −20 % tiempo, −25 % combustible |
| Fragua | Yunque de acero | armas y armaduras tier 3–4 | +3 calidad |
| Fragua | Muela de afilar | mods de filo y afilado fino | — |
| Hogar de cocina | Olla de hierro | guisos y caldos tier 2+ | +2 calidad en sopas |
| Hogar de cocina | Parrilla | asados (buff de fuerza) | −15 % tiempo |
| Hogar de cocina | Cazo de cobre | confituras y conservas | — |
| Mesa de alquimia | Mortero de piedra | polvos y ungüentos | — |
| Mesa de alquimia | Alambique | pociones destiladas tier 2+ | +2 calidad |
| Mesa de alquimia | Retorta de vidrio | ácidos, humo y bombas | — |
| Taller de sastre | Bastidor de telar | telas tier 2+ | — |
| Taller de sastre | Tina de curtido | cuero curtido y endurecido | — |
| Taller de sastre | Tijeras de maestro | armaduras ligeras tier 3+ | +2 calidad |
| Taller de ingenios | Torno de pedal | engranajes y ballestas | −15 % tiempo |
| Taller de ingenios | Compás de maestro | trampas y torretas tier 3 | +2 calidad |
| Banco de carpintero | Tornillo de banco | mangos, arcos y escudos tier 2+ | — |
| Banco de carpintero | Juego de limas | ganzúas y reparaciones finas | +1 calidad |
| Banco de cantero | Cincel de acero | piedra tallada tier 3, tótems | +2 calidad |
| Banco de cantero | Mazo de cantero | bloques y señuelos | −15 % tiempo |

Las piezas se encuentran como botín (cofres de taller abandonado en pisos de Profesión) y la mayoría
también tiene plano para fabricarlas (ver §17.6).

## Mods (piezas añadibles)

| Id | Hueco | Compatibles | Efecto | Origen |
|---|---|---|---|---|
| `mod.filo_silex` | Filo | Arma | +2 cortante | Cantero T1, botín anillo 1 |
| `mod.esquirlas_metal` | Cabeza | Arma, Madera | +4 cortante, −20 % durabilidad | Herrero T1 |
| `mod.piedra_atada` | Cabeza | Arma, Madera | +5 contundente, +1,5 kg, −5 % velocidad | Cantero T1 |
| `mod.clavo` | Cabeza | Arma, Madera | +3 perforante | Herrero T1 |
| `mod.punta_hueso` | Filo | Lanza, Flecha | +4 perforante | Carpintero T1 |
| `mod.tiras_cuero` | Empuñadura | Arma, Herramienta | +8 % velocidad, +10 % durabilidad | Sastre T1 |
| `mod.remaches` | Refuerzo | Metal | +25 % durabilidad, +0,5 kg | Herrero T2 |
| `mod.contrapeso_plomo` | Cabeza | ArmaContundente | +20 % contundente, −10 % velocidad | Herrero T2 |
| `mod.mango_fresno` | Empuñadura | Herramienta, Arma | +15 % velocidad | Carpintero T2 |
| `mod.placas_cosidas` | Refuerzo | Armadura | +6 armadura, +2 kg | Sastre+Herrero T2 |
| `mod.forro_acolchado` | Forro | Armadura | +3 armadura, +10 % durabilidad | Sastre T2 |
| `mod.afilado_fino` | Filo | Metal | +15 % cortante | Herrero T3 (muela) |
| `mod.resina_ignea` | Accesorio | Arma | +10 % a los tres daños (se destruye al quitarla) | Alquimista T3 |
| `mod.gema_tallada` | Accesorio | Arma, Armadura | +5 % a todo, −10 % durabilidad | Cantero T4 |
| `mod.correaje_ligero` | Accesorio | Armadura | −20 % peso | Sastre T3 |
