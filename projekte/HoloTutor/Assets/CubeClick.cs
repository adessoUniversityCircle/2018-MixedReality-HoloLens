using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeClick : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Logic.init = !Logic.init;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
