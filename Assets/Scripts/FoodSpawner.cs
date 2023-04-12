using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    public GameObject[] listFood;
    public int timer = 0;

    // Update is called once per frame
    void Update()
    {
        listFood = GameObject.FindGameObjectsWithTag("Food");
        timer++;
        
        if (timer > 5000)
        {
            timer = 0;
            Spawn();
        }

    }

    private void Spawn()
    {
        if (listFood.Length < 10 - listFood.Length )
        {
            for (int i = 0; i < 10 - listFood.Length; i++)
            {
                Vector3 randomSpawn = new Vector3(Random.Range(-11, 10), 6, Random.Range(-36, -54));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
    }


}
