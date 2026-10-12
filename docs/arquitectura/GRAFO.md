<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Dna"]; n18["Core.Echoes"]; n19["Core.Echoes.Consequences"]
  n20["Core.Echoes.Secrets"]; n21["Core.Echoes.Voting"]; n22["Core.Elevator"]; n23["Core.Equipment"]
  n24["Core.Events"]; n25["Core.Extraction"]; n26["Core.Floors"]; n27["Core.Floors.Omens"]
  n28["Core.Floors.Paths"]; n29["Core.Floors.Population"]; n30["Core.Floors.Remnants"]; n31["Core.Floors.Types"]
  n32["Core.Gathering"]; n33["Core.Hubs"]; n34["Core.Hud"]; n35["Core.Items"]
  n36["Core.Items.Catalog"]; n37["Core.Iteration"]; n38["Core.Iteration.Ending"]; n39["Core.Iteration.Presence"]
  n40["Core.Iteration.Readiness"]; n41["Core.Iteration.States"]; n42["Core.Iteration.Voting"]; n43["Core.Lab"]
  n44["Core.Life"]; n45["Core.Loot"]; n46["Core.Lore"]; n47["Core.Movement"]
  n48["Core.Physics"]; n49["Core.Professions"]; n50["Core.Profile"]; n51["Core.Progression"]
  n52["Core.Prototype"]; n53["Core.Randomness"]; n54["Core.Repair"]; n55["Core.Sandbox"]
  n56["Core.Siege"]; n57["Core.Siege.Defenses"]; n58["Core.Siege.Extraction"]; n59["Core.Siege.Scoring"]
  n60["Core.StateMachines"]; n61["Core.Stats"]; n62["Core.Talents"]; n63["Core.Time"]
  n64["Core.Visuals"]; n65["Core.Visuals.Packs"]; n66["Core.Vitals"]; n67["Core.World"]
  n68["Runtime.Art"]; n69["Runtime.Atico"]; n70["Runtime.Bootstrap"]; n71["Runtime.Characters"]
  n72["Runtime.Combat"]; n73["Runtime.Extraction"]; n74["Runtime.Hubs"]; n75["Runtime.Hud"]
  n76["Runtime.Incursion"]; n77["Runtime.Lab"]; n78["Runtime.Player"]; n79["Runtime.Prototype"]
  n80["Runtime.Sandbox"]; n81["Runtime.Sandbox.Ui"]; n82["Runtime.Sandbox.World"]; n83["Runtime.UI"]
  n84["Runtime.Visuals"]
  n0 --> n7 & n14 & n66
  n1 --> n7 & n14 & n47
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n39 & n63
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n61
  n8 --> n14 & n53
  n9 --> n0 & n7 & n14 & n47 & n66
  n10 --> n11 & n14
  n11 --> n0 & n14 & n45
  n12 --> n39 & n47
  n13 --> n14
  n15 --> n4 & n7 & n14 & n22 & n24 & n37 & n45 & n50
  n16 --> n14 & n35 & n36 & n49
  n17 --> n38 & n61
  n18 --> n14
  n19 --> n14 & n27
  n20 --> n14
  n21 --> n14
  n22 --> n14 & n16 & n35 & n37 & n44 & n61 & n63 & n66
  n23 --> n7 & n14 & n35 & n36 & n53 & n61
  n24 --> n14
  n25 --> n14
  n26 --> n14 & n45 & n52 & n53 & n64
  n27 --> n14 & n31 & n53
  n28 --> n14 & n49 & n53
  n29 --> n14 & n45 & n53
  n30 --> n14 & n45 & n53
  n31 --> n14
  n32 --> n14 & n36 & n45 & n53
  n33 --> n4 & n7 & n14 & n22 & n37 & n39 & n52 & n64
  n34 --> n14 & n24 & n37 & n52 & n63 & n66
  n35 --> n14
  n36 --> n14 & n35 & n66
  n37 --> n14 & n24 & n38 & n39 & n40 & n41 & n42 & n60 & n63
  n38 --> n39
  n39 --> n14
  n40 --> n14
  n41 --> n38 & n39 & n40 & n60 & n63
  n42 --> n14
  n43 --> n14 & n17
  n44 --> n14 & n37 & n66
  n45 --> n14 & n35 & n53
  n46 --> n21
  n47 --> n14 & n66
  n48 --> n14
  n49 --> n14
  n50 --> n14 & n17 & n46 & n61
  n51 --> n6 & n7 & n8 & n14 & n23 & n49 & n61 & n62 & n66
  n52 --> n14 & n48 & n63 & n64 & n66
  n53 --> n14
  n54 --> n14 & n35
  n55 --> n6 & n7 & n10 & n14 & n36 & n45 & n49 & n52 & n53 & n64 & n65
  n56 --> n14
  n57 --> n14
  n58 --> n14 & n59
  n59 --> n14
  n60 --> n14 & n63
  n61 --> n14
  n62 --> n7 & n14 & n61
  n63 --> n14
  n64 --> n14
  n66 --> n14 & n61 & n63
  n67 --> n14 & n32 & n53
  n68 --> n5
  n69 --> n4 & n7 & n15 & n24 & n34 & n37 & n38 & n52 & n56 & n58 & n64 & n66 & n70 & n72 & n76 & n78 & n79 & n84
  n70 --> n15 & n50 & n69 & n73 & n74 & n76 & n77 & n79 & n80
  n71 --> n2
  n72 --> n7 & n8 & n9 & n11 & n26 & n47 & n53 & n61 & n66 & n78
  n73 --> n15 & n17 & n25 & n37 & n38 & n45 & n50 & n70
  n74 --> n4 & n7 & n15 & n22 & n24 & n33 & n34 & n37 & n64 & n70 & n72 & n76 & n78 & n79 & n84
  n75 --> n34
  n76 --> n4 & n7 & n15 & n24 & n26 & n34 & n37 & n39 & n45 & n61 & n64 & n66 & n70 & n72 & n75 & n78 & n79 & n84
  n77 --> n15 & n17 & n37 & n43 & n50 & n70
  n78 --> n4 & n7 & n15 & n47 & n50 & n61 & n66
  n79 --> n47 & n52 & n61 & n63 & n64 & n66 & n84
  n80 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n23 & n35 & n36 & n45 & n49 & n51 & n53 & n55 & n61 & n62 & n64 & n66 & n67 & n71 & n78 & n81 & n82 & n83 & n84
  n81 --> n7 & n8 & n16 & n23 & n32 & n35 & n36 & n49 & n51 & n61 & n62 & n66 & n67 & n83
  n82 --> n32 & n35 & n51 & n67 & n84
  n84 --> n7 & n49 & n52 & n64
```
