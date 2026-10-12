<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Authoring"]; n18["Core.Crafting.Content"]; n19["Core.Crafting.Definitions"]
  n20["Core.Crafting.Knowledge"]; n21["Core.Crafting.Mods"]; n22["Core.Crafting.Queue"]; n23["Core.Crafting.Save"]
  n24["Core.Crafting.Stations"]; n25["Core.Dna"]; n26["Core.Echoes"]; n27["Core.Echoes.Consequences"]
  n28["Core.Echoes.Secrets"]; n29["Core.Echoes.Voting"]; n30["Core.Elevator"]; n31["Core.Equipment"]
  n32["Core.Events"]; n33["Core.Extraction"]; n34["Core.Floors"]; n35["Core.Floors.Omens"]
  n36["Core.Floors.Paths"]; n37["Core.Floors.Population"]; n38["Core.Floors.Remnants"]; n39["Core.Floors.Types"]
  n40["Core.Gathering"]; n41["Core.Hubs"]; n42["Core.Hud"]; n43["Core.Items"]
  n44["Core.Items.Catalog"]; n45["Core.Iteration"]; n46["Core.Iteration.Ending"]; n47["Core.Iteration.Presence"]
  n48["Core.Iteration.Readiness"]; n49["Core.Iteration.States"]; n50["Core.Iteration.Voting"]; n51["Core.Lab"]
  n52["Core.Life"]; n53["Core.Loot"]; n54["Core.Lore"]; n55["Core.Movement"]
  n56["Core.Physics"]; n57["Core.Professions"]; n58["Core.Profile"]; n59["Core.Progression"]
  n60["Core.Prototype"]; n61["Core.Randomness"]; n62["Core.Repair"]; n63["Core.Sandbox"]
  n64["Core.Siege"]; n65["Core.Siege.Defenses"]; n66["Core.Siege.Extraction"]; n67["Core.Siege.Scoring"]
  n68["Core.StateMachines"]; n69["Core.Stats"]; n70["Core.Talents"]; n71["Core.Time"]
  n72["Core.Visuals"]; n73["Core.Visuals.Packs"]; n74["Core.Vitals"]; n75["Core.World"]
  n76["Runtime.Art"]; n77["Runtime.Atico"]; n78["Runtime.Bootstrap"]; n79["Runtime.Characters"]
  n80["Runtime.Combat"]; n81["Runtime.Crafting.Authoring"]; n82["Runtime.Extraction"]; n83["Runtime.Hubs"]
  n84["Runtime.Hud"]; n85["Runtime.Incursion"]; n86["Runtime.Lab"]; n87["Runtime.Player"]
  n88["Runtime.Prototype"]; n89["Runtime.Sandbox"]; n90["Runtime.Sandbox.Ui"]; n91["Runtime.Sandbox.World"]
  n92["Runtime.UI"]; n93["Runtime.Visuals"]
  n0 --> n7 & n14 & n74
  n1 --> n7 & n14 & n55
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n47 & n71
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n69
  n8 --> n14 & n61
  n9 --> n0 & n7 & n14 & n55 & n74
  n10 --> n11 & n14
  n11 --> n0 & n14 & n53
  n12 --> n47 & n55
  n13 --> n14
  n15 --> n4 & n7 & n14 & n30 & n32 & n45 & n53 & n58
  n16 --> n14 & n43 & n44 & n57 & n61
  n17 --> n14 & n18 & n19 & n20 & n21 & n24 & n43 & n44 & n57
  n18 --> n19 & n20 & n24 & n43 & n44 & n57
  n19 --> n14 & n43 & n57
  n20 --> n14 & n19 & n43 & n57
  n21 --> n14 & n43 & n57
  n22 --> n14 & n19 & n24 & n43 & n44 & n57 & n71
  n23 --> n14 & n43 & n57
  n24 --> n14 & n19 & n43 & n57
  n25 --> n46 & n69
  n26 --> n14
  n27 --> n14 & n35
  n28 --> n14
  n29 --> n14
  n30 --> n14 & n16 & n43 & n45 & n52 & n69 & n71 & n74
  n31 --> n7 & n14 & n43 & n44 & n61 & n69
  n32 --> n14
  n33 --> n14
  n34 --> n14 & n53 & n60 & n61 & n72
  n35 --> n14 & n39 & n61
  n36 --> n14 & n57 & n61
  n37 --> n14 & n53 & n61
  n38 --> n14 & n53 & n61
  n39 --> n14
  n40 --> n14 & n44 & n53 & n61
  n41 --> n4 & n7 & n14 & n30 & n45 & n47 & n60 & n72
  n42 --> n14 & n32 & n45 & n60 & n71 & n74
  n43 --> n14
  n44 --> n14 & n43 & n74
  n45 --> n14 & n32 & n46 & n47 & n48 & n49 & n50 & n68 & n71
  n46 --> n47
  n47 --> n14
  n48 --> n14
  n49 --> n46 & n47 & n48 & n68 & n71
  n50 --> n14
  n51 --> n14 & n25
  n52 --> n14 & n45 & n74
  n53 --> n14 & n43 & n61
  n54 --> n29
  n55 --> n14 & n74
  n56 --> n14
  n57 --> n14
  n58 --> n14 & n25 & n54 & n69
  n59 --> n6 & n7 & n8 & n14 & n31 & n57 & n69 & n70 & n74
  n60 --> n14 & n56 & n71 & n72 & n74
  n61 --> n14
  n62 --> n14 & n43
  n63 --> n6 & n7 & n10 & n11 & n14 & n44 & n53 & n57 & n60 & n61 & n72 & n73
  n64 --> n14
  n65 --> n14
  n66 --> n14 & n67
  n67 --> n14
  n68 --> n14 & n71
  n69 --> n14
  n70 --> n7 & n14 & n69
  n71 --> n14
  n72 --> n14
  n74 --> n14 & n69 & n71
  n75 --> n14 & n40 & n61
  n76 --> n5
  n77 --> n4 & n7 & n15 & n32 & n42 & n45 & n46 & n60 & n64 & n66 & n72 & n74 & n78 & n80 & n85 & n87 & n88 & n93
  n78 --> n15 & n58 & n77 & n82 & n83 & n85 & n86 & n88 & n89
  n79 --> n2
  n80 --> n7 & n8 & n9 & n11 & n34 & n55 & n61 & n69 & n74 & n87
  n81 --> n16 & n17 & n19 & n20 & n21 & n24 & n43 & n57
  n82 --> n15 & n25 & n33 & n45 & n46 & n53 & n58 & n78
  n83 --> n4 & n7 & n15 & n30 & n32 & n41 & n42 & n45 & n72 & n78 & n80 & n85 & n87 & n88 & n93
  n84 --> n42
  n85 --> n4 & n7 & n15 & n32 & n34 & n42 & n45 & n47 & n53 & n69 & n72 & n74 & n78 & n80 & n84 & n87 & n88 & n93
  n86 --> n15 & n25 & n45 & n51 & n58 & n78
  n87 --> n4 & n7 & n15 & n55 & n58 & n69 & n74
  n88 --> n55 & n60 & n69 & n71 & n72 & n74 & n93
  n89 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n31 & n43 & n44 & n53 & n57 & n59 & n61 & n63 & n69 & n70 & n72 & n74 & n75 & n79 & n87 & n90 & n91 & n92 & n93
  n90 --> n7 & n8 & n16 & n31 & n40 & n43 & n44 & n57 & n59 & n69 & n70 & n74 & n75 & n92
  n91 --> n40 & n43 & n59 & n75 & n93
  n93 --> n7 & n57 & n60 & n72
```
