using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppreciationManager : MonoBehaviour
{

    [SerializeField]
    private GameObject myChar;

    // Use this for initialization
    void Start ()
    {
        MyCharDataManager.Instance.ReCreate(myChar);
        myChar.AddComponent<AudioSource>();
        myChar.AddComponent<WatsonConversation>();
    }
}
