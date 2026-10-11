<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Life`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Life`
Tests: `Assets/_Project/Tests/EditMode/Core/Life`

**Depende de:** [Core.Common](Core.Common.md), [Core.Iteration](Core.Iteration.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `CharacterLife.cs` | class `CharacterLife` | Ciclo muerte → Espectro → revivir de un jugador. |
| `IReviveCost.cs` | interface `IReviveCost` | Paga el coste de un revivir (materiales de la Litera u objeto de campo). |
| `LifeState.cs` | enum `LifeState` | Estado vital de un jugador (GDD §3.3). |
| `PartyLife.cs` | class `PartyLife` | Estado vital del grupo: quién es Espectro y si han caído todos (fin de la iteración, GDD §3.3). |
| `ReviveMethod.cs` | enum `ReviveMethod` | Formas de revivir a un Espectro (GDD §3.3). |
| `ReviveResult.cs` | enum `ReviveResult` | Resultado de un intento de revivir. |
| `ReviveRules.cs` | class `ReviveRules` | Fracción de vida con la que vuelve un Espectro según cómo se le reviva. |
