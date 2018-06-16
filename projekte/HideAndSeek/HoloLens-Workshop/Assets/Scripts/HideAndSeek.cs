using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour {

    public GameObject SnitchPrefab;
    public GameObject ExplosionPrefab;
    public GameObject UI;

    private const int MAX_SNITCH_COUNT = 3;
    private ManagerState state = ManagerState.HIDE;
    private int snitchCount = 0;

    public enum ManagerState
    {
        HIDE,
        SEEK
    }

    public void Start()
    {
        InitializeSnatch();
        UI = GameObject.Find("UI");
        UI.SetActive(false);
    }

    public void Update()
    {
        
    }

    public void InitializeSnatch()
    {
        var snitch = Instantiate(SnitchPrefab, this.transform);
        snitch.GetComponent<Snitch>().Manger = this;
        Vector3 newPosition = CameraCache.Main.transform.forward * 2 + CameraCache.Main.transform.position;
        snitch.transform.localPosition = newPosition;
    }

    public void SnitchClicked(GameObject snitch)
    {
        switch (state) {
            case ManagerState.HIDE:
                snitch.GetComponent<Snitch>().state = Snitch.SnitchState.FALLING;
                snitch.GetComponent<Snitch>().CustomRigidbody.useGravity = true;
                snitchCount++;
                if(snitchCount == MAX_SNITCH_COUNT)
                {
                    state = ManagerState.SEEK;
                } else
                {
                    StartCoroutine(AndInitializeNewSnitch(1));
                }             
                break;
            case ManagerState.SEEK:
                var explosion = Instantiate(ExplosionPrefab, this.transform);
                explosion.transform.position = snitch.transform.position;
                Destroy(snitch);
                snitchCount--;
                if(snitchCount == 0)
                {
                    UI.SetActive(true);
                    Destroy(this);
                }
                break;
        }      
    }

    IEnumerator AndInitializeNewSnitch(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        InitializeSnatch();
    }
}
