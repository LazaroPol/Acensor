<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Bestiary"]; n7["Core.Classes"]
  n8["Core.Combat"]; n9["Core.Combat.Attacks"]; n10["Core.Combat.Bosses"]; n11["Core.Combat.Enemies"]
  n12["Core.Combat.Perception"]; n13["Core.Common"]; n14["Core.Composition"]; n15["Core.Crafting"]
  n16["Core.Dna"]; n17["Core.Echoes"]; n18["Core.Echoes.Consequences"]; n19["Core.Echoes.Secrets"]
  n20["Core.Echoes.Voting"]; n21["Core.Elevator"]; n22["Core.Events"]; n23["Core.Floors"]
  n24["Core.Floors.Omens"]; n25["Core.Floors.Paths"]; n26["Core.Floors.Population"]; n27["Core.Floors.Remnants"]
  n28["Core.Floors.Types"]; n29["Core.Hubs"]; n30["Core.Hud"]; n31["Core.Items"]
  n32["Core.Iteration"]; n33["Core.Iteration.Ending"]; n34["Core.Iteration.Presence"]; n35["Core.Iteration.Readiness"]
  n36["Core.Iteration.States"]; n37["Core.Iteration.Voting"]; n38["Core.Lab"]; n39["Core.Life"]
  n40["Core.Loot"]; n41["Core.Lore"]; n42["Core.Movement"]; n43["Core.Physics"]
  n44["Core.Professions"]; n45["Core.Profile"]; n46["Core.Prototype"]; n47["Core.Randomness"]
  n48["Core.Repair"]; n49["Core.Siege"]; n50["Core.Siege.Defenses"]; n51["Core.Siege.Extraction"]
  n52["Core.Siege.Scoring"]; n53["Core.StateMachines"]; n54["Core.Stats"]; n55["Core.Time"]
  n56["Core.Visuals"]; n57["Core.Visuals.Packs"]; n58["Core.Vitals"]; n59["Runtime.Art"]
  n60["Runtime.Atico"]; n61["Runtime.Bootstrap"]; n62["Runtime.Characters"]; n63["Runtime.Combat"]
  n64["Runtime.Hubs"]; n65["Runtime.Hud"]; n66["Runtime.Incursion"]; n67["Runtime.Lab"]
  n68["Runtime.Player"]; n69["Runtime.Prototype"]; n70["Runtime.Visuals"]
  n0 --> n7 & n13 & n58
  n1 --> n7 & n13 & n42
  n3 --> n7 & n9 & n13
  n4 --> n7 & n13
  n5 --> n13 & n14 & n34 & n55
  n6 --> n8 & n10 & n11 & n13
  n7 --> n13 & n54
  n8 --> n13 & n47
  n9 --> n0 & n7 & n13 & n42 & n58
  n10 --> n11 & n13
  n11 --> n0 & n13 & n40
  n12 --> n34 & n42
  n14 --> n4 & n7 & n13 & n21 & n22 & n32
  n15 --> n13 & n31 & n44
  n16 --> n54
  n17 --> n13
  n18 --> n13 & n24
  n19 --> n13
  n20 --> n13
  n21 --> n13 & n15 & n31 & n32 & n39 & n54 & n55 & n58
  n22 --> n13
  n23 --> n13 & n40 & n46 & n47 & n56
  n24 --> n13 & n28 & n47
  n25 --> n13 & n44 & n47
  n26 --> n13 & n40 & n47
  n27 --> n13 & n40 & n47
  n28 --> n13
  n29 --> n4 & n7 & n13 & n21 & n32 & n34
  n30 --> n13 & n22 & n32 & n46 & n55 & n58
  n31 --> n13
  n32 --> n13 & n22 & n33 & n34 & n35 & n36 & n37 & n53 & n55
  n33 --> n34
  n34 --> n13
  n35 --> n13
  n36 --> n33 & n34 & n35 & n53 & n55
  n37 --> n13
  n38 --> n13 & n16
  n39 --> n13 & n32 & n58
  n40 --> n13 & n31 & n47
  n41 --> n20
  n42 --> n13 & n58
  n43 --> n13
  n44 --> n13
  n45 --> n16 & n41
  n46 --> n13 & n43 & n55 & n56 & n58
  n47 --> n13
  n48 --> n13 & n31
  n49 --> n13
  n50 --> n13
  n51 --> n13 & n52
  n52 --> n13
  n53 --> n13 & n55
  n54 --> n13
  n55 --> n13
  n56 --> n13
  n58 --> n13 & n54 & n55
  n59 --> n5
  n60 --> n4 & n7 & n14 & n22 & n30 & n32 & n46 & n56 & n61 & n66 & n69 & n70
  n61 --> n14
  n62 --> n2
  n63 --> n8 & n11 & n23 & n47 & n54 & n58
  n64 --> n4 & n7 & n14 & n21 & n29 & n32 & n61
  n65 --> n30
  n66 --> n4 & n7 & n14 & n22 & n23 & n30 & n32 & n54 & n56 & n58 & n61 & n63 & n65 & n68 & n69 & n70
  n67 --> n14 & n16 & n38
  n68 --> n42 & n54 & n58
  n69 --> n42 & n46 & n54 & n55 & n56 & n58 & n70
  n70 --> n7 & n44 & n46 & n56
```
