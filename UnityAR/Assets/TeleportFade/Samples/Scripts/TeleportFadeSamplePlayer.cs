using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GoogleARCore.Examples.HelloAR;
using Vuforia;
public class TeleportFadeSamplePlayer : MonoBehaviour {

    enum State {
        None,
        FadeOut,
        FadeIn,
    }
    public GameObject fadeObject;
    public MeshRenderer[] fadeMeshes;
    public SkinnedMeshRenderer[] fadeSkinnedMeshes;
    public ParticleSystem fadeOutParticle;
    public ParticleSystem fadeInParticle;

    List<Material> fadeMaterials = new List<Material>();
    float fadeTime;
    State state;
    [SerializeField]
    float fadeSpeed = 1.0f;
    [SerializeField]
    float risePower = 0.2f;
    [SerializeField]
    float twistPower = 3.0f;
    [SerializeField]
    float spreadPower = 0.6f;
    bool isCreate = false;
    [SerializeField]
    bool startFade = false;
    bool EndFade = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start () {


      
    }

    void Update () {
        if (GameObject.Find("skin(Clone)"))
        {
            fadeObject = GameObject.Find("skin(Clone)");
            isCreate = true;
        }
        else if(GameObject.Find("skin"))
        {
            fadeObject = GameObject.Find("skin");
            isCreate = true;
        }
        if (!isCreate) return;
            if (GameObject.Find("polySurface8"))
            {
            SkinnedMeshRenderer[] unitychanRendererList = fadeObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach(SkinnedMeshRenderer obj in unitychanRendererList)
            {
                switch(obj.name)
                {
                    case "pasted__pasted__pPipe1":
                        fadeSkinnedMeshes[0] = obj;
                        break;
                    case "polySurface1":
                        fadeSkinnedMeshes[1] = obj;
                        break;
                    case "polySurface8":
                        fadeSkinnedMeshes[2] = obj;
                        break;
                    case "polySurface10":
                        fadeSkinnedMeshes[3] = obj;
                        break;
                    case "skin_corrected:body_color_polySurface48":
                        fadeSkinnedMeshes[4] = obj;
                        break;
                    case "TEMPORARY_IMPORT_NAMESPACE___1:collar":
                        fadeSkinnedMeshes[5] = obj;
                        break;
                    case "transform13":
                        fadeSkinnedMeshes[6] = obj;
                        break;

                    default:
                        
                        break;
                }
            }

            foreach (var mesh in fadeMeshes)
            {
                foreach (var material in mesh.materials)
                {
                    fadeMaterials.Add(material);
                }
            }
            foreach (var mesh in fadeSkinnedMeshes)
            {
                foreach (var material in mesh.materials)
                {
                    fadeMaterials.Add(material);
                }
            }
            isCreate = false;
        }
        fadeTime += Time.deltaTime;
        float fadeDuration = 2.0f / fadeSpeed;
        float fadeStartDelay = 0.9f / fadeSpeed;
        float fadeRate = 0.0f;
        switch (state) {
        case State.FadeOut:
            fadeRate = Mathf.Clamp((fadeTime - fadeStartDelay) / fadeDuration, 0.0f, 1.0f);
            break;
        case State.FadeIn:
            fadeRate = 1.0f - Mathf.Clamp((fadeTime - fadeStartDelay) / fadeDuration, 0.0f, 1.0f);
            break;
        }
        var basePos = new Vector4();
        basePos.x = fadeObject.transform.position.x;
        basePos.y = fadeObject.transform.position.y;
        basePos.z = fadeObject.transform.position.z;
        //SetObjectHeight(basePos.y);
        foreach (var material in fadeMaterials) {
            material.SetVector("_ObjectBasePos", basePos);
            material.SetFloat("_FadeRate", fadeRate);
            material.SetFloat("_RisePower", risePower);
            material.SetFloat("_TwistPower", twistPower);
            material.SetFloat("_SpreadPower", spreadPower);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartFadeIn();
        }
    }
    public void SetObjectHeight(float Height)
    {
        foreach (var material in fadeMaterials)
        {
            material.SetFloat("_ObjectHeight", Height+2.0f);
        }
    }

    public void StartFadeOut() {
         
        fadeTime = 0.0f;
        state = State.FadeOut;
        if (fadeOutParticle != null) {
            var main = fadeOutParticle.main;
            main.simulationSpeed = fadeSpeed;
            foreach (var childParticle in fadeOutParticle.GetComponentsInChildren<ParticleSystem>()) {
                var mainChild = childParticle.main;
                mainChild.simulationSpeed = fadeSpeed;
            }
            Vector3 basePos = new Vector3();
            basePos.x = fadeObject.transform.position.x;
            basePos.y = fadeObject.transform.position.y;
            basePos.z = fadeObject.transform.position.z;
            fadeInParticle.transform.position = new Vector3(basePos.x, basePos.y+1.726f, basePos.z);
            fadeOutParticle.Play(true);
       
        }

    }

    public void StartFadeIn() {
        
        fadeTime = 0.0f;
        state = State.FadeIn;
        if (fadeInParticle != null) {
            var main = fadeInParticle.main;
            main.simulationSpeed = fadeSpeed;
            foreach (var childParticle in fadeInParticle.GetComponentsInChildren<ParticleSystem>()) {
                var mainChild = childParticle.main;
                mainChild.simulationSpeed = fadeSpeed;
            }
            Vector3 basePos = new Vector3();
            basePos.x = fadeObject.transform.position.x;
            basePos.y = fadeObject.transform.position.y;
            basePos.z = fadeObject.transform.position.z;
            fadeInParticle.transform.position = new Vector3(basePos.x, basePos.y , basePos.z);
            fadeInParticle.Play(true);
            //obj.SetActive(true);
        }
    }

    public void FadeSpeedChange(float value) {
        fadeSpeed = value;
    }

    public void RisePowerChange(float value) {
        risePower = value;
    }

    public void TwistPowerChange(float value) {
        twistPower = value;
    }

    public void SpreadPowerChange(float value) {
        spreadPower = value;
    }
}
