<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]
  n1["Core.Abilities.Mobility"]
  n2["Core.Armory"]
  n3["Core.Classes"]
  n4["Core.Combat"]
  n5["Core.Combat.Enemies"]
  n6["Core.Combat.Perception"]
  n7["Core.Common"]
  n8["Core.Crafting"]
  n9["Core.Elevator"]
  n10["Core.Events"]
  n11["Core.Floors"]
  n12["Core.Floors.Omens"]
  n13["Core.Floors.Paths"]
  n14["Core.Floors.Population"]
  n15["Core.Floors.Remnants"]
  n16["Core.Floors.Types"]
  n17["Core.Items"]
  n18["Core.Iteration"]
  n19["Core.Iteration.Ending"]
  n20["Core.Iteration.Presence"]
  n21["Core.Iteration.Readiness"]
  n22["Core.Iteration.States"]
  n23["Core.Iteration.Voting"]
  n24["Core.Life"]
  n25["Core.Loot"]
  n26["Core.Movement"]
  n27["Core.Physics"]
  n28["Core.Professions"]
  n29["Core.Prototype"]
  n30["Core.Randomness"]
  n31["Core.Repair"]
  n32["Core.StateMachines"]
  n33["Core.Stats"]
  n34["Core.Time"]
  n35["Core.Visuals"]
  n36["Core.Vitals"]
  n37["Runtime.Prototype"]
  n38["Runtime.Visuals"]
  n0 --> n3 & n7 & n36
  n1 --> n3 & n7 & n26
  n2 --> n3 & n7
  n3 --> n7 & n33
  n4 --> n7 & n30
  n5 --> n7 & n25
  n6 --> n20 & n26
  n8 --> n7 & n17 & n28
  n9 --> n7 & n8 & n17 & n18 & n24 & n33 & n34 & n36
  n10 --> n7
  n11 --> n7 & n25 & n30
  n12 --> n7 & n16 & n30
  n13 --> n7 & n28 & n30
  n14 --> n7 & n25 & n30
  n15 --> n7 & n25 & n30
  n16 --> n7
  n17 --> n7
  n18 --> n7 & n10 & n19 & n20 & n21 & n22 & n23 & n32 & n34
  n19 --> n20
  n20 --> n7
  n21 --> n7
  n22 --> n19 & n20 & n21 & n32 & n34
  n23 --> n7
  n24 --> n7 & n18 & n36
  n25 --> n7 & n17 & n30
  n26 --> n7 & n36
  n27 --> n7
  n28 --> n7
  n29 --> n7 & n27 & n34 & n35 & n36
  n30 --> n7
  n31 --> n7 & n17
  n32 --> n7 & n34
  n33 --> n7
  n34 --> n7
  n35 --> n7
  n36 --> n7 & n33 & n34
  n37 --> n26 & n29 & n33 & n34 & n35 & n36 & n38
  n38 --> n29 & n35
```
