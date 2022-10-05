using System;

namespace Cosmos.System
{
    internal class KeyboardManager
    {
        static KeyEvent KeyEvent;

        internal static bool TryReadKey(out KeyEvent key)
        {
            KeyEvent.Key = (ConsoleKeyEx)BEOS.Keyboard.KeyInfo.Key;
            KeyEvent.KeyChar = BEOS.Keyboard.KeyInfo.KeyChar;
            key = KeyEvent;

            bool doReturn = BEOS.Keyboard.KeyInfo.KeyState.HasFlag(ConsoleKeyState.Pressed) && BEOS.Keyboard.KeyInfo.KeyChar != '\0' ;

            BEOS.Keyboard.CleanKeyInfo();
            return doReturn;
        }
    }
}
