using Specimen.Core.Classes;
using static Specimen.Core.Talents.TalentBuilder;
using S = Specimen.Core.Stats.StatType;

namespace Specimen.Core.Talents
{
    /// <summary>Árboles de Oráculo, Druida de Ceniza, Duelista y Arcanista.</summary>
    public static partial class KitTalentTrees
    {
        private static TalentTree Oraculo() => new TalentTree(ClassKitId.Oraculo, new[]
        {
            Branch("oraculo.luz", "Luz", "Sanar es resistir",
                Power("Gracia", 0.03f), Pct("Reserva", S.MaxClassResource, 0.04f),
                Haste("Plegaria rápida", 0.05f), Pct("Fe", S.MaxHealth, 0.04f, 3), Power("Milagro mayor", 0.08f, 3)),
            Branch("oraculo.trueno", "Trueno", "Castigo del cielo",
                Pct("Estática", S.Damage, 0.03f), Flat("Descarga", S.CritChance, 0.012f),
                Power("Sobrecarga", 0.05f, 3), Haste("Conductor", 0.05f), Power("Juicio", 0.08f, 3)),
            Branch("oraculo.vision", "Visión", "Ver lo oculto",
                Pct("Clarividencia", S.MaxClassResource, 0.05f), Pct("Temple", S.Armor, 0.04f),
                Pct("Serenidad", S.MaxStamina, 0.04f, 3), Pct("Paso ligero", S.MoveSpeed, 0.02f, 3), Pct("Profecía", S.MaxHealth, 0.06f, 3))
        });

        private static TalentTree Druida() => new TalentTree(ClassKitId.DruidaCeniza, new[]
        {
            Branch("druida.raiz", "Raíz", "Brotar de las cenizas",
                Power("Savia densa", 0.03f), Pct("Corteza", S.Armor, 0.05f),
                Pct("Arraigo", S.MaxHealth, 0.04f, 3), Haste("Estaciones", 0.05f), Power("Renacer", 0.08f, 3)),
            Branch("druida.plaga", "Plaga", "La ceniza devora",
                Pct("Toxinas", S.Damage, 0.03f), Flat("Contagio", S.CritChance, 0.012f),
                Power("Esporas", 0.05f, 3), Haste("Propagación", 0.05f), Power("Pestilencia", 0.08f, 3)),
            Branch("druida.viento", "Viento", "Planear sobre la ruina",
                Pct("Brisa", S.MoveSpeed, 0.02f), Pct("Esencia", S.MaxClassResource, 0.05f),
                Pct("Aliento", S.MaxStamina, 0.05f, 3), Haste("Remolino", 0.05f), Pct("Tormenta de ceniza", S.Damage, 0.05f, 3))
        });

        private static TalentTree Duelista() => new TalentTree(ClassKitId.Duelista, new[]
        {
            Branch("duelista.filo", "Filo", "Un corte, una vida",
                Pct("Acero templado", S.Damage, 0.03f), Flat("Precisión", S.CritChance, 0.015f),
                Power("Doble filo", 0.05f, 3), Pct("Tempo", S.AttackSpeed, 0.05f, 3), Power("Golpe de gracia", 0.1f, 3)),
            Branch("duelista.danza", "Danza", "No estar donde golpean",
                Pct("Juego de pies", S.MoveSpeed, 0.02f), Pct("Fondo", S.MaxStamina, 0.05f),
                Haste("Ritmo", 0.05f), Pct("Esquiva", S.Armor, 0.05f, 3), Pct("Torbellino", S.AttackSpeed, 0.05f, 3)),
            Branch("duelista.sangre", "Sangre", "Vivir al límite",
                Pct("Vigor", S.MaxHealth, 0.03f), Pct("Energía", S.MaxClassResource, 0.04f),
                Flat("Instinto", S.CritChance, 0.01f, 3), Power("Sed", 0.05f, 3), Pct("Furia del duelo", S.Damage, 0.05f, 3))
        });

        private static TalentTree Arcanista() => new TalentTree(ClassKitId.Arcanista, new[]
        {
            Branch("arcanista.fuego", "Fuego", "Todo arde",
                Power("Ascuas", 0.03f), Flat("Combustión", S.CritChance, 0.012f),
                Pct("Llama viva", S.Damage, 0.04f, 3), Haste("Ignición", 0.05f), Power("Inferno", 0.1f, 3)),
            Branch("arcanista.arcano", "Arcano", "Doblar las reglas",
                Pct("Intelecto", S.MaxClassResource, 0.05f), Haste("Presteza", 0.04f),
                Power("Resonancia", 0.05f, 3), Pct("Sifón", S.MaxClassResource, 0.05f, 3), Power("Singularidad", 0.08f, 3)),
            Branch("arcanista.escudo", "Escudo", "Sobrevivir para lanzar",
                Pct("Piel de maná", S.Armor, 0.05f), Pct("Vitalidad", S.MaxHealth, 0.04f),
                Pct("Desplazamiento", S.MoveSpeed, 0.02f, 3), Pct("Templanza", S.MaxStamina, 0.05f, 3), Pct("Égida arcana", S.MaxHealth, 0.06f, 3))
        });
    }
}
