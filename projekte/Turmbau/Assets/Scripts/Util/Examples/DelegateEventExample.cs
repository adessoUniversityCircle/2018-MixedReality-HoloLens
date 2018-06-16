using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateEventExample : MonoBehaviour {

    //Create Delegate "Test"
    public delegate void Test();
    //Create Event "TestEvent" for Delegate "Test"
    public event Test TestEvent;

	void Start () {
        //subscribe method "TestEventAusloser" to "TestEvent" 
        TestEvent += TestEventAusloser;
	}

    private void OnDestroy() {
        //un-subscribe method "TestEventAusloser" from "TestEvent"
        TestEvent -= TestEventAusloser;
    }

    /// <summary>
    /// Function that triggers "TestEvent" and notifies all subscribers
    /// </summary>
    public void TriggerTestEvent() {
        if (TestEvent != null) {
            TestEvent();
        }
    }

    /// <summary>
    /// Method that subscribes to "TestEvent" and will be called when "TestEvent" is triggered
    /// </summary>
    private void TestEventAusloser() {
        Debug.Log("TestEvent triggerd");
    }
}
