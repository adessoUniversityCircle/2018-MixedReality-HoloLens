using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour {

    int count = 0;
    public TextMesh timetext;
    TimeSpan ts;
    Boolean end = false;
    String endtime = "";

    DateTime initial;
	// Use this for initialization
	void Start () {
        initial = DateTime.Now;

    }

    public void updatecount()
    {
        count++;
        if (count == 9)
        {
            end = true;
            endtime = (((int)ts.TotalSeconds) * -1).ToString();
        }
    }

    // Update is called once per frame
    void Update () {
       // timetext = GameObject.Find("timetext").GetComponent<TextMesh>();
        ts = initial.Subtract(DateTime.Now);
        if (end)
        {
            timetext.text = "Finished! You needed " + endtime + " seconds";
        }
        else
        {
            timetext.text = "Already blasted: " + count + " Time: " + (((int)ts.TotalSeconds) * -1).ToString();
        }
        

    }
}
