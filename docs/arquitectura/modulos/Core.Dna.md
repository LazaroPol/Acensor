<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Dna`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Dna`
Tests: `Assets/_Project/Tests/EditMode/Core/Dna`

**Depende de:** [Core.Iteration.Ending](Core.Iteration.Ending.md), [Core.Stats](Core.Stats.md)
**Lo usan:** [Core.Lab](Core.Lab.md), [Core.Profile](Core.Profile.md), [Runtime.Extraction](Runtime.Extraction.md), [Runtime.Lab](Runtime.Lab.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DefaultDnaEffects.cs` | class `DefaultDnaEffects` | Efectos por defecto: cada rama tiene un atributo principal (nodo «a») y uno secundario (nodo «b»); el bonus crece con el tier (+2 % × tier). |
| `DnaAllocation.cs` | class `DnaAllocation` | Estado de un perfil: puntos libres, nodos desbloqueados y puntos invertidos por rama. |
| `DnaBranch.cs` | enum `DnaBranch` | Ramas del árbol de ADN (GDD §12). |
| `DnaEffect.cs` | struct `DnaEffect` | Efecto de una mutación: un modificador (porcentual) sobre un atributo. |
| `DnaMutationApplier.cs` | class `DnaMutationApplier` | Aplica las mutaciones desbloqueadas a la hoja de atributos de un clon nuevo. |
| `DnaNode.cs` | class `DnaNode` | Nodo inmutable del árbol: rama, nivel (tier) y coste en puntos de ADN. |
| `DnaReward.cs` | class `DnaReward` | Puntos de ADN que deja una iteración (GDD §3.5 simplificado para la demo): la victoria paga una base, un plus por piso y otro por cada pieza |
| `DnaTree.cs` | class `DnaTree` | Catálogo de nodos del árbol, indexado por id. |
| `IDnaEffectCatalog.cs` | interface `IDnaEffectCatalog` | Efectos que concede cada nodo del árbol. |
| `ITierLock.cs` | interface `ITierLock` | Puntos que hay que haber invertido en una rama para acceder a un nivel. |
| `TierLock.cs` | class `TierLock` | Umbrales por nivel (índice 0 = tier 1). |
| `UnlockResult.cs` | enum `UnlockResult` | Resultado de intentar desbloquear un nodo de ADN. |
