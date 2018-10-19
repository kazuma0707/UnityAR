using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebImage : MonoBehaviour {

    IEnumerator Start()
    {

        // wwwクラスのコンストラクタに画像URLを指定
        string url = "https://drive.google.com/open?id=0B21PcPBwgJombE4yZFRxREl6VFk";
        WWW www = new WWW(url);

        // 画像ダウンロード完了を待機
        yield return www;

        // webサーバから取得した画像をRaw Imagで表示する
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = www.textureNonReadable;

        //ピクセルサイズ等倍に
        rawImage.SetNativeSize();
    }
    
}
