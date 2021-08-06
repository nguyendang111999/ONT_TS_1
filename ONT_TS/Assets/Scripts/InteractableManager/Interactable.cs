using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    InteractionType type;
    float radius = 3f;

}
public enum InteractionType{
    None = 0, PickUp, Talk
}
