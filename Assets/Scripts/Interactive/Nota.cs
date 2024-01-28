using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Nota : MonoBehaviour
{
    public Transform player;
    public Transform childSprite;
    public Transform childCanvas;
    float rangoMovimiento = 0.01f;
    float alturaInicial = 0f;
    public float distanciaInteraccion = 2f;
    public float i = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        alturaInicial = childSprite.position.y;
    }

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
            childCanvas.GetChild(1).gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(childCanvas.GetChild(0).gameObject.activeSelf){
                    gameObject.SetActive(false);
                }

                childCanvas.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "";
                childCanvas.GetChild(1).gameObject.SetActive(false);

                childCanvas.GetChild(0).gameObject.SetActive(true);
                NotasController controller = transform.parent.GetComponent<NotasController>();
                childCanvas.GetChild(0).gameObject.GetComponent<Image>().sprite = controller.sprites[0];
                controller.sprites.RemoveAt(0);
            }
        }
        else
        {
            childCanvas.GetChild(1).gameObject.SetActive(false);
            if(childCanvas.GetChild(0).gameObject.activeSelf)
                gameObject.SetActive(false);
        }

    }

    // Movimiento
    void FixedUpdate(){
        if(i<1f)
            i += Time.deltaTime;
        else
            i = 0f;
        childSprite.LookAt(player.position);
        childSprite.transform.Translate(Vector3.up * rangoMovimiento * Mathf.Sin(2 * Mathf.PI * i));
    }
}
