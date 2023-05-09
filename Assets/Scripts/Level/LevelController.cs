using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject food;
    public GameObject[] listFood;
    public float timer = 0;
    [SerializeField] private int foodNumber;

    public GameObject floor;

    public GameObject wallUp;
    public GameObject wallDown;
    public GameObject wallRight;
    public GameObject wallLeft;

    [SerializeField] private float floorSize;

    public int startingPopulation;





    // Start is called before the first frame update
    void Start()
    {
        // floor and walls setup
        SizeSetup();

    }

    private void Update()
    {
        SpawnFood();
        
    }

    private void SizeSetup()
    {
        floor.transform.localScale = new Vector3(FloorSize, 0.1f, FloorSize);

        wallUp.transform.localScale = new Vector3(FloorSize, 3, 0);
        wallUp.transform.position = new Vector3(0, 1, FloorSize / 2);

        wallDown.transform.localScale = new Vector3(FloorSize, 3, 0);
        wallDown.transform.position = new Vector3(0, 1, FloorSize / -2);

        wallLeft.transform.localScale = new Vector3(0, 3, FloorSize);
        wallLeft.transform.position = new Vector3(FloorSize / -2, 1, 0);

        wallRight.transform.localScale = new Vector3(0, 3, FloorSize);
        wallRight.transform.position = new Vector3(FloorSize / 2, 1, 0);
    }

    private void SpawnFood()
    {
        listFood = GameObject.FindGameObjectsWithTag("Food");
        timer += Time.deltaTime;

        if (listFood.Length < FoodNumber)
        {
            for (int i = 0; i < FoodNumber - listFood.Length; i++)
            {
                Vector3 randomSpawn = new Vector3(Random.Range(FloorSize / -2, (FloorSize / 2)), 1, Random.Range(FloorSize / -2, FloorSize / 2));
                Instantiate(food, randomSpawn, Quaternion.identity);
            }
        }
    }
    
    // getters and setters
    public float FloorSize { get => floorSize; set => floorSize = value; }
    public int FoodNumber { get => foodNumber; set => foodNumber = value; }
    public int StartingPopulation { get => startingPopulation; set => startingPopulation = value; }
}
