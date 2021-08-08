using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    PickUp, Talk,
}

public class InteractionManager : MonoBehaviour
{
    // [HideInInspector] public InteractionType currentInteractionType;
    [SerializeField] private ZoneTrigger _zone;
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private InventorySO _currentInventory;

    private void OnEnable()
    {
        _inputReader.InteractEvent += OnInteractionButtonPress;
    }
    private void OnDisable()
    {
        _inputReader.InteractEvent -= OnInteractionButtonPress;
    }

    public void OnInteractionButtonPress()
    {
        List<GameObject> listObj = _zone.currentCollisionsList;
        if(listObj == null){
            Debug.Log("No interaction");
            return;
        }
            
        foreach (GameObject obj in listObj)
        {
            if (obj.CompareTag("Pickable"))
            {
                Collect(obj);
                listObj.Remove(obj);
                break;
            }
            else if (obj.CompareTag("NPC"))
            {
                Talk(obj);
                listObj.Remove(obj);
                break;
            }
        }
    }

    public void Collect(GameObject obj)
    {
        CollectableItems collectableItem = obj.gameObject.GetComponent<CollectableItems>();
        // _currentInventory.Items.Clear();
        _currentInventory.Add(collectableItem.GetItem());
        List<ItemSO> list = _currentInventory.Items;
        Debug.Log("Inventory size: " + list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("item name: " + list[i].ItemName);
        }
        
        Destroy(collectableItem.gameObject);
    }

    public void Talk(GameObject obj)
    {
        Debug.Log("Talking");
    }
}
