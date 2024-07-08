using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardMovement : MonoBehaviour
{
    [SerializeField] Transform[] cornerPoint = new Transform[4];
    RaycastHit[] hit = new RaycastHit[4];

    Rigidbody rb;
    [SerializeField] float forwardSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float forceMultiplier;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(cornerPoint[i], hit[i]);
        }

        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * forwardSpeed, ForceMode.Acceleration);
        rb.AddTorque(transform.up * Input.GetAxis("Horizontal") * rotationSpeed, ForceMode.VelocityChange);
    }

    void ApplyForce(Transform cornerPoint, RaycastHit hit)
    {
        if (Physics.Raycast(cornerPoint.position, - cornerPoint.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - cornerPoint.position.y));
            rb.AddForceAtPosition(transform.up * force * forceMultiplier, cornerPoint.position, ForceMode.Acceleration);
        }
    }
}
