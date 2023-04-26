
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npc;
    public GameObject[] listnpc;

    // Update is called once per frame
    void Start()
    {
        listnpc = GameObject.FindGameObjectsWithTag("NPC");

        for (int i=0; i<15; i++)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(17, 43), 1, Random.Range(-17, -43));
            Instantiate(npc,randomSpawn,Quaternion.identity);
        }      
    }
}
