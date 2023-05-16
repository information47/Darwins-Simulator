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

    private void Start()
    {
    }

    public void StartGame()
    {
        configMenu.SetActive(false);
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
