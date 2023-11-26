using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int levelsActived = 1;

    private int numOfLevels = 3;

    // Modelo singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Cambiar escena
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Cerrar el juego
    public void CloseApp()
    {
        Application.Quit();
    }

    public bool CanActiveMoreLevels()
    {
        return levelsActived < numOfLevels;
    }

    public void ActiveNextLevel()
    {
        levelsActived++;
        if (CanActiveMoreLevels()) 
        {
            ChangeScene(1);
        }
        else
        {
            ChangeScene(0);
        }
    }

    public int GetCurrentLevelsActive()
    {
        return levelsActived;
    }
}
