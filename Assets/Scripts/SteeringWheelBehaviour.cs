using UnityEngine;
using System.Collections;
public class SteeringWheelBehaviour : MonoBehaviour
{
    public GameObject SteeringWheel;
    public GameObject buggy;
    public float maxSteerAngle = 45;
    private Vector3 startDir;

    void Start()
    {
        startDir = SteeringWheel.transform.forward;
    }
    public float getSteeringAngle()
    {

        float steeringWheelAngle = Mathf.Round(Vector3.SignedAngle((buggy.transform.rotation * startDir), SteeringWheel.transform.forward, Vector3.up));
        return maxSteerAngle * (steeringWheelAngle / 170);

    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(SteeringWheel.transform.position, SteeringWheel.transform.position + (buggy.transform.rotation * startDir));
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(SteeringWheel.transform.position, SteeringWheel.transform.position + SteeringWheel.transform.up);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(SteeringWheel.transform.position, SteeringWheel.transform.position + SteeringWheel.transform.forward);
    }
}
