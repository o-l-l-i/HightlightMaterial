using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HighlightMaterial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Please assign this.")]
    public Material highlightMaterial;
    GameObject selectedObject;
    Material[] originalMaterials;
    Material[] sharedMaterials;
    Renderer selectedRenderer;


    // When the pointer enters the object:
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter.");
        selectedObject = eventData.pointerCurrentRaycast.gameObject;
        selectedRenderer = selectedObject.gameObject.GetComponent<Renderer>();

        if (selectedRenderer != null)
        {
            originalMaterials = selectedRenderer.materials;
            Debug.Log(selectedObject.name + "  has " + originalMaterials.Length + " materials in the mesh renderer.");

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Debug.Log("Material " + i + ": " + originalMaterials[i].name);
            }
            ChangeMaterial();
        }
    }


    // When the pointer exits the object:
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit.");
        Debug.Log(selectedObject.name + " restoring materials.");
        RestoreMaterial();
    }


    // Applies the new material to each material of the object.
    public void ChangeMaterial()
    {
        Debug.Log("ChangeMaterial.");
        if (selectedRenderer != null)
        {
            sharedMaterials = selectedRenderer.materials;

            for (int i = 0; i < sharedMaterials.Length; i++)
            {
                sharedMaterials[i] = new Material(highlightMaterial);
            }

            selectedRenderer.materials = sharedMaterials;
        }
    }


    // Restores materials.
    public void RestoreMaterial()
    {
        Debug.Log("RestoreMaterial.");
        if (sharedMaterials != null)
        {
            for (int i = 0; i < sharedMaterials.Length; i++)
            {
                if (originalMaterials[i] != null)
                {
                    sharedMaterials[i] = originalMaterials[i];
                    Debug.Log("sharedMaterials " + i + ", assigned: " + originalMaterials[i].name );
                }
            }
            if (selectedRenderer != null)
            {
                selectedRenderer.materials = sharedMaterials;
            }
        }
    }

}