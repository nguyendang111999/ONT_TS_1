using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Tooltip("Object to spawn")]
    [SerializeField] 
    private GameObject prefab;

    [Tooltip("Spawn areas")]
    [SerializeField] 
    private SpawnLocationSO[] locations;
    [Tooltip("Player position")]
    [SerializeField] private ObjectPositionSO playerPos;

    public FloatValueSO _activeDistance;

    private List<GameObject> agents = new List<GameObject>();

    private void Awake()
    {
        ResetLocation();
    }
    private void Update()
    {
        CheckActiveLocation();
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            SpawnLocationSO location = locations[i];
            if (location.IsActive)
            {
                SpawnEnemy(location);
            }
        }
    }

    void SpawnEnemy(SpawnLocationSO location)
    {
        Vector3 startPos = location.Location.Position;
        int count = CountInactiveWolf(agents);
        int temp = location.NumberToSpawn - location.SpawnedNumber;
        if (temp > count)
        {
            while (count < temp)
            {
                var wolf = Instantiate(prefab, startPos, Quaternion.identity);
                wolf.SetActive(false);
                agents.Add(wolf);
                count++;
            }
        }

        foreach (var wolf in agents)
        {
            if (!wolf.activeSelf && location.SpawnedNumber < location.NumberToSpawn)
            {
                wolf.transform.position = location.Location.Position;
                wolf.SetActive(true);
                Damageable damageable = wolf.GetComponent<Damageable>();
                damageable.ResetHealth();
                EnemyBehaviour wolfBehaviour = wolf.GetComponent<EnemyBehaviour>();
                wolfBehaviour.SpawnLocation = location;
                location.SpawnedNumber++;
            }
        }
    }

    int CountInactiveWolf(List<GameObject> agents)
    {
        int inactiveNumber = 0;
        if (agents.Count == 0)
            return 0;
        else
        {
            foreach (GameObject wolf in agents)
            {
                if (!wolf.activeSelf)
                {
                    inactiveNumber += 1;
                }
            }
        }
        return inactiveNumber;
    }

    void CheckActiveLocation()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            SpawnLocationSO location = locations[i];
            if (location.IsSuccessed)
            {
                continue;
            }
            if (Vector3.Distance((playerPos.Transform.position), (location.Location.Position)) < _activeDistance.Value && !location.IsSuccessed)
            {
                location.IsActive = true;
            }
            if (location.TargetKilled >= location.NumberToSpawn)
            {
                location.IsSuccessed = true;
            }
            if (Vector3.Distance((playerPos.Transform.position), (location.Location.Position)) > _activeDistance.Value && !location.IsSuccessed)
            {
                ResetLocationIfNotFinished(location);
            }
        }
    }
    void ResetLocation()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            SpawnLocationSO location = locations[i];
            location.IsActive = false;
            location.IsSuccessed = false;
            location.TargetKilled = 0;
            location.SpawnedNumber = 0;
        }
    }
    void ResetLocationIfNotFinished(SpawnLocationSO location)
    {
        location.IsActive = false;
        location.SpawnedNumber = 0;
        location.TargetKilled = 0;
        foreach (var wolf in agents)
        {
            EnemyBehaviour wolfBehaviour = wolf.GetComponent<EnemyBehaviour>();
            if (wolfBehaviour.SpawnLocation == location)
            {
                wolfBehaviour.isInActive = true;
                wolf.SetActive(false);
            }
        }
    }
}
