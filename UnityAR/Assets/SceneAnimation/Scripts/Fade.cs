using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // 定数宣言
    private const float SPEED = 0.02f;      // アルファ値スピード
    public const float ALFA_MAX = 1.0f;   // アルファ値の最大値
    public const float ALFA_MIN = 0.0f;     // アルファ値の最小値
    public const bool FADE_IN = true;       // フェードイン
    public const bool FADE_OUT = false;     // フェードアウト

    private float alfa = 0.0f;                  // アルファ値
    private float red, green, blue;             // RGB
    private bool isFade;                        // フェードの状態

    // Use this for initialization
    void Start ()
    {
        // RGBを設定
        red = this.GetComponent<Image>().color.r;
        green = this.GetComponent<Image>().color.g;
        blue = this.GetComponent<Image>().color.b;

        //サイズを変更
        //this.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        isFade = FADE_IN;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // RGBAを設定
        this.GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }

    // フェードイン
    public void FadeIn()
    {
        if (alfa <= ALFA_MIN)
        {
            isFade = FADE_IN;
            return;
        }
        alfa -= SPEED;
    }

    // フェードアウト
    public void FadeOut()
    {
        if (alfa >= ALFA_MAX)
        {
            isFade = FADE_OUT;
            return;
        }
        alfa += SPEED;
    }
    
    // フェード状態のアクセッサ
    public bool IsFade
    {
        get { return isFade; }
        set { isFade = value; }
    }
}
