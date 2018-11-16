using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NowLoading : MonoBehaviour
{

    private AsyncOperation async;
    public GameObject LoadingUi;
    public Slider Slider;
    [SerializeField]
    Text nowLoading;

    public void LoadNextScene()
    {
        LoadingUi.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("Play");

        while (true)
        {
            nowLoading.text = "NowLoading";
            yield return new WaitForSeconds(0.5f);
            nowLoading.text = "NowLoading.";
            yield return new WaitForSeconds(0.5f);
            nowLoading.text = "NowLoading..";
            yield return new WaitForSeconds(0.5f);
            nowLoading.text = "NowLoading...";
            yield return new WaitForSeconds(0.5f);
        }

        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }
}