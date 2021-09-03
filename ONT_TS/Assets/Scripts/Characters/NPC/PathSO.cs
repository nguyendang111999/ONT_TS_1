using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Paths", menuName = "CharacterConfig/Fox/Path")]
public class PathSO : ScriptableObject
{
    [SerializeField]
    private SavePointLocationSO[] paths;

    public int Index { get; set; } = 0;

    public Vector3 GetNextPoint()
    {
        Vector3 a = paths[Index].Location;
        Index += 1;
        return a;
    }
    public bool IfReachedLocation() => Index > paths.Length;
}
