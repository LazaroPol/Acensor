<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# Grafo de dependencias (flecha = «usa a»)

```mermaid
flowchart LR
  n0["Core.Abilities"]; n1["Core.Abilities.Mobility"]; n2["Core.Animation"]; n3["Core.Armory"]
  n4["Core.Art"]; n5["Core.Classes"]; n6["Core.Combat"]; n7["Core.Combat.Bosses"]
  n8["Core.Combat.Enemies"]; n9["Core.Combat.Perception"]; n10["Core.Common"]; n11["Core.Composition"]
  n12["Core.Crafting"]; n13["Core.Dna"]; n14["Core.Echoes"]; n15["Core.Echoes.Consequences"]
  n16["Core.Echoes.Secrets"]; n17["Core.Echoes.Voting"]; n18["Core.Elevator"]; n19["Core.Events"]
  n20["Core.Floors"]; n21["Core.Floors.Omens"]; n22["Core.Floors.Paths"]; n23["Core.Floors.Population"]
  n24["Core.Floors.Remnants"]; n25["Core.Floors.Types"]; n26["Core.Hubs"]; n27["Core.Hud"]
  n28["Core.Items"]; n29["Core.Iteration"]; n30["Core.Iteration.Ending"]; n31["Core.Iteration.Presence"]
  n32["Core.Iteration.Readiness"]; n33["Core.Iteration.States"]; n34["Core.Iteration.Voting"]; n35["Core.Lab"]
  n36["Core.Life"]; n37["Core.Loot"]; n38["Core.Lore"]; n39["Core.Movement"]
  n40["Core.Physics"]; n41["Core.Professions"]; n42["Core.Profile"]; n43["Core.Prototype"]
  n44["Core.Randomness"]; n45["Core.Repair"]; n46["Core.Siege"]; n47["Core.Siege.Defenses"]
  n48["Core.Siege.Extraction"]; n49["Core.Siege.Scoring"]; n50["Core.StateMachines"]; n51["Core.Stats"]
  n52["Core.Time"]; n53["Core.Visuals"]; n54["Core.Visuals.Packs"]; n55["Core.Vitals"]
  n56["Runtime.Art"]; n57["Runtime.Bootstrap"]; n58["Runtime.Characters"]; n59["Runtime.Hubs"]
  n60["Runtime.Hud"]; n61["Runtime.Lab"]; n62["Runtime.Player"]; n63["Runtime.Prototype"]
  n64["Runtime.Visuals"]
  n0 --> n5 & n10 & n55
  n1 --> n5 & n10 & n39
  n3 --> n5 & n10
  n4 --> n10 & n11 & n31 & n52
  n5 --> n10 & n51
  n6 --> n10 & n44
  n7 --> n8 & n10
  n8 --> n10 & n37
  n9 --> n31 & n39
  n11 --> n3 & n5 & n10 & n18 & n19 & n29
  n12 --> n10 & n28 & n41
  n13 --> n51
  n14 --> n10
  n15 --> n10 & n21
  n16 --> n10
  n17 --> n10
  n18 --> n10 & n12 & n28 & n29 & n36 & n51 & n52 & n55
  n19 --> n10
  n20 --> n10 & n37 & n44
  n21 --> n10 & n25 & n44
  n22 --> n10 & n41 & n44
  n23 --> n10 & n37 & n44
  n24 --> n10 & n37 & n44
  n25 --> n10
  n26 --> n3 & n5 & n10 & n18 & n29
  n27 --> n10 & n19 & n29 & n43 & n52 & n55
  n28 --> n10
  n29 --> n10 & n19 & n30 & n31 & n32 & n33 & n34 & n50 & n52
  n30 --> n31
  n31 --> n10
  n32 --> n10
  n33 --> n30 & n31 & n32 & n50 & n52
  n34 --> n10
  n35 --> n10 & n13
  n36 --> n10 & n29 & n55
  n37 --> n10 & n28 & n44
  n38 --> n17
  n39 --> n10 & n55
  n40 --> n10
  n41 --> n10
  n42 --> n13 & n38
  n43 --> n10 & n40 & n52 & n53 & n55
  n44 --> n10
  n45 --> n10 & n28
  n46 --> n10
  n47 --> n10
  n48 --> n10 & n49
  n49 --> n10
  n50 --> n10 & n52
  n51 --> n10
  n52 --> n10
  n53 --> n10
  n55 --> n10 & n51 & n52
  n56 --> n4
  n57 --> n11
  n58 --> n2
  n59 --> n3 & n5 & n11 & n18 & n26 & n29 & n57
  n60 --> n27
  n61 --> n11 & n13 & n35
  n62 --> n39 & n51 & n55
  n63 --> n39 & n43 & n51 & n52 & n53 & n55 & n64
  n64 --> n5 & n41 & n43 & n53
```
