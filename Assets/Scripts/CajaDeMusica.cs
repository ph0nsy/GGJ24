using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaDeMusica : MonoBehaviour
{
    public float distanciaMaxima = 5f; // Distancia máxima para el volumen mínimo.
    public float distanciaMinima = 1f; // Distancia mínima para el volumen máximo.
    private AudioSource audioSource;
    public GameObject jugador;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Calcula la distancia entre la caja de música y el jugador.
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.transform.position);

        // Mapea la distancia a un valor entre 0 y 1.
        float valorDeVolumen = Mathf.InverseLerp(distanciaMaxima, distanciaMinima, distanciaAlJugador);

        // Ajusta el volumen del AudioSource en función de la distancia.
        audioSource.volume = Mathf.Clamp01(valorDeVolumen);

        // Reproduce el audio si el jugador está lo suficientemente cerca.
        if (distanciaAlJugador <= distanciaMaxima)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Detiene el audio si el jugador está fuera de la distancia para escuchar.
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
