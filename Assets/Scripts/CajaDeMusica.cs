using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaDeMusica : MonoBehaviour
{
    public float distanciaMaxima = 5f; // Distancia m�xima para el volumen m�nimo.
    public float distanciaMinima = 1f; // Distancia m�nima para el volumen m�ximo.
    private AudioSource audioSource;
    public GameObject jugador;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Calcula la distancia entre la caja de m�sica y el jugador.
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.transform.position);

        // Mapea la distancia a un valor entre 0 y 1.
        float valorDeVolumen = Mathf.InverseLerp(distanciaMaxima, distanciaMinima, distanciaAlJugador);

        // Ajusta el volumen del AudioSource en funci�n de la distancia.
        audioSource.volume = Mathf.Clamp01(valorDeVolumen);

        // Reproduce el audio si el jugador est� lo suficientemente cerca.
        if (distanciaAlJugador <= distanciaMaxima)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Detiene el audio si el jugador est� fuera de la distancia para escuchar.
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
