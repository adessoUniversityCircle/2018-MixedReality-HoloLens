using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphereReceiver : InteractionReceiver {
    [SerializeField]
    [Tooltip("Bullet prefab")]
    private GameObject BulletPrefab;

    protected override void InputClicked(GameObject obj, InputClickedEventData eventData) {
        switch (obj.name) {
            case "BtnReloadBullet":
                TargetSphereManager.Instance.AddKlotz(BulletPrefab);
                break;
        }
    }
}
