using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConfigMenu : MonoBehaviour
{
    public GameObject configMenu;
    public GameObject gameInterface;
    public GameObject npcManagerObject;
    public GameObject levelControllerObject;
    public GameObject npcSliderObject;
    public GameObject game;
    public GameObject mainMenu;
    public GameObject grassPainter;

    private void Start()
    {
    }

    public void StartGame()
    {
        configMenu.SetActive(false);
        gameInterface.SetActive(true);
        levelControllerObject.GetComponent<LevelController>().SizeSetup(); // Définit la taille de la map
        // grassPainter.GetComponent<GrassPainter>().PlaceGrass(new Vector3(1,1,5)); // place de l'herbe sur la map
        npcManagerObject.GetComponent<NPCManager>().InitialSpawnNPC(); // Définit le nb de NPC qui spawn
        Time.timeScale = 1f;
        game.GetComponent<GameScript>().gamePlaying = true;
    }

    public void FastStart()
    {
        levelControllerObject.GetComponent<LevelController>().FoodNumber = 65;
        levelControllerObject.GetComponent<LevelController>().FloorSize = 40;
        levelControllerObject.GetComponent<LevelController>().StartingPopulation = 15;
        levelControllerObject.GetComponent<LevelController>().repopingLimit = 10;

        mainMenu.SetActive(false);
        gameInterface.SetActive(true);
        levelControllerObject.GetComponent<LevelController>().SizeSetup(); // Définit la taille de la map
        npcManagerObject.GetComponent<NPCManager>().InitialSpawnNPC(); // Définit le nb de NPC qui spawn
        Time.timeScale = 1f;
        game.GetComponent<GameScript>().gamePlaying = true;
    }



    private void Update()
    {
    }
}
