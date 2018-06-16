using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddKlotzOnClickChanged : MonoBehaviour, IInputClickHandler
{

    public GameObject klotzPrefab1;
    public GameObject klotzPrefab2;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        
        KlotzManager.Instance.AddKlotz(klotzPrefab1);
        KlotzManager.Instance.AddKlotz(klotzPrefab2);

    }

}

