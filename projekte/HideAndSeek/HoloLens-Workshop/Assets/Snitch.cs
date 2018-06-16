using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snitch : MonoBehaviour, IInputClickHandler
{
    public enum SnitchState
    {
        FALLING,
        UNPLACED,
        FIXED
    }

    // Unity injected
    public Rigidbody CustomRigidbody;
    public HideAndSeek Manger;
    public GameObject ParticlePrefab;
    public GameObject SnitchGameObject;

    public SnitchState state = SnitchState.UNPLACED;
    private float distance = 1.0f;
    private Vector3 lastFallingPosition = Vector3.zero;
    private int positionSameCount = 0;

    private Vector3 headPosition;   //Head/Main Camera position
    private Vector3 gazeDirection;

    // Use this for initialization
    void Start () {
        Instantiate(ParticlePrefab, this.transform);
    }
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case SnitchState.FALLING:
                if(lastFallingPosition == this.transform.position)
                {
                    positionSameCount++;
                    if (positionSameCount >= 50)
                    {
                        state = SnitchState.FIXED;
                        CustomRigidbody.useGravity = false;
                    }                 
                } else
                {
                    positionSameCount = 0;
                    lastFallingPosition = this.transform.position;
                }
                break;
            case SnitchState.UNPLACED:
                MoveWithCamera();
                break;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Manger.SnitchClicked(SnitchGameObject);
    }

    private void MoveWithCamera()
    {
        headPosition = Camera.main.transform.position;
        gazeDirection = Camera.main.transform.forward;
        Vector3 holdAtPosition = gazeDirection * distance + headPosition;
        this.transform.position = holdAtPosition;
    }
}
