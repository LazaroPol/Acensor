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
  n12["Core.Echoes.Secrets"]
  n13["Core.Echoes.Voting"]
  n14["Core.Elevator"]
  n15["Core.Events"]
  n16["Core.Floors"]
  n17["Core.Floors.Omens"]
  n18["Core.Floors.Paths"]
  n19["Core.Floors.Population"]
  n20["Core.Floors.Remnants"]
  n21["Core.Floors.Types"]
  n22["Core.Items"]
  n23["Core.Iteration"]
  n24["Core.Iteration.Ending"]
  n25["Core.Iteration.Presence"]
  n26["Core.Iteration.Readiness"]
  n27["Core.Iteration.States"]
  n28["Core.Iteration.Voting"]
  n29["Core.Life"]
  n30["Core.Loot"]
  n31["Core.Movement"]
  n32["Core.Physics"]
  n33["Core.Professions"]
  n34["Core.Prototype"]
  n35["Core.Randomness"]
  n36["Core.Repair"]
  n37["Core.Siege"]
  n38["Core.StateMachines"]
  n39["Core.Stats"]
  n40["Core.Time"]
  n41["Core.Visuals"]
  n42["Core.Vitals"]
  n43["Runtime.Prototype"]
  n44["Runtime.Visuals"]
  n0 --> n3 & n8 & n42
  n1 --> n3 & n8 & n31
  n2 --> n3 & n8
  n3 --> n8 & n39
  n4 --> n8 & n35
  n5 --> n6 & n8
  n6 --> n8 & n30
  n7 --> n25 & n31
  n9 --> n8 & n22 & n33
  n10 --> n8
  n11 --> n8 & n17
  n12 --> n8
  n13 --> n8
  n14 --> n8 & n9 & n22 & n23 & n29 & n39 & n40 & n42
  n15 --> n8
  n16 --> n8 & n30 & n35
  n17 --> n8 & n21 & n35
  n18 --> n8 & n33 & n35
  n19 --> n8 & n30 & n35
  n20 --> n8 & n30 & n35
  n21 --> n8
  n22 --> n8
  n23 --> n8 & n15 & n24 & n25 & n26 & n27 & n28 & n38 & n40
  n24 --> n25
  n25 --> n8
  n26 --> n8
  n27 --> n24 & n25 & n26 & n38 & n40
  n28 --> n8
  n29 --> n8 & n23 & n42
  n30 --> n8 & n22 & n35
  n31 --> n8 & n42
  n32 --> n8
  n33 --> n8
  n34 --> n8 & n32 & n40 & n41 & n42
  n35 --> n8
  n36 --> n8 & n22
  n37 --> n8
  n38 --> n8 & n40
  n39 --> n8
  n40 --> n8
  n41 --> n8
  n42 --> n8 & n39 & n40
  n43 --> n31 & n34 & n39 & n40 & n41 & n42 & n44
  n44 --> n34 & n41
```
