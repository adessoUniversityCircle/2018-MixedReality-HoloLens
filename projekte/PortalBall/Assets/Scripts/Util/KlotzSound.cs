using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlotzSound : MonoBehaviour
{

    public AudioClip soundFile;


    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = soundFile;
    }

    void OnCollisionEnter() 
    {
        GetComponent<AudioSource>().Play();
    }
}
