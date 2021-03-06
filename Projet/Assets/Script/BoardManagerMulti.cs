using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManagerMulti : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;             //Minimum value for our Count class.
        public int maximum;             //Maximum value for our Count class.


        //Assignment constructor.
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int colonnes = 8;
    public int rows = 8;
    public Count obstacle = new Count(5, 18);

    public GameObject Door;

    public GameObject[] FloorTiles;

    public GameObject[] ObstacleTiles;

    public GameObject[] Enemis;

    public GameObject[] Wall;

    public GameObject[] Boss;

    public GameObject Player;

    public Transform boardHolder;

    public List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseGrid()
    {
        gridPositions.Clear();

        for (int i = 0; i < colonnes; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                gridPositions.Add(new Vector3(i, j, 0f));
            }
        }

    }

    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;
        for (int i = -1; i < colonnes + 1; i++)
        {
            for (int j = -1; j < rows + 1; j++)
            {
                GameObject toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];
                if (i == -1 || i == colonnes || j == -1 || j == rows)
                {
                    if (i == colonnes / 2 && j == rows)
                    {
                        toInstantiate = Door;
                    }
                    else
                    {
                        toInstantiate = Wall[Random.Range(0, Wall.Length)];
                    }

                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity);

                instance.transform.SetParent(boardHolder);
            }
        }

        PhotonNetwork.Instantiate(Player.name, Vector3.zero, Quaternion.identity);
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayObject(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupBoard(int level)
    {
        InitialiseGrid();
        BoardSetUp();
        int enemyCount = (int) Math.Log(4 * level, 2f);
        LayObject(Enemis, enemyCount, enemyCount);
        LayObject(ObstacleTiles, obstacle.minimum, obstacle.maximum);
    }

    public void SetupBossRoom()
    {
        InitialiseGrid();
        BoardSetUp();
        LayObject(Boss, 1, 1);
    }
}
