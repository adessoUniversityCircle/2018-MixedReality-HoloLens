using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity;

public class klotzScript : MonoBehaviour, IInputClickHandler {
    //Wenn zweiter Klotz gesetzt wurde, dann wenn er 0 berührt verloren

	[SerializeField]
    [Tooltip("Distance to camera, the object will be placed at")]
    public float Distance;

    private SpatialMappingManager spatialManager;
    public bool isGrabbed = true;
    public bool isFirstObject = false;
    private bool firstClicked = false;
    private GameObject current;
    private GameObject[] allGO = new GameObject[20];
    private int counterCurrent = -1;
    private int counterFrames = 0;
    void Start()
    {
        spatialManager = SpatialMappingManager.Instance;
        
        
        

    }

    private void Update() {
        //new position = gaze direction * distance + head position
        if (isGrabbed)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            Vector3 newPosition = CameraCache.Main.transform.forward * Distance + CameraCache.Main.transform.position;
            this.transform.localPosition = newPosition;

            Vector3 rotation = new Vector3(0, 0, 0);
            rotation.y = CameraCache.Main.transform.eulerAngles.y;
            rotation.z = CameraCache.Main.transform.eulerAngles.z;
            this.transform.eulerAngles = rotation;

        } else
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            /*
            try
            {
                if (counterFrames < 1000)
                {
                    counterFrames++;
                }
                else
                {


                    
                    Debug.Log("Neues Objeckt wir erstellt");
                    counterCurrent++;
                    
                    Vector3 newPosition = CameraCache.Main.transform.forward * Distance + CameraCache.Main.transform.position;
                    newPosition.y += 1;
                    allGO[counterCurrent].transform.position = newPosition;
                    Rigidbody gameObjectsRigidBody = allGO[counterCurrent].AddComponent<Rigidbody>(); // Add the rigidbody.
                    gameObjectsRigidBody.mass = 0.5f;
                    gameObjectsRigidBody.angularDrag = 0;
                    allGO[counterCurrent].transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

                    allGO[counterCurrent] = Instantiate(allGO[counterCurrent - 1]);
                    isGrabbed = true;
                    counterFrames = 0;
                }

                
            } catch (Exception e){
            }*/
        }
       
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        /*
        Debug.Log("geklickt: " + isGrabbed + " firstClicked: " + firstClicked);
        
        if (isGrabbed && firstClicked)
        {
            isGrabbed = false;
        }

        if (!firstClicked)
        {
            firstClicked = true;
        }
        
        //Debug.Log("geklickt2: " + isGrabbed + " firstClicked: " + firstClicked);
        */
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "roomMesh-0" && !isFirstObject)
        {

            Debug.Log("verloren");

            GameObject canvas = GameObject.Find("CanvasLost");

            canvas.SetActiveRecursively(true);

            Vector3 newPosition = CameraCache.Main.transform.forward * Distance + CameraCache.Main.transform.position;
            canvas.transform.localPosition = newPosition;

            Vector3 rotation = new Vector3(0, 0, 0);
            rotation.y = CameraCache.Main.transform.eulerAngles.y;
            rotation.z = CameraCache.Main.transform.eulerAngles.z;
            canvas.transform.eulerAngles = rotation;

        }

    }
}
