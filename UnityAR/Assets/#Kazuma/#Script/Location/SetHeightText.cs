using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHeightText : MonoBehaviour {
    //階数
    enum  Height
    {
        ONE=0,
        TWO=1,
        THREE=2,
        FOUR=3,
        FIVE=4,
        SIX=5,
        SEVEN=6,
        EIGHT=7
    }
    private Location2 _location;
    public Text _text;


    //高さの値
    public float[] HeightList = {
            44.0f,47.0f,50.0f,53.0f,56.0f,59.0f,62.0f,65.0f  };

    // Use this for initialization
    void Start () {
        _location = GameObject.Find("Text").GetComponent<Location2>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (HeightList[(int)Height.ONE] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.ONE])
        {
            _text.text = "ここは1階です";
        }
        if (HeightList[(int)Height.TWO] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.TWO])
        {
            _text.text = "ここは2階です";
        }
        if (HeightList[(int)Height.THREE] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.THREE])
        {
            _text.text = "ここは3階です";
        }
        if (HeightList[(int)Height.FOUR] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.FOUR])
        {
            _text.text = "ここは4階です";
        }
        if (HeightList[(int)Height.FIVE] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.FIVE])
        {
            _text.text = "ここは5階です";
        }
        if (HeightList[(int)Height.SIX] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.SIX])
        {
            _text.text = "ここは6階です";
        }
        if (HeightList[(int)Height.SEVEN] - 1.0f <= _location.Altitude && _location.Altitude >= HeightList[(int)Height.SEVEN])
        {
            _text.text = "ここは7階です";
        }
        if (HeightList[(int)Height.EIGHT]-1.0f<=_location.Altitude&&_location.Altitude>=HeightList[(int)Height.EIGHT])
        {
            _text.text = "ここは8階です";
        }
		
	}
}
