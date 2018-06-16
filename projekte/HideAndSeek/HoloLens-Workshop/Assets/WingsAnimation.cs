using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsAnimation : MonoBehaviour {

    public GameObject RightWing;
    public GameObject LeftWing;
    public GameObject Body;

    private int degreeOffset = 0;
    private int MaxDegreeOffset = 20;
    private bool up = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(up)
        {
            RightWing.transform.Rotate(Body.transform.position, 1);
            LeftWing.transform.Rotate(Body.transform.position, -1);
            degreeOffset++;
            if(degreeOffset == MaxDegreeOffset)
            {
                up = false;
            }
        } else
        {
            RightWing.transform.Rotate(Body.transform.position, -1);
            LeftWing.transform.Rotate(Body.transform.position, 1);
            degreeOffset--;
            if (degreeOffset == -MaxDegreeOffset)
            {
                up = true;
            }
        }
	}
}
