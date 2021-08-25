using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    // IEnumerator Started()
    // {
    //     Debug.Log("Run");
    //     yield return new WaitForSeconds(1);
    //     SceneManager.LoadSceneAsync(1);
    // }
    void Start()
    {
        Debug.Log("Run");
        SceneManager.LoadSceneAsync(1);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
