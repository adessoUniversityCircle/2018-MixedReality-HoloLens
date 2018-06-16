using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource source;
    private TextToSpeech tts;

    [SerializeField]
    private GameObject text;
    private TextMesh mesh;

	// Use this for initialization
	void Start () {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        tts = new TextToSpeech();
        tts.AudioSource = source;

        mesh = text.GetComponent<TextMesh>();
	}

    public void Say(string text)
    {
        Debug.Log("Ich will was sagen");
        tts.SpeakSsml("<speak>" + text + "</speak>");

        mesh.text = text;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
