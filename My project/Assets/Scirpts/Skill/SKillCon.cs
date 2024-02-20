using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillCon : MonoBehaviour
{
    public float dmg;
    public float force;
    public GameObject effect;
    Rigidbody rg;
    private void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.AddForce(transform.forward * force);
    }
    private void Update()
    {
        remove();
    }
    private void remove()
    {
        Destroy(gameObject, 2.0f);
    }
}
