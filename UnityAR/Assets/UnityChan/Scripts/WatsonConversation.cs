using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Services.Conversation.v1;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Utilities;

public class WatsonConversation : MonoBehaviour
{
    //[SerializeField]
    //private Text m_text;

    [SerializeField]
    private bool m_voiceRecFlag;


    [SerializeField]
    private SpeechToText m_SpeechToText;
    private TextToSpeech m_TextToSpeech;
    private Conversation m_Conversation;
    private string m_WorkspaceID = "7888e1fc-7642-4a1f-a13e-06761d72472f"; //各自変更してください

    [SerializeField]
    private Animator animator; //ここを追加
    private const string key_isGreet = "isGreet"; //ここを追加

    IEnumerator Start()
    {
        m_voiceRecFlag = false;

        string tts_id = "be09fdb0-d674-480d-a6bb-19e50445da09";                  // 資格情報より
        string tts_pw = "papBdt1cRbfp";                                          // 資格情報より
        string tts_url = "https://stream.watsonplatform.net/text-to-speech/api"; // 資格情報より

        string stt_id = "c27fe422-75e3-406d-8986-ec89a72d0216";                  // 資格情報より
        string stt_pw = "jIUJHIEgLlbg";                                          // 資格情報より
        string stt_url = "https://stream.watsonplatform.net/speech-to-text/api"; // 資格情報より

        string conv_id = "b56e6668-ee24-4ab4-ab22-6e9b25e7821e";                  // 資格情報より
        string conv_pw = "22RGYFuF41ec";                                          // 資格情報より
        string conv_url = "https://gateway.watsonplatform.net/assistant/api";  // 資格情報より

        Credentials tts_credentials = new Credentials(tts_id, tts_pw, tts_url);
        m_TextToSpeech = new TextToSpeech(tts_credentials);
        m_TextToSpeech.Voice = VoiceType.ja_JP_Emi;


        Credentials stt_credentials = new Credentials(stt_id, stt_pw, stt_url);
        m_SpeechToText = new SpeechToText(stt_credentials);
        m_SpeechToText.Keywords = new string[] { "ibm" };
        m_SpeechToText.KeywordsThreshold = 0.1f;

        Credentials conv_credentials = new Credentials(conv_id, conv_pw, conv_url);
        m_Conversation = new Conversation(conv_credentials);


        this.animator = GameObject.Find("skin").GetComponent<Animator>(); //ここを追加
        AudioSource audioSource = GetComponent<AudioSource>();
        while (true)
        {
            yield return RecMic(audioSource);
        }
        yield return null;
    }


    IEnumerator RecMic(AudioSource audioSource)
    {
        if (m_voiceRecFlag)
        {
            Debug.Log("Start record");
            audioSource.clip = Microphone.Start(null, true, 10, 44100);
            audioSource.loop = false;
            audioSource.spatialBlend = 0.0f;
            yield return new WaitForSeconds(2.0f);
            Microphone.End(null);
            Debug.Log("Finish record");

            // SpeechToText を日本語指定して、録音音声をテキストに変換
            m_SpeechToText.RecognizeModel = "ja-JP_BroadbandModel";
            m_SpeechToText.Recognize(HandleOnRecognize, OnFail, audioSource.clip);

            m_voiceRecFlag = false;
        }
    }


    //void OnMessage(MessageResponse resp, string customData)
    void OnMessage(object resp, Dictionary<string, object> customData)
    {
        if (resp is Dictionary<string, object>)
        {
            //this.animator.SetBool(key_isGreet, true);

            Dictionary<string, object> dic_resp = (Dictionary<string, object>)resp;

            foreach (object o in (List<object>)dic_resp["intents"])
            {
                Dictionary<string, object> dic_intent = (Dictionary<string, object>)o;
                Debug.Log("intent: " + dic_intent["intent"] + ", confidence: " + dic_intent["confidence"]);
            }

            Dictionary<string, object> dic_output = (Dictionary<string, object>)dic_resp["output"];
            string res = "";
            foreach (object o in (List<object>)dic_output["text"])
            {
                res += o.ToString();
            }

          
            m_TextToSpeech.Voice = VoiceType.ja_JP_Emi;
            m_TextToSpeech.ToSpeech(HandleToSpeechCallback, OnFail, res);

        Debug.Log("response: " + res);
        }

    }

    //void HandleToSpeechCallback(AudioClip clip)
    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData)
    {
        PlayClip(clip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            this.animator.SetBool(key_isGreet, false);
            GameObject.Destroy(audioObject, clip.length);
        }
    }

    void HandleOnRecognize(SpeechRecognitionEvent result, Dictionary<string, object> customData)
    {
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    string text = alt.transcript;
                    Debug.Log(string.Format("{0} ({1}, {2:0.00})\n", text, res.final ? "Final" : "Interim", alt.confidence));

                    //text を Conversation Service に送って処理
                    m_Conversation.VersionDate = "2017-05-26";
                    m_Conversation.Message(OnMessage, OnFail ,m_WorkspaceID, text);
                    //m_text.text = text;
                }
            }
        }
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Debug.Log("SampleSpeechToText.OnFail() Error received: " + error.ToString());
    }

    void Update()
    {
        
    }


    public void SetVoiceRecFlag(bool flag)
    {
        m_voiceRecFlag = flag;
    }
}
