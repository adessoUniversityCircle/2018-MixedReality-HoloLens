using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdessoLogoManager : KlotzManager {
    [SerializeField]
    [Tooltip("Adesso logo prefab")]
    private GameObject AdessoLogoPrefab;

    /// <summary>
    /// Adds a gameobject of type 'AdessoLogoPrefab' (<see cref="AdessoLogoPrefab"/>) to the scene
    /// </summary>
    /// <returns></returns>
    public override GameObject AddKlotz() {
        return base.AddKlotz(AdessoLogoPrefab);
    }
}
