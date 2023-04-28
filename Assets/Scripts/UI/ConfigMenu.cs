using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    public GameObject configMenu;
    public GameObject gameInterface;

    private void Start()
    {
    }

    public void StartGame()
    {
        configMenu.SetActive(false);
        gameInterface.SetActive(true);
        Time.timeScale = 1f;
    }



    private void Update()
    {
    }
}
