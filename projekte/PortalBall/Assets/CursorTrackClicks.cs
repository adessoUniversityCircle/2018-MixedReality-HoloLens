using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrackClicks : MonoBehaviour, IInputClickHandler
{
 
    GameObject portal1;
    GameObject portal2;
    GameObject goal;
    GameObject ball;

    public void Start()
    {
        portal1 = null;
        portal2 = null;
        goal = null;
        ball = null;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Vector3 hitWithWall = CursorSingleton.Instance.transform.position;
        // Debug.Log(hitWithWall);
        if (portal1 == null) {
            portal1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            portal1.transform.position = hitWithWall;
            portal1.transform.localScale = new Vector3(0.45f, 0.7f, 0.01f);
            portal1.transform.rotation = CursorSingleton.Instance.transform.rotation;
            portal1.GetComponent<Renderer>().material.color = Color.red;
            // portal1.GetComponent<BoxCollider>().isTrigger = true;
            portal1.name = "portal1";
        } else if (portal2 == null) {
            portal2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            portal2.transform.position = hitWithWall;
            portal2.transform.localScale = new Vector3(0.45f, 0.7f, 0.01f);
            portal2.GetComponent<Renderer>().material.color = Color.blue;
            portal2.transform.rotation = CursorSingleton.Instance.transform.rotation;
          
            portal2.name = "portal2";
        }
        else if (goal == null)
        {
            goal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            goal.transform.position = hitWithWall;
            goal.transform.localScale = new Vector3(0.3f, 0.05f, 0.3f);
            goal.GetComponent<Renderer>().material.color = Color.white;
            
            goal.name = "goal";
        }
        else {
            if (ball != null)
            {
                Destroy(ball);
            }

            Physics.gravity = new Vector3(0, -4.0f, 0);

            // ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ball = Instantiate(Resources.Load("ball", typeof (GameObject))) as GameObject;
           // ball.GetComponent<Renderer>().material.color = Color.green;

            Vector3 headPosition = Camera.main.transform.position;
            Vector3 gazeDirection = Camera.main.transform.forward;
            Vector3 holdAtPosition = gazeDirection *2f + headPosition;
            ball.transform.position = holdAtPosition;
            // ball.AddComponent<Rigidbody>();
            ball.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 5;

            

        }
    }


}
