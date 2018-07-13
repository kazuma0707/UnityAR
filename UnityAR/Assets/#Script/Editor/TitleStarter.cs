using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class TitleStarter : EditorWindow {
    //設定したシーンのパスを保存するKEY
    private const string SAVE_KEY = "StartScenePathKey";

    // =================================================================================
    //初期化
    // =================================================================================

    // メニューからウィンドウを表示
    [MenuItem("Window/SettingStartSceneWindow")]
    public static void Open()
    {
        TitleStarter.GetWindow<TitleStarter>(typeof(TitleStarter));
    }

    // 初期化(ウィンドウを開いた時等に実行)
    private void OnEnable()
    {
        // 保存されている最初のシーンのパスがあれば、読み込んで設定
        string startScenePath = EditorUserSettings.GetConfigValue(SAVE_KEY);
        if (!string.IsNullOrEmpty(startScenePath))
        {

            //パスからシーンを取得、シーンがなければ警告表示
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScenePath);
            if (sceneAsset == null)
            {
                Debug.LogWarning(startScenePath + "がありません！");
            }
            else
            {
                EditorSceneManager.playModeStartScene = sceneAsset;
            }

        }
    }

    //=================================================================================
    //表示するGUIの設定
    //=================================================================================

    private void OnGUI()
    {

        //更新前のplayModeStartSceneに設定されてるシーンのパスを取得
        string beforeScenePath = "";//再生後に戻るシーン
        if (EditorSceneManager.playModeStartScene != null)
        {
            beforeScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
        }

        //GUIでシーンファイルを取得し、playModeStartSceneに設定する
        EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"), 
            EditorSceneManager.playModeStartScene,
            typeof(SceneAsset),
            false);

        //更新後のplayModeStartSceneに設定されてるシーンのパスを取得
        string afterScenePath = "";
        if (EditorSceneManager.playModeStartScene != null)
        {
            afterScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
        }

        //playModeStartSceneが変更されたらパスを保存
        if (beforeScenePath != afterScenePath)
        {
            EditorUserSettings.SetConfigValue(SAVE_KEY, afterScenePath);
        }
    }


}
