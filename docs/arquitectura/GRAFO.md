<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Combat.Targeting"]; n14["Core.Common"]; n15["Core.Composition"]
  n16["Core.Crafting"]; n17["Core.Crafting.Definitions"]; n18["Core.Crafting.Knowledge"]; n19["Core.Dna"]
  n20["Core.Echoes"]; n21["Core.Echoes.Consequences"]; n22["Core.Echoes.Secrets"]; n23["Core.Echoes.Voting"]
  n24["Core.Elevator"]; n25["Core.Equipment"]; n26["Core.Events"]; n27["Core.Extraction"]
  n28["Core.Floors"]; n29["Core.Floors.Omens"]; n30["Core.Floors.Paths"]; n31["Core.Floors.Population"]
  n32["Core.Floors.Remnants"]; n33["Core.Floors.Types"]; n34["Core.Gathering"]; n35["Core.Hubs"]
  n36["Core.Hud"]; n37["Core.Items"]; n38["Core.Items.Catalog"]; n39["Core.Iteration"]
  n40["Core.Iteration.Ending"]; n41["Core.Iteration.Presence"]; n42["Core.Iteration.Readiness"]; n43["Core.Iteration.States"]
  n44["Core.Iteration.Voting"]; n45["Core.Lab"]; n46["Core.Life"]; n47["Core.Loot"]
  n48["Core.Lore"]; n49["Core.Movement"]; n50["Core.Physics"]; n51["Core.Professions"]
  n52["Core.Profile"]; n53["Core.Progression"]; n54["Core.Prototype"]; n55["Core.Randomness"]
  n56["Core.Repair"]; n57["Core.Sandbox"]; n58["Core.Siege"]; n59["Core.Siege.Defenses"]
  n60["Core.Siege.Extraction"]; n61["Core.Siege.Scoring"]; n62["Core.StateMachines"]; n63["Core.Stats"]
  n64["Core.Talents"]; n65["Core.Time"]; n66["Core.Visuals"]; n67["Core.Visuals.Packs"]
  n68["Core.Vitals"]; n69["Core.World"]; n70["Runtime.Art"]; n71["Runtime.Atico"]
  n72["Runtime.Bootstrap"]; n73["Runtime.Characters"]; n74["Runtime.Combat"]; n75["Runtime.Extraction"]
  n76["Runtime.Hubs"]; n77["Runtime.Hud"]; n78["Runtime.Incursion"]; n79["Runtime.Lab"]
  n80["Runtime.Player"]; n81["Runtime.Prototype"]; n82["Runtime.Sandbox"]; n83["Runtime.Sandbox.Ui"]
  n84["Runtime.Sandbox.World"]; n85["Runtime.UI"]; n86["Runtime.Visuals"]
  n0 --> n7 & n14 & n68
  n1 --> n7 & n14 & n49
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n14
  n4 --> n7 & n14
  n5 --> n14 & n15 & n41 & n65
  n6 --> n8 & n10 & n11 & n14
  n7 --> n14 & n63
  n8 --> n14 & n55
  n9 --> n0 & n7 & n14 & n49 & n68
  n10 --> n11 & n14
  n11 --> n0 & n14 & n47
  n12 --> n41 & n49
  n13 --> n14
  n15 --> n4 & n7 & n14 & n24 & n26 & n39 & n47 & n52
  n16 --> n14 & n37 & n38 & n51 & n55
  n17 --> n14 & n37 & n51
  n18 --> n14 & n17 & n37 & n51
  n19 --> n40 & n63
  n20 --> n14
  n21 --> n14 & n29
  n22 --> n14
  n23 --> n14
  n24 --> n14 & n16 & n37 & n39 & n46 & n63 & n65 & n68
  n25 --> n7 & n14 & n37 & n38 & n55 & n63
  n26 --> n14
  n27 --> n14
  n28 --> n14 & n47 & n54 & n55 & n66
  n29 --> n14 & n33 & n55
  n30 --> n14 & n51 & n55
  n31 --> n14 & n47 & n55
  n32 --> n14 & n47 & n55
  n33 --> n14
  n34 --> n14 & n38 & n47 & n55
  n35 --> n4 & n7 & n14 & n24 & n39 & n41 & n54 & n66
  n36 --> n14 & n26 & n39 & n54 & n65 & n68
  n37 --> n14
  n38 --> n14 & n37 & n68
  n39 --> n14 & n26 & n40 & n41 & n42 & n43 & n44 & n62 & n65
  n40 --> n41
  n41 --> n14
  n42 --> n14
  n43 --> n40 & n41 & n42 & n62 & n65
  n44 --> n14
  n45 --> n14 & n19
  n46 --> n14 & n39 & n68
  n47 --> n14 & n37 & n55
  n48 --> n23
  n49 --> n14 & n68
  n50 --> n14
  n51 --> n14
  n52 --> n14 & n19 & n48 & n63
  n53 --> n6 & n7 & n8 & n14 & n25 & n51 & n63 & n64 & n68
  n54 --> n14 & n50 & n65 & n66 & n68
  n55 --> n14
  n56 --> n14 & n37
  n57 --> n6 & n7 & n10 & n11 & n14 & n38 & n47 & n51 & n54 & n55 & n66 & n67
  n58 --> n14
  n59 --> n14
  n60 --> n14 & n61
  n61 --> n14
  n62 --> n14 & n65
  n63 --> n14
  n64 --> n7 & n14 & n63
  n65 --> n14
  n66 --> n14
  n68 --> n14 & n63 & n65
  n69 --> n14 & n34 & n55
  n70 --> n5
  n71 --> n4 & n7 & n15 & n26 & n36 & n39 & n40 & n54 & n58 & n60 & n66 & n68 & n72 & n74 & n78 & n80 & n81 & n86
  n72 --> n15 & n52 & n71 & n75 & n76 & n78 & n79 & n81 & n82
  n73 --> n2
  n74 --> n7 & n8 & n9 & n11 & n28 & n49 & n55 & n63 & n68 & n80
  n75 --> n15 & n19 & n27 & n39 & n40 & n47 & n52 & n72
  n76 --> n4 & n7 & n15 & n24 & n26 & n35 & n36 & n39 & n66 & n72 & n74 & n78 & n80 & n81 & n86
  n77 --> n36
  n78 --> n4 & n7 & n15 & n26 & n28 & n36 & n39 & n41 & n47 & n63 & n66 & n68 & n72 & n74 & n77 & n80 & n81 & n86
  n79 --> n15 & n19 & n39 & n45 & n52 & n72
  n80 --> n4 & n7 & n15 & n49 & n52 & n63 & n68
  n81 --> n49 & n54 & n63 & n65 & n66 & n68 & n86
  n82 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n13 & n16 & n25 & n37 & n38 & n47 & n51 & n53 & n55 & n57 & n63 & n64 & n66 & n68 & n69 & n73 & n80 & n83 & n84 & n85 & n86
  n83 --> n7 & n8 & n16 & n25 & n34 & n37 & n38 & n51 & n53 & n63 & n64 & n68 & n69 & n85
  n84 --> n34 & n37 & n53 & n69 & n86
  n86 --> n7 & n51 & n54 & n66
```
