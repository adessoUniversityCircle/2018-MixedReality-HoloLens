using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    GameObject basket;
    GameObject money;
    GameObject floor;

    bool sleepingToCreateMoney;

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            if(child.name.Equals("Basket"))
            {
                basket = child.gameObject;
            } else if (child.name.Equals("Money"))
            {
                money = child.gameObject;
                money.SetActive(false);
            }
        }
        floor = GameObject.Find("roomMesh-0");
        Mesh m = floor.GetComponent<Mesh>();
        float lowest = 100;
        foreach (Vector3 vec in m.vertices)
        {
            if (vec.y < lowest) lowest = vec.y;
        }
        GameObject plane = GameObject.Find("GoundPlane");
        plane.transform.position = new Vector3(plane.transform.position.x, lowest, plane.transform.position.z);
    }
    /*
	// Update is called once per frame
	void Update () {
        if (!basket.GetComponent<PlaceHandler>().IsPlacing())
        {
            GameObject throwMoney = Instantiate(money);
            throwMoney.SetActive(true);
            step++;
        }
	}*/

    public void CreateNewMoney(float seconds)
    {
        if (!sleepingToCreateMoney)
        {

            StartCoroutine(CreateNewMoneyCor(seconds));
        }
    }

    private IEnumerator CreateNewMoneyCor(float seconds)
    {
        sleepingToCreateMoney = true;
        Debug.Log("i am waiting... to create money");
        yield return new WaitForSeconds(seconds);
        Debug.Log("I am creating Money!");
        GameObject throwMoney = Instantiate(money, this.transform);
        throwMoney.SetActive(true);
        throwMoney.GetComponent<MoneyPlaceHandler>().ThisIsInHand();
        sleepingToCreateMoney = false;
    }
}
