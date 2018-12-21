using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleLocation2 : MonoBehaviour {

    /// <summary>テキストテンプレート</summary>
    private const string LonLatInfoTemplate = "緯度: {0}\n経度: {1}\n高度:{2}\n住所:{3}";

    /// <summary>表示用テキストUIオブジェクト</summary>
    private Text lonLatInfo;
    /// <summary>経緯度取得オブジェクト</summary>
    private Location2 lonLatGetter;

    /// <summary>逆ジオコーディングオブジェクト</summary>
    private LonAtTooAdr lonLatToAddr;



    /// <summary>初期化</summary>
    private void Start()
    {
        // テキストラベルオブジェクトを保持
        lonLatInfo = GameObject.Find("Text").GetComponent<Text>();


        // 経緯度取得オブジェクトオブジェクトを保持
        lonLatGetter = GetComponent<Location2>();

        // 逆ジオコーディングオブジェクトを取得
        lonLatToAddr = GetComponent<LonAtTooAdr>();
    }

    /// <summary>経緯度の値をテキストUIに反映</summary>
    private void Update()
    {
        // 経緯度の値を取得できるか判定
        if (lonLatGetter.CanGetLonLat())
        {
            StartCoroutine(lonLatToAddr.GetAddrFromLonLat(lonLatGetter.Longitude, lonLatGetter.Latitude));
            lonLatInfo.text = string.Format(LonLatInfoTemplate, 
                lonLatGetter.Latitude.ToString(), 
                lonLatGetter.Longitude.ToString(),
                lonLatGetter.Altitude.ToString(),
                lonLatToAddr.Address);
        }
        else
        {
            lonLatInfo.text = string.Format(LonLatInfoTemplate, "測定不能", "測定不能", "測定不能","測定不能");
        }
    }
}
