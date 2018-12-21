using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ConstantName;
using UnityEngine.SceneManagement;

public class ButtonController : BaseButton {
    private string LoadSceneName = SceneName.CharCreate;
    private AsyncOperation async;
    [SerializeField]
    private SetText _setText;
    [SerializeField]
    private Animator ClassPanel;
    public Animator AllButtonAnim;
    const string BOLL_ANIM= "boolAnim";
    const string ONCE= "once";


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
    
    }
    private void Update()
    {
      
        
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
        else if(ButtonName.ARScene.ReturnSelectButton.Equals(objectName))
        {
            this.ReturnSelectButtonClick();
        }
        else if(ButtonName.ARScene.AllButton.Equals(objectName))
        {
            this.AllButtonClick();
        }

    }
    private void AllButtonClick()
    {
        if (!AllButtonAnim.GetBool(ONCE))
        {
            AllButtonAnim.SetBool(ONCE, true);
        }
        else
        {
            AllButtonAnim.SetBool(ONCE, false);
        }
        if(ClassPanel.GetBool(BOLL_ANIM))
        {
            ClassPanel.SetBool(BOLL_ANIM, false);
            ClassPanel.SetBool("OnceAnim", true);
        }
    }
    private void ReturnSelectButtonClick()
    {
        SceneManager.LoadScene(SceneName.CharCreate);
      //FindObjectOfType<SceneHolder>().LoadMainScene(SceneName.CharCreate);

    }
    private void GSButtonClick()
    {
       // if (!AugmentedImageExampleController.Index.Contains(1)) return;
        _setText.SetTextNumber = DepartmentName.GAME;
    }
    private void CGButtuonClick()
    {
        //if (!AugmentedImageExampleController.Index.Contains(2)) return;
        _setText.SetTextNumber = DepartmentName.CG;
    }
    private void WDButtonClick()
    {
        //if (!AugmentedImageExampleController.Index.Contains(3)) return;
        _setText.SetTextNumber = DepartmentName.WEB;
    }
    private void CADButtonClick()
    {
        //if (!AugmentedImageExampleController.Index.Contains(4)) return;
        _setText.SetTextNumber = DepartmentName.CAD;
    }
    private void KZButtonClick()
    {
       //if (!AugmentedImageExampleController.Index.Contains(5)) return;
        _setText.SetTextNumber = DepartmentName.ADVANCED_INFORMATION;
    }
    private void SSButtonClick()
    {
        //if (!AugmentedImageExampleController.Index.Contains(6)) return;
        _setText.SetTextNumber = DepartmentName.CYBER_SECURITY;
    }
    private void ZSButtonClick()
    {
        //if (!AugmentedImageExampleController.Index.Contains(7)) return;
        _setText.SetTextNumber = DepartmentName.INFORMATION_PROCESSING;
    }
    private void ClassButtonClick()
    {
        if(!ClassPanel.GetBool(BOLL_ANIM))
        {
            ClassPanel.SetBool(BOLL_ANIM, true); ClassPanel.SetBool("OnceAnim", false);
        }
        else
        {
            ClassPanel.SetBool(BOLL_ANIM, false);
            
        }
    }
    
}
