using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public GameObject disc;
    public float rotationSpeed = 1;
    public float mass_plate, mass_rim;
    public float force_friction = 0.02f;
    private float radius_plate = 1, radius_rim = 2, acceleration;
    private float timer = 0;
    private bool accelerating;
    // Start is called before the first frame update
    void Start()
    {
        //calculate rotational inertia I=(m1r^2)+(m2r^2) m1=mass of rim m2 = mass of disc & r = radius
        float rotational_inertia = (mass_plate * Mathf.Pow(radius_plate, 2)) + (mass_rim * Mathf.Pow(radius_rim, 2));

        //find the net torque which is just friction as it's the only applied force
        float net_torque = radius_rim * force_friction;

        //find the acceleration acting on the disc
        acceleration = net_torque / rotational_inertia;
    }

    // Update is called once per frame
    void Update()
    {
        //find new velocity due to acceleration current_V - acceleration
        if (rotationSpeed <= 0 && accelerating)
        {
            rotationSpeed = 0;
            Debug.Log(disc.name +  " time rotating: " + Mathf.Round(timer*10)/10);
            accelerating = false;
        } else if (accelerating)
        {
            rotationSpeed -= acceleration;
            timer += 1*Time.deltaTime;
        }

        transform.RotateAround(transform.position, transform.forward * -1, Time.deltaTime * 90f * rotationSpeed);
    }

    void OnValidate()
    {
        float rotational_inertia = (mass_plate * Mathf.Pow(radius_plate, 2)) + (mass_rim * Mathf.Pow(radius_rim, 2));
        accelerating = true;
        timer = 0;
        Debug.Log(disc.name + " initial force: " + rotational_inertia * rotationSpeed);
    }
}
