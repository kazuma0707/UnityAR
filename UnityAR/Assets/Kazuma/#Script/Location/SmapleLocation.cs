using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmapleLocation : MonoBehaviour
{
    public Text _text;

    IEnumerator Start()
    {
        Debug.Log("Start");
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }
        Input.location.Start();
        int maxWait = 120;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            _text.text="Timed out";
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            _text.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            _text.text = "Location: " +
                  Input.location.lastData.latitude + " " +
                  Input.location.lastData.longitude + " " +
                  Input.location.lastData.altitude + " " +
                  Input.location.lastData.horizontalAccuracy + " " +
                  Input.location.lastData.timestamp;
        }
        Input.location.Stop();
    }
}