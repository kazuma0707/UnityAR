using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMesh : MonoBehaviour
{
    private SkinnedMeshRenderer rend;

    [SerializeField]
    private Mesh mesh;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        Debug.Log(rend.sharedMesh.name);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Z))
        {
            rend.sharedMesh = mesh;
            Debug.Log(rend.sharedMesh.name);
        }
	}
}
