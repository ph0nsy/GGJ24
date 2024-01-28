using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject llave;
    public GameObject ENDGAME;
    public AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!llave.gameObject.activeSelf){
            gameObject.SetActive(false);
            ENDGAME.gameObject.SetActive(true);
        }
    }
}
