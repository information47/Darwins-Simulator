using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{

    public float speed; // vitesse de mouvement
    public float changeInterval; // intervalle de temps entre chaque changement de direction al�atoire

    private float lastChangeTime; // temps du dernier changement de direction al�atoire
    private Vector3 movementDirection; // direction de mouvement actuelle

    // Initialisation
    void Start()
    {
        movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)); // direction de mouvement initiale al�atoire
        speed = 3f; // vitesse de mouvement par d�faut
        changeInterval = 3.0f; // intervalle de temps par d�faut
        lastChangeTime = Time.time; // initialisation du temps du dernier changement de direction al�atoire
    }

    // Mise � jour du mouvement
    void Update()
    {
        // v�rifie si l'interval de temps entre chaque changement de direction al�atoire est �coul�
        if (Time.time - lastChangeTime > changeInterval)
        {
            // change la direction de mouvement al�atoirement
            movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            // met � jour le temps du dernier changement de direction al�atoire
            lastChangeTime = Time.time;
        }
        // d�place l'objet dans sa direction de mouvement actuelle avec sa vitesse de mouvement
        transform.position += movementDirection * speed * Time.deltaTime;
    }
}
