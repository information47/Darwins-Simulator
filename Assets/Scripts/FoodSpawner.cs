using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    public GameObject[] listFood;
    private float timer = 0f;
    private bool eaten = false;

    // Update is called once per frame
    void Update()
    {
        listFood = GameObject.FindGameObjectsWithTag("Food");
        Spawn();
        
    }

    private void Spawn()
    {
        if (listFood.Length < 2)
        {
            eaten = true;
            Vector3 randomSpawn = new Vector3(Random.Range(-9, 8), 6, Random.Range(-17, 0));
            Instantiate(food, randomSpawn, Quaternion.identity);
            timer += Time.deltaTime;
        }
        
    }
}
