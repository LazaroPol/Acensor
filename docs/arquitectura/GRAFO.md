<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Definitions"]; n18["Core.Crafting.Knowledge"]; n19["Core.Crafting.Queue"]
  n20["Core.Crafting.Stations"]; n21["Core.Dna"]; n22["Core.Echoes"]; n23["Core.Echoes.Consequences"]
  n24["Core.Echoes.Secrets"]; n25["Core.Echoes.Voting"]; n26["Core.Elevator"]; n27["Core.Equipment"]
  n28["Core.Events"]; n29["Core.Extraction"]; n30["Core.Floors"]; n31["Core.Floors.Omens"]
  n32["Core.Floors.Paths"]; n33["Core.Floors.Population"]; n34["Core.Floors.Remnants"]; n35["Core.Floors.Types"]
  n36["Core.Gathering"]; n37["Core.Hubs"]; n38["Core.Hud"]; n39["Core.Items"]
  n40["Core.Items.Catalog"]; n41["Core.Iteration"]; n42["Core.Iteration.Ending"]; n43["Core.Iteration.Presence"]
  n44["Core.Iteration.Readiness"]; n45["Core.Iteration.States"]; n46["Core.Iteration.Voting"]; n47["Core.Lab"]
  n48["Core.Life"]; n49["Core.Loot"]; n50["Core.Lore"]; n51["Core.Movement"]
  n52["Core.Physics"]; n53["Core.Professions"]; n54["Core.Profile"]; n55["Core.Progression"]
  n56["Core.Prototype"]; n57["Core.Randomness"]; n58["Core.Repair"]; n59["Core.Sandbox"]
  n60["Core.Siege"]; n61["Core.Siege.Defenses"]; n62["Core.Siege.Extraction"]; n63["Core.Siege.Scoring"]
  n64["Core.StateMachines"]; n65["Core.Stats"]; n66["Core.Talents"]; n67["Core.Time"]
  n68["Core.Visuals"]; n69["Core.Visuals.Packs"]; n70["Core.Vitals"]; n71["Core.World"]
  n72["Runtime.Art"]; n73["Runtime.Atico"]; n74["Runtime.Bootstrap"]; n75["Runtime.Characters"]
  n76["Runtime.Combat"]; n77["Runtime.Extraction"]; n78["Runtime.Hubs"]; n79["Runtime.Hud"]
  n80["Runtime.Incursion"]; n81["Runtime.Lab"]; n82["Runtime.Player"]; n83["Runtime.Prototype"]
  n84["Runtime.Sandbox"]; n85["Runtime.Sandbox.Ui"]; n86["Runtime.Sandbox.World"]; n87["Runtime.UI"]
  n88["Runtime.Visuals"]
  n0 --> n7 & n14 & n70
  n1 --> n7 & n14 & n51
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n43 & n67
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n65
  n8 --> n14 & n57
  n9 --> n0 & n7 & n14 & n51 & n70
  n10 --> n11 & n14
  n11 --> n0 & n14 & n49
  n12 --> n43 & n51
  n13 --> n14
  n15 --> n4 & n7 & n14 & n26 & n28 & n41 & n49 & n54
  n16 --> n14 & n39 & n40 & n53 & n57
  n17 --> n14 & n39 & n53
  n18 --> n14 & n17 & n39 & n53
  n19 --> n14 & n17 & n20 & n39 & n40 & n53 & n67
  n20 --> n14 & n17 & n39 & n53
  n21 --> n42 & n65
  n22 --> n14
  n23 --> n14 & n31
  n24 --> n14
  n25 --> n14
  n26 --> n14 & n16 & n39 & n41 & n48 & n65 & n67 & n70
  n27 --> n7 & n14 & n39 & n40 & n57 & n65
  n28 --> n14
  n29 --> n14
  n30 --> n14 & n49 & n56 & n57 & n68
  n31 --> n14 & n35 & n57
  n32 --> n14 & n53 & n57
  n33 --> n14 & n49 & n57
  n34 --> n14 & n49 & n57
  n35 --> n14
  n36 --> n14 & n40 & n49 & n57
  n37 --> n4 & n7 & n14 & n26 & n41 & n43 & n56 & n68
  n38 --> n14 & n28 & n41 & n56 & n67 & n70
  n39 --> n14
  n40 --> n14 & n39 & n70
  n41 --> n14 & n28 & n42 & n43 & n44 & n45 & n46 & n64 & n67
  n42 --> n43
  n43 --> n14
  n44 --> n14
  n45 --> n42 & n43 & n44 & n64 & n67
  n46 --> n14
  n47 --> n14 & n21
  n48 --> n14 & n41 & n70
  n49 --> n14 & n39 & n57
  n50 --> n25
  n51 --> n14 & n70
  n52 --> n14
  n53 --> n14
  n54 --> n14 & n21 & n50 & n65
  n55 --> n6 & n7 & n8 & n14 & n27 & n53 & n65 & n66 & n70
  n56 --> n14 & n52 & n67 & n68 & n70
  n57 --> n14
  n58 --> n14 & n39
  n59 --> n6 & n7 & n10 & n11 & n14 & n40 & n49 & n53 & n56 & n57 & n68 & n69
  n60 --> n14
  n61 --> n14
  n62 --> n14 & n63
  n63 --> n14
  n64 --> n14 & n67
  n65 --> n14
  n66 --> n7 & n14 & n65
  n67 --> n14
  n68 --> n14
  n70 --> n14 & n65 & n67
  n71 --> n14 & n36 & n57
  n72 --> n5
  n73 --> n4 & n7 & n15 & n28 & n38 & n41 & n42 & n56 & n60 & n62 & n68 & n70 & n74 & n76 & n80 & n82 & n83 & n88
  n74 --> n15 & n54 & n73 & n77 & n78 & n80 & n81 & n83 & n84
  n75 --> n2
  n76 --> n7 & n8 & n9 & n11 & n30 & n51 & n57 & n65 & n70 & n82
  n77 --> n15 & n21 & n29 & n41 & n42 & n49 & n54 & n74
  n78 --> n4 & n7 & n15 & n26 & n28 & n37 & n38 & n41 & n68 & n74 & n76 & n80 & n82 & n83 & n88
  n79 --> n38
  n80 --> n4 & n7 & n15 & n28 & n30 & n38 & n41 & n43 & n49 & n65 & n68 & n70 & n74 & n76 & n79 & n82 & n83 & n88
  n81 --> n15 & n21 & n41 & n47 & n54 & n74
  n82 --> n4 & n7 & n15 & n51 & n54 & n65 & n70
  n83 --> n51 & n56 & n65 & n67 & n68 & n70 & n88
  n84 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n27 & n39 & n40 & n49 & n53 & n55 & n57 & n59 & n65 & n66 & n68 & n70 & n71 & n75 & n82 & n85 & n86 & n87 & n88
  n85 --> n7 & n8 & n16 & n27 & n36 & n39 & n40 & n53 & n55 & n65 & n66 & n70 & n71 & n87
  n86 --> n36 & n39 & n55 & n71 & n88
  n88 --> n7 & n53 & n56 & n68
```
