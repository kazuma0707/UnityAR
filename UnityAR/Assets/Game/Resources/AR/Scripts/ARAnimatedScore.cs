using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARAnimatedScore : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //  コルーチンを開始する
        StartCoroutine(ScoreAnimation(0.0f, ARGameManager.gameScore, 2.0f));
    }

    // スコアをアニメーションさせる
    private IEnumerator ScoreAnimation(float startScore, float endScore, float duration)
    {
        // 開始時間
        float startTime = Time.time;

        // 終了時間
        float endTime = startTime + duration;

        do
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            // 数値を更新
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);

            // テキストの更新
            this.GetComponent<Text>().text = "Your Score : ";
            this.GetComponent<Text>().text += updateValue.ToString("f0"); // （"f0" の "0" は、小数点以下の桁数指定）

            // 1フレーム待つ
            yield return null;

        } while (Time.time < endTime);

        // 最終的な着地のスコア
        this.GetComponent<Text>().text = "Your Score : ";
        this.GetComponent<Text>().text += endScore.ToString();
    }
}
