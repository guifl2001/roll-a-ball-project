using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject endPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void LoseGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }

    public void WinGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Win!";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
