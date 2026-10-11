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
  n10["Core.Echoes"]
  n11["Core.Echoes.Voting"]
  n12["Core.Elevator"]
  n13["Core.Events"]
  n14["Core.Floors"]
  n15["Core.Floors.Omens"]
  n16["Core.Floors.Paths"]
  n17["Core.Floors.Population"]
  n18["Core.Floors.Remnants"]
  n19["Core.Floors.Types"]
  n20["Core.Items"]
  n21["Core.Iteration"]
  n22["Core.Iteration.Ending"]
  n23["Core.Iteration.Presence"]
  n24["Core.Iteration.Readiness"]
  n25["Core.Iteration.States"]
  n26["Core.Iteration.Voting"]
  n27["Core.Life"]
  n28["Core.Loot"]
  n29["Core.Movement"]
  n30["Core.Physics"]
  n31["Core.Professions"]
  n32["Core.Prototype"]
  n33["Core.Randomness"]
  n34["Core.Repair"]
  n35["Core.StateMachines"]
  n36["Core.Stats"]
  n37["Core.Time"]
  n38["Core.Visuals"]
  n39["Core.Vitals"]
  n40["Runtime.Prototype"]
  n41["Runtime.Visuals"]
  n0 --> n3 & n8 & n39
  n1 --> n3 & n8 & n29
  n2 --> n3 & n8
  n3 --> n8 & n36
  n4 --> n8 & n33
  n5 --> n6 & n8
  n6 --> n8 & n28
  n7 --> n23 & n29
  n9 --> n8 & n20 & n31
  n10 --> n8
  n11 --> n8
  n12 --> n8 & n9 & n20 & n21 & n27 & n36 & n37 & n39
  n13 --> n8
  n14 --> n8 & n28 & n33
  n15 --> n8 & n19 & n33
  n16 --> n8 & n31 & n33
  n17 --> n8 & n28 & n33
  n18 --> n8 & n28 & n33
  n19 --> n8
  n20 --> n8
  n21 --> n8 & n13 & n22 & n23 & n24 & n25 & n26 & n35 & n37
  n22 --> n23
  n23 --> n8
  n24 --> n8
  n25 --> n22 & n23 & n24 & n35 & n37
  n26 --> n8
  n27 --> n8 & n21 & n39
  n28 --> n8 & n20 & n33
  n29 --> n8 & n39
  n30 --> n8
  n31 --> n8
  n32 --> n8 & n30 & n37 & n38 & n39
  n33 --> n8
  n34 --> n8 & n20
  n35 --> n8 & n37
  n36 --> n8
  n37 --> n8
  n38 --> n8
  n39 --> n8 & n36 & n37
  n40 --> n29 & n32 & n36 & n37 & n38 & n39 & n41
  n41 --> n32 & n38
```
