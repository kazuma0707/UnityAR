using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceRec_ButtonScript : MonoBehaviour
{
    private GameObject m_obj;
    [SerializeField]
    private WatsonConversation m_watson;

    private bool m_flag;

	// Use this for initialization
	void Start ()
    {
        m_obj = GameObject.FindWithTag("Player");
        m_watson = m_obj.GetComponent<WatsonConversation>();
        m_flag = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClick()
    {
        if(m_flag)
        m_watson.SetVoiceRecFlag(true);
        m_flag = false;
        StartCoroutine("Flag");
    }


    IEnumerator Flag()
    {
        yield return new WaitForSeconds(5.0f);
        m_flag = true;
    }
}
