using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;
    public float force = 1500.0f;
    public LayerMask removeMask;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }
    private void OncollisionEnter(Collision col)
    {
        if ((removeMask & 1 << col.gameObject.layer) != 0)
        {
            Destroy(col.gameObject);
        }
    }
}
