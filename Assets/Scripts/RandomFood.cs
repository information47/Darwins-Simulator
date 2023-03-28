using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFood : MonoBehaviour
{
    public GameObject theFood; // Préfabriqué de l'objet Food
    public int xPos, zPos, foodCount; // Coordonnées x et z pour la position aléatoire des Food et le compteur Food

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Food_Drop()); // Démarre la coroutine Food_Drop
    }

    IEnumerator Food_Drop()
    {
        while (foodCount < 10)
        { // Tant que le nombre de Food créés est inférieur à 10
            xPos = Random.Range(-1, 11); // Définit une position x aléatoire dans une plage de valeurs données
            zPos = Random.Range(0, -14); // Définit une position z aléatoire dans une plage de valeurs données
            Instantiate(theFood, new Vector3(xPos, 1, zPos), Quaternion.identity); // Instancie un objet Food à une position aléatoire
            yield return new WaitForSeconds(0.1f); // Attend 0.1 seconde avant la création de l'objet Food suivant
            foodCount += 1; // Incrémente le nombre de Food créés

        }
    }
}
