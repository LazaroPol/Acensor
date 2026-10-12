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
  n9["Core.Composition"]
  n10["Core.Crafting"]
  n11["Core.Dna"]
  n12["Core.Echoes"]
  n13["Core.Echoes.Consequences"]
  n14["Core.Echoes.Secrets"]
  n15["Core.Echoes.Voting"]
  n16["Core.Elevator"]
  n17["Core.Events"]
  n18["Core.Floors"]
  n19["Core.Floors.Omens"]
  n20["Core.Floors.Paths"]
  n21["Core.Floors.Population"]
  n22["Core.Floors.Remnants"]
  n23["Core.Floors.Types"]
  n24["Core.Items"]
  n25["Core.Iteration"]
  n26["Core.Iteration.Ending"]
  n27["Core.Iteration.Presence"]
  n28["Core.Iteration.Readiness"]
  n29["Core.Iteration.States"]
  n30["Core.Iteration.Voting"]
  n31["Core.Life"]
  n32["Core.Loot"]
  n33["Core.Movement"]
  n34["Core.Physics"]
  n35["Core.Professions"]
  n36["Core.Prototype"]
  n37["Core.Randomness"]
  n38["Core.Repair"]
  n39["Core.Siege"]
  n40["Core.Siege.Defenses"]
  n41["Core.Siege.Extraction"]
  n42["Core.Siege.Scoring"]
  n43["Core.StateMachines"]
  n44["Core.Stats"]
  n45["Core.Time"]
  n46["Core.Visuals"]
  n47["Core.Vitals"]
  n48["Runtime.Bootstrap"]
  n49["Runtime.Prototype"]
  n50["Runtime.Visuals"]
  n0 --> n3 & n8 & n47
  n1 --> n3 & n8 & n33
  n2 --> n3 & n8
  n3 --> n8 & n44
  n4 --> n8 & n37
  n5 --> n6 & n8
  n6 --> n8 & n32
  n7 --> n27 & n33
  n9 --> n8 & n17
  n10 --> n8 & n24 & n35
  n12 --> n8
  n13 --> n8 & n19
  n14 --> n8
  n15 --> n8
  n16 --> n8 & n10 & n24 & n25 & n31 & n44 & n45 & n47
  n17 --> n8
  n18 --> n8 & n32 & n37
  n19 --> n8 & n23 & n37
  n20 --> n8 & n35 & n37
  n21 --> n8 & n32 & n37
  n22 --> n8 & n32 & n37
  n23 --> n8
  n24 --> n8
  n25 --> n8 & n17 & n26 & n27 & n28 & n29 & n30 & n43 & n45
  n26 --> n27
  n27 --> n8
  n28 --> n8
  n29 --> n26 & n27 & n28 & n43 & n45
  n30 --> n8
  n31 --> n8 & n25 & n47
  n32 --> n8 & n24 & n37
  n33 --> n8 & n47
  n34 --> n8
  n35 --> n8
  n36 --> n8 & n34 & n45 & n46 & n47
  n37 --> n8
  n38 --> n8 & n24
  n39 --> n8
  n40 --> n8
  n41 --> n8 & n42
  n42 --> n8
  n43 --> n8 & n45
  n44 --> n8
  n45 --> n8
  n46 --> n8
  n47 --> n8 & n44 & n45
  n48 --> n9
  n49 --> n33 & n36 & n44 & n45 & n46 & n47 & n50
  n50 --> n36 & n46
```
