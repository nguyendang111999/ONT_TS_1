using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneTrigger : MonoBehaviour
{
    [HideInInspector] public List<GameObject> currentCollisionsList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable") || other.CompareTag("NPC") || other.CompareTag("Savepoint") || other.CompareTag("InfoObject"))
        {
            GameObject obj = other.gameObject;
            if (currentCollisionsList.Find(gameobject => gameObject == obj))
            {
                return;
            }
            else
            {
                currentCollisionsList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickable") || other.CompareTag("NPC") || other.CompareTag("Savepoint") || other.CompareTag("InfoObject"))
        {
            currentCollisionsList.Remove(other.gameObject);
        }
    }
}
