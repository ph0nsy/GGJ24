using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerseguirJugador : MonoBehaviour
{
    public Transform jugador; // El objeto que el enemigo persigue (el jugador).
    private NavMeshAgent agente; // Referencia al componente NavMeshAgent.
    public float fov_distance = 10f;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        // Verificar si el jugador (objetivo) y el agente existen.
        if (jugador != null && agente != null)
        {
            CheckCollisions();
        }
    }

    void CheckCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * fov_distance*3/4, new Vector3(1.25f,0.2f,1.5f)*fov_distance/2f, transform.rotation, LayerMask.GetMask("Player"));
        
        //Check when there is a new collider coming into contact with the box
        if(hitColliders.Length > 0){
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.forward, out hit, (transform.position-jugador.position).magnitude, LayerMask.GetMask("Player"))){
                // Configurar el destino del agente para que persiga al jugador.
                agente.SetDestination(jugador.position);
                transform.LookAt(jugador.position);
            }
        } else {

            // Scan
        
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (true)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position + transform.forward * fov_distance*3/4, new Vector3(1.25f,0.2f,1.5f)*fov_distance);
    }

}

