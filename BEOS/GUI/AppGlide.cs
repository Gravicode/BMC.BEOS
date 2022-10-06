
#if HasGUI
using BEOS;
using BEOS.Graph;
using BEOS.GUI;
using BEOS.GUI.Widgets;
using BEOS.Misc;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace BEOS.GUI
{   
    class AppGlide : Window
    {
        Image image;
        Graphics g;

        public unsafe AppGlide(int X, int Y) : base(X, Y, 480, 272)
        {
#if Bahasa
            Title = "GlideApp";
#else
            Title = "GlideApp";
#endif


            image = new Image(this.Width, this.Height);
            fixed (int* p = image.RawData)
                g = new Graphics(image.Width, image.Height, (uint*)p);
           /*
           BEOS.Drawing.Glide.SetupGlide(480, 272, 96, 0);
            string GlideXML = @"<Glide Version=""1.0.7""><Window Name=""instance115"" Width=""480"" Height=""272"" BackColor=""dce3e7""><Button Name=""btn"" X=""40"" Y=""60"" Width=""120"" Height=""40"" Alpha=""255"" Text=""Click Me"" Font=""4"" FontColor=""000000"" DisabledFontColor=""808080"" TintColor=""000000"" TintAmount=""0""/><TextBlock Name=""TxtTest"" X=""42"" Y=""120"" Width=""300"" Height=""32"" Alpha=""255"" Text=""TextBlock"" TextAlign=""Left"" TextVerticalAlign=""Top"" Font=""6"" FontColor=""0"" BackColor=""000000"" ShowBackColor=""False""/></Window></Glide>";

            //Resources.GetString(Resources.StringResources.Window)
            Window window = GlideLoader.LoadWindow(GlideXML);

            GlideTouch.Initialize();

            Button btn = (Button)window.GetChildByName("btn");
            TextBlock txt = (TextBlock)window.GetChildByName("TxtTest");
            btn.TapEvent += (object sender) =>
            {
                txt.Text = "Welcome to Glide for TinyCLR 2 - Cheers from Mif ;)";
                Debug.WriteLine("Button tapped.");

                window.Invalidate();
                txt.Invalidate();
            };

            BEOS.Drawing.Glide.MainWindow = window;
            */
        }

        public override void OnDraw()
        {
            base.OnDraw();

            Framebuffer.Graphics.DrawImage(X, Y, image);


        }

        bool Pressed = false;

     

        public override void OnInput()
        {
            base.OnInput();

            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                if (!Pressed)
                {
                    //Control.MousePosition.X > this.X + Btns[i].X && Control.MousePosition.X
                    Pressed = true;
                }

            }
            else
            {
                Pressed = false;
            }
        }



    }
}
#endif