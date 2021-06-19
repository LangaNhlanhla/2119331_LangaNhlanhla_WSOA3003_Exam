using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandProjectile : MonoBehaviour
{
    [Header("Unity Handles")]
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask playerMask;

    [Header("Floats")]
    [Range(0f, 1f)]
    public float bounce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
