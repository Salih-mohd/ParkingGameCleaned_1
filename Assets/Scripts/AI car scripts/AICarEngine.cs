using UnityEngine;

public class AICarEngine : MonoBehaviour
{
    public AIPath path;
    public float maxSteerAngle = 45f;
    public float turnSpeed = 5f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 80f;
    public float maxBreakTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public bool isBraking;

    [Header("Sensors")]
    public float sensorLength = 5f;
    public GameObject frontSensor;
    public GameObject leftSensor;
    public GameObject rightSensor;
    public float frontSensorAngle = 30f;
        

    

    private int currentNode = 0;
    private bool avoiding=false;
    private float targetSteerAngle = 0;


    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass=centerOfMass;
    }

    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckWayPointDistance();
        Braking();
        LerpToSteerAngle();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartingPosition=frontSensor.transform.position;
        sensorStartingPosition.z = frontSensor.transform.position.z;

        Vector3 leftSensorStartingPos=leftSensor.transform.position;
        leftSensorStartingPos.z = leftSensor.transform.position.z;

        Vector3 rightSensorStartingPos=rightSensor.transform.position;  
        rightSensorStartingPos.z=rightSensor.transform.position.z;

        float avoidMultiplier = 0;
        avoiding = false;

        // front sensor

        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(sensorStartingPosition, frontSensor.transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("AiSlowArea"))
                {
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;

                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                        Debug.DrawLine(sensorStartingPosition, hit.point, Color.red);
                }

            }
        }
        

        // left sensor

        if (Physics.Raycast(leftSensorStartingPos, leftSensor.transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("AiSlowArea"))
            {
                avoiding = true;
                avoidMultiplier += 1f;
                Debug.DrawLine(leftSensorStartingPos, hit.point, Color.red);
            }
                
        }

        // left angled sensor

        if (Physics.Raycast(leftSensorStartingPos, Quaternion.AngleAxis(-frontSensorAngle, leftSensor.transform.up
            ) * leftSensor.transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("AiSlowArea"))
            {
                avoiding = true;
                avoidMultiplier += .5f;
                Debug.DrawLine(leftSensorStartingPos, hit.point, Color.red);
            }

        }

        //right sensor

        if (Physics.Raycast(rightSensorStartingPos, rightSensor.transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("AiSlowArea"))
            {
                avoiding = true;
                avoidMultiplier -= 1f;


                Debug.DrawLine(rightSensorStartingPos, hit.point, Color.red);
            }

                
        }

        //right angled sensor

        else if (Physics.Raycast(rightSensorStartingPos, Quaternion.AngleAxis(frontSensorAngle,rightSensor.transform.up)*rightSensor.transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("AiSlowArea"))
            {
                avoiding = true;
                avoidMultiplier -= .5f;
                Debug.DrawLine(rightSensorStartingPos, hit.point, Color.red);
            }

               
        }


        if (avoiding)
        {

            targetSteerAngle=maxSteerAngle*avoidMultiplier;

           
        }

    }


    private void ApplySteer()
    {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(path.nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        targetSteerAngle = newSteer;

        
    }

    private void Drive()
    {
        // Calculate current speed in km/h (velocity magnitude in m/s * 3.6 for km/h)
        currentSpeed = GetComponent<Rigidbody>().linearVelocity.magnitude * 3.6f;

        // Apply torque if below maxSpeed
        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            // Stop accelerating if at or above maxSpeed
            wheelFL.motorTorque = 0f;
            wheelFR.motorTorque = 0f;
        }
    }

    private void CheckWayPointDistance()
    {
        // Check if path or nodes are valid
        if (path == null || path.nodes == null || path.nodes.Count == 0) return;

        // Get distance to current waypoint
        float distance = Vector3.Distance(transform.position, path.nodes[currentNode].position);

        // If close enough to the current waypoint (e.g., within 2 units), move to the next
        if (distance < 2f)
        {
            currentNode++; // Move to the next waypoint
                           // If reached the end of the list, loop back to the first waypoint
            if (currentNode >= path.nodes.Count)
            {
                currentNode = 0;
            }
        }
    }


    private void Braking()
    {
        if(isBraking)
        {
            wheelFL.brakeTorque=maxBreakTorque;
            wheelFR.brakeTorque = maxBreakTorque;
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
        }
    }

    private void LerpToSteerAngle()
    {
        wheelFL.steerAngle=Mathf.Lerp(wheelFL.steerAngle,targetSteerAngle,Time.deltaTime*turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed); 
    }

}
