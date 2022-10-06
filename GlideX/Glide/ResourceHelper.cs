using BEOS.FS;
using BEOS.Misc;

namespace Glide.Properties
{
    public class ResourceHelper
    {
        public static Bitmap GetBitmap(byte[] ImageData)
        {

            Bitmap bmp;

            bmp = new Bitmap(ImageData);
            return bmp;


        }


        public static IFont GetFont(int Size=12)
        {
            var lsfont = new IFont(new PNG(File.ReadAllBytes("Images/M+128.png")), "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~", Size);
            return lsfont;
        }

    }
    internal class Resources
    {

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Button_Down
        {
            get
            {
                //object obj = ResourceManager.GetObject("Button_Down", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Button_Down.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Button_Up
        {
            get
            {
                //object obj = ResourceManager.GetObject("Button_Up", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Button_Up.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] CheckBox_Off
        {
            get
            {
                //object obj = ResourceManager.GetObject("CheckBox_Off", resourceCulture);
                return File.ReadAllBytes("Images/Glide/CheckBox_Off.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] CheckBox_On
        {
            get
            {
                //object obj = ResourceManager.GetObject("CheckBox_On", resourceCulture);
                return File.ReadAllBytes("Images/Glide/CheckBox_On.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] DataGridIcon_Asc
        {
            get
            {
                //object obj = ResourceManager.GetObject("DataGridIcon_Asc", resourceCulture);
                return File.ReadAllBytes("Images/Glide/DataGridIcon_Asc.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] DataGridIcon_Desc
        {
            get
            {
                //object obj = ResourceManager.GetObject("DataGridIcon_Desc", resourceCulture);
                return File.ReadAllBytes("Images/Glide/DataGridIcon_Desc.gif");
            }
        }


        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] DropdownButton_Down
        {
            get
            {
                //object obj = ResourceManager.GetObject("DropdownButton_Down", resourceCulture);
                return File.ReadAllBytes("Images/Glide/DropdownButton_Down.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] DropdownButton_Up
        {
            get
            {
                //object obj = ResourceManager.GetObject("DropdownButton_Up", resourceCulture);
                return File.ReadAllBytes("Images/Glide/DropdownButton_Up.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] DropdownText_Down
        {
            get
            {
                //object obj = ResourceManager.GetObject("DropdownText_Down", resourceCulture);
                return File.ReadAllBytes("Images/Glide/DropdownText_Down.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] DropdownText_Up
        {
            get
            {
                //object obj = ResourceManager.GetObject("DropdownText_Up", resourceCulture);
                return File.ReadAllBytes("Images/Glide/DropdownText_Up.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Keyboard_320x128_Up_Lowercase
        {
            get
            {
                //object obj = ResourceManager.GetObject("Keyboard_320x128_Up_Lowercase", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Keyboard_320x128_Up_Lowercase.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Keyboard_320x128_Up_Numbers
        {
            get
            {
                //object obj = ResourceManager.GetObject("Keyboard_320x128_Up_Numbers", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Keyboard_320x128_Up_Numbers.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Keyboard_320x128_Up_Symbols
        {
            get
            {
                //object obj = ResourceManager.GetObject("Keyboard_320x128_Up_Symbols", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Keyboard_320x128_Up_Symbols.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Keyboard_320x128_Up_Uppercase
        {
            get
            {
                //object obj = ResourceManager.GetObject("Keyboard_320x128_Up_Uppercase", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Keyboard_320x128_Up_Uppercase.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] loading
        {
            get
            {
                // object obj = ResourceManager.GetObject("loading", resourceCulture);
                return File.ReadAllBytes("Images/Glide/loading.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Modal
        {
            get
            {
                //object obj = ResourceManager.GetObject("Modal", resourceCulture);
                return File.ReadAllBytes("Images/Glide/Modal.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ProgressBar
        {
            get
            {
                //object obj = ResourceManager.GetObject("ProgressBar", resourceCulture);
                return File.ReadAllBytes("Images/Glide/ProgressBar.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ProgressBar_Fill
        {
            get
            {
                //object obj = ResourceManager.GetObject("ProgressBar_Fill", resourceCulture);
                return File.ReadAllBytes("Images/Glide/ProgressBar_Fill.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] RadioButton
        {
            get
            {
                //object obj = ResourceManager.GetObject("RadioButton", resourceCulture);
                return File.ReadAllBytes("Images/Glide/RadioButton.gif");
            }
        }

        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] TextBox
        {
            get
            {
                //object obj = ResourceManager.GetObject("TextBox", resourceCulture);
                return File.ReadAllBytes("Images/Glide/TextBox.gif");
            }
        }
    }
}


