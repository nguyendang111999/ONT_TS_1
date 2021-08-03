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

    private Queue<GameObject> agents = new Queue<GameObject>();

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
            Debug.Log("Target killed: " + location.TargetKilled);
            if (location.IsActive && location.SpawnedNumber < location.NumberToSpawn)
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
    }

    void CheckActiveLocation()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            SpawnLocationSO location = locations[i];
            if (Vector3.Distance((playerPos.Transform.position), (location.Location)) < 50f && !location.IsSuccessed)
            {
                location.IsActive = true;
            }
            else
            {
                location.IsActive = false;
            }
            if (Vector3.Distance((playerPos.Transform.position), (location.Location)) < 50f && location.TargetKilled == location.NumberToSpawn)
            {
                location.IsSuccessed = true;
            }
            if (Vector3.Distance((playerPos.Transform.position), (location.Location)) > 50f && !location.IsSuccessed)
            {
                location.TargetKilled = 0;
                location.SpawnedNumber = 0;
            }
        }
    }
}
