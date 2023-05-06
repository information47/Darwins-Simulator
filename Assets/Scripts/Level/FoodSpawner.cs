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
    [SerializeField] private int foodNumber;


    private void Start()
    {
        floorSize = levelController.FloorSize;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFood();

    }

    private void SpawnFood()
    {
        listFood = GameObject.FindGameObjectsWithTag("Food");
        timer += Time.deltaTime;

        if (listFood.Length < FoodNumber)
        {
            for (int i = 0; i < FoodNumber - listFood.Length; i++)
            {
                Vector3 randomSpawn = new Vector3(Random.Range(floorSize / -2, (floorSize / 2)), 1, Random.Range(floorSize / -2, floorSize / 2));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
    }

    // getters and setters
    public int FoodNumber { get => foodNumber; set => foodNumber = value; }


}
