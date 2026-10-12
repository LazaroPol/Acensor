<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Definitions"]; n18["Core.Crafting.Knowledge"]; n19["Core.Crafting.Mods"]
  n20["Core.Crafting.Queue"]; n21["Core.Crafting.Stations"]; n22["Core.Dna"]; n23["Core.Echoes"]
  n24["Core.Echoes.Consequences"]; n25["Core.Echoes.Secrets"]; n26["Core.Echoes.Voting"]; n27["Core.Elevator"]
  n28["Core.Equipment"]; n29["Core.Events"]; n30["Core.Extraction"]; n31["Core.Floors"]
  n32["Core.Floors.Omens"]; n33["Core.Floors.Paths"]; n34["Core.Floors.Population"]; n35["Core.Floors.Remnants"]
  n36["Core.Floors.Types"]; n37["Core.Gathering"]; n38["Core.Hubs"]; n39["Core.Hud"]
  n40["Core.Items"]; n41["Core.Items.Catalog"]; n42["Core.Iteration"]; n43["Core.Iteration.Ending"]
  n44["Core.Iteration.Presence"]; n45["Core.Iteration.Readiness"]; n46["Core.Iteration.States"]; n47["Core.Iteration.Voting"]
  n48["Core.Lab"]; n49["Core.Life"]; n50["Core.Loot"]; n51["Core.Lore"]
  n52["Core.Movement"]; n53["Core.Physics"]; n54["Core.Professions"]; n55["Core.Profile"]
  n56["Core.Progression"]; n57["Core.Prototype"]; n58["Core.Randomness"]; n59["Core.Repair"]
  n60["Core.Sandbox"]; n61["Core.Siege"]; n62["Core.Siege.Defenses"]; n63["Core.Siege.Extraction"]
  n64["Core.Siege.Scoring"]; n65["Core.StateMachines"]; n66["Core.Stats"]; n67["Core.Talents"]
  n68["Core.Time"]; n69["Core.Visuals"]; n70["Core.Visuals.Packs"]; n71["Core.Vitals"]
  n72["Core.World"]; n73["Runtime.Art"]; n74["Runtime.Atico"]; n75["Runtime.Bootstrap"]
  n76["Runtime.Characters"]; n77["Runtime.Combat"]; n78["Runtime.Extraction"]; n79["Runtime.Hubs"]
  n80["Runtime.Hud"]; n81["Runtime.Incursion"]; n82["Runtime.Lab"]; n83["Runtime.Player"]
  n84["Runtime.Prototype"]; n85["Runtime.Sandbox"]; n86["Runtime.Sandbox.Ui"]; n87["Runtime.Sandbox.World"]
  n88["Runtime.UI"]; n89["Runtime.Visuals"]
  n0 --> n7 & n14 & n71
  n1 --> n7 & n14 & n52
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n44 & n68
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n66
  n8 --> n14 & n58
  n9 --> n0 & n7 & n14 & n52 & n71
  n10 --> n11 & n14
  n11 --> n0 & n14 & n50
  n12 --> n44 & n52
  n13 --> n14
  n15 --> n4 & n7 & n14 & n27 & n29 & n42 & n50 & n55
  n16 --> n14 & n40 & n41 & n54 & n58
  n17 --> n14 & n40 & n54
  n18 --> n14 & n17 & n40 & n54
  n19 --> n14 & n40 & n54
  n20 --> n14 & n17 & n21 & n40 & n41 & n54 & n68
  n21 --> n14 & n17 & n40 & n54
  n22 --> n43 & n66
  n23 --> n14
  n24 --> n14 & n32
  n25 --> n14
  n26 --> n14
  n27 --> n14 & n16 & n40 & n42 & n49 & n66 & n68 & n71
  n28 --> n7 & n14 & n40 & n41 & n58 & n66
  n29 --> n14
  n30 --> n14
  n31 --> n14 & n50 & n57 & n58 & n69
  n32 --> n14 & n36 & n58
  n33 --> n14 & n54 & n58
  n34 --> n14 & n50 & n58
  n35 --> n14 & n50 & n58
  n36 --> n14
  n37 --> n14 & n41 & n50 & n58
  n38 --> n4 & n7 & n14 & n27 & n42 & n44 & n57 & n69
  n39 --> n14 & n29 & n42 & n57 & n68 & n71
  n40 --> n14
  n41 --> n14 & n40 & n71
  n42 --> n14 & n29 & n43 & n44 & n45 & n46 & n47 & n65 & n68
  n43 --> n44
  n44 --> n14
  n45 --> n14
  n46 --> n43 & n44 & n45 & n65 & n68
  n47 --> n14
  n48 --> n14 & n22
  n49 --> n14 & n42 & n71
  n50 --> n14 & n40 & n58
  n51 --> n26
  n52 --> n14 & n71
  n53 --> n14
  n54 --> n14
  n55 --> n14 & n22 & n51 & n66
  n56 --> n6 & n7 & n8 & n14 & n28 & n54 & n66 & n67 & n71
  n57 --> n14 & n53 & n68 & n69 & n71
  n58 --> n14
  n59 --> n14 & n40
  n60 --> n6 & n7 & n10 & n11 & n14 & n41 & n50 & n54 & n57 & n58 & n69 & n70
  n61 --> n14
  n62 --> n14
  n63 --> n14 & n64
  n64 --> n14
  n65 --> n14 & n68
  n66 --> n14
  n67 --> n7 & n14 & n66
  n68 --> n14
  n69 --> n14
  n71 --> n14 & n66 & n68
  n72 --> n14 & n37 & n58
  n73 --> n5
  n74 --> n4 & n7 & n15 & n29 & n39 & n42 & n43 & n57 & n61 & n63 & n69 & n71 & n75 & n77 & n81 & n83 & n84 & n89
  n75 --> n15 & n55 & n74 & n78 & n79 & n81 & n82 & n84 & n85
  n76 --> n2
  n77 --> n7 & n8 & n9 & n11 & n31 & n52 & n58 & n66 & n71 & n83
  n78 --> n15 & n22 & n30 & n42 & n43 & n50 & n55 & n75
  n79 --> n4 & n7 & n15 & n27 & n29 & n38 & n39 & n42 & n69 & n75 & n77 & n81 & n83 & n84 & n89
  n80 --> n39
  n81 --> n4 & n7 & n15 & n29 & n31 & n39 & n42 & n44 & n50 & n66 & n69 & n71 & n75 & n77 & n80 & n83 & n84 & n89
  n82 --> n15 & n22 & n42 & n48 & n55 & n75
  n83 --> n4 & n7 & n15 & n52 & n55 & n66 & n71
  n84 --> n52 & n57 & n66 & n68 & n69 & n71 & n89
  n85 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n28 & n40 & n41 & n50 & n54 & n56 & n58 & n60 & n66 & n67 & n69 & n71 & n72 & n76 & n83 & n86 & n87 & n88 & n89
  n86 --> n7 & n8 & n16 & n28 & n37 & n40 & n41 & n54 & n56 & n66 & n67 & n71 & n72 & n88
  n87 --> n37 & n40 & n56 & n72 & n89
  n89 --> n7 & n54 & n57 & n69
```
