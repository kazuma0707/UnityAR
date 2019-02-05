using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstantName;

public class SetHairsFreeColor : MonoBehaviour
{
    private MyCharDataManager manager;             // マイキャラのデータマネージャー
    private SkinnedMeshRenderer renderer;          // レンダラー

	// Use this for initialization
	void Start ()
    {
        Transform[] meshBones = this.GetComponent<SkinnedMeshRenderer>().bones;
        foreach(Transform bone in meshBones)
        {
            Debug.Log(bone.name);
            
        }

        // マイキャラのデータマネージャーとレンダラーのコンポーネントを取得
        //FindComponent();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //// キャラクリシーンでなければ何もしない
        //if (SceneManager.GetActiveScene().name != SceneName.CharCreate) return;

        //// マイキャラのデータマネージャーとレンダラーのコンポーネントを取得
        //FindComponent();

        // 色を変える
        //Color color = MyCharDataManager.Instance.Data.hairColor2;
        //renderer.material.SetColor(MyCharDataManager.BASE_COLOR, color);
        //renderer.material.SetColor(MyCharDataManager.SECOND_SHADE_COLOR, color);
    }

    // マイキャラのデータマネージャーとレンダラーのコンポーネントを取得
    private void FindComponent()
    {
        // マネージャーがNullであれば再設定
        if (!manager) manager = GameObject.Find("MyCharDataManager").GetComponent<MyCharDataManager>();

        // レンダラーがNullであれば再設定
        if (!renderer) renderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
    }
}
