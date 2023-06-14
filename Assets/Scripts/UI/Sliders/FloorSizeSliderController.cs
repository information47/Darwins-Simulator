using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorSizeSliderController : MonoBehaviour
{
    [SerializeField] private Slider floorSizeSlider;
    [SerializeField] private Text sliderText;
    public float sliderValue;
    public GameObject levelControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        floorSizeSlider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0");
            sliderValue = floorSizeSlider.value;
            // Envois la starting population au levelController
            levelControllerObject.GetComponent<LevelController>().FloorSize = (int)sliderValue;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
