using UnityEngine;
using System.Collections;

public class AnimatePulseWaveBall : MonoBehaviour
{
    [Header("References")] [SerializeField]
    Transform ballTransform;

    [SerializeField] Transform activatedPosition;
    [SerializeField] Transform deactivatedPosition;
    [SerializeField] AnimationCurve animationCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [SerializeField] AnimationCurve animationCurveRoof = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    // trying to animate the turret base roof programmatically...
    public Transform Bone6L;
    public Transform Bone6R;
    public Transform Bone7L;
    public Transform Bone7R;
    public Transform Bone8L;
    public Transform Bone8R;

    [Header("Configurations")] [SerializeField]
    float initialShowBallDelay = 1.0f;

    private float introduceTheStarDuration = 1.0f;

    private void Awake()
    {
        ballTransform.position = deactivatedPosition.position;
    }

    private void Start()
    {
        LaserController.Instance.OnPulseWaveActivated += StartTrackingHeatChange;
        StartCoroutine(ShowBall(initialShowBallDelay));
    }

    private void StartTrackingHeatChange()
    {
        // first unsuscribe in case we were already subscribed
        LaserController.Instance.OnHeatChanged -= HandleHeatChange;
        LaserController.Instance.OnHeatChanged += HandleHeatChange;
    }

    private void StopTrackingHeatChange()
    {
        LaserController.Instance.OnHeatChanged -= HandleHeatChange;
    }

    private IEnumerator ShowBall(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        float duration = introduceTheStarDuration;
        while (duration >= 0.0f)
        {
            HandleHeatChange((duration, introduceTheStarDuration));
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void HandleHeatChange((float current, float max) eventData)
    {
        // by using the max and current heat values
        // (assuming that 0 is the lowest value)
        // we can get a percentage value (aka from 0.0 to 1.0)
        // and use that to Lerp between the activated and deactivated
        // positions of the ball. Hence, programatically animating the ball.
        var percentage = eventData.current / eventData.max;
        ballTransform.position = Vector3.Lerp(
            activatedPosition.position,
            deactivatedPosition.position,
            animationCurve.Evaluate(percentage));

        animateTurretRoof(percentage);

        // whenever the heat value reaches 0
        // we unsubscribe from the heat change events
        // and we ensure that the ball is reset back
        // to the activated position
        if (eventData.current == 0)
        {
            StopTrackingHeatChange();
            ballTransform.position = activatedPosition.position;
        }
    }

    private void animateTurretRoof(float percentage)
    {
        float progress = animationCurveRoof.Evaluate(percentage);
        Bone6L.rotation = Quaternion.Lerp(Quaternion.Euler(-0.032f, -0.252f, -7.217f),
            Quaternion.Euler(-0.032f, -0.252f, -47f), progress);
        Bone6R.rotation = Quaternion.Lerp(Quaternion.Euler(-0.032f, 0.252f, 7.217f),
            Quaternion.Euler(-0.032f, 0.252f, 47f),
            progress);
        Bone7L.rotation = Quaternion.Lerp(Quaternion.Euler(-0.228f, -0.641f, -19.572f),
            Quaternion.Euler(-0.228f, -0.641f, -47f), progress);
        Bone7R.rotation = Quaternion.Lerp(Quaternion.Euler(-0.228f, 0.641f, 19.572f),
            Quaternion.Euler(-0.228f, 0.641f, 47f),
            progress);
        Bone8L.rotation = Quaternion.Lerp(Quaternion.Euler(-0.605f, -0.942f, -32.722f),
            Quaternion.Euler(-0.605f, -0.942f, -47f), progress);
        Bone8R.rotation = Quaternion.Lerp(Quaternion.Euler(-0.605f, 0.942f, 32.722f),
            Quaternion.Euler(-0.605f, 0.942f, 47f),
            progress);
    }
}