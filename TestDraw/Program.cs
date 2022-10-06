// See https://aka.ms/new-console-template for more information
using BEOS.Drawing;
using BEOS.Drawing.Display;
using BEOS.Drawing.UI;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
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

var bmp = BEOS.Drawing.Glide.GetBitmap();
bmp.Save("test.png");