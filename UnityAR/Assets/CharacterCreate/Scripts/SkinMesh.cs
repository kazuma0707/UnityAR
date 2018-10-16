using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMesh : MonoBehaviour
{
    private SkinnedMeshRenderer skin;
    
    [SerializeField]
    private Mesh mesh;

	// Use this for initialization
	void Start ()
    {
        skin = GetComponent<SkinnedMeshRenderer>();
        Debug.Log(skin.sharedMesh.name);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Z))
        {
            skin.sharedMesh = mesh;
            Debug.Log(skin.sharedMesh.name);
        }
	}
}
