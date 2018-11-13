using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
