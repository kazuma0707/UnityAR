﻿//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   WatsonConversation.cs
//!
//! @brief  Watsonを使った会話処理スクリプト
//!
//! @date   2018/8/7 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
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
    [SerializeField]
    private Text m_text;

    [SerializeField]
    private bool m_voiceRecFlag;


    [SerializeField]
    private SpeechToText m_SpeechToText;
    private TextToSpeech m_TextToSpeech;
    private Conversation m_Conversation;
    private string m_WorkspaceID = "7888e1fc-7642-4a1f-a13e-06761d72472f";

    [SerializeField]
    private Variable m_variable_cs;

    private Animator animator;
    private const string key_isPose1 = "Pose1";
    private const string key_isPose2 = "Pose2";
    private const string key_isPose3 = "Pose3";
    private const string key_isPose4 = "Pose4";
    private const string key_isPose5 = "Pose5";
    private const string key_isPose6 = "Pose6";
    private const string key_isPose7 = "Pose7";
    private const string key_isPose8 = "Pose8";
    private const string key_isPose9 = "Pose9";
    private const string key_isPose10 = "Pose10";
    private const string key_isGreet = "isGreet";

    //----------------------------------------------------------------------
    //! @brief startメソッド
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator Start()
    {
        m_voiceRecFlag = false;

        m_text = GameObject.Find("VoiceText").GetComponent<Text>();

        m_variable_cs = GameObject.Find("Variable").GetComponent<Variable>();


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


        this.animator = this.GetComponent<Animator>(); 
        AudioSource audioSource = GetComponent<AudioSource>();
        while (true)
        {
            yield return RecMic(audioSource);
        }
        yield return null;
    }


    //----------------------------------------------------------------------
    //! @brief 録音処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator RecMic(AudioSource audioSource)
    {
        if (m_voiceRecFlag)
        {
            MyDebug.Log("Start record");
            m_text.text = "録音開始";
            audioSource.clip = Microphone.Start(null, true, 10, 44100);
            audioSource.loop = false;
            audioSource.spatialBlend = 0.0f;
            yield return new WaitForSeconds(2.0f);
            Microphone.End(null);
            MyDebug.Log("Finish record");
            m_text.text = "録音終了";


            // SpeechToText を日本語指定して、録音音声をテキストに変換
            m_SpeechToText.RecognizeModel = "ja-JP_BroadbandModel";
            m_SpeechToText.Recognize(HandleOnRecognize, OnFail, audioSource.clip);

            m_voiceRecFlag = false;

            m_text.text = "認識中";

        }
    }


    //----------------------------------------------------------------------
    //! @brief OnMessage関数
    //!
    //! @param[in] object, Dictionary<string, object>
    //!
    //! @return なし
    //----------------------------------------------------------------------
    //void OnMessage(MessageResponse resp, string customData)
    void OnMessage(object resp, Dictionary<string, object> customData)
    {
        if (resp is Dictionary<string, object>)
        {
            // アニメーションのフラグ変更
            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose1))
            {
                this.animator.SetBool(key_isPose1, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose2))
            {
                this.animator.SetBool(key_isPose2, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose3))
            {
                this.animator.SetBool(key_isPose3, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose4))
            {
                this.animator.SetBool(key_isPose4, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose5))
            {
                this.animator.SetBool(key_isPose5, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose6))
            {
                this.animator.SetBool(key_isPose6, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose7))
            {
                this.animator.SetBool(key_isPose7, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose8))
            {
                this.animator.SetBool(key_isPose8, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose9))
            {
                this.animator.SetBool(key_isPose9, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (!m_variable_cs.Pose_Flag && this.animator.GetBool(key_isPose10))
            {
                this.animator.SetBool(key_isPose10, m_variable_cs.Pose_Flag);
                m_variable_cs.Pose_Flag = true;
            }

            if (m_variable_cs.Pose_Flag && this.animator.GetBool(key_isGreet))
            {
                m_variable_cs.Pose_Flag = false;
            }

            this.animator.SetBool(key_isGreet, m_variable_cs.Pose_Flag);

            Dictionary<string, object> dic_resp = (Dictionary<string, object>)resp;

            foreach (object o in (List<object>)dic_resp["intents"])
            {
                Dictionary<string, object> dic_intent = (Dictionary<string, object>)o;
                MyDebug.Log("intent: " + dic_intent["intent"] + ", confidence: " + dic_intent["confidence"]);
            }

            Dictionary<string, object> dic_output = (Dictionary<string, object>)dic_resp["output"];
            string res = "";
            foreach (object o in (List<object>)dic_output["text"])
            {
                res += o.ToString();
            }

          
            m_TextToSpeech.Voice = VoiceType.ja_JP_Emi;
            m_TextToSpeech.ToSpeech(HandleToSpeechCallback, OnFail, res);

        MyDebug.Log("response: " + res);
        }

    }

    //----------------------------------------------------------------------
    //! @brief HandleToSpeechCallback関数
    //!
    //! @param[in] AudioClip, Dictionary<string, object>
    //!
    //! @return なし
    //----------------------------------------------------------------------
    //void HandleToSpeechCallback(AudioClip clip)
    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData)
    {
        PlayClip(clip);
    }

    //----------------------------------------------------------------------
    //! @brief 音声再生処理
    //!
    //! @param[in] Audioclip
    //!
    //! @return なし
    //----------------------------------------------------------------------
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

    //----------------------------------------------------------------------
    //! @brief HandleOnRecognize関数
    //!
    //! @param[in] SpeechRecognitionEvent, Dictionary<string, object>
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void HandleOnRecognize(SpeechRecognitionEvent result, Dictionary<string, object> customData)
    {
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    string text = alt.transcript;
                    MyDebug.Log(string.Format("{0} ({1}, {2:0.00})\n", text, res.final ? "Final" : "Interim", alt.confidence));

                    //text を Conversation Service に送って処理
                    m_Conversation.VersionDate = "2017-05-26";
                    m_Conversation.Message(OnMessage, OnFail ,m_WorkspaceID, text);
                    m_text.text = text;
                }
            }
        }
    }

    //----------------------------------------------------------------------
    //! @brief OnFail関数
    //!
    //! @param[in] RESTConnector.Error, Dictionary<string, object>
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        MyDebug.Log("SampleSpeechToText.OnFail() Error received: " + error.ToString());
    }


    //----------------------------------------------------------------------
    //! @brief Flagのセッター
    //!
    //! @param[in] bool
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetVoiceRecFlag(bool flag)
    {
        m_voiceRecFlag = flag;
    }
}
