using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kollision : MonoBehaviour {

    public GameObject explosioPrefab;

    private void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {

        Debug.Log(col.GetHashCode());

        if (col.gameObject.name == "Bullet(Clone)")
        {
            Vector3 vec = col.gameObject.transform.position;
                       
            GameObject explosionGameobject = KlotzManager.Instance.AddKlotz(explosioPrefab);

            explosionGameobject.transform.position=vec;
            
            Destroy(col.gameObject);

            Destroy(this.gameObject);

           
            
        }
    }


}
