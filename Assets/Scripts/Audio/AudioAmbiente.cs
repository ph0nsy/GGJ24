using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbiente : MonoBehaviour
{
    private AudioSource audioSource;

    // Asigna el archivo de audio en el Inspector.
    public AudioClip cancion;

    // Ajusta el volumen deseado en el Inspector (0.0f a 1.0f).
    public float volumen = 1.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Asigna la canción al AudioSource.
        audioSource.clip = cancion;

        // Establece el volumen.
        audioSource.volume = volumen;

        // Configura el loop para que la canción se reproduzca en bucle.
        audioSource.loop = true;

        // Comienza a reproducir la canción.
        audioSource.Play();
    }
}
