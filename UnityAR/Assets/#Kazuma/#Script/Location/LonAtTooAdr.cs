using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;


public class LonAtTooAdr : MonoBehaviour {

	 /// <summary>APIのパラメータテンプレートつきURL</summary>
    private const string ApiBaseUrl = "https://www.finds.jp/ws/rgeocode.php?json&lon={0}&lat={1}";

    /// <summary>住所文字列</summary>
    public string Address { get; private set; }

    /// <summary>経緯度から住所文字列を取得</summary>
    /// <param name="longitude">経度</param>
    /// <param name="latitude">緯度</param>
    /// <returns>遅延評価用戻り値</returns>
    public IEnumerator GetAddrFromLonLat(float longitude, float latitude)
    {
        // URLに経緯度パラメータを埋め込み
        string url = string.Format(ApiBaseUrl, longitude, latitude);

        // APIを実行して経緯度を保持
        using (WWW www = new WWW(url))
        {
            // API非同期実行用yield return
            yield return www;

            // 結果JSONのデシリアライズ
            var desirializedData = (Dictionary<string, object>)Json.Deserialize(www.text);

            // 成功した場合のみ処理
            if ((long)desirializedData["status"] == 200)
            {
                // 都道府県+市区町村を文字列として保持
                var result = (Dictionary<string, object>)desirializedData["result"];
                var prefecture = (Dictionary<string, object>)result["prefecture"];
                var municipality = (Dictionary<string, object>)result["municipality"];
                Address = (string)prefecture["pname"] + " " + (string)municipality["mname"];
            }
        }
    }
}
