using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    const float G = 6.674f;

    public Rigidbody rb;
    public static List<Gravity> otherGravitationalObjects;

    public float MaxAttractionDistance = 100f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (var toAttract in otherGravitationalObjects)
        {
            if (toAttract != null)
            {
                Attract(toAttract);
            }
        }
    }

    void OnEnable()
    {
        if (otherGravitationalObjects == null) {
            otherGravitationalObjects = new List<Gravity>();
        }

        otherGravitationalObjects.Add(this);
    }


    private void OnDisable()
    {
        otherGravitationalObjects.Remove(this);
    }

    // Use this for initialization
    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        var direction = rb.position - otherRb.position;
        var distance = direction.magnitude;
        if (distance < 5e-3f || distance > other.MaxAttractionDistance) {
            return;
        }
        var forceMaginitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        var force = direction.normalized * forceMaginitude;
        otherRb.AddForce(force);
    }

    public void ControlUseGravity() 
    {
        rb.useGravity = !rb.useGravity;
        if (!rb.useGravity)
        {
            rb.AddForce(Vector3.up * 10);
        }
    }

    public void ControlMass(float mass)
    {
        rb.mass = mass;
    }
}
