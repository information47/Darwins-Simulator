
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npc;
    public GameObject[] listnpc;
    public int maxX;
    public int minX;
    public int maxZ;
    public int minZ;
    public int nbspawn;

    // Update is called once per frame
    void Start()
    {
        listnpc = GameObject.FindGameObjectsWithTag(this.npc.tag);

        for (int i=0; i< nbspawn; i++)
        {
            Vector3 randomSpawn = new Vector3(Random.Range(minX,maxX), 6 , Random.Range(minZ,maxZ));
            Instantiate(npc,randomSpawn,Quaternion.identity);
        }      
    }
}
