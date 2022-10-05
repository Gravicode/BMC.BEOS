#if HasGUI
using BEOS.FS;
using BEOS.Misc;
using System.Drawing;

namespace BEOS.GUI
{
    internal class Welcome : Window
    {
        public Image img;

        public Welcome(int X, int Y) : base(X, Y, 280, 225)
        {
#if Bahasa
            this.Title = "Selamat Datang";
#else
            this.Title = "Welcome";
#endif
            img = new PNG(File.ReadAllBytes("Images/Banner.png"));
        }

        public override void OnDraw()
        {
            base.OnDraw();
            Framebuffer.Graphics.DrawImage(X, Y, img);
#if Bahasa
            WindowManager.font.DrawString(X, Y + img.Height, "Selamat datang di BEOS (Buitenzorg Embedded OS)!\nProject ini merupakan experiment bikin OS yang simpel tapi mantap.\nSilakan cek: https://github.com/gravicode/Beos!", Width);
#else
            WindowManager.font.DrawString(X, Y + img.Height, "Welcome to BEOS!\nThis project is aim to show how to make asimple but powerful operating system.\nCheck out: https://github.com/gravicode/Beos!", Width);
#endif
        }
    }
}
#endif