using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Tooltip("Object to spawn")]
    [SerializeField] private GameObject prefab;
    [Tooltip("Spawn area")]
    [SerializeField] private SpawnLocationSO[] locations;
    [Tooltip("Player position")]
    [SerializeField] private ObjectPositionSO playerPos;

    SpawnLocationSO _currentLocation;
    private Queue<GameObject> agents = new Queue<GameObject>();

    private void Update()
    {
        CheckCurrentLocation();
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            SpawnLocationSO location = locations[i];
            if (location.IsActive && location.SpawnedNumber <= location.NumberToSpawn)
            {
                SpawnEnemy(location);
            }
        }
    }
    void SpawnEnemy(SpawnLocationSO location)
    {
        for (int i = 0; i < location.NumberToSpawn; i++)
        {
            if (agents.Count == 0 || agents.Peek().activeSelf)
            {
                var wolf = Instantiate(prefab, location.Location, Quaternion.identity);
                Damageable damageable = wolf.GetComponent<Damageable>();
                damageable.Location = location;
                agents.Enqueue(wolf);
                location.SpawnedNumber += 1;
            }
        }
        // for (int i = 0; i < location.NumberToSpawn; i++)
        // {
        //     if (agents.Count == 0 || agents.Peek().activeSelf)
        //     {
        //         var wolf = Instantiate(prefab, location.Location, Quaternion.identity);
        //         Damageable damageable = wolf.GetComponent<Damageable>();
        //         damageable.Location = location;
        //         agents.Enqueue(wolf);
        //     }
        //     else
        //     {
        //         var wolf = agents.Dequeue();
        //         wolf.SetActive(true);
        //         wolf.transform.position = location.Location;
        //         Damageable damageable = wolf.GetComponent<Damageable>();
        //         damageable.ResetHealth();
        //         damageable.Location = location;
        //         agents.Enqueue(wolf);
        //     }
        // }
    }

    void CheckCurrentLocation()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            SpawnLocationSO location = locations[i];
            if (Vector3.Distance((playerPos.Transform.position), (location.Location)) < 20f && !location.IsSuccessed)
            {
                location.IsActive = true;
            }
            else
            {
                location.IsActive = false;
            }
        }
    }
}
