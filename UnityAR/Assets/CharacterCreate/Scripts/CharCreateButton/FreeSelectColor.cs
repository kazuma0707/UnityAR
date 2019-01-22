using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FreeSelectColor : MonoBehaviour
{  
    [SerializeField]
    private ColorPicker picker;      // カラーピッカー

    public Color Color = Color.white;

    // Use this for initialization
    void Start()
    {
        if (!picker)
            picker = this.gameObject.GetComponent<ColorPicker>();

        ColorChange();

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
       
    }

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | 色を変える
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void ColorChange()
    {        
        picker.onValueChanged.AddListener(color =>
        {
            //renderer.material.SetColor(MyCharDataManager.BASE_COLOR, color);
            //renderer.material.SetColor(MyCharDataManager.SECOND_SHADE_COLOR, color);
            Color = color;

            // データマネージャーに設定
           // MyCharDataManager.Instance.Data.hairColor2 = Color;
        });

        picker.CurrentColor = Color;
    }   

    public ColorPicker Picker
    {
        get { return picker; }
    }

}
