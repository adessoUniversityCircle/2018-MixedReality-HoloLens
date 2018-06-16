using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickHide : MonoBehaviour, IInputClickHandler {
    SphereCollider sphere;
    int i;
    public time time;

    [SerializeField]
    private GameObject explosion;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        time.updatecount();
        Instantiate(explosion,this.gameObject.transform.position,this.gameObject.transform.rotation,GameObject.Find("explosionParent").transform);
        this.gameObject.SetActive(false);

    }

    // Use this for initialization
    void Start () {


      
        
    }



    // Update is called once per frame
    void Update()
    {
  
    }
}
