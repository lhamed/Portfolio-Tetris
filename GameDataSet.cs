using System.Collections.Generic;

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

        public Dictionary<int, BlockShapeData[]> blockShapeDictionary = new Dictionary<int, BlockShapeData[]>();
        
        public GameDataSet(int height, int width)
        {
            this.height = height;
            this.width = width;
            fallenBlocks = new bool[height, width];
            
            //TODO blockShapeDataDictonary 준비하기 TODO 
            
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
        public int width;

        public int shapeKey;
        public int shapeRotateIndex;
    }

    public class BlockShapeData
    {
        public int Key;
        public int rotateIndex;
        public bool[,] Shape;
    }
}