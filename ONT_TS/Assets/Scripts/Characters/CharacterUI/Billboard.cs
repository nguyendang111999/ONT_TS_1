using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;
    private void Awake() {
    cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
