using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public void MovetoPos(Vector3 pos, UnityAction done)
    {
        StopAllCoroutines();
        StartCoroutine(MovingPos(pos, done));
    }
    protected IEnumerator MovingPos(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        while (true)
        {
            float delta = 2.0f * Time.deltaTime;

            if (delta > dist) delta = dist;
            dist -= delta;

            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        done?.Invoke();
    }
    protected IEnumerator Rotating()
    {
        yield return null;
    }
}
