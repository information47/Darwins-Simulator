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
        Debug.Log("NPC clicked!");
        // Ajoutez ici le code � ex�cuter lorsque le NPC est cliqu�
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse is over NPC");
        energybarCanvas.SetActive(true); // Active le canvas de la barre de sant�

        // Ajoutez ici le code � ex�cuter lorsque la souris entre sur le NPC
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is no longer over NPC");
        energybarCanvas.SetActive(false); // Active le canvas de la barre de sant�

        // Ajoutez ici le code � ex�cuter lorsque la souris quitte le NPC
    }
}
