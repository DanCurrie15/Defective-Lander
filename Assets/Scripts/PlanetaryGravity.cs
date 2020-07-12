using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryGravity : MonoBehaviour
{
    private float gravity = 200f;

    public GameObject planet;
    public Rigidbody2D rb;

    //Use FixedUpdate because we are controlling the orbit with physics
    void FixedUpdate()
    {
        Vector3 offset = planet.transform.position - transform.position;

        offset.z = 0;

        float magsqr = offset.sqrMagnitude;

        if (magsqr > 0.0001f)
        {
            //Debug.Log("GRAVITY :" + (gravity * offset.normalized / magsqr) * rb.mass);
            rb.AddForce((gravity * offset.normalized / magsqr) * rb.mass);
        }
    }
}