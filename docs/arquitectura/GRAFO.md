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
  n24["Core.Crafting.Session"]; n25["Core.Crafting.Stations"]; n26["Core.Dna"]; n27["Core.Echoes"]
  n28["Core.Echoes.Consequences"]; n29["Core.Echoes.Secrets"]; n30["Core.Echoes.Voting"]; n31["Core.Elevator"]
  n32["Core.Equipment"]; n33["Core.Events"]; n34["Core.Extraction"]; n35["Core.Floors"]
  n36["Core.Floors.Omens"]; n37["Core.Floors.Paths"]; n38["Core.Floors.Population"]; n39["Core.Floors.Remnants"]
  n40["Core.Floors.Types"]; n41["Core.Gathering"]; n42["Core.Hubs"]; n43["Core.Hud"]
  n44["Core.Items"]; n45["Core.Items.Catalog"]; n46["Core.Iteration"]; n47["Core.Iteration.Ending"]
  n48["Core.Iteration.Presence"]; n49["Core.Iteration.Readiness"]; n50["Core.Iteration.States"]; n51["Core.Iteration.Voting"]
  n52["Core.Lab"]; n53["Core.Life"]; n54["Core.Loot"]; n55["Core.Lore"]
  n56["Core.Movement"]; n57["Core.Physics"]; n58["Core.Professions"]; n59["Core.Profile"]
  n60["Core.Progression"]; n61["Core.Prototype"]; n62["Core.Randomness"]; n63["Core.Repair"]
  n64["Core.Sandbox"]; n65["Core.Siege"]; n66["Core.Siege.Defenses"]; n67["Core.Siege.Extraction"]
  n68["Core.Siege.Scoring"]; n69["Core.StateMachines"]; n70["Core.Stats"]; n71["Core.Talents"]
  n72["Core.Time"]; n73["Core.Visuals"]; n74["Core.Visuals.Packs"]; n75["Core.Vitals"]
  n76["Core.World"]; n77["Runtime.Art"]; n78["Runtime.Atico"]; n79["Runtime.Bootstrap"]
  n80["Runtime.Characters"]; n81["Runtime.Combat"]; n82["Runtime.Crafting.Authoring"]; n83["Runtime.Extraction"]
  n84["Runtime.Hubs"]; n85["Runtime.Hud"]; n86["Runtime.Incursion"]; n87["Runtime.Lab"]
  n88["Runtime.Player"]; n89["Runtime.Prototype"]; n90["Runtime.Sandbox"]; n91["Runtime.Sandbox.Ui"]
  n92["Runtime.Sandbox.World"]; n93["Runtime.UI"]; n94["Runtime.Visuals"]
  n0 --> n7 & n14 & n75
  n1 --> n7 & n14 & n56
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n48 & n72
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n70
  n8 --> n14 & n62
  n9 --> n0 & n7 & n14 & n56 & n75
  n10 --> n11 & n14
  n11 --> n0 & n14 & n54
  n12 --> n48 & n56
  n13 --> n14
  n15 --> n4 & n7 & n14 & n31 & n33 & n46 & n54 & n59
  n16 --> n14 & n44 & n45 & n58 & n62
  n17 --> n14 & n18 & n19 & n20 & n21 & n25 & n44 & n45 & n58
  n18 --> n19 & n20 & n25 & n44 & n45 & n58
  n19 --> n14 & n44 & n58
  n20 --> n14 & n19 & n44 & n58
  n21 --> n14 & n44 & n58
  n22 --> n14 & n19 & n25 & n44 & n45 & n58 & n72
  n23 --> n14 & n44 & n58
  n24 --> n14 & n17 & n19 & n20 & n22 & n25 & n44 & n58 & n62 & n72
  n25 --> n14 & n19 & n44 & n58
  n26 --> n47 & n70
  n27 --> n14
  n28 --> n14 & n36
  n29 --> n14
  n30 --> n14
  n31 --> n14 & n16 & n44 & n46 & n53 & n70 & n72 & n75
  n32 --> n7 & n14 & n44 & n45 & n62 & n70
  n33 --> n14
  n34 --> n14
  n35 --> n14 & n54 & n61 & n62 & n73
  n36 --> n14 & n40 & n62
  n37 --> n14 & n58 & n62
  n38 --> n14 & n54 & n62
  n39 --> n14 & n54 & n62
  n40 --> n14
  n41 --> n14 & n45 & n54 & n62
  n42 --> n4 & n7 & n14 & n31 & n46 & n48 & n61 & n73
  n43 --> n14 & n33 & n46 & n61 & n72 & n75
  n44 --> n14
  n45 --> n14 & n44 & n75
  n46 --> n14 & n33 & n47 & n48 & n49 & n50 & n51 & n69 & n72
  n47 --> n48
  n48 --> n14
  n49 --> n14
  n50 --> n47 & n48 & n49 & n69 & n72
  n51 --> n14
  n52 --> n14 & n26
  n53 --> n14 & n46 & n75
  n54 --> n14 & n44 & n62
  n55 --> n30
  n56 --> n14 & n75
  n57 --> n14
  n58 --> n14
  n59 --> n14 & n26 & n55 & n70
  n60 --> n6 & n7 & n8 & n14 & n32 & n58 & n70 & n71 & n75
  n61 --> n14 & n57 & n72 & n73 & n75
  n62 --> n14
  n63 --> n14 & n44
  n64 --> n6 & n7 & n10 & n11 & n14 & n45 & n54 & n58 & n61 & n62 & n73 & n74
  n65 --> n14
  n66 --> n14
  n67 --> n14 & n68
  n68 --> n14
  n69 --> n14 & n72
  n70 --> n14
  n71 --> n7 & n14 & n70
  n72 --> n14
  n73 --> n14
  n75 --> n14 & n70 & n72
  n76 --> n14 & n41 & n62
  n77 --> n5
  n78 --> n4 & n7 & n15 & n33 & n43 & n46 & n47 & n61 & n65 & n67 & n73 & n75 & n79 & n81 & n86 & n88 & n89 & n94
  n79 --> n15 & n59 & n78 & n83 & n84 & n86 & n87 & n89 & n90
  n80 --> n2
  n81 --> n7 & n8 & n9 & n11 & n35 & n56 & n62 & n70 & n75 & n88
  n82 --> n16 & n17 & n19 & n20 & n21 & n25 & n44 & n58
  n83 --> n15 & n26 & n34 & n46 & n47 & n54 & n59 & n79
  n84 --> n4 & n7 & n15 & n31 & n33 & n42 & n43 & n46 & n73 & n79 & n81 & n86 & n88 & n89 & n94
  n85 --> n43
  n86 --> n4 & n7 & n15 & n33 & n35 & n43 & n46 & n48 & n54 & n70 & n73 & n75 & n79 & n81 & n85 & n88 & n89 & n94
  n87 --> n15 & n26 & n46 & n52 & n59 & n79
  n88 --> n4 & n7 & n15 & n56 & n59 & n70 & n75
  n89 --> n56 & n61 & n70 & n72 & n73 & n75 & n94
  n90 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n17 & n18 & n19 & n24 & n32 & n44 & n45 & n54 & n58 & n60 & n62 & n64 & n70 & n71 & n73 & n75 & n76 & n80 & n88 & n91 & n92 & n93 & n94
  n91 --> n7 & n8 & n16 & n22 & n24 & n25 & n32 & n41 & n44 & n45 & n58 & n60 & n70 & n71 & n75 & n76 & n93
  n92 --> n41 & n44 & n60 & n76 & n94
  n94 --> n7 & n58 & n61 & n73
```
