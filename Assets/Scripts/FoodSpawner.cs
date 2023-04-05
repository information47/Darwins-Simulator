using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    public GameObject[] listFood;

    // Update is called once per frame
    void Update()
    {
        listFood = GameObject.FindGameObjectsWithTag("Food");
        Spawn();
        
    }

    private void Spawn()
    {
        if (listFood.Length == 0)
        {
            for (int i = 0; i < 11; i++)
            {
                Vector3 randomSpawn = new Vector3(Random.Range(-13, 11), 6, Random.Range(-18, 0));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
        
    }
}
