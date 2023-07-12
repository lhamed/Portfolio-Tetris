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

            // 새 블록 만들기 ( 블록이 없다면 )
            ProcessCreateBlock(dataSetAfterClearBlock);

            return dataSetAfterClearBlock;
        }

        GameDataSet ProcessCreateBlock(GameDataSet dataSet)
        {
            dataSet.RandomlyDropBlock();
            return dataSet;
        }

        GameDataSet ProcessUserInput(GameDataSet dataSet, InputHandler inputHandler)
        {
            if (inputHandler.isProcessed)
            {
                // 처리할 인풋이 있다. 
                if (inputHandler.currentPressedKey == ConsoleKey.RightArrow)
                {
                    if (dataSet.width > dataSet.currentFlyingBlock.width)
                    {
                        var flyingBlock = dataSet.currentFlyingBlock;
                        int shapeKey = flyingBlock.shapeKey;
                        int rotateIndex = flyingBlock.shapeRotateIndex;
                        var shapeData = dataSet.blockShapeDictionary[shapeKey][rotateIndex].Shape;
                        for (int i = 0; i < shapeData.GetLength(0); i++)
                        {
                            for (int j = 0; j < shapeData.GetLength(1); j++)
                            {
                                if (shapeData[i, j])
                                {
                                    int height = flyingBlock.height + i;
                                    int width = flyingBlock.width + j;

                                    if (width + 1 >= dataSet.width)
                                    {
                                        inputHandler.isProcessed = false;
                                        return dataSet;
                                    }

                                    if (dataSet.fallenBlocks[height, width + 1])
                                    {
                                        inputHandler.isProcessed = false;
                                        return dataSet;
                                        // 움직일 수 없는 상태다. 
                                    }
                                }
                            }
                        }

                        dataSet.currentFlyingBlock.width += 1; //TODO width max 와 min 을 내부에서 한정지어야 한다. 
                    }
                }
                else if (inputHandler.currentPressedKey == ConsoleKey.LeftArrow)
                {
                    if (dataSet.width > 0)
                    {
                        var flyingBlock = dataSet.currentFlyingBlock;
                        int shapeKey = flyingBlock.shapeKey;
                        int rotateIndex = flyingBlock.shapeRotateIndex;
                        var shapeData = dataSet.blockShapeDictionary[shapeKey][rotateIndex].Shape;
                        for (int i = 0; i < shapeData.GetLength(0); i++)
                        {
                            for (int j = 0; j < shapeData.GetLength(1); j++)
                            {
                                if (shapeData[i, j])
                                {
                                    int height = flyingBlock.height + i;
                                    int width = flyingBlock.width + j;

                                    if (width <= 0)
                                    {
                                        inputHandler.isProcessed = false;
                                        return dataSet;
                                    }

                                    if (dataSet.fallenBlocks[height, width - 1])
                                    {
                                        inputHandler.isProcessed = false;
                                        return dataSet;
                                        // 움직일 수 없는 상태다. 
                                    }
                                }
                            }
                        }

                        dataSet.currentFlyingBlock.width -= 1; //TODO width max 와 min 을 내부에서 한정지어야 한다. 
                    }
                }
                else if (inputHandler.currentPressedKey == ConsoleKey.UpArrow)
                {
                    dataSet.currentFlyingBlock.shapeRotateIndex += 1; // TODO 3을 넘으면 0으로 가도록 해야한다. (회전경우의수 한정)
                    if (dataSet.currentFlyingBlock.shapeRotateIndex == 4)
                    {
                        dataSet.currentFlyingBlock.shapeRotateIndex = 0;
                    }
                    inputHandler.isProcessed = false;
                    return dataSet;
                }

                inputHandler.isProcessed = false;
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
            bool isNotMovableHeight = false;
            var flyingBlock = dataSet.currentFlyingBlock;
            int shapeKey = flyingBlock.shapeKey;
            int rotateIndex = flyingBlock.shapeRotateIndex;
            var shapeData = dataSet.blockShapeDictionary[shapeKey][rotateIndex].Shape;
            for (int i = 0; i < shapeData.GetLength(0); i++)
            {
                for (int j = 0; j < shapeData.GetLength(1); j++)
                {
                    if (shapeData[i, j])
                    {
                        int height = flyingBlock.height + i;
                        int width = flyingBlock.width + j;

                        // 바닥 체크 
                        if (dataSet.height - 1 <= height)
                        {
                            isNotMovableHeight = true;
                            break;
                        }

                        if (dataSet.fallenBlocks[height + 1, width])
                        {
                            isNotMovableHeight = true;
                            break;
                            // 움직일 수 없는 상태다. 
                        }
                    }
                }

                if (isNotMovableHeight)
                {
                    break;
                }
            }

            if (isNotMovableHeight)
            {
                dataSet.ChangeFlyingBlockToFallenBlock(dataSet.currentFlyingBlock);
                return dataSet;
            }

            dataSet.currentFlyingBlock.height += 1;
            return dataSet;
        }

        GameDataSet ClearFullBlock(GameDataSet dataSet)
        {
            var possibleClearIndex = new List<int>();
            var blocks = dataSet.fallenBlocks;

            // 모든 행을 탐색 
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                bool thisRowIsClear = true;
                // 모든 열이 true 인지 탐색 
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    // 하나라도 false 라면 클리어 되면 안된다.  
                    if (blocks[i, j] == false)
                    {
                        thisRowIsClear = false;
                    }
                }

                if (thisRowIsClear)
                {
                    possibleClearIndex.Add(i);
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