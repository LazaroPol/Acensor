<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Content"]; n18["Core.Crafting.Definitions"]; n19["Core.Crafting.Knowledge"]
  n20["Core.Crafting.Mods"]; n21["Core.Crafting.Queue"]; n22["Core.Crafting.Stations"]; n23["Core.Dna"]
  n24["Core.Echoes"]; n25["Core.Echoes.Consequences"]; n26["Core.Echoes.Secrets"]; n27["Core.Echoes.Voting"]
  n28["Core.Elevator"]; n29["Core.Equipment"]; n30["Core.Events"]; n31["Core.Extraction"]
  n32["Core.Floors"]; n33["Core.Floors.Omens"]; n34["Core.Floors.Paths"]; n35["Core.Floors.Population"]
  n36["Core.Floors.Remnants"]; n37["Core.Floors.Types"]; n38["Core.Gathering"]; n39["Core.Hubs"]
  n40["Core.Hud"]; n41["Core.Items"]; n42["Core.Items.Catalog"]; n43["Core.Iteration"]
  n44["Core.Iteration.Ending"]; n45["Core.Iteration.Presence"]; n46["Core.Iteration.Readiness"]; n47["Core.Iteration.States"]
  n48["Core.Iteration.Voting"]; n49["Core.Lab"]; n50["Core.Life"]; n51["Core.Loot"]
  n52["Core.Lore"]; n53["Core.Movement"]; n54["Core.Physics"]; n55["Core.Professions"]
  n56["Core.Profile"]; n57["Core.Progression"]; n58["Core.Prototype"]; n59["Core.Randomness"]
  n60["Core.Repair"]; n61["Core.Sandbox"]; n62["Core.Siege"]; n63["Core.Siege.Defenses"]
  n64["Core.Siege.Extraction"]; n65["Core.Siege.Scoring"]; n66["Core.StateMachines"]; n67["Core.Stats"]
  n68["Core.Talents"]; n69["Core.Time"]; n70["Core.Visuals"]; n71["Core.Visuals.Packs"]
  n72["Core.Vitals"]; n73["Core.World"]; n74["Runtime.Art"]; n75["Runtime.Atico"]
  n76["Runtime.Bootstrap"]; n77["Runtime.Characters"]; n78["Runtime.Combat"]; n79["Runtime.Extraction"]
  n80["Runtime.Hubs"]; n81["Runtime.Hud"]; n82["Runtime.Incursion"]; n83["Runtime.Lab"]
  n84["Runtime.Player"]; n85["Runtime.Prototype"]; n86["Runtime.Sandbox"]; n87["Runtime.Sandbox.Ui"]
  n88["Runtime.Sandbox.World"]; n89["Runtime.UI"]; n90["Runtime.Visuals"]
  n0 --> n7 & n14 & n72
  n1 --> n7 & n14 & n53
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n45 & n69
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n67
  n8 --> n14 & n59
  n9 --> n0 & n7 & n14 & n53 & n72
  n10 --> n11 & n14
  n11 --> n0 & n14 & n51
  n12 --> n45 & n53
  n13 --> n14
  n15 --> n4 & n7 & n14 & n28 & n30 & n43 & n51 & n56
  n16 --> n14 & n41 & n42 & n55 & n59
  n17 --> n18 & n19 & n22 & n41 & n42 & n55
  n18 --> n14 & n41 & n55
  n19 --> n14 & n18 & n41 & n55
  n20 --> n14 & n41 & n55
  n21 --> n14 & n18 & n22 & n41 & n42 & n55 & n69
  n22 --> n14 & n18 & n41 & n55
  n23 --> n44 & n67
  n24 --> n14
  n25 --> n14 & n33
  n26 --> n14
  n27 --> n14
  n28 --> n14 & n16 & n41 & n43 & n50 & n67 & n69 & n72
  n29 --> n7 & n14 & n41 & n42 & n59 & n67
  n30 --> n14
  n31 --> n14
  n32 --> n14 & n51 & n58 & n59 & n70
  n33 --> n14 & n37 & n59
  n34 --> n14 & n55 & n59
  n35 --> n14 & n51 & n59
  n36 --> n14 & n51 & n59
  n37 --> n14
  n38 --> n14 & n42 & n51 & n59
  n39 --> n4 & n7 & n14 & n28 & n43 & n45 & n58 & n70
  n40 --> n14 & n30 & n43 & n58 & n69 & n72
  n41 --> n14
  n42 --> n14 & n41 & n72
  n43 --> n14 & n30 & n44 & n45 & n46 & n47 & n48 & n66 & n69
  n44 --> n45
  n45 --> n14
  n46 --> n14
  n47 --> n44 & n45 & n46 & n66 & n69
  n48 --> n14
  n49 --> n14 & n23
  n50 --> n14 & n43 & n72
  n51 --> n14 & n41 & n59
  n52 --> n27
  n53 --> n14 & n72
  n54 --> n14
  n55 --> n14
  n56 --> n14 & n23 & n52 & n67
  n57 --> n6 & n7 & n8 & n14 & n29 & n55 & n67 & n68 & n72
  n58 --> n14 & n54 & n69 & n70 & n72
  n59 --> n14
  n60 --> n14 & n41
  n61 --> n6 & n7 & n10 & n11 & n14 & n42 & n51 & n55 & n58 & n59 & n70 & n71
  n62 --> n14
  n63 --> n14
  n64 --> n14 & n65
  n65 --> n14
  n66 --> n14 & n69
  n67 --> n14
  n68 --> n7 & n14 & n67
  n69 --> n14
  n70 --> n14
  n72 --> n14 & n67 & n69
  n73 --> n14 & n38 & n59
  n74 --> n5
  n75 --> n4 & n7 & n15 & n30 & n40 & n43 & n44 & n58 & n62 & n64 & n70 & n72 & n76 & n78 & n82 & n84 & n85 & n90
  n76 --> n15 & n56 & n75 & n79 & n80 & n82 & n83 & n85 & n86
  n77 --> n2
  n78 --> n7 & n8 & n9 & n11 & n32 & n53 & n59 & n67 & n72 & n84
  n79 --> n15 & n23 & n31 & n43 & n44 & n51 & n56 & n76
  n80 --> n4 & n7 & n15 & n28 & n30 & n39 & n40 & n43 & n70 & n76 & n78 & n82 & n84 & n85 & n90
  n81 --> n40
  n82 --> n4 & n7 & n15 & n30 & n32 & n40 & n43 & n45 & n51 & n67 & n70 & n72 & n76 & n78 & n81 & n84 & n85 & n90
  n83 --> n15 & n23 & n43 & n49 & n56 & n76
  n84 --> n4 & n7 & n15 & n53 & n56 & n67 & n72
  n85 --> n53 & n58 & n67 & n69 & n70 & n72 & n90
  n86 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n29 & n41 & n42 & n51 & n55 & n57 & n59 & n61 & n67 & n68 & n70 & n72 & n73 & n77 & n84 & n87 & n88 & n89 & n90
  n87 --> n7 & n8 & n16 & n29 & n38 & n41 & n42 & n55 & n57 & n67 & n68 & n72 & n73 & n89
  n88 --> n38 & n41 & n57 & n73 & n90
  n90 --> n7 & n55 & n58 & n70
```
