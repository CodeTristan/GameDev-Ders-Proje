using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    [Header("Settings")]
    public float flyForce;

    Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 force = new Vector3(0, 1, 0) * flyForce + transform.up;
            body.AddForce(force, ForceMode.Acceleration);
        }
    }
}
