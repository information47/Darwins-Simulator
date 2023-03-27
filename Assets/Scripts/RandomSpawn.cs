using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject theNPC; // Préfabriqué de l'objet NPC
    public int xPos, zPos, npcCount; // Coordonnées x et z pour la position aléatoire des NPC et le compteur NPC

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NPC_Drop()); // Démarre la coroutine NPC_Drop
    }

    IEnumerator NPC_Drop()
    {
        while (npcCount < 10)
        { // Tant que le nombre de NPC créés est inférieur à 10
            xPos = Random.Range(-1, 11); // Définit une position x aléatoire dans une plage de valeurs données
            zPos = Random.Range(0, -14); // Définit une position z aléatoire dans une plage de valeurs données
            Instantiate(theNPC, new Vector3(xPos, 1, zPos), Quaternion.identity); // Instancie un objet NPC à une position aléatoire
            yield return new WaitForSeconds(0.1f); // Attend 0.1 seconde avant la création de l'objet NPC suivant
            npcCount += 1; // Incrémente le nombre de NPC créés

        }
    }
}
