using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Core.Combat;
using K = Specimen.Core.Progression.SpellKind;

namespace Specimen.Core.Progression
{
    /// <summary>Hechizos y técnicas de cada kit (5 por kit, de nivel 2 a 14). Valores provisionales de balance.</summary>
    public static class KitSpellbooks
    {
        public static Spellbook For(ClassKitId kit) => new Spellbook(Spells(kit));

        private static IEnumerable<SpellDefinition> Spells(ClassKitId kit)
        {
            switch (kit)
            {
                case ClassKitId.Bastion: return new[]
                {
                    S("bastion.golpe", "Golpe aplastante", "Martillazo que hace 180 % de daño.", 2, K.Golpe, 1.8f, 15, 6),
                    S("bastion.grito", "Grito de guerra", "+25 % de daño durante 8 s.", 4, K.Furia, 0.25f, 20, 20, d: 8),
                    S("bastion.temblor", "Temblor", "Golpea el suelo: 120 % de daño en 4 m.", 7, K.Area, 1.2f, 30, 10, r: 4),
                    S("bastion.muralla", "Muralla", "Reduce el daño recibido un 70 % durante 4 s.", 10, K.Escudo, 0.7f, 35, 25, d: 4),
                    S("bastion.carga", "Carga", "Avanza 6 m embistiendo.", 14, K.Impulso, 6f, 20, 12) };
                case ClassKitId.Vigia: return new[]
                {
                    S("vigia.estocada", "Estocada", "Lanzazo que hace 170 % de daño.", 2, K.Golpe, 1.7f, 10, 5),
                    S("vigia.salto", "Salto atrás", "Retrocede 5 m.", 4, K.Impulso, 5f, 10, 8),
                    S("vigia.barrido", "Barrido", "120 % de daño a todo en 3,5 m.", 7, K.Area, 1.2f, 25, 9, r: 3.5f),
                    S("vigia.jabalina", "Jabalina", "Proyectil del 220 % de daño.", 10, K.Proyectil, 2.2f, 25, 10),
                    S("vigia.foco", "Foco", "+30 % de daño durante 10 s.", 14, K.Furia, 0.3f, 30, 30, d: 10) };
                case ClassKitId.Oraculo: return new[]
                {
                    S("oraculo.chispa", "Chispa", "Proyectil eléctrico del 150 % de daño.", 2, K.Proyectil, 1.5f, 12, 3, DamageType.Shock),
                    S("oraculo.renovar", "Renovar", "Cura 60 de vida.", 4, K.Curacion, 60f, 30, 10),
                    S("oraculo.egida", "Égida", "Reduce el daño un 50 % durante 6 s.", 7, K.Escudo, 0.5f, 30, 20, d: 6),
                    S("oraculo.tormenta", "Tormenta", "130 % de daño en 5 m.", 10, K.Area, 1.3f, 45, 12, DamageType.Shock, 5),
                    S("oraculo.milagro", "Milagro", "Cura 140 de vida.", 14, K.Curacion, 140f, 60, 40) };
                case ClassKitId.DruidaCeniza: return new[]
                {
                    S("druida.espinas", "Espinas", "Proyectil tóxico del 160 % de daño.", 2, K.Proyectil, 1.6f, 12, 4, DamageType.Toxic),
                    S("druida.savia", "Savia", "Cura 50 de vida.", 4, K.Curacion, 50f, 25, 12),
                    S("druida.nube", "Nube de ceniza", "140 % de daño en 4,5 m.", 7, K.Area, 1.4f, 35, 10, DamageType.Toxic, 4.5f),
                    S("druida.corteza", "Corteza", "Reduce el daño un 45 % durante 8 s.", 10, K.Escudo, 0.45f, 30, 25, d: 8),
                    S("druida.plaga", "Plaga", "220 % de daño en 6 m.", 14, K.Area, 2.2f, 60, 25, DamageType.Toxic, 6) };
                case ClassKitId.Duelista: return new[]
                {
                    S("duelista.corte", "Corte doble", "Dos tajos: 200 % de daño.", 2, K.Golpe, 2f, 15, 5),
                    S("duelista.finta", "Finta", "Se desliza 4 m.", 4, K.Impulso, 4f, 10, 6),
                    S("duelista.adrenalina", "Adrenalina", "+35 % de daño durante 8 s.", 7, K.Furia, 0.35f, 25, 25, d: 8),
                    S("duelista.remolino", "Remolino", "150 % de daño en 3 m.", 10, K.Area, 1.5f, 30, 10, r: 3),
                    S("duelista.ejecucion", "Ejecución", "Golpe del 350 % de daño.", 14, K.Golpe, 3.5f, 40, 15) };
                default: return new[]
                {
                    S("arcanista.dardo", "Dardo de fuego", "Proyectil del 180 % de daño.", 2, K.Proyectil, 1.8f, 12, 2.5f, DamageType.Fire),
                    S("arcanista.parpadeo", "Parpadeo", "Se teletransporta 6 m adelante.", 4, K.Impulso, 6f, 20, 10),
                    S("arcanista.nova", "Nova de llamas", "150 % de daño en 4 m.", 7, K.Area, 1.5f, 35, 10, DamageType.Fire, 4),
                    S("arcanista.barrera", "Barrera arcana", "Reduce el daño un 60 % durante 5 s.", 10, K.Escudo, 0.6f, 35, 25, d: 5),
                    S("arcanista.meteoro", "Meteoro", "300 % de daño en 5 m.", 14, K.Area, 3f, 70, 30, DamageType.Fire, 5) };
            }
        }

        private static SpellDefinition S(string id, string name, string text, int level, K kind, float power, float cost,
            float cooldown, DamageType type = DamageType.Physical, float r = 0f, float d = 0f)
            => new SpellDefinition(id, name, text, level, kind, power, cost, cooldown, type, r, d);
    }
}
