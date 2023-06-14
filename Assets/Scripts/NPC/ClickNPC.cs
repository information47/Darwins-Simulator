using UnityEngine;

public class ClickNPC : MonoBehaviour
{

    public GameObject healthBarCanvas;
    public GameObject energybarCanvas;

    private void Start()
    {
        energybarCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        // Ajoutez ici le code à exécuter lorsque le NPC est cliqué
    }

    private void OnMouseEnter()
    {
        energybarCanvas.SetActive(true); // Active le canvas de la barre de santé

        // Ajoutez ici le code à exécuter lorsque la souris entre sur le NPC
    }

    private void OnMouseExit()
    {
        energybarCanvas.SetActive(false); // Active le canvas de la barre de santé

        // Ajoutez ici le code à exécuter lorsque la souris quitte le NPC
    }
}
