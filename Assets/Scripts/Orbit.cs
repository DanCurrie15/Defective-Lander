using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject planet;
    public float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(planet.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
