using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    private bool _IsGameOver;

    public void GameOver()
    {
        _IsGameOver = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _IsGameOver == true)
        {
            SceneManager.LoadScene("Game"); //current game scene
        }
    }
}
