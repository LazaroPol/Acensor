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
  n10["Core.Echoes.Voting"]
  n11["Core.Elevator"]
  n12["Core.Events"]
  n13["Core.Floors"]
  n14["Core.Floors.Omens"]
  n15["Core.Floors.Paths"]
  n16["Core.Floors.Population"]
  n17["Core.Floors.Remnants"]
  n18["Core.Floors.Types"]
  n19["Core.Items"]
  n20["Core.Iteration"]
  n21["Core.Iteration.Ending"]
  n22["Core.Iteration.Presence"]
  n23["Core.Iteration.Readiness"]
  n24["Core.Iteration.States"]
  n25["Core.Iteration.Voting"]
  n26["Core.Life"]
  n27["Core.Loot"]
  n28["Core.Movement"]
  n29["Core.Physics"]
  n30["Core.Professions"]
  n31["Core.Prototype"]
  n32["Core.Randomness"]
  n33["Core.Repair"]
  n34["Core.StateMachines"]
  n35["Core.Stats"]
  n36["Core.Time"]
  n37["Core.Visuals"]
  n38["Core.Vitals"]
  n39["Runtime.Prototype"]
  n40["Runtime.Visuals"]
  n0 --> n3 & n8 & n38
  n1 --> n3 & n8 & n28
  n2 --> n3 & n8
  n3 --> n8 & n35
  n4 --> n8 & n32
  n5 --> n6 & n8
  n6 --> n8 & n27
  n7 --> n22 & n28
  n9 --> n8 & n19 & n30
  n10 --> n8
  n11 --> n8 & n9 & n19 & n20 & n26 & n35 & n36 & n38
  n12 --> n8
  n13 --> n8 & n27 & n32
  n14 --> n8 & n18 & n32
  n15 --> n8 & n30 & n32
  n16 --> n8 & n27 & n32
  n17 --> n8 & n27 & n32
  n18 --> n8
  n19 --> n8
  n20 --> n8 & n12 & n21 & n22 & n23 & n24 & n25 & n34 & n36
  n21 --> n22
  n22 --> n8
  n23 --> n8
  n24 --> n21 & n22 & n23 & n34 & n36
  n25 --> n8
  n26 --> n8 & n20 & n38
  n27 --> n8 & n19 & n32
  n28 --> n8 & n38
  n29 --> n8
  n30 --> n8
  n31 --> n8 & n29 & n36 & n37 & n38
  n32 --> n8
  n33 --> n8 & n19
  n34 --> n8 & n36
  n35 --> n8
  n36 --> n8
  n37 --> n8
  n38 --> n8 & n35 & n36
  n39 --> n28 & n31 & n35 & n36 & n37 & n38 & n40
  n40 --> n31 & n37
```
