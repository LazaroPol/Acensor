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
  n24["Core.Hud"]
  n25["Core.Items"]
  n26["Core.Iteration"]
  n27["Core.Iteration.Ending"]
  n28["Core.Iteration.Presence"]
  n29["Core.Iteration.Readiness"]
  n30["Core.Iteration.States"]
  n31["Core.Iteration.Voting"]
  n32["Core.Life"]
  n33["Core.Loot"]
  n34["Core.Movement"]
  n35["Core.Physics"]
  n36["Core.Professions"]
  n37["Core.Prototype"]
  n38["Core.Randomness"]
  n39["Core.Repair"]
  n40["Core.Siege"]
  n41["Core.Siege.Defenses"]
  n42["Core.Siege.Extraction"]
  n43["Core.Siege.Scoring"]
  n44["Core.StateMachines"]
  n45["Core.Stats"]
  n46["Core.Time"]
  n47["Core.Visuals"]
  n48["Core.Vitals"]
  n49["Runtime.Bootstrap"]
  n50["Runtime.Hud"]
  n51["Runtime.Player"]
  n52["Runtime.Prototype"]
  n53["Runtime.Visuals"]
  n0 --> n3 & n8 & n48
  n1 --> n3 & n8 & n34
  n2 --> n3 & n8
  n3 --> n8 & n45
  n4 --> n8 & n38
  n5 --> n6 & n8
  n6 --> n8 & n33
  n7 --> n28 & n34
  n9 --> n8 & n17
  n10 --> n8 & n25 & n36
  n12 --> n8
  n13 --> n8 & n19
  n14 --> n8
  n15 --> n8
  n16 --> n8 & n10 & n25 & n26 & n32 & n45 & n46 & n48
  n17 --> n8
  n18 --> n8 & n33 & n38
  n19 --> n8 & n23 & n38
  n20 --> n8 & n36 & n38
  n21 --> n8 & n33 & n38
  n22 --> n8 & n33 & n38
  n23 --> n8
  n24 --> n8 & n17 & n26 & n37 & n46 & n48
  n25 --> n8
  n26 --> n8 & n17 & n27 & n28 & n29 & n30 & n31 & n44 & n46
  n27 --> n28
  n28 --> n8
  n29 --> n8
  n30 --> n27 & n28 & n29 & n44 & n46
  n31 --> n8
  n32 --> n8 & n26 & n48
  n33 --> n8 & n25 & n38
  n34 --> n8 & n48
  n35 --> n8
  n36 --> n8
  n37 --> n8 & n35 & n46 & n47 & n48
  n38 --> n8
  n39 --> n8 & n25
  n40 --> n8
  n41 --> n8
  n42 --> n8 & n43
  n43 --> n8
  n44 --> n8 & n46
  n45 --> n8
  n46 --> n8
  n47 --> n8
  n48 --> n8 & n45 & n46
  n49 --> n9
  n50 --> n24
  n51 --> n34 & n45 & n48
  n52 --> n34 & n37 & n45 & n46 & n47 & n48 & n53
  n53 --> n37 & n47
```
