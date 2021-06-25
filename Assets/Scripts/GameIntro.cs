using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform introCameraPosition;
    [SerializeField] Transform gameCameraPosition;

    [SerializeField] Camera cameraReference;
    [SerializeField] float introOrthographicSize;
    [SerializeField] float gameOrthographicSize;


    private void Awake()
    {
        cameraTransform.position = introCameraPosition.position;
        cameraTransform.rotation = introCameraPosition.rotation;
        cameraReference.orthographicSize = introOrthographicSize;
    }

    private void Start()
    {
        StartCoroutine(MoveCamera(4f, 1f));
    }

    private IEnumerator MoveCamera(float startDelay, float duration)
    {
        yield return new WaitForSeconds(startDelay);

        float remaining = duration;
        var startAt = introCameraPosition.position;
        var endAt = gameCameraPosition.position;

        var rotStartAt = introCameraPosition.rotation;
        var rotEndAt = gameCameraPosition.rotation;

        var sizeStartAt = introOrthographicSize;
        var sizeEndAt = gameOrthographicSize;

        while(remaining > 0)
        {
            yield return new WaitForEndOfFrame();
            remaining -= Time.deltaTime;

            cameraTransform.position = Vector3.Lerp(startAt, endAt, 1 - remaining);
            cameraTransform.rotation = Quaternion.Lerp(rotStartAt, rotEndAt, 1 - remaining);
            cameraReference.orthographicSize = Mathf.Lerp(sizeStartAt, sizeEndAt, 1 - remaining);
        }
    }
}
