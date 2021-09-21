using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineFreeLook _tpCam;
    private void Awake() {
        _tpCam = gameObject.GetComponent<CinemachineFreeLook>();
    }
    // public IEnumerator Shake(float duration, float magnitude){
    //     Vector3 originalPos = transform.localPosition;
    //     float elapsed = 0f;
    //     while(elapsed < duration){
    //         float x = Random.Range(-1f, 1f) * magnitude;
    //         float y = Random.Range(-1f, 1f) * magnitude;

    //         transform.localPosition = new Vector3(x, y, originalPos.z);

    //         elapsed += Time.deltaTime;

    //         yield return null;
    //     }
        
    //     transform.localPosition = originalPos;
    // }
    public IEnumerator Shake(float duration, float magnitude, PlayerController controller){
        CinemachineBasicMultiChannelPerlin multiChannelPerlin = _tpCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float elapsed = 0f;
        controller.Headache = true;

        while(elapsed < duration){
            multiChannelPerlin.m_AmplitudeGain = magnitude;
            elapsed += Time.deltaTime;

            yield return null;
        }
        multiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
