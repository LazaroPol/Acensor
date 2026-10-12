<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Definitions"]; n18["Core.Crafting.Knowledge"]; n19["Core.Crafting.Stations"]
  n20["Core.Dna"]; n21["Core.Echoes"]; n22["Core.Echoes.Consequences"]; n23["Core.Echoes.Secrets"]
  n24["Core.Echoes.Voting"]; n25["Core.Elevator"]; n26["Core.Equipment"]; n27["Core.Events"]
  n28["Core.Extraction"]; n29["Core.Floors"]; n30["Core.Floors.Omens"]; n31["Core.Floors.Paths"]
  n32["Core.Floors.Population"]; n33["Core.Floors.Remnants"]; n34["Core.Floors.Types"]; n35["Core.Gathering"]
  n36["Core.Hubs"]; n37["Core.Hud"]; n38["Core.Items"]; n39["Core.Items.Catalog"]
  n40["Core.Iteration"]; n41["Core.Iteration.Ending"]; n42["Core.Iteration.Presence"]; n43["Core.Iteration.Readiness"]
  n44["Core.Iteration.States"]; n45["Core.Iteration.Voting"]; n46["Core.Lab"]; n47["Core.Life"]
  n48["Core.Loot"]; n49["Core.Lore"]; n50["Core.Movement"]; n51["Core.Physics"]
  n52["Core.Professions"]; n53["Core.Profile"]; n54["Core.Progression"]; n55["Core.Prototype"]
  n56["Core.Randomness"]; n57["Core.Repair"]; n58["Core.Sandbox"]; n59["Core.Siege"]
  n60["Core.Siege.Defenses"]; n61["Core.Siege.Extraction"]; n62["Core.Siege.Scoring"]; n63["Core.StateMachines"]
  n64["Core.Stats"]; n65["Core.Talents"]; n66["Core.Time"]; n67["Core.Visuals"]
  n68["Core.Visuals.Packs"]; n69["Core.Vitals"]; n70["Core.World"]; n71["Runtime.Art"]
  n72["Runtime.Atico"]; n73["Runtime.Bootstrap"]; n74["Runtime.Characters"]; n75["Runtime.Combat"]
  n76["Runtime.Extraction"]; n77["Runtime.Hubs"]; n78["Runtime.Hud"]; n79["Runtime.Incursion"]
  n80["Runtime.Lab"]; n81["Runtime.Player"]; n82["Runtime.Prototype"]; n83["Runtime.Sandbox"]
  n84["Runtime.Sandbox.Ui"]; n85["Runtime.Sandbox.World"]; n86["Runtime.UI"]; n87["Runtime.Visuals"]
  n0 --> n7 & n14 & n69
  n1 --> n7 & n14 & n50
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n42 & n66
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n64
  n8 --> n14 & n56
  n9 --> n0 & n7 & n14 & n50 & n69
  n10 --> n11 & n14
  n11 --> n0 & n14 & n48
  n12 --> n42 & n50
  n13 --> n14
  n15 --> n4 & n7 & n14 & n25 & n27 & n40 & n48 & n53
  n16 --> n14 & n38 & n39 & n52 & n56
  n17 --> n14 & n38 & n52
  n18 --> n14 & n17 & n38 & n52
  n19 --> n14 & n17 & n38 & n52
  n20 --> n41 & n64
  n21 --> n14
  n22 --> n14 & n30
  n23 --> n14
  n24 --> n14
  n25 --> n14 & n16 & n38 & n40 & n47 & n64 & n66 & n69
  n26 --> n7 & n14 & n38 & n39 & n56 & n64
  n27 --> n14
  n28 --> n14
  n29 --> n14 & n48 & n55 & n56 & n67
  n30 --> n14 & n34 & n56
  n31 --> n14 & n52 & n56
  n32 --> n14 & n48 & n56
  n33 --> n14 & n48 & n56
  n34 --> n14
  n35 --> n14 & n39 & n48 & n56
  n36 --> n4 & n7 & n14 & n25 & n40 & n42 & n55 & n67
  n37 --> n14 & n27 & n40 & n55 & n66 & n69
  n38 --> n14
  n39 --> n14 & n38 & n69
  n40 --> n14 & n27 & n41 & n42 & n43 & n44 & n45 & n63 & n66
  n41 --> n42
  n42 --> n14
  n43 --> n14
  n44 --> n41 & n42 & n43 & n63 & n66
  n45 --> n14
  n46 --> n14 & n20
  n47 --> n14 & n40 & n69
  n48 --> n14 & n38 & n56
  n49 --> n24
  n50 --> n14 & n69
  n51 --> n14
  n52 --> n14
  n53 --> n14 & n20 & n49 & n64
  n54 --> n6 & n7 & n8 & n14 & n26 & n52 & n64 & n65 & n69
  n55 --> n14 & n51 & n66 & n67 & n69
  n56 --> n14
  n57 --> n14 & n38
  n58 --> n6 & n7 & n10 & n11 & n14 & n39 & n48 & n52 & n55 & n56 & n67 & n68
  n59 --> n14
  n60 --> n14
  n61 --> n14 & n62
  n62 --> n14
  n63 --> n14 & n66
  n64 --> n14
  n65 --> n7 & n14 & n64
  n66 --> n14
  n67 --> n14
  n69 --> n14 & n64 & n66
  n70 --> n14 & n35 & n56
  n71 --> n5
  n72 --> n4 & n7 & n15 & n27 & n37 & n40 & n41 & n55 & n59 & n61 & n67 & n69 & n73 & n75 & n79 & n81 & n82 & n87
  n73 --> n15 & n53 & n72 & n76 & n77 & n79 & n80 & n82 & n83
  n74 --> n2
  n75 --> n7 & n8 & n9 & n11 & n29 & n50 & n56 & n64 & n69 & n81
  n76 --> n15 & n20 & n28 & n40 & n41 & n48 & n53 & n73
  n77 --> n4 & n7 & n15 & n25 & n27 & n36 & n37 & n40 & n67 & n73 & n75 & n79 & n81 & n82 & n87
  n78 --> n37
  n79 --> n4 & n7 & n15 & n27 & n29 & n37 & n40 & n42 & n48 & n64 & n67 & n69 & n73 & n75 & n78 & n81 & n82 & n87
  n80 --> n15 & n20 & n40 & n46 & n53 & n73
  n81 --> n4 & n7 & n15 & n50 & n53 & n64 & n69
  n82 --> n50 & n55 & n64 & n66 & n67 & n69 & n87
  n83 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n26 & n38 & n39 & n48 & n52 & n54 & n56 & n58 & n64 & n65 & n67 & n69 & n70 & n74 & n81 & n84 & n85 & n86 & n87
  n84 --> n7 & n8 & n16 & n26 & n35 & n38 & n39 & n52 & n54 & n64 & n65 & n69 & n70 & n86
  n85 --> n35 & n38 & n54 & n70 & n87
  n87 --> n7 & n52 & n55 & n67
```
