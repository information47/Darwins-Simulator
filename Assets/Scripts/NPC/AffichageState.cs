using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageState : MonoBehaviour
{
    public TextMesh textMesh;
    public GameObject npc;
    private int age;
    private int Food;
    // Start is called before the first frame update
    void Start()
    {
       /* age = npc.age;
        Food = npc.food;*/
        
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Texte dynamique";
    }
}
