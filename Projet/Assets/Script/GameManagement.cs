using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static BoardManager boardscript = null;
    public static GameManagement instance = null;
    private List<Entity> enemi;

    private int level = 1;

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
        boardscript = GetComponent<BoardManager>();
        enemi = new List<Entity>();
        InitGame();
    }

    void levelClear()
    {
        level++;
        InitGame();
    }
    public void InitGame()
    {
        boardscript.SetupBoard(level);
    }

    public void GameOver()
    {
        enabled = false;
    }
}
    
