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
        
        if (timer > 2500)
        {
            timer = 0;
            Spawn();
        }

    }

    private void Spawn()
    {
        if (listFood.Length < 10 - listFood.Length )
        {
            for (int i = 0; i < 15 - listFood.Length; i++)
            {
                Vector3 randomSpawn = new Vector3(Random.Range(16, 44), 1, Random.Range(-16, -44));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
    }


}
