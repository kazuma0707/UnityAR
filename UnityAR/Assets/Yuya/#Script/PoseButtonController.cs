using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseButtonController : MonoBehaviour
{
    private bool isDisplay = false;
    [SerializeField]
    private GameObject PoseScrollView;


    //----------------------------------------------------------------------
    //! @brief クリック処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Onclick()
    {
        if(!isDisplay)
        {
            PoseScrollView.SetActive(true);
            isDisplay = true;
        }
        else
        {
            PoseScrollView.SetActive(false);
            isDisplay = false;
        }
    }
}
