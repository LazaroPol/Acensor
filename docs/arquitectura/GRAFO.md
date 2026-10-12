<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Content"]; n18["Core.Crafting.Definitions"]; n19["Core.Crafting.Knowledge"]
  n20["Core.Crafting.Mods"]; n21["Core.Crafting.Queue"]; n22["Core.Crafting.Save"]; n23["Core.Crafting.Stations"]
  n24["Core.Dna"]; n25["Core.Echoes"]; n26["Core.Echoes.Consequences"]; n27["Core.Echoes.Secrets"]
  n28["Core.Echoes.Voting"]; n29["Core.Elevator"]; n30["Core.Equipment"]; n31["Core.Events"]
  n32["Core.Extraction"]; n33["Core.Floors"]; n34["Core.Floors.Omens"]; n35["Core.Floors.Paths"]
  n36["Core.Floors.Population"]; n37["Core.Floors.Remnants"]; n38["Core.Floors.Types"]; n39["Core.Gathering"]
  n40["Core.Hubs"]; n41["Core.Hud"]; n42["Core.Items"]; n43["Core.Items.Catalog"]
  n44["Core.Iteration"]; n45["Core.Iteration.Ending"]; n46["Core.Iteration.Presence"]; n47["Core.Iteration.Readiness"]
  n48["Core.Iteration.States"]; n49["Core.Iteration.Voting"]; n50["Core.Lab"]; n51["Core.Life"]
  n52["Core.Loot"]; n53["Core.Lore"]; n54["Core.Movement"]; n55["Core.Physics"]
  n56["Core.Professions"]; n57["Core.Profile"]; n58["Core.Progression"]; n59["Core.Prototype"]
  n60["Core.Randomness"]; n61["Core.Repair"]; n62["Core.Sandbox"]; n63["Core.Siege"]
  n64["Core.Siege.Defenses"]; n65["Core.Siege.Extraction"]; n66["Core.Siege.Scoring"]; n67["Core.StateMachines"]
  n68["Core.Stats"]; n69["Core.Talents"]; n70["Core.Time"]; n71["Core.Visuals"]
  n72["Core.Visuals.Packs"]; n73["Core.Vitals"]; n74["Core.World"]; n75["Runtime.Art"]
  n76["Runtime.Atico"]; n77["Runtime.Bootstrap"]; n78["Runtime.Characters"]; n79["Runtime.Combat"]
  n80["Runtime.Extraction"]; n81["Runtime.Hubs"]; n82["Runtime.Hud"]; n83["Runtime.Incursion"]
  n84["Runtime.Lab"]; n85["Runtime.Player"]; n86["Runtime.Prototype"]; n87["Runtime.Sandbox"]
  n88["Runtime.Sandbox.Ui"]; n89["Runtime.Sandbox.World"]; n90["Runtime.UI"]; n91["Runtime.Visuals"]
  n0 --> n7 & n14 & n73
  n1 --> n7 & n14 & n54
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n46 & n70
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n68
  n8 --> n14 & n60
  n9 --> n0 & n7 & n14 & n54 & n73
  n10 --> n11 & n14
  n11 --> n0 & n14 & n52
  n12 --> n46 & n54
  n13 --> n14
  n15 --> n4 & n7 & n14 & n29 & n31 & n44 & n52 & n57
  n16 --> n14 & n42 & n43 & n56 & n60
  n17 --> n18 & n19 & n23 & n42 & n43 & n56
  n18 --> n14 & n42 & n56
  n19 --> n14 & n18 & n42 & n56
  n20 --> n14 & n42 & n56
  n21 --> n14 & n18 & n23 & n42 & n43 & n56 & n70
  n22 --> n14 & n42 & n56
  n23 --> n14 & n18 & n42 & n56
  n24 --> n45 & n68
  n25 --> n14
  n26 --> n14 & n34
  n27 --> n14
  n28 --> n14
  n29 --> n14 & n16 & n42 & n44 & n51 & n68 & n70 & n73
  n30 --> n7 & n14 & n42 & n43 & n60 & n68
  n31 --> n14
  n32 --> n14
  n33 --> n14 & n52 & n59 & n60 & n71
  n34 --> n14 & n38 & n60
  n35 --> n14 & n56 & n60
  n36 --> n14 & n52 & n60
  n37 --> n14 & n52 & n60
  n38 --> n14
  n39 --> n14 & n43 & n52 & n60
  n40 --> n4 & n7 & n14 & n29 & n44 & n46 & n59 & n71
  n41 --> n14 & n31 & n44 & n59 & n70 & n73
  n42 --> n14
  n43 --> n14 & n42 & n73
  n44 --> n14 & n31 & n45 & n46 & n47 & n48 & n49 & n67 & n70
  n45 --> n46
  n46 --> n14
  n47 --> n14
  n48 --> n45 & n46 & n47 & n67 & n70
  n49 --> n14
  n50 --> n14 & n24
  n51 --> n14 & n44 & n73
  n52 --> n14 & n42 & n60
  n53 --> n28
  n54 --> n14 & n73
  n55 --> n14
  n56 --> n14
  n57 --> n14 & n24 & n53 & n68
  n58 --> n6 & n7 & n8 & n14 & n30 & n56 & n68 & n69 & n73
  n59 --> n14 & n55 & n70 & n71 & n73
  n60 --> n14
  n61 --> n14 & n42
  n62 --> n6 & n7 & n10 & n11 & n14 & n43 & n52 & n56 & n59 & n60 & n71 & n72
  n63 --> n14
  n64 --> n14
  n65 --> n14 & n66
  n66 --> n14
  n67 --> n14 & n70
  n68 --> n14
  n69 --> n7 & n14 & n68
  n70 --> n14
  n71 --> n14
  n73 --> n14 & n68 & n70
  n74 --> n14 & n39 & n60
  n75 --> n5
  n76 --> n4 & n7 & n15 & n31 & n41 & n44 & n45 & n59 & n63 & n65 & n71 & n73 & n77 & n79 & n83 & n85 & n86 & n91
  n77 --> n15 & n57 & n76 & n80 & n81 & n83 & n84 & n86 & n87
  n78 --> n2
  n79 --> n7 & n8 & n9 & n11 & n33 & n54 & n60 & n68 & n73 & n85
  n80 --> n15 & n24 & n32 & n44 & n45 & n52 & n57 & n77
  n81 --> n4 & n7 & n15 & n29 & n31 & n40 & n41 & n44 & n71 & n77 & n79 & n83 & n85 & n86 & n91
  n82 --> n41
  n83 --> n4 & n7 & n15 & n31 & n33 & n41 & n44 & n46 & n52 & n68 & n71 & n73 & n77 & n79 & n82 & n85 & n86 & n91
  n84 --> n15 & n24 & n44 & n50 & n57 & n77
  n85 --> n4 & n7 & n15 & n54 & n57 & n68 & n73
  n86 --> n54 & n59 & n68 & n70 & n71 & n73 & n91
  n87 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n30 & n42 & n43 & n52 & n56 & n58 & n60 & n62 & n68 & n69 & n71 & n73 & n74 & n78 & n85 & n88 & n89 & n90 & n91
  n88 --> n7 & n8 & n16 & n30 & n39 & n42 & n43 & n56 & n58 & n68 & n69 & n73 & n74 & n90
  n89 --> n39 & n42 & n58 & n74 & n91
  n91 --> n7 & n56 & n59 & n71
```
