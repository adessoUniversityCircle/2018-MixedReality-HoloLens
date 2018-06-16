using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterInViewOnEnable : MonoBehaviour {
    [SerializeField]
    [Tooltip("Distance to camera, the object will be placed at")]
    private float Distance;

    private void OnEnable() { 
        //new position = gaze direction * distance + head position
        Vector3 newPosition = CameraCache.Main.transform.forward * Distance + CameraCache.Main.transform.position;
        this.transform.localPosition = newPosition;
    }
}
