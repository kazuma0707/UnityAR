using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ranking : MonoBehaviour {
    private string RANKING_PREF_KEY = "ranking";
    private int RANKING_NUM = 10;
    private float[] ranking;
    Array _array;

    [SerializeField, Range(0, 1000)]
    int size;
    public GUIStyleState stylestate;
    private GUIStyle style;
    public Color color;
    // Use this for initialization
    void Start () {
        style = new GUIStyle();
        ranking = new float[RANKING_NUM];
        Debug.Log(Screen.height);

        //saveRanking(10);
        //saveRanking(100);
        //saveRanking(122);


        stylestate.textColor = color;
        style.normal = stylestate;
    }
  
	// Update is called once per frame
	void Update () {
        style.fontSize = size;



        Render();



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
        
    }
    private void Render()
    {
 
    }
    private void OnGUI()
    {
        Vector2 label_ranking = new Vector2(Screen.width, Screen.height);
        Rect rect_ranking = new Rect(new Vector2(label_ranking.x/2 , label_ranking.y/2 ), label_ranking);
        string ranking_string = "";
        for (int i = 0; i < ranking.Length; i++)
        {
            ranking_string = ranking_string + (i + 1) + "位" + ranking[i] + "秒\n";
        }
      
        GUI.Label(rect_ranking, ranking_string, style);
        
       

    }
}
