using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity;

public class MoneyPlaceHandler : PlaceHandler
{

    private Transform cameraTransform;
    Controller controller;
    Collider col;
    bool isPlaced;

    override public void Start()
    {
        base.Start();
        cameraTransform = CameraCache.Main.transform;
        controller = transform.parent.GetComponent<Controller>();
    }

    override public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!isPlaced)
        {
            isPlacing = !isPlacing;
            if (isPlacing)
            {
                billboard.enabled = true;
                rigi.useGravity = false;
                rigi.isKinematic = true;
            }
            else
            {
                billboard.enabled = false;
                rigi.useGravity = true;
                rigi.isKinematic = false;
                Vector3 newForce = cameraTransform.forward * 110;
                newForce.y += 130;
                rigi.AddForce(newForce);
                isPlaced = true;
                controller.CreateNewMoney(3);
                Destroy(this.gameObject, 40.0f);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Money collided with " + collision.gameObject.name);
        
        this.transform.Rotate(-90, 0, 0);
    }

    public void ThisIsInHand()
    {
    }

    new private void Update()
    {
        base.Update();
        if(this.transform.position.y < -10)
        {
            Debug.Log("Money is destroying itself because it's too low");
            Destroy(this.gameObject, 0);
        }
    }
}
