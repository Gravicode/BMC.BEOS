////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Copyright (c) GHI Electronics, LLC.
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
//using Microsoft.SPOT;
//using Microsoft.SPOT.Presentation.Media;
using BEOS.Drawing.Display;
using BEOS.Drawing.Geom;
using TinyCLR2.Glide.Ext;

namespace BEOS.Drawing.UI
{
    /// <summary>
    /// The PasswordBox component allows text input without revealing the text.
    /// </summary>
    public class PasswordBox : InputBox
    {
        /// <summary>
        /// Creates a new PasswordBox.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="alpha">Alpha</param>
        /// <param name="x">X-axis position.</param>
        /// <param name="y">Y-axis position.</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public PasswordBox(string name, ushort alpha, int x, int y, int width, int height)
        {
            Name = name;
            Alpha = alpha;
            X = x;
            Y = y;
            Width = width;
            Height = height;

            // Default
            Text = String.Empty;
            if (TextAlign == null) TextAlign = new System.Drawing.StringFormat();
            TextAlign.Alignment = System.Drawing.StringAlignment.Near; //Bitmaps.DT_AlignmentLeft;
            Font = FontManager.GetFont(FontManager.FontType.droid_reg12);
            FontColor = TinyCLR2.Glide.Ext.Colors.Black;
        }

        /// <summary>
        /// Renders the PasswordBox onto it's parent container's graphics.
        /// </summary>
        public override void Render()
        {
            int x = Parent.X + X;
            int y = Parent.Y + Y;
            ushort alpha = (Enabled) ? Alpha : (ushort)(Alpha / 3);

            Parent.Graphics.Scale9Image(x, y, Width, Height, _TextBox, 5, 5, 5, 5, alpha);
            int len = Text.Length;
            string str = String.Empty;
            for (int i = 0; i < len; i++)
                str += "*";
            Parent.Graphics.DrawTextInRect(ShortenText(str), x + leftMargin, y + ((Height - Font.Height) / 2), Width - (leftMargin * 2), Height, TextAlign, FontColor, Font);
        }

        /// <summary>
        /// Disposes all disposable objects in this object.
        /// </summary>
        public override void Dispose()
        {
            _TextBox.Dispose();
        }
    }
}