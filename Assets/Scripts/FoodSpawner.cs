using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    public GameObject[] listFood;
    public float timer = 0;
    public LevelController levelController;
    private float floorSize;

    private void Start()
    {
        floorSize = levelController.floorSize;
    }

    // Update is called once per frame
    void Update()
    {
        listFood = GameObject.FindGameObjectsWithTag("Food");
        timer += Time.deltaTime;
        
        if (timer > 1)
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
                Vector3 randomSpawn = new Vector3(Random.Range(floorSize / -2, (floorSize / 2)), 1, Random.Range(floorSize / -2, floorSize / 2));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
    }


}
