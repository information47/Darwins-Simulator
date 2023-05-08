using UnityEngine;
using Random = UnityEngine.Random;

public class NpcController : MonoBehaviour
{
    // [SerializeField] private GameObject Npc;
    private FieldOfView fow;

    // color
    private Renderer rend;

    // Raycast
    private RayCastController rayCastController;

    [SerializeField] private float hitDivider = 1f;
    [SerializeField] private float rayDistance = 40f;

    // movement
    private int food;
    private Vector3 lastPosition;
    private float distanceTraveled = 0f;

    // carateristiques
    [SerializeField] private double energy = 100;
    [SerializeField] private double vitality = 100;
    private float energyDecrease = 0.5f;
    private float energyThreshold = 30;
    private int energyToReproduce = 130;
    private float vitalityLoss = 0.1f;


    // network
    public NeatNetwork myNetwork;
    private int myBrainIndex;
    private int inputNodes;
    private int outputNodes;
    
    //inputs for neural network
    [SerializeField] private float[] inputs;

    //outputs of neural network
    [SerializeField] private float[] outputs;

    private float fitness;


    private void Start()
    {
        inputs = new float[inputNodes];
        rayCastController = new RayCastController();
        rend = GetComponent<Renderer>();
        fow = GetComponent<FieldOfView>();
        
        // modify NPC size
        // this.transform.localScale = new Vector3((float)1.5, 1, (float)1.5); 

    }

    void Update()
    {
        energyLoss();

        if (vitality <= 0)
        {
            Death();
        }

        // fetch datas from sensors
        InputSensors();

        // send sensors data as input in the network
        Outputs = myNetwork.FeedForwardNetwork(inputs);

        moveNPC(Outputs[0], Outputs[1]);

      
    }

    void moveNPC(float speed, float rotation)
    {
        // get position
        Vector3 input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, speed * 2f), 1f);
        input = transform.TransformDirection(input);

        // move NPC
        transform.position += input * Time.deltaTime;

        // rotation of NPC
        transform.eulerAngles += new Vector3(0, (rotation * 90), 0) * Time.deltaTime;
    }
    
    void energyLoss()
    {
        //calcul de la taille du NPC
        float size = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        
        // calcul de la distance parcourue
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        //diminution de ll'énergie en fonction du temps
        energy -= energyDecrease * 2 * Time.deltaTime;

        // diminution de l'énergie en fonction de la distance parcourue et de la taille du NPC
        energy -= distanceTraveled * energyDecrease/2 * size ;
        distanceTraveled = 0f;

        if ( energy <= EnergyThreshold)
        {
            vitality -= VitalityLoss;
        }
        else if ( energy >= EnergyToReproduce)
        {
            //Reproduce();
        }

    }

    void Reproduce()
    {
        energy /= 2f; // transfert de la moitié de l'énergie au nouvel enfant

        Vector2 randomCircle = Random.insideUnitCircle * 2f; // génère une position aléatoire dans un cercle de rayon 2 autour du parent
        Vector3 childPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
        // Instantiate(Npc, childPosition, Quaternion.identity); // crée un nouvel objet NPC



    }

    private void InputSensors()
    {
        // these sensors return rayHit distance if ray touch a wall (three directions)
        inputs[0] = (rayCastController.RayHit (transform.position, transform.forward, "Wall", 40f, 1f ) );
        inputs[1] = (rayCastController.RayHit (transform.position, transform.forward + transform.right, "Wall", 40f, 1f ) );
        inputs[2] = (rayCastController.RayHit (transform.position, transform.forward - transform.right, "Wall", 40f, 1f ) );

        
        inputs[3] = fow.ClosetTargetDist();
        inputs[4] = fow.ClosetTargetAngle();

    }

    private void Death()
    {
        GameObject.FindObjectOfType<NPCManager>().Death(fitness, MyBrainIndex);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            this.Food += 1;
            energy += 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            // fitness = 0;
            Death();
        }
    }

    // getters and setters
    public int MyBrainIndex { get => myBrainIndex; set => myBrainIndex = value; }
    public float[] Outputs { get => outputs; set => outputs = value; }
    public float EnergyThreshold { get => energyThreshold; set => energyThreshold = value; }
    public int EnergyToReproduce { get => energyToReproduce; set => energyToReproduce = value; }
    public float VitalityLoss { get => vitalityLoss; set => vitalityLoss = value; }
    public int Food { get => food; set => food = value; }
    public int InputNodes { get => inputNodes; set => inputNodes = value; }
    public int OutputNodes { get => outputNodes; set => outputNodes = value; }
}
