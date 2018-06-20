using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour {
    

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "portal1")
        {

            Debug.Log("Collision with PORTAL1");
            Vector3 currVel = (Quaternion.AngleAxis(-270, Vector3.up) * GetComponent<Rigidbody>().velocity) * 1f;
            

            GameObject portal2 = GameObject.Find("portal2");
            GameObject testCube = this.gameObject;
            testCube.transform.position = portal2.transform.position;
            testCube.GetComponent<Rigidbody>().velocity = currVel;


        } else if (collision.gameObject.name == "portal2")
        {
            
            Debug.Log("Collision with PORTAL2");

        }
        else if (collision.gameObject.name == "goal")
        {

            Debug.Log("GOAL!!!");
            GameObject particles = Instantiate(Resources.Load("ligh_sphere")) as GameObject;
            particles.transform.position = collision.gameObject.transform.position;

        }
    }
}
