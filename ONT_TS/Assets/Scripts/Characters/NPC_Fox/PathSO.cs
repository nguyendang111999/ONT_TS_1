using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Paths", menuName = "CharacterConfig/Fox/Path")]
public class PathSO : ScriptableObject
{
    [SerializeField]
    private LocationSO[] paths;

    public int Index { get; set; } = 0;

    public Vector3 GetNextPoint()
    {
        Index ++;
        Vector3 a = paths[Index].Position;
        return a;
    }
    public Vector3 GetCurrentPoint(){
        Vector3 a = paths[Index].Position;
        return a;
    }
    public bool IsLastLocation() => Index == paths.Length - 1;
    public bool IndexInBound() => Index < paths.Length - 1;
}
