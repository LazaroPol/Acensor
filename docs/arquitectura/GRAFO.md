<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Common"]; n14["Core.Composition"]; n15["Core.Crafting"]
  n16["Core.Dna"]; n17["Core.Echoes"]; n18["Core.Echoes.Consequences"]; n19["Core.Echoes.Secrets"]
  n20["Core.Echoes.Voting"]; n21["Core.Elevator"]; n22["Core.Events"]; n23["Core.Extraction"]
  n24["Core.Floors"]; n25["Core.Floors.Omens"]; n26["Core.Floors.Paths"]; n27["Core.Floors.Population"]
  n28["Core.Floors.Remnants"]; n29["Core.Floors.Types"]; n30["Core.Gathering"]; n31["Core.Hubs"]
  n32["Core.Hud"]; n33["Core.Items"]; n34["Core.Items.Catalog"]; n35["Core.Iteration"]
  n36["Core.Iteration.Ending"]; n37["Core.Iteration.Presence"]; n38["Core.Iteration.Readiness"]; n39["Core.Iteration.States"]
  n40["Core.Iteration.Voting"]; n41["Core.Lab"]; n42["Core.Life"]; n43["Core.Loot"]
  n44["Core.Lore"]; n45["Core.Movement"]; n46["Core.Physics"]; n47["Core.Professions"]
  n48["Core.Profile"]; n49["Core.Progression"]; n50["Core.Prototype"]; n51["Core.Randomness"]
  n52["Core.Repair"]; n53["Core.Sandbox"]; n54["Core.Siege"]; n55["Core.Siege.Defenses"]
  n56["Core.Siege.Extraction"]; n57["Core.Siege.Scoring"]; n58["Core.StateMachines"]; n59["Core.Stats"]
  n60["Core.Talents"]; n61["Core.Time"]; n62["Core.Visuals"]; n63["Core.Visuals.Packs"]
  n64["Core.Vitals"]; n65["Core.World"]; n66["Runtime.Art"]; n67["Runtime.Atico"]
  n68["Runtime.Bootstrap"]; n69["Runtime.Characters"]; n70["Runtime.Combat"]; n71["Runtime.Extraction"]
  n72["Runtime.Hubs"]; n73["Runtime.Hud"]; n74["Runtime.Incursion"]; n75["Runtime.Lab"]
  n76["Runtime.Player"]; n77["Runtime.Prototype"]; n78["Runtime.Sandbox"]; n79["Runtime.Sandbox.Ui"]
  n80["Runtime.Sandbox.World"]; n81["Runtime.UI"]; n82["Runtime.Visuals"]
  n0 --> n7 & n13 & n64
  n1 --> n7 & n13 & n45
  n2 --> n3 & n7 & n9
  n3 --> n7 & n9 & n13
  n4 --> n7 & n13
  n5 --> n13 & n14 & n37 & n61
  n6 --> n8 & n10 & n11 & n13
  n7 --> n13 & n59
  n8 --> n13 & n51
  n9 --> n0 & n7 & n13 & n45 & n64
  n10 --> n11 & n13
  n11 --> n0 & n13 & n43
  n12 --> n37 & n45
  n14 --> n4 & n7 & n13 & n21 & n22 & n35 & n43 & n48
  n15 --> n13 & n33 & n34 & n47
  n16 --> n36 & n59
  n17 --> n13
  n18 --> n13 & n25
  n19 --> n13
  n20 --> n13
  n21 --> n13 & n15 & n33 & n35 & n42 & n59 & n61 & n64
  n22 --> n13
  n23 --> n13
  n24 --> n13 & n43 & n50 & n51 & n62
  n25 --> n13 & n29 & n51
  n26 --> n13 & n47 & n51
  n27 --> n13 & n43 & n51
  n28 --> n13 & n43 & n51
  n29 --> n13
  n30 --> n13 & n34 & n43 & n51
  n31 --> n4 & n7 & n13 & n21 & n35 & n37 & n50 & n62
  n32 --> n13 & n22 & n35 & n50 & n61 & n64
  n33 --> n13
  n34 --> n13 & n33 & n64
  n35 --> n13 & n22 & n36 & n37 & n38 & n39 & n40 & n58 & n61
  n36 --> n37
  n37 --> n13
  n38 --> n13
  n39 --> n36 & n37 & n38 & n58 & n61
  n40 --> n13
  n41 --> n13 & n16
  n42 --> n13 & n35 & n64
  n43 --> n13 & n33 & n51
  n44 --> n20
  n45 --> n13 & n64
  n46 --> n13
  n47 --> n13
  n48 --> n13 & n16 & n44 & n59
  n49 --> n6 & n7 & n8 & n13 & n47 & n59 & n60 & n64
  n50 --> n13 & n46 & n61 & n62 & n64
  n51 --> n13
  n52 --> n13 & n33
  n53 --> n6 & n7 & n10 & n13 & n34 & n43 & n47 & n50 & n51 & n62 & n63
  n54 --> n13
  n55 --> n13
  n56 --> n13 & n57
  n57 --> n13
  n58 --> n13 & n61
  n59 --> n13
  n60 --> n7 & n13 & n59
  n61 --> n13
  n62 --> n13
  n64 --> n13 & n59 & n61
  n65 --> n13 & n30 & n51
  n66 --> n5
  n67 --> n4 & n7 & n14 & n22 & n32 & n35 & n36 & n50 & n54 & n56 & n62 & n64 & n68 & n70 & n74 & n76 & n77 & n82
  n68 --> n14 & n48 & n67 & n71 & n72 & n74 & n75 & n77 & n78
  n69 --> n2
  n70 --> n7 & n8 & n9 & n11 & n24 & n45 & n51 & n59 & n64 & n76
  n71 --> n14 & n16 & n23 & n35 & n36 & n43 & n48 & n68
  n72 --> n4 & n7 & n14 & n21 & n22 & n31 & n32 & n35 & n62 & n68 & n70 & n74 & n76 & n77 & n82
  n73 --> n32
  n74 --> n4 & n7 & n14 & n22 & n24 & n32 & n35 & n37 & n43 & n59 & n62 & n64 & n68 & n70 & n73 & n76 & n77 & n82
  n75 --> n14 & n16 & n35 & n41 & n48 & n68
  n76 --> n4 & n7 & n14 & n45 & n48 & n59 & n64
  n77 --> n45 & n50 & n59 & n61 & n62 & n64 & n82
  n78 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n15 & n33 & n43 & n45 & n47 & n49 & n51 & n53 & n59 & n60 & n62 & n64 & n65 & n69 & n76 & n79 & n80 & n81 & n82
  n79 --> n7 & n8 & n15 & n30 & n33 & n34 & n47 & n49 & n59 & n60 & n64 & n65 & n81
  n80 --> n30 & n33 & n49 & n65 & n82
  n82 --> n7 & n47 & n50 & n62
```
