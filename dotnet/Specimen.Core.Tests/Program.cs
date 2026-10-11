using NUnitLite;

namespace Specimen.Tests
{
    /// <summary>Punto de entrada del runner headless. El código de salida es el nº de tests fallidos.</summary>
    public static class Program
    {
        public static int Main(string[] args) => new AutoRun(typeof(Program).Assembly).Execute(args);
    }
}
