using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    public Camera OtherCam;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        if (OtherCam.enabled != true)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.position - OtherCam.transform.position);
        }
    }
}
