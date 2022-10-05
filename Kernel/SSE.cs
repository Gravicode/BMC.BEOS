using System.Runtime.InteropServices;

namespace BEOS
{
    internal static class SSE
    {
        [DllImport("*")]
        public static extern void enable_sse();
    }
}