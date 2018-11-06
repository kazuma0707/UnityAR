using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChangeButton : MonoBehaviour
{
    [SerializeField]
    private GameObject resourceObj;            // 差し替えるモデル

    [SerializeField]
    private Material[] resourceMatsNormal;          // 対応するマテリアル・ノーマルVer. (0：BODY_COLOR, 1：HEAD_COLOR)
    [SerializeField]
    private Material[] resourceMatsBrown;           // 対応するマテリアル・褐色Ver. (0：BODY_COLOR, 1：HEAD_COLOR)
    [SerializeField]
    private Material[] resourceMatsBihaku;          // 対応するマテリアル・美白Ver.(0：BODY_COLOR, 1：HEAD_COLOR)

    [SerializeField]
    private ClothNum cn;                       // 服の番号

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 体の色に合わせて服(＋それに合わせた体の色)を変える
        switch (MyCharDataManager.Instance.Data.bcn)
        {
            case BodyColorNum.NORMAL:
            default:
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, resourceMatsNormal);                              
                break;
            case BodyColorNum.BROWN:
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, resourceMatsBrown);
                break;
            case BodyColorNum.BIHAKU:
                // 服を変える
                MyCharDataManager.Instance.ChangeClothObj(resourceObj, resourceMatsBihaku);
                break;
        }

        // 現在選択中の服を登録
        MyCharDataManager.Instance.Data.clothNum = cn;
    }
}
