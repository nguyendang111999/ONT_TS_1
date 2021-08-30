using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Float Value")]
public class FloatValueSO : ScriptableObject
{
    [SerializeField] private float value;
    public float Value => value;
}
