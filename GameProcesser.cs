using System;
using System.Collections.Generic;

namespace Portfolio_Tetris
{
    /// <summary>
    /// 게임 데이터셋을 게임을 진행시키거나 인풋을 반영한다. 
    /// </summary>
    public class GameProcesser
    {
        public GameDataSet Process(GameDataSet dataSet, InputHandler inputHandler)
        {
            // 새 블록 만들기 ( 블록이 없다면 )
            var dataSetAfterCreation = ProcessCreateBlock(dataSet);

            // 유저 인풋 처리 ( 블록 회전 )
            var dataSetAfterInput = ProcessUserInput(dataSetAfterCreation, inputHandler);

            // 게임 내 중력 처리 ( 한칸 떨어지게 )
            var dataSetAfterGravity = ProcessGravity(dataSetAfterInput);

            // 가로로 다 채운 블록을 사라지게 하는 처리 ( 블록 클리어 처리 )
            var dataSetAfterClearBlock = ClearFullBlock(dataSetAfterGravity);

            return dataSetAfterClearBlock;
        }

        GameDataSet ProcessCreateBlock(GameDataSet dataSet)
        {
            dataSet.RandomlyDropBlock();
            return dataSet;
        }

        GameDataSet ProcessUserInput(GameDataSet dataSet, InputHandler inputHandler)
        {
            var isKeyExist = inputHandler.isProcessed;
            if (isKeyExist == true)
            {
                // 처리할 인풋이 있다. 
                if (inputHandler.currentPressedKey == ConsoleKey.RightArrow)
                {
                    dataSet.currentFlyingBlock.width += 1; //TODO width max 와 min 을 내부에서 한정지어야 한다. 
                }
                else if (inputHandler.currentPressedKey == ConsoleKey.LeftArrow)
                {
                    dataSet.currentFlyingBlock.width -= 1; //TODO width max 와 min 을 내부에서 한정지어야 한다. 
                }
                else if (inputHandler.currentPressedKey == ConsoleKey.UpArrow)
                {
                    dataSet.currentFlyingBlock.shapeRotateIndex += 1; // TODO 3을 넘으면 0으로 가도록 해야한다. (회전경우의수 한정)
                }

                return dataSet;
            }
            else
            {
                // 처리할 인풋이 없다. 
                return dataSet;
            }
        }

        GameDataSet ProcessGravity(GameDataSet dataSet)
        {
            // 한칸 아래에 벽돌이 하나라도 있을 경우 또는 가장 바닥일 경우  
            // 탐색 

            if (dataSet.currentFlyingBlock.Height + 1 >= dataSet.height)
            {
                dataSet.ChangeFlyingBlockToFallenBlock(dataSet.currentFlyingBlock);
            }
            else
            {
                dataSet.currentFlyingBlock.Height += 1;
            }
            return dataSet;
        }

        GameDataSet ClearFullBlock(GameDataSet dataSet)
        {
            var possibleClearIndex = new List<int>();
            var blocks = dataSet.fallenBlocks;

            // 모든 행을 탐색 
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                // 모든 열이 true 인지 탐색 
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    // 하나라도 false 라면 다음 행으로 넘어가야한다. 
                    if (blocks[i, j] == false)
                    {
                        break;
                    }

                    if (blocks[i, blocks.GetLength(1) - 1])
                    {
                        // 모두 true 이므로 사라질 수 있는 행이다. 
                        possibleClearIndex.Add(i);
                    }
                }
            }

            foreach (var index in possibleClearIndex)
            {
                dataSet.ClearLineNumber(index);
            }

            return dataSet;
        }
    }
}