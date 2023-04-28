using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public Text sliderText;
    public float sliderValue;

    private void Start()
    {
        // On s'abonne à l'événement OnValueChanged du slider
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        sliderText.text = value.ToString();
        sliderValue = value;
    }
}
