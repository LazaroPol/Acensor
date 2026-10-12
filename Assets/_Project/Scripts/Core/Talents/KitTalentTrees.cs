using Specimen.Core.Classes;
using Specimen.Core.Stats;
using static Specimen.Core.Talents.TalentBuilder;
using S = Specimen.Core.Stats.StatType;

namespace Specimen.Core.Talents
{
    /// <summary>Árbol de talentos de cada kit: tres ramas de 5 filas (defensa, ofensiva y especialidad).</summary>
    public static partial class KitTalentTrees
    {
        public static TalentTree For(ClassKitId kit)
        {
            switch (kit)
            {
                case ClassKitId.Bastion: return Bastion();
                case ClassKitId.Vigia: return Vigia();
                case ClassKitId.Oraculo: return Oraculo();
                case ClassKitId.DruidaCeniza: return Druida();
                case ClassKitId.Duelista: return Duelista();
                default: return Arcanista();
            }
        }

        private static TalentTree Bastion() => new TalentTree(ClassKitId.Bastion, new[]
        {
            Branch("bastion.prot", "Protección", "Aguantar la línea",
                Pct("Piel de hierro", S.Armor, 0.04f), Pct("Robustez", S.MaxHealth, 0.03f),
                Flat("Escudo pesado", S.Armor, 4f, 3), Haste("Disciplina", 0.05f), Pct("Muralla viviente", S.MaxHealth, 0.05f, 3)),
            Branch("bastion.furia", "Furia", "Cada golpe cuenta",
                Pct("Brazo fuerte", S.Damage, 0.03f), Flat("Golpe certero", S.CritChance, 0.01f),
                Power("Martillo pesado", 0.05f, 3), Pct("Frenesí", S.AttackSpeed, 0.04f, 3), Power("Aniquilación", 0.08f, 3)),
            Branch("bastion.mando", "Mando", "El grupo primero",
                Pct("Resuello", S.MaxStamina, 0.04f), Pct("Ira contenida", S.MaxClassResource, 0.04f),
                Haste("Voz de mando", 0.05f), Pct("Paso firme", S.MoveSpeed, 0.02f, 3), Pct("Estandarte", S.Damage, 0.04f, 3))
        });

        private static TalentTree Vigia() => new TalentTree(ClassKitId.Vigia, new[]
        {
            Branch("vigia.guardia", "Guardia", "Nada pasa",
                Pct("Cota ligera", S.Armor, 0.04f), Pct("Vigilia", S.MaxHealth, 0.03f),
                Pct("Reflejos", S.MaxStamina, 0.05f, 3), Haste("Atención", 0.05f), Pct("Centinela", S.MaxHealth, 0.05f, 3)),
            Branch("vigia.asta", "Asta", "Alcance es poder",
                Pct("Punta afilada", S.Damage, 0.03f), Flat("Ojo de halcón", S.CritChance, 0.012f),
                Power("Lanza larga", 0.05f, 3), Pct("Ritmo", S.AttackSpeed, 0.04f, 3), Power("Empalar", 0.08f, 3)),
            Branch("vigia.explorar", "Exploración", "Ver primero",
                Pct("Zancada", S.MoveSpeed, 0.02f), Pct("Concentración", S.MaxClassResource, 0.04f),
                Haste("Anticipación", 0.05f), Pct("Resistencia", S.MaxStamina, 0.05f, 3), Flat("Punto débil", S.CritChance, 0.02f, 3))
        });
    }
}
