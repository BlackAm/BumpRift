using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISpawnCon : MonoBehaviour
{
    public GameObject Item1, ItemsObject;
    private Transform Items;
    private int i = 0;
    private Vector3 SpawnRan;
    //public int CreateMin = 0;
    //public int CreateMax = 12;
    public int CreateNumber = 12;

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
        Items = ItemsObject.transform;
        for (i = 0; i < CreateNumber; i++)
        {
            SpawnRan = new Vector3(Random.Range(-700, 700), 1, Random.Range(-700, 700));
            Instantiate(Item1, SpawnRan, this.transform.rotation, Items);
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

    // Update is called once per frame
    void Update()
    {
        //if(i<CreateNumber)
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
}
