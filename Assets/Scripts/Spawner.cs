
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npc;
    public GameObject[] listnpc;

    // Update is called once per frame
    void Update()
    {
        listnpc = GameObject.FindGameObjectsWithTag("NPC");

        if (listnpc.Length < 10)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(0,4), 6 , Random.Range(-12,3));
            Instantiate(npc,randomSpawn,Quaternion.identity);
        }      
    }
}