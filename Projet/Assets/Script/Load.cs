using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManagement.instance == null)
        {
            Instantiate(gameManager);
        }
    }

}
