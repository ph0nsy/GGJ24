using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_pared : MonoBehaviour
{
    
    public GameObject active_;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0.5f,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        bool algo = false;
        foreach (Transform child in transform) {
            if (algo) {
                if(active_.activeSelf == true) {
                    child.GetComponent<Light>().enabled = true;
                } else {
                    child.GetComponent<Light>().enabled = false;
                }
            } else {
                algo = true;
            }
        }
    }

    void OnMouseOver ()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if(active_.activeInHierarchy == true) {
                active_.SetActive(false);
            }
            else {
                active_.SetActive(true);
            }
        }
    }
}
