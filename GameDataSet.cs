using System;
using System.Collections.Generic;

namespace Portfolio_Tetris
{
    /// <summary>
    /// 현재 게임의 상태를 모두 가지고 있다. 
    /// </summary>
    public class GameDataSet
    {
        public int height;
        public int width;
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

        public void RandomlyDropBlock()
        {
            Random rand = new Random(DateTime.Now.Second);
            int target = rand.Next(0, blockShapeDictionary.Count);

            FlyingBlock flyingBlock = new FlyingBlock()
            {
                height = 0,
                width = 5,
                shapeKey = target,
                shapeRotateIndex = 0
            };

            AddFlyingBlock(flyingBlock);
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

        public void ChangeFlyingBlockToFallenBlock(FlyingBlock flyingBlock)
        {
            int shapeKey = flyingBlock.shapeKey;
            int rotateIndex = flyingBlock.shapeRotateIndex;
            var shapeData = blockShapeDictionary[shapeKey][rotateIndex].Shape;
            for (int i = 0; i < shapeData.GetLength(0); i++)
            {
                for (int j = 0; j < shapeData.GetLength(1); j++)
                {
                    if (shapeData[i, j] == true)
                    {
                        int height = flyingBlock.height + i;
                        int width = flyingBlock.width + j;
                        fallenBlocks[height, width] = true;
                    }
                }
            }

            currentFlyingBlock = null;
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
                        {false, false, true, true},
                        {false, true, true,false},
                        {false, false, false,false},
                        {false, false, false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 1, Shape = new bool[,]
                    {
                        {false, false, false, false},
                        {false, false, true,false},
                        {false, false, true,true},
                        {false, false, false,true},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 2, Shape = new bool[,]
                    {
                        {false, false, true, true},
                        {false, true, true,false},
                        {false, false, false,false},
                        {false, false, false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 3, Shape = new bool[,]
                    {
                        {false, false, false, false},
                        {false, false, true,false},
                        {false, false, true,true},
                        {false, false, false,true},
                    }
                }
            };

            dictonary.Add(0, blockShapeOne);
            
            BlockShapeData[] blockShapeTwo = new BlockShapeData[]
            {
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 0, Shape = new bool[,]
                    {
                        {true, true},
                        {true, true}
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 1, Shape = new bool[,]
                    {
                        {true, true},
                        {true, true}
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 2, Shape = new bool[,]
                    {
                        {true, true},
                        {true, true}
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 3, Shape = new bool[,]
                    {
                        {true, true},
                        {true, true}
                    }
                }
            };

            dictonary.Add(1, blockShapeTwo);
            
            BlockShapeData[] blockShapeThree = new BlockShapeData[]
            {
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 0, Shape = new bool[,]
                    {
                        {false, false,false,false},
                        {true, true,true,true},
                        {false, false,false,false},
                        {false, false,false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 1, Shape = new bool[,]
                    {
                        {false, false,true,false,},
                        {false, false,true,false,},
                        {false, false,true,false,},
                        {false, false,true,false,},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 2, Shape = new bool[,]
                    {
                        {false, false,false,false},
                        {true, true,true,true},
                        {false, false,false,false},
                        {false, false,false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 3, Shape = new bool[,]
                    {
                        {false, false,true,false,},
                        {false, false,true,false,},
                        {false, false,true,false,},
                        {false, false,true,false,},
                    }
                }
            };

            dictonary.Add(2, blockShapeThree);
            
            
            BlockShapeData[] blockShapeFour = new BlockShapeData[]
            {
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 0, Shape = new bool[,]
                    {
                        {false, true,true,true},
                        {false, false,false,true},
                        {false, false,false,false},
                        {false, false,false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 1, Shape = new bool[,]
                    {
                        {false, false,false,true},
                        {false, false,false,true},
                        {false, false,true,true},
                        {false, false,false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 2, Shape = new bool[,]
                    {
                        {false, false,false,false},
                        {false, true,false,false},
                        {false, true,true,true},
                        {false, false,false,false},

                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 3, Shape = new bool[,]
                    {
                        {false, true,true,true},
                        {false, false,false,true},
                        {false, false,false,false},
                        {false, false,false,false},
                    }
                }
            };

            dictonary.Add(3, blockShapeFour);

            BlockShapeData[] blockShapeFive = new BlockShapeData[]
            {
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 0, Shape = new bool[,]
                    {
                        {false, true,true,true},
                        {false, false,true,false},
                        {false, false,false,false},
                        {false, false,false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 1, Shape = new bool[,]
                    {
                        {false, false,false,true},
                        {false, false,true,true},
                        {false, false,false,true},
                        {false, false,false,false},
                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 2, Shape = new bool[,]
                    {
                        {false, false,false,false},
                        {false,false,true,false},
                        {false, true,true,true},
                        {false, false,false,false},

                    }
                },
                new BlockShapeData()
                {
                    Key = 0, rotateIndex = 3, Shape = new bool[,]
                    {
                        {false, true,true,true},
                        {false, false,true,true},
                        {false, false,false,false},
                        {false, false,false,false},
                    }
                }
            };

            dictonary.Add(4, blockShapeFive);
            
            return dictonary;
        }
    }

    /// <summary>
    /// 현재 움직일 수 있는 (떨어지고 있는) 블록이다. 
    /// </summary>
    public class FlyingBlock
    {
        public int height;
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