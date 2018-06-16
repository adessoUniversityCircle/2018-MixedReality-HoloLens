using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.WebCam;
using System.IO;
using System.Net;
using System.Net.Security;
using Newtonsoft.Json.Linq;

public class TapToScan : MonoBehaviour, IInputClickHandler {

    PhotoCapture photoCapture;

    Resolution res;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Hurra!");

        //photoCapture.TakePhotoAsync(@"C:\Users\asd14\Pictures\hololens.jpg", PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
        photoCapture.TakePhotoAsync(OnCapturedPhotoToMemory);
    }

    private void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        Debug.Log(result.success);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start!");


        PhotoCapture.CreateAsync(false, x => OnPhotoCaptureCreated(x));
        
    }

    private void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCapture = captureObject;

        CameraParameters parameters = new CameraParameters();
        res = PhotoCapture.SupportedResolutions.OrderByDescending(supported => supported.height).First();
        parameters.hologramOpacity = 0.0f;
        parameters.cameraResolutionHeight = res.height;
        parameters.cameraResolutionWidth = res.width;
        parameters.pixelFormat = CapturePixelFormat.PNG;

        photoCapture.StartPhotoModeAsync(parameters, x => Debug.Log(x));
    }

    private void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        Debug.Log(photoCaptureFrame.dataLength);
        Debug.Log(photoCaptureFrame.hasLocationData);

        var rgb = new List<byte>();
        photoCaptureFrame.CopyRawImageDataIntoBuffer(rgb);

        //var bytes = File.ReadAllBytes(@"C:\Users\asd14\Pictures\hololens.jpg");
        //var stream = new MemoryStream(bytes);
        var bytes = rgb.ToArray();

        UploadFile(bytes, "https://hologate.tav.cc/image");


        StartCoroutine("DoRequest");
    }

    IEnumerator UploadFileCo(byte[] bytes, string uploadURL)
    {
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);

        // version 2
        postForm.AddBinaryData("file", bytes, "hololensphoto.jpg", "image/jpeg");

        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
        {
            Debug.Log("upload done :" + upload.text);
            var scanResult = JObject.Parse(upload.text);

            string text;
            if (scanResult["barcode"].ToString() == "")
                text = scanResult["face_details"].Count() + " Gesichter gefunden: \n" + scanResult["face_details"].First()["age_range"]["low"] + "-" + scanResult["face_details"].First()["age_range"]["high"] + " Jahre, " + (scanResult["face_details"].First()["gender"]["value"].ToString() == "Male" ? "männlich" : "weiblich") + "\n" + scanResult["face_details"].First()["emotions"].ToString();
            else
                text = scanResult["barcode"].ToString();

            GameObject.Find("TextToSpeechManager").GetComponent<AudioManager>().Say(text);
        }
        else
            Debug.Log("Error during upload: " + upload.error);
    }

    void UploadFile(byte[] bytes, string uploadURL)
    {
        StartCoroutine(UploadFileCo(bytes, uploadURL));
    }

    IEnumerator DoRequest()
    {
        Debug.Log("vor Request");

        /*foreach (FaceDetail face in response.FaceDetails)
        {
            Debug.Log(string.Format("BoundingBox: top={0} left={1} width={2} height={3}", face.BoundingBox.Left,
                face.BoundingBox.Top, face.BoundingBox.Width, face.BoundingBox.Height));
            Debug.Log(string.Format("Confidence: {0}\nLandmarks: {1}\nPose: pitch={2} roll={3} yaw={4}\nQuality: {5}",
                face.Confidence, face.Landmarks.Count, face.Pose.Pitch,
                face.Pose.Roll, face.Pose.Yaw, face.Quality));

        }*/

        yield return null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
