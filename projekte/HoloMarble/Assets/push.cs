using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push : MonoBehaviour, IInputClickHandler {

    [SerializeField]
    private float forceFactor = 1;

    public void OnInputClicked(InputClickedEventData eventData) {
        Debug.Log("Domino click handler called, domino pushed");
        Vector3 fwd = CameraCache.Main.transform.forward;
        gameObject.GetComponent<Rigidbody>().AddForce(fwd*forceFactor, ForceMode.Impulse);
    }

}
