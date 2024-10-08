﻿using System.Text.Json;
using System.Linq;

namespace project_TextRPG
{
    public class Program
    {
        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            //DataIO.GetInstance().Load();
            //Character t = DataIO.GetInstance().GetLoadedData().Player;
            //DataIO.GetInstance().DebugData();
            //return;

            Character player = null;
            IScene startScene = new StartScene(COMMON_NAME/*, true*/);
            startScene.Start(player);
            player = startScene.End();

            player.Start();

            // 게임 시작
            IScene gameScene = new GameScene(COMMON_NAME);
            gameScene.Start(player);
        }

        
    }
}
