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
  n24["Core.Floors.Types"]; n25["Core.Hud"]; n26["Core.Items"]; n27["Core.Iteration"]
  n28["Core.Iteration.Ending"]; n29["Core.Iteration.Presence"]; n30["Core.Iteration.Readiness"]; n31["Core.Iteration.States"]
  n32["Core.Iteration.Voting"]; n33["Core.Lab"]; n34["Core.Life"]; n35["Core.Loot"]
  n36["Core.Lore"]; n37["Core.Movement"]; n38["Core.Physics"]; n39["Core.Professions"]
  n40["Core.Profile"]; n41["Core.Prototype"]; n42["Core.Randomness"]; n43["Core.Repair"]
  n44["Core.Siege"]; n45["Core.Siege.Defenses"]; n46["Core.Siege.Extraction"]; n47["Core.Siege.Scoring"]
  n48["Core.StateMachines"]; n49["Core.Stats"]; n50["Core.Time"]; n51["Core.Visuals"]
  n52["Core.Vitals"]; n53["Runtime.Art"]; n54["Runtime.Bootstrap"]; n55["Runtime.Hud"]
  n56["Runtime.Lab"]; n57["Runtime.Player"]; n58["Runtime.Prototype"]; n59["Runtime.Visuals"]
  n0 --> n4 & n9 & n52
  n1 --> n4 & n9 & n37
  n2 --> n4 & n9
  n3 --> n9 & n10 & n29 & n50
  n4 --> n9 & n49
  n5 --> n9 & n42
  n6 --> n7 & n9
  n7 --> n9 & n35
  n8 --> n29 & n37
  n10 --> n9 & n18
  n11 --> n9 & n26 & n39
  n12 --> n49
  n13 --> n9
  n14 --> n9 & n20
  n15 --> n9
  n16 --> n9
  n17 --> n9 & n11 & n26 & n27 & n34 & n49 & n50 & n52
  n18 --> n9
  n19 --> n9 & n35 & n42
  n20 --> n9 & n24 & n42
  n21 --> n9 & n39 & n42
  n22 --> n9 & n35 & n42
  n23 --> n9 & n35 & n42
  n24 --> n9
  n25 --> n9 & n18 & n27 & n41 & n50 & n52
  n26 --> n9
  n27 --> n9 & n18 & n28 & n29 & n30 & n31 & n32 & n48 & n50
  n28 --> n29
  n29 --> n9
  n30 --> n9
  n31 --> n28 & n29 & n30 & n48 & n50
  n32 --> n9
  n33 --> n9 & n12
  n34 --> n9 & n27 & n52
  n35 --> n9 & n26 & n42
  n36 --> n16
  n37 --> n9 & n52
  n38 --> n9
  n39 --> n9
  n40 --> n12 & n36
  n41 --> n9 & n38 & n50 & n51 & n52
  n42 --> n9
  n43 --> n9 & n26
  n44 --> n9
  n45 --> n9
  n46 --> n9 & n47
  n47 --> n9
  n48 --> n9 & n50
  n49 --> n9
  n50 --> n9
  n51 --> n9
  n52 --> n9 & n49 & n50
  n53 --> n3
  n54 --> n10
  n55 --> n25
  n56 --> n33
  n57 --> n37 & n49 & n52
  n58 --> n37 & n41 & n49 & n50 & n51 & n52 & n59
  n59 --> n41 & n51
```
