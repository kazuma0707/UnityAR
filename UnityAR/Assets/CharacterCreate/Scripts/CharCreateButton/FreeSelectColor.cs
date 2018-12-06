using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstantName;

public class FreeSelectColor : MonoBehaviour
{  
    [SerializeField]
    private ColorPicker picker;      // カラーピッカー
    [SerializeField]
    private Renderer renderer;    // 色を変えたいオブジェクトのレンダラー

    public Color Color = Color.red;

    // Use this for initialization
    void Start()
    {
        // キャラクリシーンでなければ何もしない
        //if (SceneManager.GetActiveScene().name != SceneName.CharCreate) return;

        // 特定のコンポーネントを取得できれば
        if (FindComponent())
        {
            ColorChange();

            renderer.material.SetColor(MyCharDataManager.BASE_COLOR, picker.CurrentColor);
            renderer.material.SetColor(MyCharDataManager.SECOND_SHADE_COLOR, picker.CurrentColor);

            picker.CurrentColor = Color;
        }        
    }

    // Update is called once per frame
    void Update ()
    {
        // ピッカーの更新
        //if(picker) picker.CurrentColor = MyCharDataManager.Instance.Data.hairColor2;
    }

    // 色を変える
    public void ColorChange()
    {        
        picker.onValueChanged.AddListener(color =>
        {
            renderer.material.SetColor(MyCharDataManager.BASE_COLOR, color);
            renderer.material.SetColor(MyCharDataManager.SECOND_SHADE_COLOR, color);
            Color = color;

            // データマネージャーに設定
            MyCharDataManager.Instance.Data.hairColor2 = Color;
        });
    }

    // 特定のコンポーネントを探す・取得する
    private bool FindComponent()
    {
        // ピッカーを探す
        if (!picker)
        {
            picker = GameObject.Find("Picker 2.0").GetComponent<ColorPicker>();
            return false;
        }

        // レンダラーを取得する
        if (!renderer)
        {
            renderer = this.gameObject.GetComponent<Renderer>();
            return false;
        }

        return true;
    }

    public ColorPicker Picker
    {
        get { return picker; }
    }

}
