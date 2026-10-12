<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]
  n1["Core.Abilities.Mobility"]
  n2["Core.Armory"]
  n3["Core.Art"]
  n4["Core.Classes"]
  n5["Core.Combat"]
  n6["Core.Combat.Bosses"]
  n7["Core.Combat.Enemies"]
  n8["Core.Combat.Perception"]
  n9["Core.Common"]
  n10["Core.Composition"]
  n11["Core.Crafting"]
  n12["Core.Dna"]
  n13["Core.Echoes"]
  n14["Core.Echoes.Consequences"]
  n15["Core.Echoes.Secrets"]
  n16["Core.Echoes.Voting"]
  n17["Core.Elevator"]
  n18["Core.Events"]
  n19["Core.Floors"]
  n20["Core.Floors.Omens"]
  n21["Core.Floors.Paths"]
  n22["Core.Floors.Population"]
  n23["Core.Floors.Remnants"]
  n24["Core.Floors.Types"]
  n25["Core.Hud"]
  n26["Core.Items"]
  n27["Core.Iteration"]
  n28["Core.Iteration.Ending"]
  n29["Core.Iteration.Presence"]
  n30["Core.Iteration.Readiness"]
  n31["Core.Iteration.States"]
  n32["Core.Iteration.Voting"]
  n33["Core.Life"]
  n34["Core.Loot"]
  n35["Core.Movement"]
  n36["Core.Physics"]
  n37["Core.Professions"]
  n38["Core.Prototype"]
  n39["Core.Randomness"]
  n40["Core.Repair"]
  n41["Core.Siege"]
  n42["Core.Siege.Defenses"]
  n43["Core.Siege.Extraction"]
  n44["Core.Siege.Scoring"]
  n45["Core.StateMachines"]
  n46["Core.Stats"]
  n47["Core.Time"]
  n48["Core.Visuals"]
  n49["Core.Vitals"]
  n50["Runtime.Art"]
  n51["Runtime.Bootstrap"]
  n52["Runtime.Hud"]
  n53["Runtime.Player"]
  n54["Runtime.Prototype"]
  n55["Runtime.Visuals"]
  n0 --> n4 & n9 & n49
  n1 --> n4 & n9 & n35
  n2 --> n4 & n9
  n3 --> n9 & n10 & n29 & n47
  n4 --> n9 & n46
  n5 --> n9 & n39
  n6 --> n7 & n9
  n7 --> n9 & n34
  n8 --> n29 & n35
  n10 --> n9 & n18
  n11 --> n9 & n26 & n37
  n13 --> n9
  n14 --> n9 & n20
  n15 --> n9
  n16 --> n9
  n17 --> n9 & n11 & n26 & n27 & n33 & n46 & n47 & n49
  n18 --> n9
  n19 --> n9 & n34 & n39
  n20 --> n9 & n24 & n39
  n21 --> n9 & n37 & n39
  n22 --> n9 & n34 & n39
  n23 --> n9 & n34 & n39
  n24 --> n9
  n25 --> n9 & n18 & n27 & n38 & n47 & n49
  n26 --> n9
  n27 --> n9 & n18 & n28 & n29 & n30 & n31 & n32 & n45 & n47
  n28 --> n29
  n29 --> n9
  n30 --> n9
  n31 --> n28 & n29 & n30 & n45 & n47
  n32 --> n9
  n33 --> n9 & n27 & n49
  n34 --> n9 & n26 & n39
  n35 --> n9 & n49
  n36 --> n9
  n37 --> n9
  n38 --> n9 & n36 & n47 & n48 & n49
  n39 --> n9
  n40 --> n9 & n26
  n41 --> n9
  n42 --> n9
  n43 --> n9 & n44
  n44 --> n9
  n45 --> n9 & n47
  n46 --> n9
  n47 --> n9
  n48 --> n9
  n49 --> n9 & n46 & n47
  n50 --> n3
  n51 --> n10
  n52 --> n25
  n53 --> n35 & n46 & n49
  n54 --> n35 & n38 & n46 & n47 & n48 & n49 & n55
  n55 --> n38 & n48
```
