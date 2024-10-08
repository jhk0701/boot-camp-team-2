﻿namespace project_TextRPG
{
    class GameScene : IScene
    {
        public string SceneName { get; set; }
        public Character Player { get; set; }

        Feature[] features;

        public GameScene(string name)
        {
            SceneName = name;

            features = [
                new FeatureStatus("상태보기", this),
                new FeatureInventory("인벤토리", this),
                new FeatureStore("사내 편의점", this),
                new FeatureBattle("전투 시작", this),
                new FeatureRest("휴식하기", this),
                new FeatureQuest("사내 게시판", this),
                //new FeatureGatcha("가챠 뽑기", this)
            ];
        }

        /// <summary>
        /// 씬 시작
        /// </summary>
        /// <param name="visitor"></param>
        public void Start(Character visitor) 
        {
            Player = visitor;

            foreach (Feature feature in features)
                feature.Initialize(Player);

            ShowMenu();
        }

        /// <summary>
        /// 씬 종료
        /// </summary>
        /// <returns></returns>
        public Character End()
        {
            return Player;
        }

        /// <summary>
        /// 이 씬의 메뉴 출력
        /// </summary>
        void ShowMenu()
        {
            Console.Clear();
            Utility.ShowScript(
                $"{SceneName}에 오신 여러분 환영합니다.\n",
                "이제 전투를 시작할 수 있습니다.\n"
            );

            // 옵션 출력 : 임시로 추가
            for (int i = 0; i < features.Length; i++)
                Utility.ShowScript($"{i + 1}. {features[i].Name}");

            Console.WriteLine();
            int select = Utility.GetSelection(1, features.Length);

            // 자동 저장
            DataIO.GetInstance().Save(Player, QuestManager.GetInstance());

            Select(select);
        }

        /// <summary>
        /// 플레이어가 선택한 행동을 실행하는 함수
        /// </summary>
        /// <param name="select"></param>
        void Select(int select)
        {
            // 여기부터 플레이어가 선택한 기능 실행
            features[select - 1].Start();
        }

        /// <summary>
        /// 씬으로 되돌아 가는 기능
        /// </summary>
        public void Return()
        {
            ShowMenu();
        }

    }
}
