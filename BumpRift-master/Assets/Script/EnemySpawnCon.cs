using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCon : MonoBehaviour
{
    public GameObject Enem, EnemyObject;
    private Transform Enemys;
    private int i = 0;
    private Vector3 SpawnRan;
    //public int CreateMin = 0;
    //public int CreateMax = 12;
    public int CreateNumber = 12;
    //private bool IsDelayed = false;
    public float AutoSpawnDelay = 20f;
    public float FasterSpawnTime = 200f;
    public float FasterSpawnDelay = 15f;
    private bool isSpawned = false;

    //public int[] items;
    //public int[] getRandomInt(int length, int min, int max)
    //{
    //    int[] randArray = new int[length];
    //    bool isSame;

    //    for (int i = 0; i < length; ++i)
    //    {
    //        while (true)
    //        {
    //            randArray[i] = Random.Range(min, max);
    //            isSame = false;

    //            for (int j = 0; j < i; ++j)
    //            {
    //                if (randArray[j] == randArray[i])
    //                {
    //                    isSame = true;
    //                    break;
    //                }
    //            }
    //            if (!isSame) break;
    //        }
    //    }
    //    return randArray;
    //}
    // Start is called before the first frame update
    void Start()
    {
        Enemys = EnemyObject.transform;
        for (i = 0; i < CreateNumber; i++)
        {
            SpawnRan = new Vector3(Random.Range(-700, 700), 1, Random.Range(-700, 700));
            Instantiate(Enem, SpawnRan, this.transform.rotation, Enemys);
            i++;
        }
        //if (i < CreateNumber)
        //{
        //    SpawnRan = new Vector3(Random.Range(-700, 700), 1, Random.Range(-700, 700));
        //    Instantiate(Item1, SpawnRan, this.transform.rotation);
        //    i++;
        //}
        //else
        //{
        //    return;
        //}
    }
    void SpawnEnem()
    {
        //Debug.Log("생성!");
        SpawnRan = new Vector3(Random.Range(-700, 700), 1, Random.Range(-700, 700));
        Instantiate(Enem, SpawnRan, this.transform.rotation, Enemys);
        isSpawned = false;
        //IsDelayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            if (FasterSpawnTime < Time.realtimeSinceStartup)
                Invoke("SpawnEnem", FasterSpawnDelay);
            else
                Invoke("SpawnEnem", AutoSpawnDelay);
        }
    }
}
