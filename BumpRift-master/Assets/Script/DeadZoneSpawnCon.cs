using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneSpawnCon : MonoBehaviour
{
    public GameObject DeadZone, DeadObject;
    private Transform DeadZones;
    private int i = 0;
    private Vector3 SpawnRan;
    public float AutoSpawnDelay = 20f;
    public float FasterSpawnTime = 200f;
    public float FasterSpawnDelay = 10f;
    private bool isSpawned = false;

    void Start()
    {
        DeadZones = DeadObject.transform;
        SpawnZone();
    }
    void SpawnZone()
    {
        SpawnRan = new Vector3(Random.Range(-700, 700), 1, Random.Range(-700, 700));
        Instantiate(DeadZone, SpawnRan, this.transform.rotation, DeadZones);
        isSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            if (FasterSpawnTime < Time.realtimeSinceStartup)
                Invoke("SpawnZone", FasterSpawnDelay);
            else
                Invoke("SpawnZone", AutoSpawnDelay);
        }
    }
}
