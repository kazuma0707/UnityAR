using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;

public class ButtonController : BaseButton {
    [SerializeField]
    private  GameObject Panel;

    protected override void OnClick(string objectName)
    {
        // 渡されたオブジェクト名で処理を分岐
        if ("GSButton".Equals(objectName))
        {
            // Button1がクリックされたとき
            this.GSButtonClick();
        }
        else if ("CGButtuon".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.CGButtuonClick();
        }
        else if ("WDButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.WDButtonClick();
        }
        else if ("CADButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.CADButtonClick();
        }
        else if ("KZButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.KZButtonClick();
        }
        else if ("SSButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.SSButtonClick();
        }
        else if ("ZSButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.ZSButtonClick();
        }
        else if ("ClassButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.ClassButtonClick();
        }
    }

    private void GSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(1)) return;
        Debug.Log("GSButton Click");
    }
    private void CGButtuonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(2)) return;
        Debug.Log("CGButtuon Click");
    }
    private void WDButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(3)) return;
        Debug.Log("WDButton Click");
    }
    private void CADButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(4)) return;
        Debug.Log("CADButton Click");
    }
    private void KZButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(5)) return;
        Debug.Log("KZButton Click");
    }
    private void SSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(6)) return;
        Debug.Log("SSButton Click");
    }
    private void ZSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(7)) return;
        Debug.Log("ZSButton Click");
    }
    private void ClassButtonClick()
    {
        if(!this.Panel.activeSelf)
        {
            this.Panel.SetActive(true);
        }
        else
        {
            this.Panel.SetActive(false);
        }
        Debug.Log("ClassButton Click");
    }
}
