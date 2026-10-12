<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Appearance`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Appearance`
Tests: `Assets/_Project/Tests/EditMode/Core/Appearance`

**Depende de:** [Core.Classes](Core.Classes.md), [Core.Combat.Attacks](Core.Combat.Attacks.md), [Core.Common](Core.Common.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `AppearanceModels.cs` | class `AppearanceModels` | Nombres de modelo y textura de los packs de Quaternius para una apariencia: el modelo base lleva el esqueleto y el Animator; el atuendo (si  |
| `BodyType.cs` | enum `BodyType` | Cuerpo base del personaje (los packs traen esqueleto común para ambos). |
| `CharacterAppearance.cs` | class `CharacterAppearance` | Aspecto de un personaje: cuerpo, atuendo, variante de textura (1 = la base) y peinados. |
| `ClassAppearances.cs` | class `ClassAppearances` | Aspecto de cada kit de clase (GDD §4): silueta y paleta distintas para leerlos de un vistazo. |
| `ClassLoadout.cs` | class `ClassLoadout` | Equipo visible de un kit: arma (tipo y variante) y estilo de ataque con el que se usa. |
| `ClassLoadouts.cs` | class `ClassLoadouts` | Arma de cada kit: los de melé llevan armas de filo o contundentes; los de hechizo, un foco. |
| `OutfitId.cs` | enum `OutfitId` | Atuendo: Explorador (completo, con capucha), Campesino (sobre el cuerpo base) o Ajustado (solo cuerpo). |
| `WeaponId.cs` | enum `WeaponId` | Tipos de arma del pack RPG Weapons (cada tipo trae 7 variantes). |
| `WeaponModels.cs` | class `WeaponModels` | Carpeta y nombre de modelo de cada arma en el pack RPG Weapons («Long_Sword/Long_Sword_03»). |
