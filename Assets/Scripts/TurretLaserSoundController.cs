using UnityEngine;

public class TurretLaserSoundController : MonoBehaviour
{
    private void Start()
    {
        LaserController.Instance.OnLaserStart += () => ToggleLaserSoundFx(true);
        LaserController.Instance.OnLaserStop += () => ToggleLaserSoundFx(false);
    }

    private void ToggleLaserSoundFx(bool playSound)
    {
        if(playSound)
        {
            SoundFXManager.StartLoopSound(SoundFxKey.LoopingLaser);
        }
        else
        {
            SoundFXManager.StopLoopSound(SoundFxKey.LoopingLaser);
        }
    }
}
