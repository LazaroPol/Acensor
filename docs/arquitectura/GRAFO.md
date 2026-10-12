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
  n56["Core.Lore"]; n57["Core.Movement"]; n58["Core.Networking"]; n59["Core.Physics"]
  n60["Core.Professions"]; n61["Core.Profile"]; n62["Core.Progression"]; n63["Core.Prototype"]
  n64["Core.Randomness"]; n65["Core.Repair"]; n66["Core.Sandbox"]; n67["Core.Siege"]
  n68["Core.Siege.Defenses"]; n69["Core.Siege.Extraction"]; n70["Core.Siege.Scoring"]; n71["Core.StateMachines"]
  n72["Core.Stats"]; n73["Core.Talents"]; n74["Core.Time"]; n75["Core.Visuals"]
  n76["Core.Visuals.Packs"]; n77["Core.Vitals"]; n78["Core.World"]; n79["Runtime.Art"]
  n80["Runtime.Atico"]; n81["Runtime.Bootstrap"]; n82["Runtime.Characters"]; n83["Runtime.Combat"]
  n84["Runtime.Crafting.Authoring"]; n85["Runtime.Extraction"]; n86["Runtime.Hubs"]; n87["Runtime.Hud"]
  n88["Runtime.Incursion"]; n89["Runtime.Lab"]; n90["Runtime.Networking"]; n91["Runtime.Player"]
  n92["Runtime.Prototype"]; n93["Runtime.Sandbox"]; n94["Runtime.Sandbox.Ui"]; n95["Runtime.Sandbox.World"]
  n96["Runtime.UI"]; n97["Runtime.Visuals"]
  n0 --> n7 & n14 & n77
  n1 --> n7 & n14 & n57
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n49 & n74
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n72
  n8 --> n14 & n64
  n9 --> n0 & n7 & n14 & n57 & n77
  n10 --> n11 & n14
  n11 --> n0 & n14 & n55
  n12 --> n49 & n57
  n13 --> n14
  n15 --> n4 & n7 & n14 & n32 & n34 & n47 & n55 & n61
  n16 --> n14 & n45 & n46 & n60 & n64
  n17 --> n14 & n18 & n19 & n20 & n21 & n26 & n45 & n46 & n60
  n18 --> n19 & n20 & n26 & n45 & n46 & n60
  n19 --> n14 & n45 & n60
  n20 --> n14 & n19 & n45 & n60
  n21 --> n14 & n45 & n60
  n22 --> n14 & n17 & n19 & n20 & n21 & n24 & n25 & n33 & n45 & n46 & n55 & n60 & n64
  n23 --> n14 & n19 & n26 & n45 & n46 & n60 & n74
  n24 --> n14 & n45 & n60
  n25 --> n14 & n17 & n19 & n20 & n23 & n26 & n45 & n60 & n64 & n74
  n26 --> n14 & n19 & n45 & n60
  n27 --> n48 & n72
  n28 --> n14
  n29 --> n14 & n37
  n30 --> n14
  n31 --> n14
  n32 --> n14 & n16 & n45 & n47 & n54 & n72 & n74 & n77
  n33 --> n7 & n14 & n45 & n46 & n64 & n72
  n34 --> n14
  n35 --> n14
  n36 --> n14 & n55 & n63 & n64 & n75
  n37 --> n14 & n41 & n64
  n38 --> n14 & n60 & n64
  n39 --> n14 & n55 & n64
  n40 --> n14 & n55 & n64
  n41 --> n14
  n42 --> n14 & n46 & n55 & n64
  n43 --> n4 & n7 & n14 & n32 & n47 & n49 & n63 & n75
  n44 --> n14 & n34 & n47 & n63 & n74 & n77
  n45 --> n14
  n46 --> n14 & n45 & n77
  n47 --> n14 & n34 & n48 & n49 & n50 & n51 & n52 & n71 & n74
  n48 --> n49
  n49 --> n14
  n50 --> n14
  n51 --> n48 & n49 & n50 & n71 & n74
  n52 --> n14
  n53 --> n14 & n27
  n54 --> n14 & n47 & n77
  n55 --> n14 & n45 & n64
  n56 --> n31
  n57 --> n14 & n77
  n58 --> n14
  n59 --> n14
  n60 --> n14
  n61 --> n14 & n27 & n56 & n72
  n62 --> n6 & n7 & n8 & n14 & n33 & n60 & n72 & n73 & n77
  n63 --> n14 & n59 & n74 & n75 & n77
  n64 --> n14
  n65 --> n14 & n45
  n66 --> n6 & n7 & n10 & n11 & n14 & n46 & n55 & n60 & n63 & n64 & n75 & n76
  n67 --> n14
  n68 --> n14
  n69 --> n14 & n70
  n70 --> n14
  n71 --> n14 & n74
  n72 --> n14
  n73 --> n7 & n14 & n72
  n74 --> n14
  n75 --> n14
  n77 --> n14 & n72 & n74
  n78 --> n14 & n42 & n64
  n79 --> n5
  n80 --> n4 & n7 & n15 & n34 & n44 & n47 & n48 & n63 & n67 & n69 & n75 & n77 & n81 & n83 & n88 & n91 & n92 & n97
  n81 --> n15 & n61 & n80 & n85 & n86 & n88 & n89 & n92 & n93
  n82 --> n2
  n83 --> n7 & n8 & n9 & n11 & n36 & n57 & n64 & n72 & n77 & n91
  n84 --> n16 & n17 & n19 & n20 & n21 & n26 & n45 & n60
  n85 --> n15 & n27 & n35 & n47 & n48 & n55 & n61 & n81
  n86 --> n4 & n7 & n15 & n32 & n34 & n43 & n44 & n47 & n75 & n81 & n83 & n88 & n91 & n92 & n97
  n87 --> n44
  n88 --> n4 & n7 & n15 & n34 & n36 & n44 & n47 & n49 & n55 & n72 & n75 & n77 & n81 & n83 & n87 & n91 & n92 & n97
  n89 --> n15 & n27 & n47 & n53 & n61 & n81
  n90 --> n58
  n91 --> n4 & n7 & n15 & n57 & n61 & n72 & n77
  n92 --> n57 & n63 & n72 & n74 & n75 & n77 & n97
  n93 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n17 & n18 & n19 & n20 & n21 & n22 & n24 & n25 & n33 & n45 & n46 & n55 & n60 & n62 & n64 & n66 & n72 & n73 & n75 & n77 & n78 & n82 & n91 & n94 & n95 & n96 & n97
  n94 --> n7 & n8 & n16 & n21 & n22 & n23 & n25 & n26 & n33 & n42 & n45 & n46 & n60 & n62 & n72 & n73 & n77 & n78 & n96
  n95 --> n42 & n45 & n62 & n78 & n97
  n97 --> n7 & n60 & n63 & n75
```
