// using System.Collections;
// using UnityEngine;
// using UnityEditor;
// [CustomEditor (typeof(FieldOfView))]
// public class FieldOfViewEditor : Editor
// {
//     #if UNITY_EDITOR
//     private void OnSceneGUI() {
//         FieldOfView fov = (FieldOfView) target;
//         Handles.color = Color.green;
//         Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

//         Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle/2, false);
//         Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle/2, false);
        
//         Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
//         Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

//         Handles.color = Color.red;
//         foreach(ObjectPositionSO visibleTarget in fov.visibleTargets){
//             Handles.DrawLine(fov.transform.position, visibleTarget.Transform.position);
//         }
//     }
//     #endif
// }
