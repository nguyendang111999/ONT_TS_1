using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractionType type;
    public GameObject interactableGameobject;
    public Interactable(InteractionType t, GameObject obj)
	{
		type = t;
		interactableGameobject = obj;
	}

}
public enum InteractionType{
    PickUp, Talk, None = 0,
}
