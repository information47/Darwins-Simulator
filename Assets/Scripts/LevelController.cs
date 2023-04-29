using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject floor;

    public GameObject wallUp;
    public GameObject wallDown;
    public GameObject wallRight;
    public GameObject wallLeft;


    public GameObject slider;
    public float floorSize;



    // Start is called before the first frame update
    void Start()
    {
        floor.transform.localScale = new Vector3(floorSize, 0.1f, floorSize);
        
        wallUp.transform.localScale = new Vector3(floorSize, 3, 0);
        wallUp.transform.position = new Vector3(0, 1, floorSize/2);

        wallDown.transform.localScale = new Vector3(floorSize, 3, 0);
        wallDown.transform.position = new Vector3(0, 1, floorSize / -2);

        wallLeft.transform.localScale = new Vector3(0, 3, floorSize);
        wallLeft.transform.position = new Vector3(floorSize / -2, 1, 0);

        wallRight.transform.localScale = new Vector3(0, 3, floorSize);
        wallRight.transform.position = new Vector3(floorSize / 2, 1, 0);
    }

}
