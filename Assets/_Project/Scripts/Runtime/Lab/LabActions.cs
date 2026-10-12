using System;

namespace Specimen.Runtime.Lab
{
    /// <summary>Acciones del menú del Laboratorio, inyectadas por el bootstrap para que la pantalla no sepa de escenas.</summary>
    public sealed class LabActions
    {
        public LabActions(Action unlockSelected, Action newRun, Func<bool> canReturn, Action returnToElevator)
        {
            UnlockSelected = unlockSelected ?? throw new ArgumentNullException(nameof(unlockSelected));
            NewRun = newRun ?? throw new ArgumentNullException(nameof(newRun));
            CanReturn = canReturn ?? throw new ArgumentNullException(nameof(canReturn));
            ReturnToElevator = returnToElevator ?? throw new ArgumentNullException(nameof(returnToElevator));
        }

        public Action UnlockSelected { get; }
        /// <summary>Empieza una iteración nueva: aparece en la Armería con las mutaciones compradas.</summary>
        public Action NewRun { get; }
        /// <summary>Solo durante una partida en curso (se entró desde el Ascensor).</summary>
        public Func<bool> CanReturn { get; }
        public Action ReturnToElevator { get; }
    }
}
