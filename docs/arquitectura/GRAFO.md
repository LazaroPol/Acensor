<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Authoring"]; n18["Core.Crafting.Content"]; n19["Core.Crafting.Definitions"]
  n20["Core.Crafting.Knowledge"]; n21["Core.Crafting.Mods"]; n22["Core.Crafting.Play"]; n23["Core.Crafting.Queue"]
  n24["Core.Crafting.Save"]; n25["Core.Crafting.Session"]; n26["Core.Crafting.Stations"]; n27["Core.Dna"]
  n28["Core.Echoes"]; n29["Core.Echoes.Consequences"]; n30["Core.Echoes.Secrets"]; n31["Core.Echoes.Voting"]
  n32["Core.Elevator"]; n33["Core.Equipment"]; n34["Core.Events"]; n35["Core.Extraction"]
  n36["Core.Floors"]; n37["Core.Floors.Omens"]; n38["Core.Floors.Paths"]; n39["Core.Floors.Population"]
  n40["Core.Floors.Remnants"]; n41["Core.Floors.Types"]; n42["Core.Gathering"]; n43["Core.Hubs"]
  n44["Core.Hud"]; n45["Core.Items"]; n46["Core.Items.Catalog"]; n47["Core.Iteration"]
  n48["Core.Iteration.Ending"]; n49["Core.Iteration.Presence"]; n50["Core.Iteration.Readiness"]; n51["Core.Iteration.States"]
  n52["Core.Iteration.Voting"]; n53["Core.Lab"]; n54["Core.Life"]; n55["Core.Loot"]
  n56["Core.Lore"]; n57["Core.Movement"]; n58["Core.Physics"]; n59["Core.Professions"]
  n60["Core.Profile"]; n61["Core.Progression"]; n62["Core.Prototype"]; n63["Core.Randomness"]
  n64["Core.Repair"]; n65["Core.Sandbox"]; n66["Core.Siege"]; n67["Core.Siege.Defenses"]
  n68["Core.Siege.Extraction"]; n69["Core.Siege.Scoring"]; n70["Core.StateMachines"]; n71["Core.Stats"]
  n72["Core.Talents"]; n73["Core.Time"]; n74["Core.Visuals"]; n75["Core.Visuals.Packs"]
  n76["Core.Vitals"]; n77["Core.World"]; n78["Runtime.Art"]; n79["Runtime.Atico"]
  n80["Runtime.Bootstrap"]; n81["Runtime.Characters"]; n82["Runtime.Combat"]; n83["Runtime.Crafting.Authoring"]
  n84["Runtime.Extraction"]; n85["Runtime.Hubs"]; n86["Runtime.Hud"]; n87["Runtime.Incursion"]
  n88["Runtime.Lab"]; n89["Runtime.Player"]; n90["Runtime.Prototype"]; n91["Runtime.Sandbox"]
  n92["Runtime.Sandbox.Ui"]; n93["Runtime.Sandbox.World"]; n94["Runtime.UI"]; n95["Runtime.Visuals"]
  n0 --> n7 & n14 & n76
  n1 --> n7 & n14 & n57
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n49 & n73
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n71
  n8 --> n14 & n63
  n9 --> n0 & n7 & n14 & n57 & n76
  n10 --> n11 & n14
  n11 --> n0 & n14 & n55
  n12 --> n49 & n57
  n13 --> n14
  n15 --> n4 & n7 & n14 & n32 & n34 & n47 & n55 & n60
  n16 --> n14 & n45 & n46 & n59 & n63
  n17 --> n14 & n18 & n19 & n20 & n21 & n26 & n45 & n46 & n59
  n18 --> n19 & n20 & n26 & n45 & n46 & n59
  n19 --> n14 & n45 & n59
  n20 --> n14 & n19 & n45 & n59
  n21 --> n14 & n45 & n59
  n22 --> n14 & n17 & n19 & n20 & n21 & n24 & n25 & n33 & n45 & n46 & n55 & n59 & n63
  n23 --> n14 & n19 & n26 & n45 & n46 & n59 & n73
  n24 --> n14 & n45 & n59
  n25 --> n14 & n17 & n19 & n20 & n23 & n26 & n45 & n59 & n63 & n73
  n26 --> n14 & n19 & n45 & n59
  n27 --> n48 & n71
  n28 --> n14
  n29 --> n14 & n37
  n30 --> n14
  n31 --> n14
  n32 --> n14 & n16 & n45 & n47 & n54 & n71 & n73 & n76
  n33 --> n7 & n14 & n45 & n46 & n63 & n71
  n34 --> n14
  n35 --> n14
  n36 --> n14 & n55 & n62 & n63 & n74
  n37 --> n14 & n41 & n63
  n38 --> n14 & n59 & n63
  n39 --> n14 & n55 & n63
  n40 --> n14 & n55 & n63
  n41 --> n14
  n42 --> n14 & n46 & n55 & n63
  n43 --> n4 & n7 & n14 & n32 & n47 & n49 & n62 & n74
  n44 --> n14 & n34 & n47 & n62 & n73 & n76
  n45 --> n14
  n46 --> n14 & n45 & n76
  n47 --> n14 & n34 & n48 & n49 & n50 & n51 & n52 & n70 & n73
  n48 --> n49
  n49 --> n14
  n50 --> n14
  n51 --> n48 & n49 & n50 & n70 & n73
  n52 --> n14
  n53 --> n14 & n27
  n54 --> n14 & n47 & n76
  n55 --> n14 & n45 & n63
  n56 --> n31
  n57 --> n14 & n76
  n58 --> n14
  n59 --> n14
  n60 --> n14 & n27 & n56 & n71
  n61 --> n6 & n7 & n8 & n14 & n33 & n59 & n71 & n72 & n76
  n62 --> n14 & n58 & n73 & n74 & n76
  n63 --> n14
  n64 --> n14 & n45
  n65 --> n6 & n7 & n10 & n11 & n14 & n46 & n55 & n59 & n62 & n63 & n74 & n75
  n66 --> n14
  n67 --> n14
  n68 --> n14 & n69
  n69 --> n14
  n70 --> n14 & n73
  n71 --> n14
  n72 --> n7 & n14 & n71
  n73 --> n14
  n74 --> n14
  n76 --> n14 & n71 & n73
  n77 --> n14 & n42 & n63
  n78 --> n5
  n79 --> n4 & n7 & n15 & n34 & n44 & n47 & n48 & n62 & n66 & n68 & n74 & n76 & n80 & n82 & n87 & n89 & n90 & n95
  n80 --> n15 & n60 & n79 & n84 & n85 & n87 & n88 & n90 & n91
  n81 --> n2
  n82 --> n7 & n8 & n9 & n11 & n36 & n57 & n63 & n71 & n76 & n89
  n83 --> n16 & n17 & n19 & n20 & n21 & n26 & n45 & n59
  n84 --> n15 & n27 & n35 & n47 & n48 & n55 & n60 & n80
  n85 --> n4 & n7 & n15 & n32 & n34 & n43 & n44 & n47 & n74 & n80 & n82 & n87 & n89 & n90 & n95
  n86 --> n44
  n87 --> n4 & n7 & n15 & n34 & n36 & n44 & n47 & n49 & n55 & n71 & n74 & n76 & n80 & n82 & n86 & n89 & n90 & n95
  n88 --> n15 & n27 & n47 & n53 & n60 & n80
  n89 --> n4 & n7 & n15 & n57 & n60 & n71 & n76
  n90 --> n57 & n62 & n71 & n73 & n74 & n76 & n95
  n91 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n17 & n18 & n19 & n20 & n21 & n22 & n24 & n25 & n33 & n45 & n46 & n55 & n59 & n61 & n63 & n65 & n71 & n72 & n74 & n76 & n77 & n81 & n89 & n92 & n93 & n94 & n95
  n92 --> n7 & n8 & n16 & n21 & n22 & n23 & n25 & n26 & n33 & n42 & n45 & n46 & n59 & n61 & n71 & n72 & n76 & n77 & n94
  n93 --> n42 & n45 & n61 & n77 & n95
  n95 --> n7 & n59 & n62 & n74
```
