﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Copyright (c) GHI Electronics, LLC.
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
//using Microsoft.SPOT;
//using Microsoft.SPOT.Hardware;
using BEOS.Drawing.Display;
using BEOS.Drawing.Geom;
using BEOS.Drawing.UI;
using Glide.Properties;
using System.Drawing;
using Size = BEOS.Drawing.Geom.Size;
using BEOS.Misc;
//using System.Threading.Channels;

namespace BEOS.Drawing
{
    /// <summary>
    /// The Glide class provides core functionality.
    /// </summary>
    public static class Glide
    {
        internal static BEOS.Graph.Graphics screen;
        private static Window _mainWindow;
        private static BEOS.Drawing.UI.Keyboard _keyboard;
        private static KeyboardText _keyboardText;
        private static InputBox _inputBox;
        private static Dropdown _dropdown;
        private static List _list;
        private static Size screenSize;
        //public static IntPtr Hdc;
        static Glide()
        {
            //SetupGlide(480,272,96,0);
        }
        static Bitmap Bmp { set; get; }
        public static void SetupGlide(int width,int height, int bitsPerPixel, int orientationDeg)
        {
            //HardwareProvider.HwProvider.GetLCDMetrics(out width, out height, out bitsPerPixel, out orientationDeg);
            LCD = new Size() { Width = width, Height = height };
            Bmp = new Bitmap(width, height);
            screen = System.Drawing.Graphics.FromImage(Bmp);

            IsEmulator = false;
            /*
            // Are we in the emulator?
            if (SystemInfo.SystemID.SKU == 3)
                IsEmulator = true;
            else
                IsEmulator = false;
            */
            FitToScreen = false;

            Keyboard = DefaultKeyboard();
            MessageBoxManager = new MessageBoxManager();

            // Show loading
            System.Drawing.Bitmap loading = ResourceHelper.GetBitmap(Resources.loading);

            screen.DrawImage(loading, (LCD.Width - loading.Width) / 2, (LCD.Height - loading.Height) / 2, loading.Width, loading.Height);
            //xxscreen.Flush();
            //screen.Flush(0,0,width,height);
        }

        public static Bitmap GetBitmap()
        {
            return Bmp;
        }
        /// <summary>
        /// Returns the screen resolution.
        /// </summary>
        public static Size LCD
        {
            get
            {
                return Glide.screenSize;
            }
            private set
            {
                Glide.screenSize = value;
            }
        }

        /// <summary>
        /// Returns a reference to the bitmap that represents the current screen.
        /// This is only useful for drawing the bitmap to a display that does not
        /// support bitmap.flush().
        /// </summary>
        public static System.Drawing.Graphics Screen
        {
            get
            {
                return Glide.screen;
            }
        }

        /// <summary>
        /// This method changes the underlying size of the bitmap that is drawn to
        /// the screen. Do not call this method if you are using a regular display.
        /// It is only useful when you are using a non-native display such as a
        /// SPI display like our DisplayN18.
        /// </summary>
        /// <param name="width">The width of the display.</param>
        /// <param name="height">The height of the display.</param>
        public static void SetScreenSize(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                Console.WriteLine("Width and height must be positive.");
                return;
            }
                //throw new ArgumentException("Width and height must be positive.");

            Glide.screenSize.Width = width;
            Glide.screenSize.Height = height;
            
            Glide.screen.Dispose();
            Bmp = new Bitmap(width, height);
            unsafe
            {
                fixed (int* p = Bmp.RawData)
                Glide.screen = new BEOS.Graph.Graphics(Bmp.Width, Bmp.Height, (uint*)p);
            }
            //Glide.screen = Graphics.FromImage(Bmp);

            Glide.MessageBoxManager = new MessageBoxManager();
        }

        /// <summary>
        /// Opens the keyboard.
        /// </summary>
        /// <param name="sender">Object associated with the event.</param>
        public static void OpenKeyboard(object sender)
        {
            _inputBox = (InputBox)sender;

            // Make objects non-interactive
            for (int i = 0; i < MainWindow.NumChildren; i++)
                MainWindow[i].Interactive = false;

            _keyboardText.Text = _inputBox.Text;
            _keyboardText.IsPassword = (sender is PasswordBox) ? true : false;
            _keyboard.Start();

            MainWindow.AddChild(_keyboard);
            MainWindow.AddChild(_keyboardText);
            MainWindow.Invalidate();
        }

        /// <summary>
        /// Closes the keyboard.
        /// </summary>
        public static void CloseKeyboard()
        {
            _keyboard.Stop();

            MainWindow.RemoveChild(_keyboard);
            MainWindow.RemoveChild(_keyboardText);

            // Make objects interactive
            for (int i = 0; i < MainWindow.NumChildren; i++)
                MainWindow[i].Interactive = true;

            _inputBox.Text = _keyboardText.Text;

            MainWindow.Invalidate();
        }

        private static void KeyboardTapKey(object sender, TapKeyEventArgs args)
        {
            switch (args.Value.ToLower())
            {
                case BEOS.Drawing.UI.Keyboard.ActionKey.Backspace:
                    if (_keyboardText.Text.Length > 0)
                    {
                        _keyboardText.Text = _keyboardText.Text.Substring(0, _keyboardText.Text.Length - 1);
                        _keyboardText.Invalidate();
                    }
                    if (_keyboardText.Text.Length == 0 && !_keyboard.Uppercase)
                    {
                        _keyboard.Uppercase = true;
                        _keyboard.CurrentView = BEOS.Drawing.UI.Keyboard.View.Letters;
                        _keyboard.CalculateKeys();
                        _keyboard.Invalidate();
                        _keyboard.DrawKeyDown(args.Index);
                    }
                    break;

                case BEOS.Drawing.UI.Keyboard.ActionKey.Return:
                    CloseKeyboard();
                    break;

                case BEOS.Drawing.UI.Keyboard.ActionKey.Space:
                    _keyboardText.Text += " ";
                    _keyboardText.Invalidate();
                    break;

                case BEOS.Drawing.UI.Keyboard.ActionKey.Tab:
                    _keyboardText.Text += "   ";
                    _keyboardText.Invalidate();
                    break;

                default:
                    _keyboardText.Text += args.Value;
                    _keyboardText.Invalidate();
                    /*
                    if (_keyboardText.Text.Length == 1)
                    {
                        if (_keyboard.CurrentView == Keyboard.View.Letters)
                        {
                            _keyboard.Uppercase = false;
                            _keyboard.CurrentView = Keyboard.View.Letters;
                        }
                    }
                    */
                    break;
            }
        }

        /// <summary>
        /// Opens a List component.
        /// </summary>
        /// <param name="sender">Object associated with the event.</param>
        /// <param name="list">List component that needs to be opened.</param>
        public static void OpenList(object sender, List list)
        {
            if (_list == null)
            {
                _dropdown = (Dropdown)sender;
                _list = list;
                _list.TapOptionEvent += new OnTapOption(list_TapOptionEvent);

                for (int i = 0; i < MainWindow.NumChildren; i++)
                    MainWindow[i].Interactive = false;

                MainWindow.AddChild(list);
                MainWindow.Invalidate();
            }
            else
            {
                Console.WriteLine("You already have a List open.");
            }
            //throw new SystemException("You already have a List open.");
        }

        /// <summary>
        /// Closes a List component.
        /// </summary>
        public static void CloseList()
        {
            if (_list != null)
            {
                _list.TapOptionEvent -= new OnTapOption(list_TapOptionEvent);
                MainWindow.RemoveChild(_list);
                _list = null;

                for (int i = 0; i < MainWindow.NumChildren; i++)
                    MainWindow[i].Interactive = true;

                MainWindow.Invalidate();
            }
        }

        private static void list_TapOptionEvent(object sender, TapOptionEventArgs args)
        {
            _dropdown.Text = args.Label;
            _dropdown.Value = args.Value;
            _dropdown.TriggerValueChangedEvent(_dropdown);

            CloseList();
        }

        /// <summary>
        /// Flushes specified area to the screen.
        /// </summary>
        /// <param name="x">X-axis position.</param>
        /// <param name="y">Y-axis position.</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public static void Flush(int x, int y, int width, int height)
        {
            // Ignore flushes if no MainWindow is set.
            if (_mainWindow == null)
                return;

            int offsetY = y - _mainWindow.ListY;

            //screen.DrawImage(x, offsetY, _mainWindow.Graphics.GetBitmap(), x, y, width, height, 0xff);
            /*
            // Object must be partially visible so flushing doesn't error.
            if (_mainWindow.Rect.Contains(x, offsetY, width, height))
                Glide.screen.Flush(x, offsetY, width, height);
            */
            // Object must be partially visible so flushing doesn't error.
            if (_mainWindow.Rect.Contains(x, offsetY, width, height))
            { } //xxGlide.screen.Flush();
        }

        /// <summary>
        /// Flushes the rectangular area the DisplayObject occupies to the screen.
        /// </summary>
        /// <param name="displayObject">DisplayObject</param>
        public static void Flush(DisplayObject displayObject)
        {
            Flush(displayObject.Parent.X + displayObject.X, displayObject.Parent.Y + displayObject.Y, displayObject.Width, displayObject.Height);
        }

        /// <summary>
        /// Flushes the rectangular area to the screen.
        /// </summary>
        /// <param name="rect">Rectangle</param>
        public static void Flush(Geom.Rectangle rect)
        {
            Flush(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Returns a keyboard used by 320x240 LCDs.
        /// </summary>
        public static BEOS.Drawing.UI.Keyboard DefaultKeyboard()
        {
            BEOS.Drawing.UI.Keyboard keyboard = new (320, 128, 3, 32, 0);

            keyboard.BitmapUp = new Bitmap[4]
            { 
                ResourceHelper.GetBitmap(Resources.Keyboard_320x128_Up_Uppercase), 
                ResourceHelper.GetBitmap(Resources.Keyboard_320x128_Up_Lowercase), 
                ResourceHelper.GetBitmap(Resources.Keyboard_320x128_Up_Numbers), 
                ResourceHelper.GetBitmap(Resources.Keyboard_320x128_Up_Symbols)
            };

            return keyboard;
        }

        /// <summary>
        /// Indicates whether or not we're using the emulator.
        /// </summary>
        public static bool IsEmulator;

        /// <summary>
        /// Indicates whether or not to resize windows to the LCD's resolution.
        /// </summary>
        /// <remarks>This does not affect component placement. They will remain in their assigned position.</remarks>
        public static bool FitToScreen;

        /// <summary>
        /// Current Keyboard used by Glide.
        /// </summary>
        public static BEOS.Drawing.UI.Keyboard Keyboard
        {
            get { return _keyboard; }
            set
            {
                _keyboard = value;
                _keyboard.TapKeyEvent += new OnTapKey(KeyboardTapKey);
                _keyboardText = new KeyboardText(0, 0, LCD.Width, LCD.Height - _keyboard.Height);
            }
        }

        /// <summary>
        /// A message box that helps inform and instruct the user.
        /// </summary>
        public static MessageBoxManager MessageBoxManager;

        /// <summary>
        /// The window currently in focus.
        /// </summary>
        public static Window MainWindow
        {
            get { return _mainWindow; }
            set
            {
                // Ignore events on the current window
                if (_mainWindow != null)
                    _mainWindow.IgnoreEvents();

                // Change to the new window
                _mainWindow = value;

                // Begin handling events
                if (_mainWindow != null)
                {
                    _mainWindow.HandleEvents();
                    // Call render after because windows only flush if they're handling events
                    _mainWindow.Invalidate();
                }
            }
        }

    }
}