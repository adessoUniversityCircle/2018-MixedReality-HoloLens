using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour, IInputClickHandler
{
    Vector3[] pos = new Vector3[300];
    int num = 10;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        for (int i = 0; i < num; i++)
        {
            float alpha = i * Mathf.PI / num;
            pos[i] = 0.1f * new Vector3(Mathf.Sin(alpha), Mathf.Cos(alpha), 1.6f);
        }
        Debug.Log(gameObject.name);
        Debug.Log(pos[1]);
        gameObject.GetComponent<LineRenderer>().SetVertexCount(num);
        gameObject.GetComponent<LineRenderer>().SetPositions(pos);
        num += 10;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<LineRenderer>();
		
	}
}
