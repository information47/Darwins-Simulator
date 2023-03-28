using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFood : MonoBehaviour
{
    public GameObject theFood; // Pr�fabriqu� de l'objet Food
    public int xPos, zPos, foodCount; // Coordonn�es x et z pour la position al�atoire des Food et le compteur Food

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Food_Drop()); // D�marre la coroutine Food_Drop
    }

    IEnumerator Food_Drop()
    {
        while (foodCount < 10)
        { // Tant que le nombre de Food cr��s est inf�rieur � 10
            xPos = Random.Range(-1, 11); // D�finit une position x al�atoire dans une plage de valeurs donn�es
            zPos = Random.Range(0, -14); // D�finit une position z al�atoire dans une plage de valeurs donn�es
            Instantiate(theFood, new Vector3(xPos, 1, zPos), Quaternion.identity); // Instancie un objet Food � une position al�atoire
            yield return new WaitForSeconds(0.1f); // Attend 0.1 seconde avant la cr�ation de l'objet Food suivant
            foodCount += 1; // Incr�mente le nombre de Food cr��s

        }
    }
}
