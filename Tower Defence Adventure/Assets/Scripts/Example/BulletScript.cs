using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [Header("Unity Handles")]
    public GameObject shrapnelEffect; // Particle effect that simulates shrapnel
    Rigidbody rb;

    [Header("Floats")]
    public float bulletSpeed;
    public float bulletDuration;

 
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, bulletDuration);
    }

    void FixedUpdate()
    {

        rb.MovePosition(transform.position + (transform.up * bulletSpeed));
    }


    // Instantiates shrapnel and destroys the bullet.
    private void OnCollisionEnter(Collision collision)
    {
      //  Instantiate(shrapnelEffect,transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
