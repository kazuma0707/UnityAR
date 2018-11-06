using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstantName;

public class SceneHolder : MonoBehaviour {
    Scene LodedScene;

	// Use this for initialization
	void Start () {
        LoadMainScene(SceneName.ARScene);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadMainScene(string scene)
    {
      SceneManager.UnloadSceneAsync(LodedScene.name);
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
        LodedScene = SceneManager.GetSceneByName(scene.ToString());
    }
}
