using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nachladen : MonoBehaviour, IInputClickHandler
{

    public GameObject klotzPrefab1;

    public void OnInputClicked(InputClickedEventData eventData)
    {

        KlotzManager.Instance.AddKlotz(klotzPrefab1);

    }

}