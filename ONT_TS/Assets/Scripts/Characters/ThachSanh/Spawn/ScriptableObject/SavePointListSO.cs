using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SavePointListSO : ScriptableObject
{
    [Tooltip("Name of the scene holding this save point list")]
    [SerializeField]
    private string _sceneName;
    [Tooltip("List of save points")]
    [SerializeField]
    private List<SavePointLocationSO> _list;

    public string SceneName => _sceneName;
    private List<SavePointLocationSO> List => _list;
}
