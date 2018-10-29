using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Ranking : MonoBehaviour {
    private string RANKING_PREF_KEY = "ranking";
    private int RANKING_NUM = 5;
    private float[] ranking;
    Array _array;

    [SerializeField, Range(0, 1000)]
    int size;
    public GUIStyleState stylestate;
    private GUIStyle style;
    public Color color;
    [SerializeField]
    Vector2 label_ranking;
    [SerializeField]
    Vector2 label_score;

    [SerializeField]
    Text rankText;
    [SerializeField]
    Text scoreText;

    bool rankInFlag = false;
    public GameObject fireworksParticle;
    GameObject obj;

    // Use this for initialization
    void Start () {
        style = new GUIStyle();
        ranking = new float[RANKING_NUM];

        getRanking();
        //saveRanking(10);
        //saveRanking(100);
        //saveRanking(122);
        saveRanking(GameManager.gameScore);

        stylestate.textColor = color;
        style.normal = stylestate;

        displayScore();

        //GUI.Label(rect_ranking, ranking_string, style);

        //GUI.Label(rect_score, score_string, style);
    }
  
	// Update is called once per frame
	void Update () {
        style.fontSize = size;



        Render();

        if (rankInFlag && !obj)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(1, 4), 0.0f);
            obj = Instantiate(fireworksParticle,pos,Quaternion.identity);
            if(!obj.GetComponent<ParticleSystem>().isPlaying)
            {
                obj = null;
            }
        }

    }
    public void getRanking()
    {
        var _ranking = PlayerPrefs.GetString(RANKING_PREF_KEY);
        if(_ranking.Length>0)
        {
            var _score = _ranking.Split(","[0]);
            ranking = new float[RANKING_NUM];
            for(var i=0;i<_score.Length&&i<RANKING_NUM;i++)
            {
                ranking[i] = float.Parse(_score[i]);
            }
        }

    }
    public void saveRanking(float new_score)
    {
        string rankingString = "";
        if(ranking.Length!=0)
        {
            float _tmp = 0.0f;
            for(int i=0;i<ranking.Length;i++)
            {
                if(ranking[i]<new_score)
                {
                    _tmp = ranking[i];
                    ranking[i] = new_score;
                    new_score = _tmp;
                    rankInFlag = true;
                }
            }
        }
        else
        {
            ranking[0] = new_score;
        }
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingString += ranking[i].ToString();
            if(i + 1 < ranking.Length)
            {
                rankingString += ",";
            }
        }
        PlayerPrefs.SetString(RANKING_PREF_KEY, rankingString);
    }
   public void deleteRanking()
    {
        PlayerPrefs.DeleteKey(RANKING_PREF_KEY);
        for (int i = 0; i < ranking.Length; i++)
        {
            ranking[i] = 0;
        }
            displayScore();

    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }

    private void displayScore()
    {
        Rect rect_ranking = new Rect(new Vector2(Screen.width, Screen.height), label_ranking);
        Rect rect_score = new Rect(new Vector2(label_score.x, label_score.y), label_score);
        string ranking_string = "";
        string score_string = "";
        for (int i = 0; i < ranking.Length; i++)
        {
            //ranking_string = ranking_string + (i + 1) + "位" + "  " + ranking[i] + "秒\n";
            ranking_string = ranking_string + (i + 1) + "位\n";
            score_string = score_string + ranking[i] + "秒\n";
        }
        style.alignment = TextAnchor.UpperRight;
        rankText.text = ranking_string.ToString();
        scoreText.text = score_string.ToString();
    }

    private void Render()
    {
 
    }
    private void OnGUI()
    {
        //Vector2 label_ranking = new Vector2(Screen.width, Screen.height);


    }
}
