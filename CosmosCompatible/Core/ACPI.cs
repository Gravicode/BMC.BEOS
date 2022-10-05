namespace Cosmos.Core
{
    internal class ACPI
    {
        public static void Shutdown() => BEOS.Driver.Power.Shutdown();
    }
}
