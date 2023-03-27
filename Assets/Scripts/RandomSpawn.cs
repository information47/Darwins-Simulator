using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject theNPC; // Pr�fabriqu� de l'objet NPC
    public int xPos, zPos, npcCount; // Coordonn�es x et z pour la position al�atoire des NPC et le compteur NPC

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NPC_Drop()); // D�marre la coroutine NPC_Drop
    }

    IEnumerator NPC_Drop()
    {
        while (npcCount < 10)
        { // Tant que le nombre de NPC cr��s est inf�rieur � 10
            xPos = Random.Range(-1, 11); // D�finit une position x al�atoire dans une plage de valeurs donn�es
            zPos = Random.Range(0, -14); // D�finit une position z al�atoire dans une plage de valeurs donn�es
            Instantiate(theNPC, new Vector3(xPos, 1, zPos), Quaternion.identity); // Instancie un objet NPC � une position al�atoire
            yield return new WaitForSeconds(0.1f); // Attend 0.1 seconde avant la cr�ation de l'objet NPC suivant
            npcCount += 1; // Incr�mente le nombre de NPC cr��s

        }
    }
}
