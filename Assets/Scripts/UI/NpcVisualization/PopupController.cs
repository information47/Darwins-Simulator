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
        // fermer popup
        if (Input.GetKeyDown(KeyCode.M))
        {
            HidePopup();
        }
    }

    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
    public void ShowPopup(NeatNetwork network)
    {
        gameObject.SetActive(true);

        // get the NPC windowgraph object
        WindowGraph windowGraph = FindObjectOfType<WindowGraph>();

        if (windowGraph != null)
        {
            windowGraph.ShowNetwork(network);
        }
    }
}
