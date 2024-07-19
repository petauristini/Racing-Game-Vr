using UnityEngine;
using System.Collections;
public class CarBehaviour : MonoBehaviour
{
    public WheelCollider wheelColliderFL;
    public WheelCollider wheelColliderFR;
    public WheelCollider wheelColliderRL;
    public WheelCollider wheelColliderRR;
    public SteeringWheelBehaviour SteeringWheelBehaviour;
    public GameObject SteeringWheel;
    public GameObject Accelerator;
    public GameObject buggy;
    public float maxTorque = 500;
    private Vector3 startTorque;
    public Transform centerOfMass;
    private Rigidbody _rigidbody;
    public float sidewaysStiffness = 1.5f;
    public float forewardStiffness = 1.5f;

    void Start() {
        startTorque = Accelerator.transform.up;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(centerOfMass.localPosition.x,
        centerOfMass.localPosition.y,
        centerOfMass.localPosition.z);
        SetWheelFrictionStiffness(forewardStiffness, sidewaysStiffness);
    }

    void FixedUpdate()
    {
        float acceleration = Mathf.Round(Accelerator.transform.localEulerAngles.x);
        if (acceleration > 20) {
            acceleration = acceleration - 360;
        }
        float steeringWheelAngle = SteeringWheelBehaviour.getSteeringAngle();
        SetSteerAngle(steeringWheelAngle);

        // Determine if the car is driving forwards or backwards
        bool velocityIsForeward = Vector3.Angle(transform.forward,
         _rigidbody.velocity) < 50f;
        // get the current speed from the velocity vector
        float _currentSpeedKMH = _rigidbody.velocity.magnitude * 3.6f;
        // Determine if the cursor key input means braking
        bool doBraking = _currentSpeedKMH > 0.5f &&
        (acceleration < 0 && velocityIsForeward ||
        acceleration > 0 && !velocityIsForeward);
        if (doBraking)
        {
            wheelColliderFL.brakeTorque = 5000;
            wheelColliderFR.brakeTorque = 5000;
            wheelColliderRL.brakeTorque = 5000;
            wheelColliderRR.brakeTorque = 5000;
            wheelColliderFL.motorTorque = 0;
            wheelColliderFR.motorTorque = 0;
        }
        else
        {
            wheelColliderFL.brakeTorque = 0;
            wheelColliderFR.brakeTorque = 0;
            wheelColliderRL.brakeTorque = 0;
            wheelColliderRR.brakeTorque = 0;
            wheelColliderFL.motorTorque = maxTorque * (acceleration / 15);
            wheelColliderFR.motorTorque = wheelColliderFL.motorTorque;
        }

    }
    void SetSteerAngle(float angle)
    {
        wheelColliderFL.steerAngle = angle;
        wheelColliderFR.steerAngle = angle;
    }
    void SetWheelFrictionStiffness(float newForwardStiffness, float newSidewaysStiffness)
    {
        WheelFrictionCurve fwWFC = wheelColliderFL.forwardFriction;
        WheelFrictionCurve swWFC = wheelColliderFL.sidewaysFriction;
        fwWFC.stiffness = newForwardStiffness;
        swWFC.stiffness = newSidewaysStiffness;
        wheelColliderFL.forwardFriction = fwWFC;
        wheelColliderFL.sidewaysFriction = swWFC;
        wheelColliderFR.forwardFriction = fwWFC;
        wheelColliderFR.sidewaysFriction = swWFC;
        wheelColliderRL.forwardFriction = fwWFC;
        wheelColliderRL.sidewaysFriction = swWFC;
        wheelColliderRR.forwardFriction = fwWFC;
        wheelColliderRR.sidewaysFriction = swWFC;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(SteeringWheel.transform.position, SteeringWheel.transform.position + (buggy.transform.rotation * startDir));
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(SteeringWheel.transform.position, SteeringWheel.transform.position + SteeringWheel.transform.up);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(SteeringWheel.transform.position, SteeringWheel.transform.position + SteeringWheel.transform.forward);
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(Accelerator.transform.position, Accelerator.transform.position + (buggy.transform.rotation * startTorque));
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Accelerator.transform.position, Accelerator.transform.position + Accelerator.transform.up);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(Accelerator.transform.position, Accelerator.transform.position + Accelerator.transform.right);
    }
}
