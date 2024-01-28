using TMPro;
using UnityEngine;

public class CuboInteractivo : MonoBehaviour
{
    public float distanciaInteraccion = 2f;
    public GameObject jugador;
    private bool estaPresionandoE = false;
    public float var = 0;
    public float incrementoVar = 10f;
    public GameObject texto;

    void Update()
    {
        // Obt�n el vector de direcci�n del jugador hacia el cubo.
        Vector3 direccionJugadorAlCubo = transform.position - jugador.transform.position;
        direccionJugadorAlCubo.Normalize();

        // Obt�n la direcci�n hacia donde est� mirando el jugador.
        Vector3 direccionMiradaJugador = jugador.transform.forward;

        // Calcula el producto punto entre las dos direcciones.
        float productoPunto = Vector3.Dot(direccionJugadorAlCubo, direccionMiradaJugador);

        // Comprueba si el jugador est� mirando al cubo y est� dentro de la distancia de interacci�n.
        if (productoPunto > 0 && Vector3.Distance(transform.position, jugador.transform.position) < distanciaInteraccion)
        {
            texto.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                estaPresionandoE = true;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                estaPresionandoE = false;
                Debug.Log("Soltaste la tecla E");
            }

            if (estaPresionandoE && var < 100)
            {
                var += incrementoVar * Time.deltaTime;
                Debug.Log(var);
                texto.transform.GetComponent<TMP_Text>().text = "Progreso: " + var.ToString("F1") + "%";
            }
        }
        else
        {
            texto.SetActive(false);
        }
    }
}
