using System;

namespace Portfolio_Tetris
{
    /// <summary>
    /// 게임데이터셋을 콘솔로 그리는 객체. 
    /// </summary>
    public class ConsolePainter
    {
        public void Paint(GameDataSet dataSet)
        {
            Console.Clear();
            // 이미 떨어진 블록 그리기
            bool[,] blocks = dataSet.fallenBlocks.Clone() as bool[,];

            var flyingBlock = dataSet.currentFlyingBlock;
            int shapeKey = flyingBlock.shapeKey;
            int rotateIndex = flyingBlock.shapeRotateIndex;
            var shapeData = dataSet.blockShapeDictionary[shapeKey][rotateIndex].Shape;
            for (int i = 0; i < shapeData.GetLength(0); i++)
            {
                for (int j = 0; j < shapeData.GetLength(0); j++)
                {
                    if (shapeData[i, j] == true)
                    {
                        int height = flyingBlock.Height + i;
                        int width = flyingBlock.width + j;
                        blocks[height, width] = true;
                    }
                }
            }
            
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                string lineString = "";
                // 모든 열이 true 인지 탐색 
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j] == true)
                    {
                        lineString += "[X]";
                    }
                    else
                    {
                        lineString += "[_]";
                    }
                }

                Console.WriteLine(lineString);
            }
        }
    }
}