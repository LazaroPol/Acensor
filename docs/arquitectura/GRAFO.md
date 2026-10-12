<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Armory"]; n3["Core.Art"]
  n4["Core.Classes"]; n5["Core.Combat"]; n6["Core.Combat.Bosses"]; n7["Core.Combat.Enemies"]
  n8["Core.Combat.Perception"]; n9["Core.Common"]; n10["Core.Composition"]; n11["Core.Crafting"]
  n12["Core.Dna"]; n13["Core.Echoes"]; n14["Core.Echoes.Consequences"]; n15["Core.Echoes.Secrets"]
  n16["Core.Echoes.Voting"]; n17["Core.Elevator"]; n18["Core.Events"]; n19["Core.Floors"]
  n20["Core.Floors.Omens"]; n21["Core.Floors.Paths"]; n22["Core.Floors.Population"]; n23["Core.Floors.Remnants"]
  n24["Core.Floors.Types"]; n25["Core.Hubs"]; n26["Core.Hud"]; n27["Core.Items"]
  n28["Core.Iteration"]; n29["Core.Iteration.Ending"]; n30["Core.Iteration.Presence"]; n31["Core.Iteration.Readiness"]
  n32["Core.Iteration.States"]; n33["Core.Iteration.Voting"]; n34["Core.Lab"]; n35["Core.Life"]
  n36["Core.Loot"]; n37["Core.Lore"]; n38["Core.Movement"]; n39["Core.Physics"]
  n40["Core.Professions"]; n41["Core.Profile"]; n42["Core.Prototype"]; n43["Core.Randomness"]
  n44["Core.Repair"]; n45["Core.Siege"]; n46["Core.Siege.Defenses"]; n47["Core.Siege.Extraction"]
  n48["Core.Siege.Scoring"]; n49["Core.StateMachines"]; n50["Core.Stats"]; n51["Core.Time"]
  n52["Core.Visuals"]; n53["Core.Vitals"]; n54["Runtime.Art"]; n55["Runtime.Bootstrap"]
  n56["Runtime.Hubs"]; n57["Runtime.Hud"]; n58["Runtime.Lab"]; n59["Runtime.Player"]
  n60["Runtime.Prototype"]; n61["Runtime.Visuals"]
  n0 --> n4 & n9 & n53
  n1 --> n4 & n9 & n38
  n2 --> n4 & n9
  n3 --> n9 & n10 & n30 & n51
  n4 --> n9 & n50
  n5 --> n9 & n43
  n6 --> n7 & n9
  n7 --> n9 & n36
  n8 --> n30 & n38
  n10 --> n2 & n4 & n9 & n17 & n18 & n28
  n11 --> n9 & n27 & n40
  n12 --> n50
  n13 --> n9
  n14 --> n9 & n20
  n15 --> n9
  n16 --> n9
  n17 --> n9 & n11 & n27 & n28 & n35 & n50 & n51 & n53
  n18 --> n9
  n19 --> n9 & n36 & n43
  n20 --> n9 & n24 & n43
  n21 --> n9 & n40 & n43
  n22 --> n9 & n36 & n43
  n23 --> n9 & n36 & n43
  n24 --> n9
  n25 --> n2 & n4 & n9 & n17 & n28
  n26 --> n9 & n18 & n28 & n42 & n51 & n53
  n27 --> n9
  n28 --> n9 & n18 & n29 & n30 & n31 & n32 & n33 & n49 & n51
  n29 --> n30
  n30 --> n9
  n31 --> n9
  n32 --> n29 & n30 & n31 & n49 & n51
  n33 --> n9
  n34 --> n9 & n12
  n35 --> n9 & n28 & n53
  n36 --> n9 & n27 & n43
  n37 --> n16
  n38 --> n9 & n53
  n39 --> n9
  n40 --> n9
  n41 --> n12 & n37
  n42 --> n9 & n39 & n51 & n52 & n53
  n43 --> n9
  n44 --> n9 & n27
  n45 --> n9
  n46 --> n9
  n47 --> n9 & n48
  n48 --> n9
  n49 --> n9 & n51
  n50 --> n9
  n51 --> n9
  n52 --> n9
  n53 --> n9 & n50 & n51
  n54 --> n3
  n55 --> n10
  n56 --> n2 & n4 & n10 & n17 & n25 & n28 & n55
  n57 --> n26
  n58 --> n34
  n59 --> n38 & n50 & n53
  n60 --> n38 & n42 & n50 & n51 & n52 & n53 & n61
  n61 --> n42 & n52
```
