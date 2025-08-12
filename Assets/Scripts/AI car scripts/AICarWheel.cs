using Unity.Mathematics;
using UnityEngine;

public class AICarWheel : MonoBehaviour
{
    public WheelCollider targetWheel;

    private Vector3 wheelPosition=new Vector3();
    private Quaternion wheelRotation=new Quaternion();

    private void Update()
    {
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation); 
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }

}
