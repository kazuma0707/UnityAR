using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSelectColor : MonoBehaviour
{
    // 定数宣言 //////////////////////////////////////////////////////////////////////////////////

    private const string BASE_COLOR = "_BaseColor";                   // _BaseColor
    private const string FIST_SHADE_COLOR = "_1st_ShadeColor";        // _1st_ShadeColor

    //////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField]
    private ColorPicker picker;      // カラーピッカー
    [SerializeField]
    private Renderer renderer;    // 色を変えたいオブジェクトのレンダラー

    public Color Color = Color.red;

    // Use this for initialization
    void Start()
    {
        picker.onValueChanged.AddListener(color =>
        {
            renderer.material.SetColor(BASE_COLOR, color);
            renderer.material.SetColor(FIST_SHADE_COLOR, color);
            Color = color;
        });

        renderer.material.SetColor(BASE_COLOR, picker.CurrentColor);
        renderer.material.SetColor(FIST_SHADE_COLOR, picker.CurrentColor);

        picker.CurrentColor = Color;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!renderer)
        {
            renderer = GameObject.Find("polySurface10").GetComponent<Renderer>();
        }               
    }
}
