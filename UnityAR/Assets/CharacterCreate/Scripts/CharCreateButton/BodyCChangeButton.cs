using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCChangeButton : MonoBehaviour
{
    [SerializeField]
    private Material[] resourceMatsNormal;          // 対応するマテリアル・ノーマルVer. (0：BODY_COLOR, 1：HEAD_COLOR)
    [SerializeField]
    private Material[] resourceMatsSeihuku;         // 対応するマテリアル・制服Ver. (0：BODY_COLOR, 1：HEAD_COLOR)
    [SerializeField]
    private Material[] resourceMatsNanka;           // 対応するマテリアル・なんかVer.(0：BODY_COLOR, 1：HEAD_COLOR)

    [SerializeField]
    private BodyColorNum bcn;           // 体の色の番号

    //----------------------------------------------------------------------------------------------
    // 関数の内容 | クリックされたときの処理
    // 　引　数   | なし
    //  戻 り 値  | なし
    //----------------------------------------------------------------------------------------------
    public void OnClick()
    {
        // 服に合わせて体の色を変える
        switch (MyCharDataManager.Instance.Data.clothNum)
        {
            case ClothNum.NORMAL:
            default:
                MyCharDataManager.Instance.ChangeBodyColor(resourceMatsNormal);            
                break;
            case ClothNum.SEIHUKU:
                MyCharDataManager.Instance.ChangeBodyColor(resourceMatsSeihuku);
                break;
            case ClothNum.NANKA:
                MyCharDataManager.Instance.ChangeBodyColor(resourceMatsNanka);
                break;
        }

        // 体の色の番号を登録
        MyCharDataManager.Instance.Data.bcn = bcn;
    }    
}
