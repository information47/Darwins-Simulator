using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{

    public float speed; // vitesse de mouvement
    public float changeInterval; // intervalle de temps entre chaque changement de direction aléatoire

    private float lastChangeTime; // temps du dernier changement de direction aléatoire
    private Vector3 movementDirection; // direction de mouvement actuelle

    Rigidbody rb;

    // Initialisation
    void Start()
    {
        movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)); // direction de mouvement initiale aléatoire
        speed = 3f; // vitesse de mouvement par défaut
        changeInterval = 3.0f; // intervalle de temps par défaut
        lastChangeTime = Time.time; // initialisation du temps du dernier changement de direction aléatoire
        rb = GetComponent<Rigidbody>();
    }

    // Mise à jour du mouvement
    void Update()
    {
        // vérifie si l'interval de temps entre chaque changement de direction aléatoire est écoulé
        if (Time.time - lastChangeTime > changeInterval)
        {
            // change la direction de mouvement aléatoirement
            movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            // met à jour le temps du dernier changement de direction aléatoire
            lastChangeTime = Time.time;
        }

        // lance un rayon devant le NPC pour détecter une collision
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementDirection, out hit, 1f))
        {
            // si une collision est détectée, change la direction de mouvement aléatoirement
            movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }

        // déplace l'objet dans sa direction de mouvement actuelle avec sa vitesse de mouvement
        transform.position += movementDirection * speed * Time.deltaTime;

        // Rotation du personnage
        if (movementDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0f, movementDirection.z));
        }
    }
}
