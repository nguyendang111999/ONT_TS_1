using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _tpCam;
    private void Awake() {
        _tpCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }
    
    public IEnumerator Shake(float duration, float magnitude, PlayerController controller){
        CinemachineBasicMultiChannelPerlin multiChannelPerlin = _tpCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
