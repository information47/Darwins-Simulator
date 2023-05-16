using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSliderController : MonoBehaviour
{
    [SerializeField] private Slider foodSlider;
    [SerializeField] private Text sliderText;
    public float sliderValue;
    public GameObject levelControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        foodSlider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0");
            sliderValue = foodSlider.value;
            // Envois la starting population au levelController
            levelControllerObject.GetComponent<LevelController>().FoodNumber = (int)sliderValue;

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
