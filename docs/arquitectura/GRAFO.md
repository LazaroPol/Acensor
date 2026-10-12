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
  n28["Core.Floors.Remnants"]; n29["Core.Floors.Types"]; n30["Core.Hubs"]; n31["Core.Hud"]
  n32["Core.Items"]; n33["Core.Iteration"]; n34["Core.Iteration.Ending"]; n35["Core.Iteration.Presence"]
  n36["Core.Iteration.Readiness"]; n37["Core.Iteration.States"]; n38["Core.Iteration.Voting"]; n39["Core.Lab"]
  n40["Core.Life"]; n41["Core.Loot"]; n42["Core.Lore"]; n43["Core.Movement"]
  n44["Core.Physics"]; n45["Core.Professions"]; n46["Core.Profile"]; n47["Core.Prototype"]
  n48["Core.Randomness"]; n49["Core.Repair"]; n50["Core.Sandbox"]; n51["Core.Siege"]
  n52["Core.Siege.Defenses"]; n53["Core.Siege.Extraction"]; n54["Core.Siege.Scoring"]; n55["Core.StateMachines"]
  n56["Core.Stats"]; n57["Core.Time"]; n58["Core.Visuals"]; n59["Core.Visuals.Packs"]
  n60["Core.Vitals"]; n61["Runtime.Art"]; n62["Runtime.Atico"]; n63["Runtime.Bootstrap"]
  n64["Runtime.Characters"]; n65["Runtime.Combat"]; n66["Runtime.Extraction"]; n67["Runtime.Hubs"]
  n68["Runtime.Hud"]; n69["Runtime.Incursion"]; n70["Runtime.Lab"]; n71["Runtime.Player"]
  n72["Runtime.Prototype"]; n73["Runtime.Sandbox"]; n74["Runtime.Visuals"]
  n0 --> n7 & n13 & n60
  n1 --> n7 & n13 & n43
  n3 --> n7 & n9 & n13
  n4 --> n7 & n13
  n5 --> n13 & n14 & n35 & n57
  n6 --> n8 & n10 & n11 & n13
  n7 --> n13 & n56
  n8 --> n13 & n48
  n9 --> n0 & n7 & n13 & n43 & n60
  n10 --> n11 & n13
  n11 --> n0 & n13 & n41
  n12 --> n35 & n43
  n14 --> n4 & n7 & n13 & n21 & n22 & n33 & n41 & n46
  n15 --> n13 & n32 & n45
  n16 --> n34 & n56
  n17 --> n13
  n18 --> n13 & n25
  n19 --> n13
  n20 --> n13
  n21 --> n13 & n15 & n32 & n33 & n40 & n56 & n57 & n60
  n22 --> n13
  n23 --> n13
  n24 --> n13 & n41 & n47 & n48 & n58
  n25 --> n13 & n29 & n48
  n26 --> n13 & n45 & n48
  n27 --> n13 & n41 & n48
  n28 --> n13 & n41 & n48
  n29 --> n13
  n30 --> n4 & n7 & n13 & n21 & n33 & n35 & n47 & n58
  n31 --> n13 & n22 & n33 & n47 & n57 & n60
  n32 --> n13
  n33 --> n13 & n22 & n34 & n35 & n36 & n37 & n38 & n55 & n57
  n34 --> n35
  n35 --> n13
  n36 --> n13
  n37 --> n34 & n35 & n36 & n55 & n57
  n38 --> n13
  n39 --> n13 & n16
  n40 --> n13 & n33 & n60
  n41 --> n13 & n32 & n48
  n42 --> n20
  n43 --> n13 & n60
  n44 --> n13
  n45 --> n13
  n46 --> n13 & n16 & n42 & n56
  n47 --> n13 & n44 & n57 & n58 & n60
  n48 --> n13
  n49 --> n13 & n32
  n50 --> n6 & n7 & n10 & n13 & n32 & n41 & n45 & n47 & n48 & n58 & n59
  n51 --> n13
  n52 --> n13
  n53 --> n13 & n54
  n54 --> n13
  n55 --> n13 & n57
  n56 --> n13
  n57 --> n13
  n58 --> n13
  n60 --> n13 & n56 & n57
  n61 --> n5
  n62 --> n4 & n7 & n14 & n22 & n31 & n33 & n34 & n47 & n51 & n53 & n58 & n60 & n63 & n65 & n69 & n71 & n72 & n74
  n63 --> n14 & n46 & n62 & n66 & n67 & n69 & n70 & n72 & n73
  n64 --> n2
  n65 --> n7 & n8 & n9 & n11 & n24 & n43 & n48 & n56 & n60 & n71
  n66 --> n14 & n16 & n23 & n33 & n34 & n41 & n46 & n63
  n67 --> n4 & n7 & n14 & n21 & n22 & n30 & n31 & n33 & n58 & n63 & n65 & n69 & n71 & n72 & n74
  n68 --> n31
  n69 --> n4 & n7 & n14 & n22 & n24 & n31 & n33 & n35 & n41 & n56 & n58 & n60 & n63 & n65 & n68 & n71 & n72 & n74
  n70 --> n14 & n16 & n33 & n39 & n46 & n63
  n71 --> n4 & n7 & n14 & n43 & n46 & n56 & n60
  n72 --> n43 & n47 & n56 & n57 & n58 & n60 & n74
  n73 --> n0 & n2 & n6 & n7 & n8 & n9 & n11 & n32 & n41 & n43 & n45 & n48 & n50 & n56 & n58 & n60 & n64 & n71 & n72 & n74
  n74 --> n7 & n45 & n47 & n58
```
