using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NpcController : MonoBehaviour
{
    // [SerializeField] private GameObject Npc;
    private FieldOfView fow;

    // color
    private Renderer rend;
    public ParticleSystem deathParticles;

    // Raycast
    private RayCastController rayCastController;

    [SerializeField] private float hitDivider = 1f;
    [SerializeField] private float rayDistance = 50f;

    //Camera && Popup
    public Camera npcCamera;
    public Canvas Popup;
    private int FoodEated = 0;
    private float tempsDeCreation ;
    float tempsEcoule;
    float age;
    public Text textComponent;
    public Text textFood;
    public Text textEnergy;
    public Text textVitality;

    // movement
    private int food;
    private Vector3 lastPosition;
    private float distanceTraveled;

    // carateristiques
    [SerializeField] private float energy = 100;
    [SerializeField] private float vitality = 100;
    private float energyDecrease = 0.5f;
    private float energyLimit = 30;
    private int energyToReproduce = 130;
    private float vitalityLoss = 0.2f;
    private float speedMultiplier = 4;


    // network
    public NeatNetwork myNetwork;
    private int id;
    private int inputNodes;
    private int outputNodes;
    
    //inputs for neural network
    [SerializeField] private float[] inputs;

    //outputs of neural network
    [SerializeField] private float[] outputs;

    private float fitness;

    private void Awake()
    {
        // R�cup�rer la r�f�rence � la cam�ra sp�cifique au NPC
        npcCamera = GetComponentInChildren<Camera>();
        npcCamera.enabled = false;
        Popup.gameObject.SetActive(false);
    }
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private float maxVitality = 100;

    [SerializeField] private Energybar energybar;
    [SerializeField] private float maxEnergy = 100;

    private void Start()
    {
        healthbar.UpdateHealthBar(maxVitality, vitality);
        //energybar.UpdateEnergyBar(maxEnergy, energy);

        inputs = new float[inputNodes];
        rayCastController = new RayCastController();
        rend = GetComponent<Renderer>();
        fow = GetComponent<FieldOfView>();

        //age compteur
        tempsDeCreation = Time.time;


        // modify NPC size
        // this.transform.localScale = new Vector3((float)1.5, 1, (float)1.5); 

    }

    void Update()
    {
        EnergyLoss();

        if (vitality <= 0)
        {
            Death();
        }

        // fetch datas from sensors
        InputSensors();

        // send sensors data as input in the network
        outputs = myNetwork.FeedForwardNetwork(inputs);

        MoveNPC(Mathf.Abs(outputs[0]), outputs[1]);

        AgeCounter();
        string formatedage = age.ToString("F2");
        textComponent.text = formatedage ;
        textFood.text = FoodEated.ToString("N0");
        textEnergy.text = energy.ToString("N0");
        textEnergy.text = energy.ToString("N0");
        textVitality.text = vitality.ToString("N0");
    }
    

    private void InputSensors()
    {
        // these sensors return rayHit distance if ray touch a wall (three directions)
        inputs[0] = (rayCastController.RayHit(transform.position, transform.forward, "Wall", rayDistance, 1f));
        inputs[1] = (rayCastController.RayHit(transform.position, transform.forward + transform.right, "Wall", rayDistance, hitDivider));
        inputs[2] = (rayCastController.RayHit(transform.position, transform.forward - transform.right, "Wall", rayDistance, hitDivider));


        inputs[3] = fow.ClosetTargetDist();
        inputs[4] = fow.ClosetTargetAngle(divider: 2);
        inputs[5] = energy / 20;
        inputs[6] = vitality / 20;
    }

    void MoveNPC(float speed, float rotation)
    {
        // get position
        Vector3 input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, speed * speedMultiplier), 1f);
        input = transform.TransformDirection(input);

        // move NPC
        transform.position += input * Time.deltaTime;

        // rotation of NPC
        transform.eulerAngles += new Vector3(0, (rotation * 90), 0) * Time.deltaTime;
    }
    
    void EnergyLoss()
    {
        // consommation du cerveau en energie
        double brainConsumption = myNetwork.Connections.Count * 0.001;
        energy -= (float)brainConsumption;

        //calcul de la taille du NPC
        float size = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        
        // calcul de la distance parcourue
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        //diminution de l'energie en fonction du temps
        energy -= energyDecrease * 0.5f * Time.deltaTime;

        // diminution de l'�nergie en fonction de la distance parcourue et de la taille du NPC
        energy -= distanceTraveled * energyDecrease/2 * size ;
        distanceTraveled = 0f;

        //Energybar.UpdateEnergyBar(maxEnergy, energy); // MISE A JOUR DE ENERGYBAR

         vitality -= LossBasedOnEnergy();

        if ( energy >= energyToReproduce)
        {
            //Reproduce();
        }
        
        healthbar.UpdateHealthBar(maxVitality, vitality);

    }

    void Reproduce()
    {
        energy /= 2f; // transfert de la moitie de l'energie au nouvel enfant

        Vector2 randomCircle = Random.insideUnitCircle * 2f; // g�n�re une position al�atoire dans un cercle de rayon 2 autour du parent
        Vector3 childPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
        // Instantiate(Npc, childPosition, Quaternion.identity); // cr�e un nouvel objet NPC



    }
    private float LossBasedOnEnergy()
    {
        double loss = -0.0001 * (energy - energyLimit);
        if (loss<0) loss = 0;
       
        return (float)loss;
    }

    private void Death()
    {
        // Si le Popup de ce NPC est actuellement affiché
        if (Popup.gameObject.activeInHierarchy)
        {
            // Cacher le Popup avant de détruire le NPC
            Popup.gameObject.GetComponent<PopupController>().HidePopup();
        }

        GameObject.FindObjectOfType<NPCManager>().Death(fitness, id);
        ParticleSystem particleSystemInstance = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particleSystemInstance.gameObject, 2.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            this.food += 1;
            energy += 10;
            fitness++;
            FoodEated++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Obs"))
        {
            // fitness = 0;
            Death();
        }
    }

    private void OnMouseDown()
    {
        // Appeler une fonction pour afficher le canvas ou activer le GameObject WindowGraph
        if (npcCamera != null)
        {
            // Utilise la méthode SwitchCamera pour changer de caméra
            GameScript.Instance.SwitchCamera(npcCamera);

            // Cache tous les autres popups    // Si le Popup de ce NPC est actuellement affiché
            if (Popup.gameObject.activeInHierarchy)
            {
                // Cacher le Popup avant de détruire le NPC
                Popup.gameObject.GetComponent<PopupController>().HidePopup();
            }
            GameScript.Instance.HideAllPopups();

            //affiche Popup 
            Popup.gameObject.GetComponent<PopupController>().ShowPopup(myNetwork);
        }
    }

    private void AgeCounter()
    {
        tempsEcoule = Time.time - tempsDeCreation;
        age = tempsEcoule/10;
        

    }

    // getters and setters
    public int Id { get => id; set => id = value; }
    public int InputNodes { get => inputNodes; set => inputNodes = value; }
    public int OutputNodes { get => outputNodes; set => outputNodes = value; }
}
