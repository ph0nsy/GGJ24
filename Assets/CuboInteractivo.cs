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
    private float tiempoInicial = 10f; // Tiempo inicial de espera antes de permitir la interacción.
    private bool puedeInteractuar = false;

    void Start()
    {
        // Iniciar la cuenta atrás de espera.
        Invoke("PermitirInteraccion", tiempoInicial);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, jugador.transform.position) < distanciaInteraccion && puedeInteractuar)
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

    void PermitirInteraccion()
    {
        // Habilitar la interacción después del tiempo de espera.
        puedeInteractuar = true;
    }
}
