using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
            LoadingController.instance.LoadScene(1);
    }
}
