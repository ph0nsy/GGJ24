using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luces : MonoBehaviour
{
    public GameObject generadorAsociado;
    public AudioClip sound;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(generadorAsociado.GetComponent<CuboInteractivo>().var >= 100){
            foreach(Transform child in transform){
                child.gameObject.SetActive(true);
            }
            audioSource.PlayOneShot(sound);
        }
    }
}
