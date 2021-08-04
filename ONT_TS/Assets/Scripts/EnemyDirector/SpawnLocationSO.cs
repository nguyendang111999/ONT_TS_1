using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Director/Spawn Location")]
public class SpawnLocationSO : ScriptableObject
{
    [Tooltip("Enemy location to spawn")]
    [SerializeField] Vector3 location;
    [Tooltip("Spawn range")]
    [SerializeField] float range;
    [Tooltip("Max number of enemy to spawn")]
    [SerializeField] int numberToSpawn;
    [Tooltip("Max number of enemy at once")]
    [SerializeField] int spawnRate;
    [SerializeField] int spawnedNumber = 0;

    private bool isSuccessed = false;//Check if user is finished this area

    public Vector3 Location
    {
        get { return location; }
    }
    public float Range
    {
        get { return range; }
    }
    public int NumberToSpawn
    {
        get
        { return numberToSpawn; }
    }
    public int Rate
    {
        get { return spawnRate; }
    }

    public int SpawnedNumber{
        get{ return spawnedNumber;}
        set{ spawnedNumber = value;}
    }
    public bool IsActive{
        get; set;
    }
    public bool IsSuccessed{
        get { return isSuccessed;}
        set { isSuccessed = value;}
    }
    public int TargetKilled {get; set;} = 0;
}
