using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity;

public class BasketPlaceHandler : PlaceHandler {
    int clickedTimes = 0;
    Controller controller;

    public override void Start()
    {
        base.Start();
        controller = transform.parent.GetComponent<Controller>();
    }

    override public void OnInputClicked(InputClickedEventData eventData)
    {
        if (clickedTimes < 2)
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
            }
            clickedTimes++;

            //if (eventData != null) {
            //    hitPoint = GazeManager.Instance.HitPosition;

            //    // calc hold distance
            //    Vector3 distanceVec = headPosition - hitPoint;
            //    Distance = Mathf.Sqrt(Mathf.Pow(distanceVec.x, 2) + Mathf.Pow(distanceVec.y, 2) + Mathf.Pow(distanceVec.z, 2)) + 0.7524f; //0.7524 => approximated offset from displayHolder center point to hitPoint
            //}

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Basket collided with " + collision.gameObject.name);
        if (!isPlacing)
        {
            Debug.Log("Basket is creating Money");
            rigi.isKinematic = true;
            controller.CreateNewMoney(0.5f);
        }
    }

    public override void UpdatePlacing()
    {
        base.UpdatePlacing();
        this.transform.Rotate(-90, 0, 0);
    }

}
