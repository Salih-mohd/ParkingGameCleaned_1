using UnityEngine;

public class Sedan2 : BaseCar
{

    
    

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void ApplyBrakes()
    {
        foreach(var wheel  in wheels)
        {
            wheel.WheelCollider.brakeTorque = brakeTorque* 1.5f;
        }
    }

    

}
