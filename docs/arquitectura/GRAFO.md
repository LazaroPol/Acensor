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
  n10["Core.Elevator"]
  n11["Core.Events"]
  n12["Core.Floors"]
  n13["Core.Floors.Omens"]
  n14["Core.Floors.Paths"]
  n15["Core.Floors.Population"]
  n16["Core.Floors.Remnants"]
  n17["Core.Floors.Types"]
  n18["Core.Items"]
  n19["Core.Iteration"]
  n20["Core.Iteration.Ending"]
  n21["Core.Iteration.Presence"]
  n22["Core.Iteration.Readiness"]
  n23["Core.Iteration.States"]
  n24["Core.Iteration.Voting"]
  n25["Core.Life"]
  n26["Core.Loot"]
  n27["Core.Movement"]
  n28["Core.Physics"]
  n29["Core.Professions"]
  n30["Core.Prototype"]
  n31["Core.Randomness"]
  n32["Core.Repair"]
  n33["Core.StateMachines"]
  n34["Core.Stats"]
  n35["Core.Time"]
  n36["Core.Visuals"]
  n37["Core.Vitals"]
  n38["Runtime.Prototype"]
  n39["Runtime.Visuals"]
  n0 --> n3 & n8 & n37
  n1 --> n3 & n8 & n27
  n2 --> n3 & n8
  n3 --> n8 & n34
  n4 --> n8 & n31
  n5 --> n6 & n8
  n6 --> n8 & n26
  n7 --> n21 & n27
  n9 --> n8 & n18 & n29
  n10 --> n8 & n9 & n18 & n19 & n25 & n34 & n35 & n37
  n11 --> n8
  n12 --> n8 & n26 & n31
  n13 --> n8 & n17 & n31
  n14 --> n8 & n29 & n31
  n15 --> n8 & n26 & n31
  n16 --> n8 & n26 & n31
  n17 --> n8
  n18 --> n8
  n19 --> n8 & n11 & n20 & n21 & n22 & n23 & n24 & n33 & n35
  n20 --> n21
  n21 --> n8
  n22 --> n8
  n23 --> n20 & n21 & n22 & n33 & n35
  n24 --> n8
  n25 --> n8 & n19 & n37
  n26 --> n8 & n18 & n31
  n27 --> n8 & n37
  n28 --> n8
  n29 --> n8
  n30 --> n8 & n28 & n35 & n36 & n37
  n31 --> n8
  n32 --> n8 & n18
  n33 --> n8 & n35
  n34 --> n8
  n35 --> n8
  n36 --> n8
  n37 --> n8 & n34 & n35
  n38 --> n27 & n30 & n34 & n35 & n36 & n37 & n39
  n39 --> n30 & n36
```
