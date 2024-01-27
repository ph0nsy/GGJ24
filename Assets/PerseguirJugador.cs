using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerseguirJugador : MonoBehaviour
{
    public Transform jugador; // El objeto que el enemigo persigue (el jugador).
    private NavMeshAgent agente; // Referencia al componente NavMeshAgent.

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Verificar si el jugador (objetivo) y el agente existen.
        if (jugador != null && agente != null)
        {
            // Configurar el destino del agente para que persiga al jugador.
            agente.SetDestination(jugador.position);
        }
    }
}

