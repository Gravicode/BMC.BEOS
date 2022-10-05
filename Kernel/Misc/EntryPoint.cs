using Internal.Runtime.CompilerHelpers;
using BEOS;
using BEOS.Driver;
using BEOS.FS;
using BEOS.Graph;
using BEOS.Misc;
using BEOS.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BEOS.Misc
{
    internal static unsafe class EntryPoint
    {
        [RuntimeExport("Entry")]
        public static void Entry(MultibootInfo* Info, IntPtr Modules, IntPtr Trampoline)
        {
            Allocator.Initialize((IntPtr)0x20000000);

            StartupCodeHelpers.InitializeModules(Modules, true);

#if HasGC
            GC.AllowCollect = false;
#endif

            PageTable.Initialise();

            ASC16.Initialise();

            VBEInfo* info = (VBEInfo*)Info->VBEInfo;
            if (info->PhysBase != 0)
            {
                Framebuffer.Initialize(info->ScreenWidth, info->ScreenHeight, (uint*)info->PhysBase);
                Framebuffer.Graphics.Clear(0x0);
            }
            else 
            {
                for (; ; ) Native.Hlt();
            }

            Console.Setup();
            IDT.Disable();
            GDT.Initialise();
            IDT.Initialize();
            Interrupts.Initialize();
            IDT.Enable();

            SSE.enable_sse();
            //AVX.init_avx();

            ACPI.Initialize();
#if UseAPIC
            PIC.Disable();
            LocalAPIC.Initialize();
            IOAPIC.Initialize();
#else
        PIC.Enable();
#endif
            Timer.Initialize();

            Keyboard.Initialize();

            Serial.Initialise();

            PS2Controller.Initialize();
            VMwareTools.Initialize();

            SMBIOS.Initialise();

            PCI.Initialise();

            IDE.Initialize();
            SATA.Initialize();

            ThreadPool.Initialize();

            Console.WriteLine($"[SMP] Trampoline: 0x{((ulong)Trampoline).ToString("x2")}");
            Native.Movsb((byte*)SMP.Trampoline, (byte*)Trampoline, 512);

            SMP.Initialize((uint)SMP.Trampoline);

#if HasGC
            GC.AllowCollect = true;
#endif

            //Only fixed size vhds are supported!
            Console.Write("[Initrd] Initrd: 0x");
            Console.WriteLine((Info->Modules[0]).ToString("x2"));
            Console.WriteLine("[Initrd] Initializing Ramdisk");
            new Ramdisk((IntPtr)(Info->Modules[0]));
            //new FATFS();
            new TarFS();

            KMain();
        }

        [DllImport("*")]
        public static extern void KMain();
    }
}