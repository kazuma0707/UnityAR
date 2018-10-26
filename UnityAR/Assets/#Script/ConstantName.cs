using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//列挙型を管理する
 namespace ConstantName
{
    //学科の定数
    public class DepartmentName
    {
        public const int GAME = 1;//ゲームサイエンス学科
        public const int CG = 2;//CGスペシャリスト学科
        public const int WEB = 3;//Webデザイン学科
        public const int CAD = 4;//CAD学科
        public const int CYBER_SECURITY = 5;//サイバーセキュリティ学科
        public const int ADVANCED_INFORMATION = 6;//高度情報学科
        public const int INFORMATION_PROCESSING = 7;//情報処理学科
    }
    //シーンの定数
    public class SceneName
    {
        public const string Game = "Game";
        public const string CharCreate = "CharCreate";
        public const string ARScene = "ARScene";
        public const string Appreciation = "Appreciation";
    }
    //ボタンの定数
    public class ButtonName
    {
        
        public class ARScene
        {
            public const string GSButton = "GSButton";
            public const string CGButtuon = "CGButtuon";
            public const string WDButton = "WDButton";
            public const string CADButton = "CADButton";
            public const string KZButton = "KZButton";
            public const string SSButton = "SSButton";
            public const string ZSButton = "ZSButton";
            public const string ClassButton = "ClassButton";
        }
    }
}
