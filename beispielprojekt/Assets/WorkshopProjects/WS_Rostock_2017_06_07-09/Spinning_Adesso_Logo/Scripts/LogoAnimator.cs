using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogoAnimator : MonoBehaviour, IInputClickHandler {
    private bool isOut;
    private bool isMoving;
    private Vector3 pos1;
    public float distance = 1;
    private Vector3 direction;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (isOut == false)
        {
            pos1 = transform.position;
            direction = new Vector3(pos1.x, 0, pos1.z);
            isMoving = true;
        }
    }
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime / 10);

            if ((transform.position - pos1).magnitude >= distance)
            {
                isMoving = false;
                isOut = true;
            }

        }
        else if (isOut)
        {
            this.GetComponentsInParent<Transform>(false)[1].Rotate(new Vector3(0, 1, 0), Time.deltaTime*25);
        }
	}
}
