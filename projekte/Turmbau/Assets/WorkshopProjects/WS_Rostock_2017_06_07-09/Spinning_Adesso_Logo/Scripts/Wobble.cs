using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour {
    public float minValue = 0;
    public float maxValue = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float factor = maxValue/2 + (maxValue/2) * Mathf.Sin(Time.time);
        transform.localScale = new Vector3(factor, factor, factor);	
	}
}
