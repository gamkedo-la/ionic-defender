using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float Strength;
    public float Duration;
    [SerializeField] private Transform shakeTarget;
    private bool isShaking = false;
    private void Start()
    {
        LaserController.Instance.OnPulseWaveActivated += Shake;
    }

    public void Shake()
    {
        Shake(Duration);
    }

    public void Shake(float duration)
    {
        if(isShaking)
        {
            // if the shaking effect is already happening, we just ignore the call.
            return;
        }

        if(duration < 0)
        {
            Debug.LogWarning($"Shake duration given was [{duration}], but it must be greater than 0. Setting it to 0.");
            duration = 0;
        }

        StartCoroutine(DoCameraShake(duration));
    }

    private IEnumerator DoCameraShake(float cameraShakeDuration)
    {
        isShaking = true;
        var initialPosition = shakeTarget.position;

        float remainingTime = cameraShakeDuration;
        while(remainingTime > 0)
        {
            shakeTarget.Translate(Random.insideUnitCircle * Strength);
            remainingTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        shakeTarget.position = initialPosition;  
        isShaking = false;
    }
}