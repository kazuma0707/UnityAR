using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationRender : MonoBehaviour {
    public LocationUpdater updater;
    public Text text;

    void Update()
    {
        text.text = updater.Status.ToString()
                  + "\n" + "lat:" + updater.Location.latitude.ToString()
                  + "\n" + "lng:" + updater.Location.longitude.ToString()
                  + "\n" + "Height" + Input.location.lastData.verticalAccuracy;
    }
}
