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
  n11["Core.Echoes.Consequences"]
  n12["Core.Echoes.Voting"]
  n13["Core.Elevator"]
  n14["Core.Events"]
  n15["Core.Floors"]
  n16["Core.Floors.Omens"]
  n17["Core.Floors.Paths"]
  n18["Core.Floors.Population"]
  n19["Core.Floors.Remnants"]
  n20["Core.Floors.Types"]
  n21["Core.Items"]
  n22["Core.Iteration"]
  n23["Core.Iteration.Ending"]
  n24["Core.Iteration.Presence"]
  n25["Core.Iteration.Readiness"]
  n26["Core.Iteration.States"]
  n27["Core.Iteration.Voting"]
  n28["Core.Life"]
  n29["Core.Loot"]
  n30["Core.Movement"]
  n31["Core.Physics"]
  n32["Core.Professions"]
  n33["Core.Prototype"]
  n34["Core.Randomness"]
  n35["Core.Repair"]
  n36["Core.StateMachines"]
  n37["Core.Stats"]
  n38["Core.Time"]
  n39["Core.Visuals"]
  n40["Core.Vitals"]
  n41["Runtime.Prototype"]
  n42["Runtime.Visuals"]
  n0 --> n3 & n8 & n40
  n1 --> n3 & n8 & n30
  n2 --> n3 & n8
  n3 --> n8 & n37
  n4 --> n8 & n34
  n5 --> n6 & n8
  n6 --> n8 & n29
  n7 --> n24 & n30
  n9 --> n8 & n21 & n32
  n10 --> n8
  n11 --> n8 & n16
  n12 --> n8
  n13 --> n8 & n9 & n21 & n22 & n28 & n37 & n38 & n40
  n14 --> n8
  n15 --> n8 & n29 & n34
  n16 --> n8 & n20 & n34
  n17 --> n8 & n32 & n34
  n18 --> n8 & n29 & n34
  n19 --> n8 & n29 & n34
  n20 --> n8
  n21 --> n8
  n22 --> n8 & n14 & n23 & n24 & n25 & n26 & n27 & n36 & n38
  n23 --> n24
  n24 --> n8
  n25 --> n8
  n26 --> n23 & n24 & n25 & n36 & n38
  n27 --> n8
  n28 --> n8 & n22 & n40
  n29 --> n8 & n21 & n34
  n30 --> n8 & n40
  n31 --> n8
  n32 --> n8
  n33 --> n8 & n31 & n38 & n39 & n40
  n34 --> n8
  n35 --> n8 & n21
  n36 --> n8 & n38
  n37 --> n8
  n38 --> n8
  n39 --> n8
  n40 --> n8 & n37 & n38
  n41 --> n30 & n33 & n37 & n38 & n39 & n40 & n42
  n42 --> n33 & n39
```
