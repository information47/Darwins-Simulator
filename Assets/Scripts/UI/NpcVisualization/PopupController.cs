using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        HidePopup();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HidePopup()
    {

        gameObject.SetActive(false);
    }
}
