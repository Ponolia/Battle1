using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    Transform _target;
    public void SetTarget()
    {
        _target = GameManager.Inst.inGameManager.myPlayer.transform;
        StartCoroutine(Following());
    }

    IEnumerator Following()
    {
        while (true)
        {
            Vector3 newPosition = _target.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            yield return null;
        }
    }
}
