using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using EnumName;

public class ButtonController : BaseButton {
    [SerializeField]
    private  GameObject Panel;
    public SetText _SetText;
   public bool isClassButton { get; set; }//学科ボタンが押されたか？
    //ボタンのOnClicikを管理する
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
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.GAME)) return;
        _SetText.SetTextNumber = DepartmentName.GAME;

        Debug.Log("GSButton Click");
    }
    private void CGButtuonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.CG)) return;
        _SetText.SetTextNumber = DepartmentName.CG;

        Debug.Log("CGButtuon Click");
    }
    private void WDButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.WEB)) return;
        _SetText.SetTextNumber = DepartmentName.WEB;

        Debug.Log("WDButton Click");
    }
    private void CADButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.CAD)) return;
        _SetText.SetTextNumber = DepartmentName.CAD;
        Debug.Log("CADButton Click");
    }
    private void SSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.CYBER_SECURITY)) return;
        _SetText.SetTextNumber = DepartmentName.CYBER_SECURITY;

        Debug.Log("SSButton Click");
    }
    private void KZButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.ADVANCED_INFORMATION)) return;
        _SetText.SetTextNumber = DepartmentName.ADVANCED_INFORMATION;

        Debug.Log("KZButton Click");
    }
    private void ZSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(DepartmentName.INFORMATION_PROCESSING)) return;
        _SetText.SetTextNumber = DepartmentName.INFORMATION_PROCESSING;

        Debug.Log("ZSButton Click");
    }

    //学科ボタン
    private void ClassButtonClick()
    {
        if(!this.Panel.activeSelf)
        {
            this.Panel.SetActive(true);
            this.isClassButton = true;
        }
        else
        {
            this.Panel.SetActive(false);
            this.isClassButton = false;

        }
        Debug.Log("ClassButton Click");
    }
}
