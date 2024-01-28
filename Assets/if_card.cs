using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class if_card : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    public GameObject card;
    public GameObject texto;
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

        if( ((player_vector-card_vector).magnitude < min_dist) && (Input.GetKeyDown(KeyCode.E)) ) {
            card.SetActive(false);
        } // si (player < min_dist) and (keydown.E) "coge" tarjeta
        
        if ((card.activeSelf == false) && ((player_vector-door_vector).magnitude < min_dist) && (Input.GetKeyDown(KeyCode.E))) {
            door.SetActive(false);
        } // si (tarjeta "cogida") and (player < min_dist) and (keydow.E) "abre" puerta
        
        if ((card.activeSelf == true) && ((player_vector-door_vector).magnitude < min_dist) && (Input.GetKeyDown(KeyCode.E))) {
            texto.SetActive(true);
            texto.transform.GetComponent<TMP_Text>().text = "Necesito una tarjeta";
        }
        if((player_vector-door_vector).magnitude > (min_dist+1)) {    
            texto.SetActive(false);
        }

        
        
    }


}
