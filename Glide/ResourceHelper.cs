using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Glide.Properties
{
    public class ResourceHelper
    {
        public static Bitmap GetBitmap(byte[] ImageData)
        {
            
            Bitmap bmp;
            using (var ms = new MemoryStream(ImageData))
            {
                bmp = new Bitmap(ms);
                return bmp;
            }
           
        }
        static PrivateFontCollection fontCollection;
        public static Font GetFont(int fontSize = 12 , FontStyle fontStyle = FontStyle.Regular)
        {
            if (fontCollection == null)
            {
                fontCollection = new PrivateFontCollection();
                var fontBytes = File.ReadAllBytes("Fonts/calibri.ttf");
                var handle = GCHandle.Alloc(fontBytes, GCHandleType.Pinned);
                IntPtr pointer = handle.AddrOfPinnedObject();
                try
                {
                    fontCollection.AddMemoryFont(pointer, fontBytes.Length);
                    var FontFamily = fontCollection.Families[0];
                    var Fnt = new Font(FontFamily, fontSize, fontStyle, GraphicsUnit.Pixel);
                    return Fnt;
                }
                finally
                {
                    //handle.Free();
                }
            }
            else
            {
                var FontFamily = fontCollection.Families[0];
                var Fnt = new Font(FontFamily, fontSize, fontStyle, GraphicsUnit.Pixel);
                return Fnt;
            }
            return default;
        }

        private PrivateFontCollection _privateFontCollection = new PrivateFontCollection();

        public FontFamily GetFontFamilyByName(string name)
        {
            return _privateFontCollection.Families.FirstOrDefault(x => x.Name == name);
        }

        public void AddFont(string fullFileName)
        {
            AddFont(File.ReadAllBytes(fullFileName));
        }

        public void AddFont(byte[] fontBytes)
        {
            var handle = GCHandle.Alloc(fontBytes, GCHandleType.Pinned);
            IntPtr pointer = handle.AddrOfPinnedObject();
            try
            {
                _privateFontCollection.AddMemoryFont(pointer, fontBytes.Length);
            }
            finally
            {
                handle.Free();
            }
        }


        public static LoadedFont LoadFont(FileInfo file, int fontSize, FontStyle fontStyle)
        {
            var fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(file.FullName);
            if (fontCollection.Families.Length < 0)
            {
                throw new InvalidOperationException("No font familiy found when loading font");
            }

            var loadedFont = new LoadedFont();
            loadedFont.FontFamily = fontCollection.Families[0];
            loadedFont.Font = new Font(loadedFont.FontFamily, fontSize, fontStyle, GraphicsUnit.Pixel);
            return loadedFont;
        }

    }
    public struct LoadedFont
    {
        public Font Font { get; set; }
        public FontFamily FontFamily { get; set; }
    }

}
