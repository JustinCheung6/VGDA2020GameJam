using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private Canvas gameOverMenu;
    [SerializeField] private Canvas HUDMenu;

    [SerializeField] private Text finalLevel = null;
    void Start()
    {
        
    }

    public void GameOver()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("BlockManager");
        int levelScore = gameManager.GetComponent<BlockManager>().Level;

        finalLevel.text = ("Final Level: " + levelScore);

        gameManager.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);

        gameOverMenu.enabled = true;
        HUDMenu.enabled = false;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LeaveGame()
    {
        Application.Quit();
    }
}
