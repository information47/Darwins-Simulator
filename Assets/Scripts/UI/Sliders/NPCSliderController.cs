using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSliderController : MonoBehaviour
{
    [SerializeField] private Slider npcSlider;
    [SerializeField] private Text sliderText;
    public float sliderValue;
    public GameObject levelControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        npcSlider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0");
            sliderValue = npcSlider.value;
            // Envois la starting population au levelController
            levelControllerObject.GetComponent<LevelController>().startingPopulation = (int)sliderValue;

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
