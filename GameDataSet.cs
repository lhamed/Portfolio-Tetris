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

            //TODO 따로 팩토리로 분리할 것 
            blockShapeDictionary = CreateBlockConfiguration();
        }

        public void AddFlyingBlock(FlyingBlock block)
        {
            // 이미 있다면 추가해주지 않는다.  
            if (currentFlyingBlock != null)
            {
                return;
            }

            currentFlyingBlock = block;
        }

        public void ClearLineNumber(int index)
        {
            // 모든 열이 true 인지 탐색 
            for (int j = 0; j < fallenBlocks.GetLength(1); j++)
            {
                fallenBlocks[index, j] = false;
            }
        }
        
        
        Dictionary<int, BlockShapeData[]> CreateBlockConfiguration()
        {
            var dictonary = new Dictionary<int, BlockShapeData[]>();
            BlockShapeData[] blockShapeOne = new BlockShapeData[]
            {
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 0, Shape = new bool[,]
                    {
                        {true, false},
                        {true, true}
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 1, Shape = new bool[,]
                    {
                        {false, true},
                        {true, true}
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 2, Shape = new bool[,]
                    {
                        {true, true},
                        {false, true}
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 3, Shape = new bool[,]
                    {
                        {true, true},
                        {true, false}
                    }
                }
            };
            
            dictonary.Add(1,blockShapeOne);
            return null;
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