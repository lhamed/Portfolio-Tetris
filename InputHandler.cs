using System;

namespace Portfolio_Tetris
{
    /// <summary>
    /// 유저의 콘솔 인풋을 처리한다. 
    /// </summary>
    public class InputHandler
    {
        public ConsoleKey currentPressedKey;
        public bool isProcessed = false;
        
        public void ProcessInputOnce()
        {
            isProcessed = true;
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.RightArrow :
                    isProcessed = true;
                    currentPressedKey = ConsoleKey.RightArrow;
                    break;
                case ConsoleKey.LeftArrow :
                    isProcessed = true;
                    currentPressedKey = ConsoleKey.LeftArrow;
                    break;
                case ConsoleKey.UpArrow :
                    isProcessed = true;
                    currentPressedKey = ConsoleKey.UpArrow;
                    break;
                case ConsoleKey.DownArrow :
                    isProcessed = true;
                    currentPressedKey = ConsoleKey.DownArrow;
                    break;
            }
        }
    }
}