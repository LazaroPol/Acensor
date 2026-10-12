<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Definitions"]; n18["Core.Dna"]; n19["Core.Echoes"]
  n20["Core.Echoes.Consequences"]; n21["Core.Echoes.Secrets"]; n22["Core.Echoes.Voting"]; n23["Core.Elevator"]
  n24["Core.Equipment"]; n25["Core.Events"]; n26["Core.Extraction"]; n27["Core.Floors"]
  n28["Core.Floors.Omens"]; n29["Core.Floors.Paths"]; n30["Core.Floors.Population"]; n31["Core.Floors.Remnants"]
  n32["Core.Floors.Types"]; n33["Core.Gathering"]; n34["Core.Hubs"]; n35["Core.Hud"]
  n36["Core.Items"]; n37["Core.Items.Catalog"]; n38["Core.Iteration"]; n39["Core.Iteration.Ending"]
  n40["Core.Iteration.Presence"]; n41["Core.Iteration.Readiness"]; n42["Core.Iteration.States"]; n43["Core.Iteration.Voting"]
  n44["Core.Lab"]; n45["Core.Life"]; n46["Core.Loot"]; n47["Core.Lore"]
  n48["Core.Movement"]; n49["Core.Physics"]; n50["Core.Professions"]; n51["Core.Profile"]
  n52["Core.Progression"]; n53["Core.Prototype"]; n54["Core.Randomness"]; n55["Core.Repair"]
  n56["Core.Sandbox"]; n57["Core.Siege"]; n58["Core.Siege.Defenses"]; n59["Core.Siege.Extraction"]
  n60["Core.Siege.Scoring"]; n61["Core.StateMachines"]; n62["Core.Stats"]; n63["Core.Talents"]
  n64["Core.Time"]; n65["Core.Visuals"]; n66["Core.Visuals.Packs"]; n67["Core.Vitals"]
  n68["Core.World"]; n69["Runtime.Art"]; n70["Runtime.Atico"]; n71["Runtime.Bootstrap"]
  n72["Runtime.Characters"]; n73["Runtime.Combat"]; n74["Runtime.Extraction"]; n75["Runtime.Hubs"]
  n76["Runtime.Hud"]; n77["Runtime.Incursion"]; n78["Runtime.Lab"]; n79["Runtime.Player"]
  n80["Runtime.Prototype"]; n81["Runtime.Sandbox"]; n82["Runtime.Sandbox.Ui"]; n83["Runtime.Sandbox.World"]
  n84["Runtime.UI"]; n85["Runtime.Visuals"]
  n0 --> n7 & n14 & n67
  n1 --> n7 & n14 & n48
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n40 & n64
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n62
  n8 --> n14 & n54
  n9 --> n0 & n7 & n14 & n48 & n67
  n10 --> n11 & n14
  n11 --> n0 & n14 & n46
  n12 --> n40 & n48
  n13 --> n14
  n15 --> n4 & n7 & n14 & n23 & n25 & n38 & n46 & n51
  n16 --> n14 & n36 & n37 & n50 & n54
  n17 --> n14 & n36 & n50
  n18 --> n39 & n62
  n19 --> n14
  n20 --> n14 & n28
  n21 --> n14
  n22 --> n14
  n23 --> n14 & n16 & n36 & n38 & n45 & n62 & n64 & n67
  n24 --> n7 & n14 & n36 & n37 & n54 & n62
  n25 --> n14
  n26 --> n14
  n27 --> n14 & n46 & n53 & n54 & n65
  n28 --> n14 & n32 & n54
  n29 --> n14 & n50 & n54
  n30 --> n14 & n46 & n54
  n31 --> n14 & n46 & n54
  n32 --> n14
  n33 --> n14 & n37 & n46 & n54
  n34 --> n4 & n7 & n14 & n23 & n38 & n40 & n53 & n65
  n35 --> n14 & n25 & n38 & n53 & n64 & n67
  n36 --> n14
  n37 --> n14 & n36 & n67
  n38 --> n14 & n25 & n39 & n40 & n41 & n42 & n43 & n61 & n64
  n39 --> n40
  n40 --> n14
  n41 --> n14
  n42 --> n39 & n40 & n41 & n61 & n64
  n43 --> n14
  n44 --> n14 & n18
  n45 --> n14 & n38 & n67
  n46 --> n14 & n36 & n54
  n47 --> n22
  n48 --> n14 & n67
  n49 --> n14
  n50 --> n14
  n51 --> n14 & n18 & n47 & n62
  n52 --> n6 & n7 & n8 & n14 & n24 & n50 & n62 & n63 & n67
  n53 --> n14 & n49 & n64 & n65 & n67
  n54 --> n14
  n55 --> n14 & n36
  n56 --> n6 & n7 & n10 & n11 & n14 & n37 & n46 & n50 & n53 & n54 & n65 & n66
  n57 --> n14
  n58 --> n14
  n59 --> n14 & n60
  n60 --> n14
  n61 --> n14 & n64
  n62 --> n14
  n63 --> n7 & n14 & n62
  n64 --> n14
  n65 --> n14
  n67 --> n14 & n62 & n64
  n68 --> n14 & n33 & n54
  n69 --> n5
  n70 --> n4 & n7 & n15 & n25 & n35 & n38 & n39 & n53 & n57 & n59 & n65 & n67 & n71 & n73 & n77 & n79 & n80 & n85
  n71 --> n15 & n51 & n70 & n74 & n75 & n77 & n78 & n80 & n81
  n72 --> n2
  n73 --> n7 & n8 & n9 & n11 & n27 & n48 & n54 & n62 & n67 & n79
  n74 --> n15 & n18 & n26 & n38 & n39 & n46 & n51 & n71
  n75 --> n4 & n7 & n15 & n23 & n25 & n34 & n35 & n38 & n65 & n71 & n73 & n77 & n79 & n80 & n85
  n76 --> n35
  n77 --> n4 & n7 & n15 & n25 & n27 & n35 & n38 & n40 & n46 & n62 & n65 & n67 & n71 & n73 & n76 & n79 & n80 & n85
  n78 --> n15 & n18 & n38 & n44 & n51 & n71
  n79 --> n4 & n7 & n15 & n48 & n51 & n62 & n67
  n80 --> n48 & n53 & n62 & n64 & n65 & n67 & n85
  n81 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n24 & n36 & n37 & n46 & n50 & n52 & n54 & n56 & n62 & n63 & n65 & n67 & n68 & n72 & n79 & n82 & n83 & n84 & n85
  n82 --> n7 & n8 & n16 & n24 & n33 & n36 & n37 & n50 & n52 & n62 & n63 & n67 & n68 & n84
  n83 --> n33 & n36 & n52 & n68 & n85
  n85 --> n7 & n50 & n53 & n65
```
