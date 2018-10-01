using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;
using ConstantName;

public class ButtonController : BaseButton {
    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private SetText _setText;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void OnClick(string objectName)
    {
        // 渡されたオブジェクト名で処理を分岐
        if (ButtonName.ARScene.GSButton.Equals(objectName))
        {
            // Button1がクリックされたとき
            this.GSButtonClick();
        }
        else if (ButtonName.ARScene.CGButtuon.Equals(objectName))
        {
            // Button2がクリックされたとき
            this.CGButtuonClick();
        }
        else if (ButtonName.ARScene.WDButton.Equals(objectName))
        {
            // Button2がクリックされたとき
            this.WDButtonClick();
        }
        else if (ButtonName.ARScene.CADButton.Equals(objectName))
        {
            // Button2がクリックされたとき
            this.CADButtonClick();
        }
        else if (ButtonName.ARScene.KZButton.Equals(objectName))
        {
            // Button2がクリックされたとき
            this.KZButtonClick();
        }
        else if ("SSButton".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.SSButtonClick();
        }
        else if (ButtonName.ARScene.ZSButton.Equals(objectName))
        {
            // Button2がクリックされたとき
            this.ZSButtonClick();
        }
        else if (ButtonName.ARScene.ClassButton.Equals(objectName))
        {
            // Button2がクリックされたとき
            this.ClassButtonClick();
        }
    }

    private void GSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(1)) return;
        _setText.SetTextNumber = DepartmentName.GAME;
    }
    private void CGButtuonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(2)) return;
        _setText.SetTextNumber = DepartmentName.CG;
    }
    private void WDButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(3)) return;
        _setText.SetTextNumber = DepartmentName.WEB;
    }
    private void CADButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(4)) return;
        _setText.SetTextNumber = DepartmentName.CAD;
    }
    private void KZButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(5)) return;
        _setText.SetTextNumber = DepartmentName.ADVANCED_INFORMATION;
    }
    private void SSButtonClick()
    {
       // if (!AugmentedImageExampleController.Index.Contains(6)) return;
        _setText.SetTextNumber = DepartmentName.CYBER_SECURITY;
        Debug.Log("SS");
    }
    private void ZSButtonClick()
    {
        if (!AugmentedImageExampleController.Index.Contains(7)) return;
        _setText.SetTextNumber = DepartmentName.INFORMATION_PROCESSING;
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
