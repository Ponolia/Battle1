using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform followTarget = null;

    public Vector3 cameraPos = new Vector3(0.0f, 10.0f, -5.0f);
    public float smoothMoveSpeed = 10.0f;
    public float zoomSpeed = 3.0f;
    public Vector2 zoomRange = new Vector2(5.0f, 10.0f);
    Vector3 myDir = Vector3.zero;
    float myDist = 0.0f;

    Vector3 targetPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(followTarget);
        myDir = transform.position - followTarget.position;
        myDist = myDir.magnitude;
        myDir.Normalize();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        myDist -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        myDist = Mathf.Clamp(myDist, zoomRange.x, zoomRange.y);
        targetPosition = followTarget.position + myDir * myDist;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothMoveSpeed);
    }
    public void SetTarget(Transform target)
    {
        transform.position = target.position + cameraPos;
        transform.LookAt(target);
        myDist = cameraPos.magnitude;
        myDir = cameraPos.normalized;

        targetPosition = transform.position;

        followTarget = target;
    }
}

