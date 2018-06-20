using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;

public abstract class KlotzManager : Singleton<KlotzManager> {
    [SerializeField]
    private GameObject klotzHolder;
    [SerializeField]
    private List<GameObject> KlotzList;

    public virtual void Start() {
        KlotzList = new List<GameObject>();

        //if no specific container for klotz objecst is given
        if(klotzHolder == null) {
            //use this gameobject as container
            klotzHolder = this.gameObject;
        }
    }

    public abstract GameObject AddKlotz();

    /// <summary>
    /// Adds a gameobject of klotzPrefab type to the scene.
    /// </summary>
    /// <param name="klotzPrefab"></param>
    /// <returns></returns>
    public virtual GameObject AddKlotz(GameObject klotzPrefab) {   
        GameObject klotz = Instantiate(klotzPrefab, klotzHolder.transform);
        KlotzList.Add(klotz);
        return klotz;
    }

    /// <summary>
    /// Removes the given klotzGameobject from the scene.
    /// </summary>
    /// <param name="klotzGameobject"></param>
    public virtual void RemoveKlotz(GameObject klotzGameobject) {
        KlotzList.Remove(klotzGameobject);
        Destroy(klotzGameobject);
    }

    /// <summary>
    /// Removes all klotz objects from the scene.
    /// </summary>
    public virtual void RemoveAllKlotzes() {
        foreach(GameObject klotz in KlotzList) {
            Destroy(klotz);
        }
        KlotzList.Clear();
    }
}
