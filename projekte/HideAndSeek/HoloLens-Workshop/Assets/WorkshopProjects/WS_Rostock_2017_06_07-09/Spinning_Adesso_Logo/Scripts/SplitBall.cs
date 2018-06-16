using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class SplitBall : MonoBehaviour, IInputClickHandler {
    bool isSplitting = false;
    bool isSplitted = false;
    public GameObject upperHalf;
    public GameObject bottomHalf;
    public float distance = 1;
    private float baseY;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (isSplitted == false)
        {
            baseY = upperHalf.transform.position.y;
            isSplitting = true;
        }
    }

    void Update () {
        if (isSplitting)
        {
            upperHalf.transform.Translate(new Vector3(0, -0.01f,0) * Time.deltaTime);
            bottomHalf.transform.Translate(new Vector3(0, -0.01f, 0) * Time.deltaTime);

            if (upperHalf.transform.position.y-baseY >= distance/2)
            {
                isSplitting = false;
                isSplitted = true;
                this.GetComponent<Collider>().enabled = false;
            }
        }
	}
}

