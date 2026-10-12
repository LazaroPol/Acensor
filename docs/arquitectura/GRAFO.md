<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Classes"]; n7["Core.Combat"]
  n8["Core.Combat.Bosses"]; n9["Core.Combat.Enemies"]; n10["Core.Combat.Perception"]; n11["Core.Common"]
  n12["Core.Composition"]; n13["Core.Crafting"]; n14["Core.Dna"]; n15["Core.Echoes"]
  n16["Core.Echoes.Consequences"]; n17["Core.Echoes.Secrets"]; n18["Core.Echoes.Voting"]; n19["Core.Elevator"]
  n20["Core.Events"]; n21["Core.Floors"]; n22["Core.Floors.Omens"]; n23["Core.Floors.Paths"]
  n24["Core.Floors.Population"]; n25["Core.Floors.Remnants"]; n26["Core.Floors.Types"]; n27["Core.Hubs"]
  n28["Core.Hud"]; n29["Core.Items"]; n30["Core.Iteration"]; n31["Core.Iteration.Ending"]
  n32["Core.Iteration.Presence"]; n33["Core.Iteration.Readiness"]; n34["Core.Iteration.States"]; n35["Core.Iteration.Voting"]
  n36["Core.Lab"]; n37["Core.Life"]; n38["Core.Loot"]; n39["Core.Lore"]
  n40["Core.Movement"]; n41["Core.Physics"]; n42["Core.Professions"]; n43["Core.Profile"]
  n44["Core.Prototype"]; n45["Core.Randomness"]; n46["Core.Repair"]; n47["Core.Siege"]
  n48["Core.Siege.Defenses"]; n49["Core.Siege.Extraction"]; n50["Core.Siege.Scoring"]; n51["Core.StateMachines"]
  n52["Core.Stats"]; n53["Core.Time"]; n54["Core.Visuals"]; n55["Core.Visuals.Packs"]
  n56["Core.Vitals"]; n57["Runtime.Art"]; n58["Runtime.Bootstrap"]; n59["Runtime.Characters"]
  n60["Runtime.Hubs"]; n61["Runtime.Hud"]; n62["Runtime.Lab"]; n63["Runtime.Player"]
  n64["Runtime.Prototype"]; n65["Runtime.Visuals"]
  n0 --> n6 & n11 & n56
  n1 --> n6 & n11 & n40
  n3 --> n6 & n11
  n4 --> n6 & n11
  n5 --> n11 & n12 & n32 & n53
  n6 --> n11 & n52
  n7 --> n11 & n45
  n8 --> n9 & n11
  n9 --> n11 & n38
  n10 --> n32 & n40
  n12 --> n4 & n6 & n11 & n19 & n20 & n30
  n13 --> n11 & n29 & n42
  n14 --> n52
  n15 --> n11
  n16 --> n11 & n22
  n17 --> n11
  n18 --> n11
  n19 --> n11 & n13 & n29 & n30 & n37 & n52 & n53 & n56
  n20 --> n11
  n21 --> n11 & n38 & n45
  n22 --> n11 & n26 & n45
  n23 --> n11 & n42 & n45
  n24 --> n11 & n38 & n45
  n25 --> n11 & n38 & n45
  n26 --> n11
  n27 --> n4 & n6 & n11 & n19 & n30
  n28 --> n11 & n20 & n30 & n44 & n53 & n56
  n29 --> n11
  n30 --> n11 & n20 & n31 & n32 & n33 & n34 & n35 & n51 & n53
  n31 --> n32
  n32 --> n11
  n33 --> n11
  n34 --> n31 & n32 & n33 & n51 & n53
  n35 --> n11
  n36 --> n11 & n14
  n37 --> n11 & n30 & n56
  n38 --> n11 & n29 & n45
  n39 --> n18
  n40 --> n11 & n56
  n41 --> n11
  n42 --> n11
  n43 --> n14 & n39
  n44 --> n11 & n41 & n53 & n54 & n56
  n45 --> n11
  n46 --> n11 & n29
  n47 --> n11
  n48 --> n11
  n49 --> n11 & n50
  n50 --> n11
  n51 --> n11 & n53
  n52 --> n11
  n53 --> n11
  n54 --> n11
  n56 --> n11 & n52 & n53
  n57 --> n5
  n58 --> n12
  n59 --> n2
  n60 --> n4 & n6 & n12 & n19 & n27 & n30 & n58
  n61 --> n28
  n62 --> n12 & n14 & n36
  n63 --> n40 & n52 & n56
  n64 --> n40 & n44 & n52 & n53 & n54 & n56 & n65
  n65 --> n6 & n42 & n44 & n54
```
