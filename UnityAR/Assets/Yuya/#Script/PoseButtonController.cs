using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseButtonController : MonoBehaviour
{
    private bool isDisplay = false;
    [SerializeField]
    private GameObject PoseScrollView;

    // Use this for initialization
    void Start ()
    {

	}
	

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
