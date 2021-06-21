using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManagerMulti : MonoBehaviour
{
    public static BoardManagerMulti Boardscript = null;
    public static BossManagerMulti instance = null;
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
        Boardscript = GetComponent<BoardManagerMulti>();
        enemi = new List<Entity>();
        Boardscript.SetupBossRoom();
    }
}
