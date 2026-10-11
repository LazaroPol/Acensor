<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Armory"]
  n1["Core.Classes"]
  n2["Core.Combat"]
  n3["Core.Combat.Enemies"]
  n4["Core.Common"]
  n5["Core.Crafting"]
  n6["Core.Elevator"]
  n7["Core.Events"]
  n8["Core.Floors"]
  n9["Core.Floors.Omens"]
  n10["Core.Floors.Paths"]
  n11["Core.Floors.Population"]
  n12["Core.Floors.Remnants"]
  n13["Core.Floors.Types"]
  n14["Core.Items"]
  n15["Core.Iteration"]
  n16["Core.Iteration.Ending"]
  n17["Core.Iteration.Presence"]
  n18["Core.Iteration.Readiness"]
  n19["Core.Iteration.States"]
  n20["Core.Iteration.Voting"]
  n21["Core.Life"]
  n22["Core.Loot"]
  n23["Core.Movement"]
  n24["Core.Physics"]
  n25["Core.Professions"]
  n26["Core.Prototype"]
  n27["Core.Randomness"]
  n28["Core.Repair"]
  n29["Core.StateMachines"]
  n30["Core.Stats"]
  n31["Core.Time"]
  n32["Core.Visuals"]
  n33["Core.Vitals"]
  n34["Runtime.Prototype"]
  n35["Runtime.Visuals"]
  n0 --> n1 & n4
  n1 --> n4 & n30
  n2 --> n4 & n27
  n3 --> n4 & n22
  n5 --> n4 & n14 & n25
  n6 --> n4 & n5 & n14 & n15 & n21 & n30 & n31 & n33
  n7 --> n4
  n8 --> n4 & n22 & n27
  n9 --> n4 & n13 & n27
  n10 --> n4 & n25 & n27
  n11 --> n4 & n22 & n27
  n12 --> n4 & n22 & n27
  n13 --> n4
  n14 --> n4
  n15 --> n4 & n7 & n16 & n17 & n18 & n19 & n20 & n29 & n31
  n16 --> n17
  n17 --> n4
  n18 --> n4
  n19 --> n16 & n17 & n18 & n29 & n31
  n20 --> n4
  n21 --> n4 & n15 & n33
  n22 --> n4 & n14 & n27
  n23 --> n4 & n33
  n24 --> n4
  n25 --> n4
  n26 --> n4 & n24 & n31 & n32 & n33
  n27 --> n4
  n28 --> n4 & n14
  n29 --> n4 & n31
  n30 --> n4
  n31 --> n4
  n32 --> n4
  n33 --> n4 & n30 & n31
  n34 --> n23 & n26 & n30 & n31 & n32 & n33 & n35
  n35 --> n26 & n32
```
