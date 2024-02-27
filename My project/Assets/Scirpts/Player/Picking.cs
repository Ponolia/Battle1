using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public LayerMask movekMask;
    public LayerMask objMask;
    public LayerMask attackMask;
    public UnityEvent<Vector3> moveAct;
    public UnityEvent<Transform> attackAct;
    public UnityEvent<Transform> objAct;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, movekMask | attackMask))
            {
                if ((1 << hit.transform.gameObject.layer & attackMask) != 0)
                {
                    attackAct?.Invoke(hit.transform);
                }
                else
                {
                    moveAct?.Invoke(hit.point);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, objMask))
            {
                objAct?.Invoke(hit.transform);
            }
        }
    }
}
