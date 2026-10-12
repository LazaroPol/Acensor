<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Appearance"]
  n4["Core.Armory"]; n5["Core.Art"]; n6["Core.Classes"]; n7["Core.Combat"]
  n8["Core.Combat.Attacks"]; n9["Core.Combat.Bosses"]; n10["Core.Combat.Enemies"]; n11["Core.Combat.Perception"]
  n12["Core.Common"]; n13["Core.Composition"]; n14["Core.Crafting"]; n15["Core.Dna"]
  n16["Core.Echoes"]; n17["Core.Echoes.Consequences"]; n18["Core.Echoes.Secrets"]; n19["Core.Echoes.Voting"]
  n20["Core.Elevator"]; n21["Core.Events"]; n22["Core.Floors"]; n23["Core.Floors.Omens"]
  n24["Core.Floors.Paths"]; n25["Core.Floors.Population"]; n26["Core.Floors.Remnants"]; n27["Core.Floors.Types"]
  n28["Core.Hubs"]; n29["Core.Hud"]; n30["Core.Items"]; n31["Core.Iteration"]
  n32["Core.Iteration.Ending"]; n33["Core.Iteration.Presence"]; n34["Core.Iteration.Readiness"]; n35["Core.Iteration.States"]
  n36["Core.Iteration.Voting"]; n37["Core.Lab"]; n38["Core.Life"]; n39["Core.Loot"]
  n40["Core.Lore"]; n41["Core.Movement"]; n42["Core.Physics"]; n43["Core.Professions"]
  n44["Core.Profile"]; n45["Core.Prototype"]; n46["Core.Randomness"]; n47["Core.Repair"]
  n48["Core.Siege"]; n49["Core.Siege.Defenses"]; n50["Core.Siege.Extraction"]; n51["Core.Siege.Scoring"]
  n52["Core.StateMachines"]; n53["Core.Stats"]; n54["Core.Time"]; n55["Core.Visuals"]
  n56["Core.Visuals.Packs"]; n57["Core.Vitals"]; n58["Runtime.Art"]; n59["Runtime.Bootstrap"]
  n60["Runtime.Characters"]; n61["Runtime.Hubs"]; n62["Runtime.Hud"]; n63["Runtime.Lab"]
  n64["Runtime.Player"]; n65["Runtime.Prototype"]; n66["Runtime.Visuals"]
  n0 --> n6 & n12 & n57
  n1 --> n6 & n12 & n41
  n3 --> n6 & n8 & n12
  n4 --> n6 & n12
  n5 --> n12 & n13 & n33 & n54
  n6 --> n12 & n53
  n7 --> n12 & n46
  n8 --> n0 & n6 & n12 & n41 & n57
  n9 --> n10 & n12
  n10 --> n12 & n39
  n11 --> n33 & n41
  n13 --> n4 & n6 & n12 & n20 & n21 & n31
  n14 --> n12 & n30 & n43
  n15 --> n53
  n16 --> n12
  n17 --> n12 & n23
  n18 --> n12
  n19 --> n12
  n20 --> n12 & n14 & n30 & n31 & n38 & n53 & n54 & n57
  n21 --> n12
  n22 --> n12 & n39 & n46
  n23 --> n12 & n27 & n46
  n24 --> n12 & n43 & n46
  n25 --> n12 & n39 & n46
  n26 --> n12 & n39 & n46
  n27 --> n12
  n28 --> n4 & n6 & n12 & n20 & n31
  n29 --> n12 & n21 & n31 & n45 & n54 & n57
  n30 --> n12
  n31 --> n12 & n21 & n32 & n33 & n34 & n35 & n36 & n52 & n54
  n32 --> n33
  n33 --> n12
  n34 --> n12
  n35 --> n32 & n33 & n34 & n52 & n54
  n36 --> n12
  n37 --> n12 & n15
  n38 --> n12 & n31 & n57
  n39 --> n12 & n30 & n46
  n40 --> n19
  n41 --> n12 & n57
  n42 --> n12
  n43 --> n12
  n44 --> n15 & n40
  n45 --> n12 & n42 & n54 & n55 & n57
  n46 --> n12
  n47 --> n12 & n30
  n48 --> n12
  n49 --> n12
  n50 --> n12 & n51
  n51 --> n12
  n52 --> n12 & n54
  n53 --> n12
  n54 --> n12
  n55 --> n12
  n57 --> n12 & n53 & n54
  n58 --> n5
  n59 --> n13
  n60 --> n2
  n61 --> n4 & n6 & n13 & n20 & n28 & n31 & n59
  n62 --> n29
  n63 --> n13 & n15 & n37
  n64 --> n41 & n53 & n57
  n65 --> n41 & n45 & n53 & n54 & n55 & n57 & n66
  n66 --> n6 & n43 & n45 & n55
```
