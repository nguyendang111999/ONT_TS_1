using UnityEngine;

public class ThachSanhAudioManager : AudioManager
{
    public void PlayMelleSound1() => PlayAudio("AxeMelee1Sound");
    public void PlayMelleSound2() => PlayAudio("AxeMelee2Sound");
    public void PlayRunSound() => PlayAudio("RunningSound");
}
