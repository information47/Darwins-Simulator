using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepopSliderController : MonoBehaviour
{
    [SerializeField] private Slider repopSlider;
    [SerializeField] private Text sliderText;
    public float sliderValue;
    public GameObject levelControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        repopSlider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0");
            sliderValue = repopSlider.value;
            // Envois la starting population au levelController
            levelControllerObject.GetComponent<LevelController>().repopingLimit = (int)sliderValue;

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
