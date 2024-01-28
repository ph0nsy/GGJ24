using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notaVariant : MonoBehaviour
{
public Transform player;
    public Transform childCanvas;
    float rangoMovimiento = 0.01f;
    float alturaInicial = 0f;
    public float distanciaInteraccion = 2f;
    public float i = 0.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // Obt�n el vector de direcci�n del jugador hacia el cubo.
        Vector3 direccionJugadorNota = transform.position - player.transform.position;
        direccionJugadorNota.Normalize();

        // Obt�n la direcci�n hacia donde est� mirando el jugador.
        Vector3 direccionMiradaJugador = player.transform.forward;

        // Calcula el producto punto entre las dos direcciones.
        float productoPunto = Vector3.Dot(direccionJugadorNota, direccionMiradaJugador);

        // Comprueba si el jugador est� mirando al cubo y est� dentro de la distancia de interacci�n.
        if (productoPunto > 0 && Vector3.Distance(transform.position, player.transform.position) < distanciaInteraccion)
        {
            childCanvas.GetChild(0).gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                    gameObject.SetActive(false);
            }
        }
        else
        {
            childCanvas.GetChild(0).gameObject.SetActive(false);
        }

    }
}
