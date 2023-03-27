using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject theNPC;
    public int xPos, zPos, npcCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NPC_Drop());
    }

    IEnumerator NPC_Drop()
    {
        while(npcCount < 10) {
            xPos = Random.Range(-1, 11);
            zPos = Random.Range(0, -14);
            Instantiate(theNPC, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            npcCount += 1;

        }
    }
}
