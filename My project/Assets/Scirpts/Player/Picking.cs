using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public LayerMask clickMask;
    public LayerMask attackMask;
    public UnityEvent<Vector3> clickAct;
    public UnityEvent<Transform> attackAct;
   
    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickMask | attackMask))
            {
                if ((1 << hit.transform.gameObject.layer & attackMask) != 0)
                {
                    attackAct?.Invoke(hit.transform);
                }
                else
                {
                    clickAct?.Invoke(hit.point);
                }
            }
        }
    }
}
