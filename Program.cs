using System;
using System.Threading;

namespace Portfolio_Tetris
{
    class Program
    {
        static GameDataSet gameDataSet = new GameDataSet(15, 10);
        static InputHandler inputHandler = new InputHandler();
        static GameProcesser processer = new GameProcesser();
        private static ConsolePainter painter = new ConsolePainter();

        static void Main(string[] args)
        {
            while (true)
            {
                inputHandler.ProcessInputOnce();
                processer.Process(gameDataSet, inputHandler);
                painter.Paint(gameDataSet);
                Thread.Sleep(1000);
            }
        }
    }
}