using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public static BoardManager Boardscript = null;
    public static BossManager instance = null;
    private List<Entity> enemi;

    private static int level = 4;
    
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
        Boardscript.SetupBossRoom();
    }


    public void GameOver()
    {
        enabled = false;
    }
}
