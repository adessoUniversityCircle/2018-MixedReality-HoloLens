using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.WSA.WebCam;

public class Logic : MonoBehaviour, IInputClickHandler
{
    private PhotoCapture photoCapture;
    private Resolution cameraResolution;

    private void PlaceText(Vector3 postion, string text)
    {
        var obj = Resources.Load("3DTextPrefab") as GameObject;

        obj.GetComponent<TextMesh>().text = text;

        obj.GetComponent<TextMesh>().fontSize = 40;

        //obj.transform.Translate(0, 0, 1);

        obj.GetComponent<TextMesh>().color = Color.white;

        obj.transform.position = postion;

        Instantiate(obj);
    }

    private void PlaceBigText(Vector3 postion, string text)
    {
        var obj = Resources.Load("3DTextPrefab") as GameObject;

        obj.GetComponent<TextMesh>().text = text;

        //obj.transform.Translate(0, 0, 1);

        obj.transform.position = postion;

        obj.GetComponent<TextMesh>().fontSize = 80;

        obj.GetComponent<TextMesh>().color = Color.red;

        Instantiate(obj);
    }


    // Use this for initialization
    void Start()
    {

        //this.PlaceText(new Vector3(0, 0, 1), "abc");

        cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

    }

    string filePath;
    void takePhoto()
    {
        // ToDo: Comment in
        //this.PlaceText(this.emotionPersonPosition, "Happy 99%;");
        //return;


        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            this.photoCapture = captureObject;

            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;
            // Activate the camera
            photoCapture.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
                filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
                // Take a picture
                this.photoCapture.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.PNG, OnCapturedPhotoToDisk);
            });
        });

    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            img();

            this.photoCapture.StopPhotoModeAsync(OnStoppedPhotoMode);
        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        this.photoCapture.Dispose();
        this.photoCapture = null;
    }

    void img()
    {
        var imageBytes = System.IO.File.ReadAllBytes(filePath);

        
        StartCoroutine(Upload(imageBytes));

        //System.IO.File.Delete(filePath);
    }

    IEnumerator Upload(byte[] imageBytes)
    {

        string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";
        UnityWebRequest www = UnityWebRequest.Put(uri, imageBytes);

        www.method = "POST";

        www.SetRequestHeader("Ocp-Apim-Subscription-Key", "0123f7913ff145768a1505b60b487903");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var body = www.downloadHandler.text;

            var wrapperBody = "{\"results\": " + body + "}";

            try
            {
                var results = JsonUtility.FromJson<ArrayWrapper>(wrapperBody);

                var emotion = results.results.FirstOrDefault();

                if (emotion != null)
                {
                    var emotionText = emotion.scores.GetMax();
                    this.PlaceText(this.overPosition3(this.emotionPersonPosition), emotionText);
                }

                var i = 0;
            }
            catch (Exception e)
            {
                var x = e;
            }


            //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

            Debug.Log("Form upload complete!");
        }
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CursorSingleton.Instance.transform.position.z);
       
    }

    private void personHandUp()
    {
        int person = 2;
        var positionOverPerson = this.overPosition(this.persons[person]);
        this.PlaceBigText(positionOverPerson, "Need help !");
    }

    private Vector3 overPosition(Vector3 position)
    {
        return new Vector3(position.x, position.y + .5f, position.z);
    }

    private Vector3 overPosition2(Vector3 position)
    {
        return new Vector3(position.x, position.y + .2f, position.z);
    }

    private Vector3 overPosition3(Vector3 position)
    {
        return new Vector3(position.x, position.y + .1f, position.z);
    }


    private Vector3 emotionPersonPosition;

    public static bool init = true;

    private List<Vector3> persons = new List<Vector3>();

    void fakeEmotions()
    {
        foreach(var person in persons)
        {
            this.PlaceText(this.overPosition2(person), getEmotion());
        }
    }

    System.Random rnd = new System.Random();
    string getEmotion()
    {
        var num = rnd.Next(1, 4);

        switch (num)
        {
            case 1:

                return "Happy :)";
            case 2:
                return "Sad :(";
            case 3:
                return "Confused :/";
            case 4:
                return "Surprised :o";
        }
        return "Neutral :|";
    }

    void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
    {
        if (init)
        {
            persons.Add(CursorSingleton.Instance.transform.position);
            this.PlaceText(CursorSingleton.Instance.transform.position, "Person " + persons.Count);

            if(persons.Count == 4)
            {
                Invoke("personHandUp", 15);
            }
            if(persons.Count == 3)
            {
                Invoke("fakeEmotions", 10);
            }
            return;
        }


        // emotion logik
        this.emotionPersonPosition = CursorSingleton.Instance.transform.position;
        takePhoto();
    }
}

[Serializable]
public class ArrayWrapper
{
    public List<EmotionResult> results;
}


[Serializable]
public class EmotionResult
{
    public FaceRectangle faceRectangle;

    public Scores scores;
}

[Serializable]
public class FaceRectangle
{
    public double height;

    public double width;

    public double left;

    public double top;
}

[Serializable]
public class Scores
{
    public double anger;

    public double contempt;

    public double disgust;

    public double fear;

    public double happiness;

    public double neutral;

    public double sadness;

    public double surprise;


    public string GetMax()
    {
        List<KeyValuePair<string, double>> props = new List<KeyValuePair<string, double>>();

        props.Add(new KeyValuePair<string, double>("anger", this.anger));
        props.Add(new KeyValuePair<string, double>("contempt", this.contempt));
        props.Add(new KeyValuePair<string, double>("disgust", this.disgust));
        props.Add(new KeyValuePair<string, double>("fear", this.fear));
        props.Add(new KeyValuePair<string, double>("happiness", this.happiness));
        //props.Add(new KeyValuePair<string, double>("neutral", this.neutral));
        props.Add(new KeyValuePair<string, double>("sadness", this.sadness));
        props.Add(new KeyValuePair<string, double>("surprise", this.surprise));


        var maxValue = props.Max(x => x.Value);

        var max = props.First(x => x.Value == maxValue);

        return max.Key + " -> " + max.Value;
    }
}


/*
    [Serializable]
 public class EmotionResult
{
    public FaceRectangle FaceRectangle;

    public Scores Scores;
}
[Serializable]
public class FaceRectangle
{
    public int Height ;

    public int Width ;

    public int Left ;

    public int Top ;
}
[Serializable]
public class Scores
{
    public float Anger ;

    public float Contempt ;

    public float Disgust ;

    public float Fear ;

    public float Happiness ;

    public float Neutral ;

    public float Sadness ;

    public float Surprise ;
}
     
*/
