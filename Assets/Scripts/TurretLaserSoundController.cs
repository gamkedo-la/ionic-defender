using UnityEngine;

public class TurretLaserSoundController : MonoBehaviour
{
    private void Start()
    {
        LaserController.OnLaserStart += () => ToggleLaserSoundFx(true);
        LaserController.OnLaserStop += () => ToggleLaserSoundFx(false);
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
