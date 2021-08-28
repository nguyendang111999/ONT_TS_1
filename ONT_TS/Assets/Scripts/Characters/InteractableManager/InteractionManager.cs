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
    [SerializeField] private InventorySO _foodInventory;

    private void OnEnable()
    {
        _inputReader.InteractEvent += OnInteractionButtonPress;
    }
    private void OnDisable()
    {
        _inputReader.InteractEvent -= OnInteractionButtonPress;
    }

    private void OnInteractionButtonPress()
    {
        Debug.Log("Press");
        List<GameObject> listObj = _zone.currentCollisionsList;
        if(listObj == null){
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
            if (obj.CompareTag("NPC"))
            {
                Talk(obj);
                listObj.Remove(obj);
                break;
            }
            if(obj.CompareTag("InfoObject")){
                Debug.Log("InfoObject");
                ObjectInfoHandler infoHandler = obj.GetComponent<ObjectInfoHandler>();
                infoHandler.Interact();
                break;
            }
            if(obj.CompareTag("Savepoint")){
                Debug.Log("Save point");
                SavePointHandler sph = obj.GetComponent<SavePointHandler>();
                sph.SaveToList();
                break;
            }
        }
    }

    public void Collect(GameObject obj)
    {
        if(_foodInventory.SlotIsFull()){
            return;
        }
        CollectableItems collectableItem = obj.gameObject.GetComponent<CollectableItems>();
        _foodInventory.Add(collectableItem.GetItem());
        List<ItemSO> list = _foodInventory.Items;
        Destroy(collectableItem.gameObject);
    }

    public void Talk(GameObject obj)
    {
        Debug.Log("Talking");
    }
}
