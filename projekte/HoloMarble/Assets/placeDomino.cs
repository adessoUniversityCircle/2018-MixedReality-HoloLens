using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class placeDomino : MonoBehaviour, IInputClickHandler, IHoldHandler {
    private bool placeingMode = true;
    private bool circleMode = false;
    private int circleCoutner = 0;
    private CircleArc circle;

    [SerializeField]
    private GameObject DominoStein;

    [SerializeField]
    private GameObject Container;

    [SerializeField]
    private float offSet = 2;

    

    public void OnHoldCanceled(HoldEventData eventData) {
        // throw new System.NotImplementedException();
    }

    public void OnHoldCompleted(HoldEventData eventData) {
        placeingMode = !placeingMode;

    }

    public void OnHoldStarted(HoldEventData eventData) {
        // throw new System.NotImplementedException();
    }

    public void OnInputClicked(InputClickedEventData eventData) {

        SpatialMappingManager.Instance.StopObserver();

        if (!placeingMode) return;
        else if (circleMode) circleClick();
        else {
            Vector3 pos = GazeManager.Instance.HitPosition;
            pos = pos + new Vector3(0, 0.028f, 0);
            Debug.Log("Cursor pos: " + pos);
            GameObject neu = Instantiate(DominoStein, pos, new Quaternion(0, 0, 0, 0), Container.transform);
        }
    }

    private void circleClick() {
        Vector3 pos = GazeManager.Instance.HitPosition;
        pos = pos + new Vector3(0, 0.028f, 0);
        Debug.Log("Cursor pos: " + pos);
        switch (circleCoutner) {
            case 1:
                Debug.Log("CircleMode: Point A set");
                circle.setPointA(pos);
                break;
            case 2:
                circle.setPointB(pos);
                Debug.Log("CircleMode: Point B set");
                break;
            case 3:
                circle.setPointC(pos);
                Debug.Log("CircleMode: Point C set");
                circle.getDominoPoints(0.05f)
                .ForEach((p) => {
                    Instantiate(DominoStein, p + new Vector3(0, 0.028f, 0), new Quaternion(0, 0, 0, 0), Container.transform);
                });
                leaveCircleMode();
                break;
        }
        circleCoutner += 1;
    }

    private void OnDestroy() {
        SpatialMappingManager.Instance.StartObserver();
    }

    public void enterCircleMode() {
        if (!placeingMode) return;
        Debug.Log("Circle Mode startet");
        circleMode = true;
        circleCoutner = 1;
        circle = new CircleArc();
    }

    public void leaveCircleMode() {
        Debug.Log("Circle Mode beendet");
        circleMode = false;
        circleCoutner = 0;
    }
}

public class CircleArc {
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 a;
    public Vector3 c;
    public Vector3 a_norm;
    public Vector3 norm;
    public float radius;
    public float alpha;
    public float distance;

    public void setPointA(Vector3 firstPoint) {
        pointA = firstPoint;
    }

    public void setPointB(Vector3 secondPoint) {
        pointB = secondPoint;
        radius = Vector3.Distance(pointB, pointA);
        Debug.Log("Radius: " +  radius);
        a = pointA - pointB;
    }
    public void setPointC(Vector3 thirdPoint) {
        pointC = (thirdPoint - pointB).normalized * radius;
        c = pointC - pointB;
        norm = Vector3.Cross(a, c).normalized;
        float dot = Vector3.Dot(c, a);
        float acos_v = dot / (radius * radius);
        float temp_alpha = Mathf.Acos(acos_v);
        alpha = System.Math.Min(temp_alpha, 2 * Mathf.PI - temp_alpha);
        a_norm = Vector3.Cross(a, norm).normalized;
    }

    public List<Vector3> getDominoPoints(float spacing) {
        distance = radius * alpha;
        float numberOfDominos = Mathf.Floor(distance / spacing);
        List<Vector3> result = new List<Vector3>();
        Vector3 pl = new Vector3();
        for (int i = 0; i < numberOfDominos; i++) {
            pl = radius * a_norm * Mathf.Sin(i * alpha / numberOfDominos) +
                a * Mathf.Cos(i * alpha / numberOfDominos);
            pl += pointB;
            result.Add(pl);
        }
        return result;
    }
}