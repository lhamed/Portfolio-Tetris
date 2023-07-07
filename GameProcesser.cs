using System;

namespace Portfolio_Tetris
{
    /// <summary>
    /// 게임 데이터셋을 게임을 진행시키거나 인풋을 반영한다. 
    /// </summary>
    public class GameProcesser
    {
        public GameDataSet Process(GameDataSet dataSet, InputHandler inputHandler)
        {
            // 유저 인풋 처리 ( 블록 회전 )
            var dataSetAfterInput = ProcessUserInput(dataSet,inputHandler);

            // 게임 내 중력 처리 ( 한칸 떨어지게 )
            var dataSetAfterGravity = ProcessGravity(dataSetAfterInput);

            // 가로로 다 채운 블록을 사라지게 하는 처리 ( 블록 클리어 처리 )
            var dataSetAfterClearBlock = ClearFullBlock(dataSetAfterGravity);

            return dataSetAfterClearBlock;
        }


        GameDataSet ProcessUserInput(GameDataSet dataSet, InputHandler inputHandler)
        {
            var isKeyExist = inputHandler.isProcessed;
            if (isKeyExist == true)
            {
                // 처리할 인풋이 있다. 
                if (inputHandler.currentPressedKey == ConsoleKey.RightArrow)
                {
                    dataSet.currentFlyingBlock.width += 1; 
                }
                else  if (inputHandler.currentPressedKey == ConsoleKey.LeftArrow)
                {
                    dataSet.currentFlyingBlock.width -= 1; 
                }
                else  if (inputHandler.currentPressedKey == ConsoleKey.LeftArrow)
                {
                    //todo 블록을 돌린다. 
                }
            }
            else
            {
                // 처리할 인풋이 없다. 
                return dataSet;
            }
        }

        GameDataSet ProcessGravity(GameDataSet dataSet)
        {
            return null;
        }

        GameDataSet ClearFullBlock(GameDataSet dataSet)
        {
            return null;
        }
    }
}