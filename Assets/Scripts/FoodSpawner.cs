using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    public GameObject[] listFood;
    public int timer = 0;
    public int maxX ;
    public int minX;
    public int maxZ;
    public int minZ;

    // Update is called once per frame
    void Update()
    {
        listFood = GameObject.FindGameObjectsWithTag(food.tag);
        timer++;
        
        if (timer > 2500)
        {
            timer = 0;
            Spawn();
        }

    }

    private void Spawn()
    {
        if (listFood.Length < 20 - listFood.Length )
        {
            for (int i = 0; i < 15 - listFood.Length; i++)
            {
                Vector3 randomSpawn = new Vector3(Random.Range(minX, maxX), 6, Random.Range(minZ, maxZ));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
    }


}
