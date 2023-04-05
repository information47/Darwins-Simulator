
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npc;
    public GameObject[] listnpc;

    // Update is called once per frame
    void Start()
    {
        listnpc = GameObject.FindGameObjectsWithTag("NPC");

<<<<<<< HEAD
        for (int i=0; i<10; i++)
=======
        if (listnpc.Length < 1)
>>>>>>> a2d766cd4c56f9070c33a1477db8d8a215983891
        {
            Vector3 randomSpawn = new Vector3(Random.Range(0,4), 6 , Random.Range(-12,3));
            Instantiate(npc,randomSpawn,Quaternion.identity);
        }      
    }
}
