using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity;

public class createCubes : MonoBehaviour, IInputClickHandler
{
    [SerializeField]
    private GameObject cubeObject;

    [SerializeField]
    [Tooltip("Distance to camera, the object will be placed at")]
    public float Distance;

    private List<GameObject> allGO = new List<GameObject>();
    private GameObject current;
    private bool firstClicked = false;
    private int frameCount = 1001;
    private Boolean show = true;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (firstClicked)
        {
            Debug.Log("klick registriert");
            allGO[allGO.Count - 1].GetComponent<klotzScript>().isGrabbed = false;

            show = true;
            frameCount = 0;
        }
        firstClicked = true;



    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (show && (frameCount > 500))
        {
            Vector3 newPosition = CameraCache.Main.transform.forward
                * Distance
                + CameraCache.Main.transform.position;
            current = Instantiate(cubeObject, newPosition, CameraCache.Main.transform.rotation);
            if(allGO.Count == 0)
            {
                current.GetComponent<klotzScript>().isFirstObject = true;
            }
            
            allGO.Add(current);
            show = false;
        }
        else if (show)
        {
            frameCount++;
        }
    }

}
