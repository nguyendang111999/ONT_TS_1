using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameTrigger : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnTriggerEnter(Collider other) {
            // LoadingController.instance.LoadScene(1);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut(){
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(8f);
        LoadingController.instance.LoadScene(0);
    }

}
