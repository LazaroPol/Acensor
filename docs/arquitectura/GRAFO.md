<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Armory"]
  n1["Core.Classes"]
  n2["Core.Combat"]
  n3["Core.Combat.Enemies"]
  n4["Core.Combat.Perception"]
  n5["Core.Common"]
  n6["Core.Crafting"]
  n7["Core.Elevator"]
  n8["Core.Events"]
  n9["Core.Floors"]
  n10["Core.Floors.Omens"]
  n11["Core.Floors.Paths"]
  n12["Core.Floors.Population"]
  n13["Core.Floors.Remnants"]
  n14["Core.Floors.Types"]
  n15["Core.Items"]
  n16["Core.Iteration"]
  n17["Core.Iteration.Ending"]
  n18["Core.Iteration.Presence"]
  n19["Core.Iteration.Readiness"]
  n20["Core.Iteration.States"]
  n21["Core.Iteration.Voting"]
  n22["Core.Life"]
  n23["Core.Loot"]
  n24["Core.Movement"]
  n25["Core.Physics"]
  n26["Core.Professions"]
  n27["Core.Prototype"]
  n28["Core.Randomness"]
  n29["Core.Repair"]
  n30["Core.StateMachines"]
  n31["Core.Stats"]
  n32["Core.Time"]
  n33["Core.Visuals"]
  n34["Core.Vitals"]
  n35["Runtime.Prototype"]
  n36["Runtime.Visuals"]
  n0 --> n1 & n5
  n1 --> n5 & n31
  n2 --> n5 & n28
  n3 --> n5 & n23
  n4 --> n18 & n24
  n6 --> n5 & n15 & n26
  n7 --> n5 & n6 & n15 & n16 & n22 & n31 & n32 & n34
  n8 --> n5
  n9 --> n5 & n23 & n28
  n10 --> n5 & n14 & n28
  n11 --> n5 & n26 & n28
  n12 --> n5 & n23 & n28
  n13 --> n5 & n23 & n28
  n14 --> n5
  n15 --> n5
  n16 --> n5 & n8 & n17 & n18 & n19 & n20 & n21 & n30 & n32
  n17 --> n18
  n18 --> n5
  n19 --> n5
  n20 --> n17 & n18 & n19 & n30 & n32
  n21 --> n5
  n22 --> n5 & n16 & n34
  n23 --> n5 & n15 & n28
  n24 --> n5 & n34
  n25 --> n5
  n26 --> n5
  n27 --> n5 & n25 & n32 & n33 & n34
  n28 --> n5
  n29 --> n5 & n15
  n30 --> n5 & n32
  n31 --> n5
  n32 --> n5
  n33 --> n5
  n34 --> n5 & n31 & n32
  n35 --> n24 & n27 & n31 & n32 & n33 & n34 & n36
  n36 --> n27 & n33
```
