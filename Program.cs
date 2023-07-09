using System;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio_Tetris
{
    class Program
    {
        static GameDataSet gameDataSet = new GameDataSet(15, 10);
        static InputHandler inputHandler = new InputHandler();
        static GameProcesser processer = new GameProcesser();
        private static ConsolePainter painter = new ConsolePainter();

        static async Task Main(string[] args)
        {
            Task inputTask = ReadInputAsync();

            while (true)
            {
                processer.Process(gameDataSet, inputHandler);
                painter.Paint(gameDataSet);
                Thread.Sleep(1000);
            }
            await inputTask;
        }

        static async Task ReadInputAsync()
        {
            while (true)
            {
                await Task.Run(() => inputHandler.ProcessInputOnce());
                await Task.Delay(100);
            }
        }
    }
}