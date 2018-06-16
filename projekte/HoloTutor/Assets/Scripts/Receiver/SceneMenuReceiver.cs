using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMenuReceiver : InteractionReceiver {
    private string currentScene; //current active/added scene

    [SerializeField]
    [Tooltip("Containers to get Interactables/Buttons from that should receive input. Interactable objects should be the only children of a container and at root")]
    private List<GameObject> InteractableContainers;

    private void Start() {
        LoadInteractables();

        currentScene = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void LoadInteractables() {
        foreach(GameObject container in InteractableContainers) {
            foreach(Transform child in container.transform) {
                if (child.gameObject.activeInHierarchy) {
                    this.interactables.Add(child.gameObject);
                }
            }
        }
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene mScene, LoadSceneMode mMode) {
        //update currentScene with newly added scene
        currentScene = mScene.name;
    }

    private void OnSceneUnloaded(Scene mScene) {
        //only MainScene and one other scene should be active
        //if the other scene is removed/unloaded only MainScene should remain
        //and is therefore the currentScene
        currentScene = "MainScene";
    }

    /// <summary>
    /// Handler to capture and process button clicks in the SceneScelectionMenu
    /// </summary>
    /// <param name="obj">Gameobject that was clicked</param>
    /// <param name="eventData">Click event data</param>
    protected override void InputClicked(GameObject obj, InputClickedEventData eventData) {
        switch (obj.name) {
            case "BtnReset":
                RemoveScene(currentScene);
                break;

            default:
                /**
                 * A button used to load a scene should follow the name convention 'Btn<NAME_OF_SCENE>'
                 * e.g 'BtnAdessoLogo' to easily load a new scene without further 
                 * logic implementation needed
                 **/

                string selectedSceneName = obj.name.Replace("Btn", "");

                //check if selected scene is already active
                //if yes, check if a handler for this scene exists, load scene if not
                if (!currentScene.Equals(selectedSceneName) || 
                    !HandleSceneSelection(selectedSceneName)) {
                    //load selected scene
                    LoadScene(selectedSceneName);
                }
                break;
        }
    }

    /// <summary>
    /// Unloads the currently active scene <see cref="currentScene"/> (unless it is the MainScene)
    /// </summary>
    /// <param name="mSceneName"></param>
    private void RemoveScene(string mSceneName) {
        if (currentScene.Equals("MainScene")) {
            return;
        }

        SceneManager.UnloadSceneAsync(mSceneName);
    }

    /// <summary>
    /// Loads a scene by a given scene name. Before loading the scene, the currently active scene is unloaded <see cref="RemoveScene(string)"/>
    /// </summary>
    /// <param name="mSceneName">Scene name</param>
    private void LoadScene(string mSceneName) {
        //remove current scene
        RemoveScene(currentScene);
        //Load new scene
        SceneManager.LoadScene(mSceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Use this to handle what should happen if a currently active scene is selected again.
    /// </summary>
    /// <param name="mSceneName">Name of the scene that is selected from the menu</param>
    /// <returns>True if a handler for the scene exists, else/default false</returns>
    private bool HandleSceneSelection(string mSceneName) {
        switch (mSceneName) {
            case "AdessoLogo":
                AdessoLogoManager.Instance.AddKlotz();
                return true;

            case "TargetSphere":
                TargetSphereManager.Instance.AddKlotz();
                return true;

            default:
                return false;
        }
    }
}
