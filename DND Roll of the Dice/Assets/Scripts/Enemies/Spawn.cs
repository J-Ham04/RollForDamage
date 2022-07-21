using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn
{
    public string spawnerName;

    public GameObject spawner;

    public float timeBetweenSpawn;
    public float timeBeforeFirstSpawn;
    [HideInInspector] public float timeBeforeNextSpawn;

    public bool speedUpOverTime;
}
