using HoloToolkit.Unity;
using HoloToolkit.Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphereManager : KlotzManager {
    [SerializeField]
    [Tooltip("Target sphere prefab")]
    private GameObject TargetSphere;

    private GameObject BtnReloadBullet;
    
    public override void Start() {
        base.Start();

        AddKlotz();
        AddReloadBulletButton();
    }

    protected override void OnDestroy() {
        RemoveReloadBulletButton();

        base.OnDestroy();
    }

    public override GameObject AddKlotz() {
        GameObject newTarget = base.AddKlotz(TargetSphere);

        Vector3 variation = new Vector3(Random.Range(-0.5f, 0.5f),
                                        Random.Range(-0.5f, 0.5f),
                                        Random.Range(-0.5f, 0.5f));

        Vector3 newPosition = CameraCache.Main.transform.forward * 2 + variation + CameraCache.Main.transform.position;


        //change position to add target somewhere around the player
        newTarget.transform.localPosition = newPosition;
        return newTarget;
    }

    private void AddReloadBulletButton() {
        //get tools content
        GameObject toolsContent = GameObject.FindGameObjectWithTag("SceneSelectionMenuToolsContent");

        //get reload bullet button and set active
        BtnReloadBullet = toolsContent.transform.Find("BtnReloadBullet").gameObject;
        BtnReloadBullet.SetActive(true);
        //update object collection
        toolsContent.GetComponent<ObjectCollection>().UpdateCollection();

        //add bullet reload button to receiver interactables
        GetComponent<TargetSphereReceiver>().interactables.Add(BtnReloadBullet);
    }

    private void RemoveReloadBulletButton() {
        //deactivate reload bullet button 
        BtnReloadBullet.SetActive(false);

        GameObject toolsContent = GameObject.FindGameObjectWithTag("SceneSelectionMenuToolsContent");
        //update object collection
        toolsContent.GetComponent<ObjectCollection>().UpdateCollection();
    }
}
