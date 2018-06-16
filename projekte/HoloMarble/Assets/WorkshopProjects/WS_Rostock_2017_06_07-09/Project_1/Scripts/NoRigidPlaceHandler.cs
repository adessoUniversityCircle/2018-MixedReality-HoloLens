using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity;

public class NoRigidPlaceHandler : MonoBehaviour, IInputClickHandler
{
    [SerializeField]
    [Tooltip("Distance to hold the Display at when placing is manually started (not through user tap)")]
    private float Distance = 4.0f;  //Distance to hold the display at while placing

    private Vector3 hitPoint;   //Point where the users gaze hited the gameobject
    //private Vector3 offset;

    private Vector3 headPosition;   //Head/Main Camera position
    private Vector3 gazeDirection;  //Direction the user is gazing
    private bool isPlacing = false;

    private SpatialMappingManager spatialManager;
    [SerializeField]
    private Billboard billboard;

    public Material defaultSurfaceMaterial;
    public Material whilePlacingSurfaceMaterial;

    void Start()
    {
        spatialManager = SpatialMappingManager.Instance;
        OnInputClicked(null);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        isPlacing = !isPlacing;
        if (isPlacing)
        {
            billboard.enabled = true;
        }
        else
        {
            billboard.enabled = false;
        }

        //if (eventData != null) {
        //    hitPoint = GazeManager.Instance.HitPosition;

        //    // calc hold distance
        //    Vector3 distanceVec = headPosition - hitPoint;
        //    Distance = Mathf.Sqrt(Mathf.Pow(distanceVec.x, 2) + Mathf.Pow(distanceVec.y, 2) + Mathf.Pow(distanceVec.z, 2)) + 0.7524f; //0.7524 => approximated offset from displayHolder center point to hitPoint
        //}
    }

    // Update is called once per frame
    private void Update()
    {

        //If in placing mode, update placement to match users gaze
        if (isPlacing)
        {
            ShowMesh();
            UpdatePlacing();
        }
        else
        {
            HideMesh();
            enabled = false;
        }

        if (transform.position.y < -50)
        {
            KlotzManager.Instance.RemoveKlotz(transform.parent.gameObject);
        }

    }

    /// <summary>
    /// Changes the Surface-Material to show all the spatial mapping
    /// </summary>
    private void ShowMesh()
    {
        spatialManager.SurfaceMaterial = whilePlacingSurfaceMaterial;
    }

    /// <summary>
    /// Changes the Surface-Material to hide all the spatial mapping
    /// </summary>
    private void HideMesh()
    {
        spatialManager.SurfaceMaterial = defaultSurfaceMaterial;
    }

    /// <summary>
    /// Update display position while placing, based on the users gaze.
    /// </summary>
    private void UpdatePlacing()
    {
        headPosition = Camera.main.transform.position;
        gazeDirection = Camera.main.transform.forward;


        /* Calc. new display position (holdAtPosition). 
         * holdAtPosition is a point on the gaze direction vector (headposition -> gazeDirection)
         * whereas Distance gives the distance at which the display is held
         * see: linear function
         */
        Vector3 holdAtPosition = gazeDirection * Distance + headPosition; //+ offset

        //Update position
        this.transform.position = holdAtPosition;
    }
}
