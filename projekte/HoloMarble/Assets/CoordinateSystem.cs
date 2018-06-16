using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class CoordinateSystem : MonoBehaviour, IInputClickHandler, IHoldHandler
{
    [SerializeField]
    [Tooltip("Coord Sys prefab")]
    private GameObject prefab;

    [SerializeField]
    [Tooltip("Graph prefab")]
    private GameObject graph_prefab;

    private GameObject coordSys;
    private GameObject graph;
    bool is_fixed = false;
    bool is_holding = false;
    Vector3 center;
    Vector3 X_Axis;
    Vector3 Y_Axis;
    Vector3 Z_Axis;
    Matrix4x4 world_to_cs;
    Matrix4x4 cs_to_world;

    public void OnHoldCanceled(HoldEventData eventData)
    {
        if (is_holding)
        {
            is_holding = false;
        }
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        is_holding = false;
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        if (is_fixed)
        {
            is_holding = true;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!is_fixed)
        {
            is_fixed = true;
            Transform child = coordSys.transform.transform.GetChild(2).transform;
            Debug.Log(child.name);
            this.graph = Instantiate(graph_prefab, child);
        }
    }

    // Use this for initialization
    void Start () {
        // Debug.Log("Start");
        Vector3 pos = CameraCache.Main.transform.forward * 1.5f + CameraCache.Main.transform.position;
        // Quaternion rot = CameraCache.Main.transform.rotation;
        coordSys = Instantiate(prefab, pos, new Quaternion(0, 0, 0, 0));
        // Debug.Log("CoordSys places: " + coordSys.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        if (!is_fixed)
        {
            Vector3 pos = CameraCache.Main.transform.forward * 1.5f + CameraCache.Main.transform.position;
            // Quaternion rot = CameraCache.Main.transform.rotation;
            coordSys.transform.position = pos;
            // coordSys.transform.rotation = rot;
        }
		else if (is_holding)
        {
            coordSys.transform.Rotate(transform.up, 0.4f);
        }
	}
}
