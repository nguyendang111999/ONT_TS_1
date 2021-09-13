using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New List", menuName = "SavePoint/Savepoint List")]
public class SavePointListSO : ScriptableObject
{
    [Tooltip("Name of the scene holding this save point list")]
    [SerializeField]
    private string _sceneName;

    [Tooltip("List of save points")]
    [SerializeField]
    private List<LocationSO> _list;

    public string SceneName => _sceneName;
    public List<LocationSO> List => _list;

    public void AddSavePoint(LocationSO spl)
    {
        if (CheckIfSaved(spl))
            return;
        else
        {
            _list.Add(spl);
        }
    }

    public Vector3 GetLastSavePoint()
    {
        if (_list.Count > 0)
            return _list[_list.Count - 1].Position;
        else return Vector3.zero;
    }

    public bool CheckIfSaved(LocationSO spl) =>
    (_list.Find(obj => obj == spl) != null) ? true : false;
}
