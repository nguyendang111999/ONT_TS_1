using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject>
{

}
public class ZoneTrigger : MonoBehaviour
{
    public List<GameObject> currentCollisionsList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable") || other.CompareTag("NPC"))
        {
            GameObject obj = other.gameObject;
            if (currentCollisionsList.Find(gameobject => gameObject==obj)){
                return;
            }
            else
            {
                currentCollisionsList.Add(other.gameObject);
            }
            foreach (GameObject o in currentCollisionsList)
            {
                Debug.Log("Add ObjectName: " + obj.name);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickable") || other.CompareTag("NPC"))
        {
            currentCollisionsList.Remove(other.gameObject);
            foreach (GameObject o in currentCollisionsList)
            {
                Debug.Log("Delete ObjectName: " + o.name);
            }
        }
    }
}
