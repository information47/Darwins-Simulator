
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npc;
    public GameObject[] listnpc;

    // Update is called once per frame
    void Update()
    {
        listnpc = GameObject.FindGameObjectsWithTag("NPC");

        if (listnpc.Length < 1)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(-13,11), 6 , Random.Range(-18,0));
            Instantiate(npc,randomSpawn,Quaternion.identity);
        }      
    }
}
