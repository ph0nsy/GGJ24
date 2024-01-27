using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2.0f; // Rango de interacción
    private InteractiveObject currentInteractiveObject; // Objeto interactivo actual

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractiveObject != null)
        {
            // Verificar si el jugador está lo suficientemente cerca del objeto interactivo
            float distanceToInteractiveObject = Vector3.Distance(transform.position, currentInteractiveObject.transform.position);

            if (distanceToInteractiveObject <= interactionRange)
            {
                // Incrementar el número del objeto interactivo si no ha alcanzado el máximo
                if (currentInteractiveObject.currentNumber < currentInteractiveObject.maxNumber)
                {
                    currentInteractiveObject.currentNumber++;
                    UpdateNumberUI();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cuando el jugador entra en el rango del objeto interactivo, asigna el objeto actual
        InteractiveObject interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject != null)
        {
            currentInteractiveObject = interactiveObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador sale del rango del objeto interactivo, borra el objeto actual
        InteractiveObject interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject != null && interactiveObject == currentInteractiveObject)
        {
            currentInteractiveObject = null;
        }
    }

    private void UpdateNumberUI()
    {
        // Verificar si el objeto interactivo tiene un renderizador y una textura para mostrar el número
        Renderer renderer = currentInteractiveObject.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            // Actualizar la textura con el número actual
            Material material = renderer.material;
            material.mainTexture = CreateNumberTexture(currentInteractiveObject.currentNumber);
        }
    }

    private Texture2D CreateNumberTexture(int number)
    {
        // Crea una textura dinámica con el número y la devuelve
        // Aquí puedes personalizar la apariencia de la textura según tus necesidades

        Texture2D texture = new Texture2D(128, 128);
        Color[] pixels = new Color[128 * 128];

        // Rellenar la textura con el número
        string numberText = number.ToString();
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.black;
        style.fontSize = 40;

        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {
                pixels[x + y * 128] = Color.clear;
            }
        }

        GUI.skin.label = style;
        GUI.Label(new Rect(0, 0, 128, 128), numberText);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
