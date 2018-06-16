using HoloToolkit.Unity;
using HoloToolkit.Unity.Collections;
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Provides events to handle UI interactions, e.g. toggeling the toolbox.
/// </summary>
public class UIManager : Singleton<UIManager> {
    [SerializeField]
    [Tooltip("Scene Menu")]
    private GameObject SceneMenu;

    public void ShowSceneMenu() {
        SceneMenu.SetActive(true);
    }

    public void HideSceneMenu() {
        SceneMenu.SetActive(false);
    }
}
