namespace Portfolio_Tetris
{
    /// <summary>
    /// 현재 게임의 상태를 모두 가지고 있다. 
    /// </summary>
    public class GameDataSet
    {
        private int height;
        private int width;
        public FlyingBlock currentFlyingBlock;
        public bool[,] fallenBlocks;

        public GameDataSet(int height, int width)
        {
            this.height = height;
            this.width = width;
            fallenBlocks = new bool[height, width];
        }

        public void AddFlyingBlock(FlyingBlock block)
        {
            // 이미 있다면 나중에 처리해주자. 
            if (currentFlyingBlock != null)
            {
                //TODO 
            }
            currentFlyingBlock = block;
        }
    }

    /// <summary>
    /// 현재 움직일 수 있는 (떨어지고 있는) 블록이다. 
    /// </summary>
    public class FlyingBlock
    {
        public int Height;
        public bool[,] Shape;
    }
}