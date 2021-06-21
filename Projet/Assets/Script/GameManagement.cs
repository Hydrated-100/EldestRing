using System.Collections;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static BoardManager Boardscript = null;
    public static GameManagement instance = null;
    private List<Entity> enemi;

    private static int level = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        Boardscript = GetComponent<BoardManager>();
        enemi = new List<Entity>();
        InitGame();
    }

    public void levelClear()
    {
        level += 1;
        InitGame();
    }

    public void InitGame()
    {

        if (level >= 3)
        {
            level = 0;
            Debug.Log("Salle du boss");
            SceneManager.LoadScene("NiveauBoss");
        }
        else
        {
            Debug.Log(level);
            Boardscript.SetupBoard(level);  
        }
        
    }

    public void GameOver()
    {
        enabled = false;
    }
}
    
