<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]
  n1["Core.Abilities.Mobility"]
  n2["Core.Armory"]
  n3["Core.Classes"]
  n4["Core.Combat"]
  n5["Core.Combat.Bosses"]
  n6["Core.Combat.Enemies"]
  n7["Core.Combat.Perception"]
  n8["Core.Common"]
  n9["Core.Crafting"]
  n10["Core.Dna"]
  n11["Core.Echoes"]
  n12["Core.Echoes.Consequences"]
  n13["Core.Echoes.Secrets"]
  n14["Core.Echoes.Voting"]
  n15["Core.Elevator"]
  n16["Core.Events"]
  n17["Core.Floors"]
  n18["Core.Floors.Omens"]
  n19["Core.Floors.Paths"]
  n20["Core.Floors.Population"]
  n21["Core.Floors.Remnants"]
  n22["Core.Floors.Types"]
  n23["Core.Items"]
  n24["Core.Iteration"]
  n25["Core.Iteration.Ending"]
  n26["Core.Iteration.Presence"]
  n27["Core.Iteration.Readiness"]
  n28["Core.Iteration.States"]
  n29["Core.Iteration.Voting"]
  n30["Core.Life"]
  n31["Core.Loot"]
  n32["Core.Movement"]
  n33["Core.Physics"]
  n34["Core.Professions"]
  n35["Core.Prototype"]
  n36["Core.Randomness"]
  n37["Core.Repair"]
  n38["Core.Siege"]
  n39["Core.Siege.Defenses"]
  n40["Core.Siege.Scoring"]
  n41["Core.StateMachines"]
  n42["Core.Stats"]
  n43["Core.Time"]
  n44["Core.Visuals"]
  n45["Core.Vitals"]
  n46["Runtime.Prototype"]
  n47["Runtime.Visuals"]
  n0 --> n3 & n8 & n45
  n1 --> n3 & n8 & n32
  n2 --> n3 & n8
  n3 --> n8 & n42
  n4 --> n8 & n36
  n5 --> n6 & n8
  n6 --> n8 & n31
  n7 --> n26 & n32
  n9 --> n8 & n23 & n34
  n11 --> n8
  n12 --> n8 & n18
  n13 --> n8
  n14 --> n8
  n15 --> n8 & n9 & n23 & n24 & n30 & n42 & n43 & n45
  n16 --> n8
  n17 --> n8 & n31 & n36
  n18 --> n8 & n22 & n36
  n19 --> n8 & n34 & n36
  n20 --> n8 & n31 & n36
  n21 --> n8 & n31 & n36
  n22 --> n8
  n23 --> n8
  n24 --> n8 & n16 & n25 & n26 & n27 & n28 & n29 & n41 & n43
  n25 --> n26
  n26 --> n8
  n27 --> n8
  n28 --> n25 & n26 & n27 & n41 & n43
  n29 --> n8
  n30 --> n8 & n24 & n45
  n31 --> n8 & n23 & n36
  n32 --> n8 & n45
  n33 --> n8
  n34 --> n8
  n35 --> n8 & n33 & n43 & n44 & n45
  n36 --> n8
  n37 --> n8 & n23
  n38 --> n8
  n39 --> n8
  n40 --> n8
  n41 --> n8 & n43
  n42 --> n8
  n43 --> n8
  n44 --> n8
  n45 --> n8 & n42 & n43
  n46 --> n32 & n35 & n42 & n43 & n44 & n45 & n47
  n47 --> n35 & n44
```
