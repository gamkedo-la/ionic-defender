using UnityEngine;
using System.Collections;

public class AnimatePulseWaveBall : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] Transform activatedPosition;
    [SerializeField] Transform deactivatedPosition;

    private void Awake()
    {
        LaserController.OnPulseWaveActivated += StartTrackingHeatChange;
        ballTransform.position = deactivatedPosition.position;
    }

    private void Start()
    {
        StartCoroutine(ShowBall());
    }

    private void StartTrackingHeatChange()
    {
        // first unsuscribe in case we were already subscribed
        LaserController.OnHeatChanged -= HandleHeatChange;
        LaserController.OnHeatChanged += HandleHeatChange;
    }

    private void StopTrackingHeatChange()
    {
        LaserController.OnHeatChanged -= HandleHeatChange;
    }

    private IEnumerator ShowBall()
    {
        float duration = 1.0f;
        while(duration > 0.0f)
        {
            ballTransform.position = Vector3.Lerp(
            activatedPosition.position,
            deactivatedPosition.position,
            duration);
            yield return new WaitForEndOfFrame();
            duration -= Time.deltaTime;
        }


        ballTransform.position = activatedPosition.position;
    }

    private void HandleHeatChange((float current, float max) eventData)
    {
        // by using the max and current heat values
        // (assuming that 0 is the lowest value)
        // we can get a percentage value (aka from 0.0 to 1.0)
        // and use that to Lerp between the activated and deactivated
        // positions of the ball. Hence, programatically animating the ball.
        ballTransform.position = Vector3.Lerp(
            activatedPosition.position,
            deactivatedPosition.position,
            eventData.current/eventData.max);

        // whenever the heat value reaches 0
        // we unsubscribe from the heat change events
        // and we ensure that the ball is reset back
        // to the activated position
        if(eventData.current == 0)
        {
            StopTrackingHeatChange();
            ballTransform.position = activatedPosition.position;
        }
    }
}
