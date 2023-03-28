using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public int compteur = 0;

    public float speed; // vitesse de mouvement
    public float changeInterval; // intervalle de temps entre chaque changement de direction al�atoire

    private float lastChangeTime; // temps du dernier changement de direction al�atoire
    private Vector3 movementDirection; // direction de mouvement actuelle

    Rigidbody rb;

    // Initialisation
    void Start()
    {
        movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)); // direction de mouvement initiale al�atoire
        speed = 3f; // vitesse de mouvement par d�faut
        changeInterval = 3.0f; // intervalle de temps par d�faut
        lastChangeTime = Time.time; // initialisation du temps du dernier changement de direction al�atoire
        rb = GetComponent<Rigidbody>();
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
            compteur ++ ;
        }

        // lance un rayon devant le NPC pour d�tecter une collision
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementDirection, out hit, 1f))
        {
            // si une collision est d�tect�e, change la direction de mouvement al�atoirement
            movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }

        // d�place l'objet dans sa direction de mouvement actuelle avec sa vitesse de mouvement
        transform.position += movementDirection * speed * Time.deltaTime;

        // Rotation du personnage
        if (movementDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0f, movementDirection.z));
        }

        if (compteur == 5)
        {
            Destroy(this.gameObject);
        }
    }
}
