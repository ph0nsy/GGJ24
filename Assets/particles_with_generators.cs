using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles_with_generators : MonoBehaviour
{
    public GameObject card;
    public GameObject[] generadores;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool particles = true;

        foreach (GameObject generator in generadores) {
            if (generator.GetComponent<CuboInteractivo>().var < 100) {
                particles = false;
            }
        }

        if(particles) {
            ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
            em.enabled = true;
        }
    }
}
