using System.Collections.Generic;
using Specimen.Core.Classes;

namespace Specimen.Core.Sandbox
{
    /// <summary>Efecto de la habilidad de firma de cada kit (DefaultClassAbilities) en el Campo de pruebas.</summary>
    public static class SignatureEffects
    {
        private static readonly Dictionary<ClassKitId, SignatureEffect> Effects = new Dictionary<ClassKitId, SignatureEffect>
        {
            [ClassKitId.Bastion] = new SignatureEffect(SignatureKind.Escudo, 0.5f, 0f, 5f),
            [ClassKitId.Vigia] = new SignatureEffect(SignatureKind.Impulso, 4f),
            [ClassKitId.Oraculo] = new SignatureEffect(SignatureKind.Curacion, 40f),
            [ClassKitId.DruidaCeniza] = new SignatureEffect(SignatureKind.Area, 1.6f, 4f),
            [ClassKitId.Duelista] = new SignatureEffect(SignatureKind.Torbellino, 2f, 3f),
            [ClassKitId.Arcanista] = new SignatureEffect(SignatureKind.Orbe, 2.5f, 1.2f)
        };

        public static SignatureEffect For(ClassKitId kit) => Effects[kit];
    }
}
