using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class if_card : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    public GameObject card;
    public GameObject texto_nota;
    public GameObject texto_personaje;

    public float min_dist = 2f;
    // Start is called before the first frame update
    void Start()
    {
        /*
            1. Interactuar con llave (desaparece)
            2. Si NO llave y interactuar con puerta (mensaje falta llave)
            3. Si SI llave y interactuar con puerta (puerta desaparece)
        */

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_vector = new Vector3();
        Vector3 card_vector = new Vector3();
        Vector3 door_vector = new Vector3();

        player_vector = player.transform.position;
        card_vector = card.transform.position;
        door_vector = door.transform.position;

        if( ((player_vector-card_vector).magnitude < (min_dist-1)) && (card.activeSelf == true) ) {
            texto_nota.SetActive(true);
        } 

        if( ((player_vector-card_vector).magnitude > min_dist) ) {
            texto_nota.SetActive(false);
        } 

        if( ((player_vector-card_vector).magnitude < min_dist) && (Input.GetKeyDown(KeyCode.E)) ) {
            card.SetActive(false);
            texto_nota.SetActive(false);
        } // si (player < min_dist) and (keydown.E) "coge" tarjeta
        
        if ((card.activeSelf == false) && ((player_vector-door_vector).magnitude < min_dist) && (Input.GetKeyDown(KeyCode.E))) {
            door.SetActive(false);
        } // si (tarjeta "cogida") and (player < min_dist) and (keydow.E) "abre" puerta
        
        if ((card.activeSelf == true) && ((player_vector-door_vector).magnitude < min_dist) && (Input.GetKeyDown(KeyCode.E))) {
            texto_personaje.SetActive(true);
        }
        if((player_vector-door_vector).magnitude > (min_dist+1)) {    
            texto_personaje.SetActive(false);
        }

        
        
    }


}
