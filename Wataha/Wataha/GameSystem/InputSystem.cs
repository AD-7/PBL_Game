using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Wataha.GameSystem
{
    static class InputSystem
    {
        public static MouseState mouseState;
        public static MouseState mouseStateOld;
        public static KeyboardState newKeybordState;
        public static KeyboardState oldKeybordState;
        public static Rectangle Cursor;

        public static void UpdateCursorPosition()
        {
            InputSystem.mouseStateOld = InputSystem.mouseState;
            InputSystem.mouseState = Mouse.GetState();
            
            Cursor.X = InputSystem.mouseState.X; Cursor.Y = InputSystem.mouseState.Y;
        }
    }
}
