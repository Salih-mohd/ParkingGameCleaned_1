using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class BaseCar : MonoBehaviour
{
    [Header("car properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    // Inputs
    protected float vInput;
    protected float hInput;
    [HideInInspector] public bool isBraking;
    [HideInInspector] public bool isForwardGear = true;

    // References
    protected WheelControl[] wheels;
    protected Rigidbody rigidBody;

    public InputActionAsset inputActions;

    // actions
    protected InputAction moveAction;
    protected InputAction brakeAction;
    protected InputAction gearAction;
    protected InputAction camSwitchAction;
    //dance actions
    public InputAction danceActUp;
    public InputAction danceActDown;
    public InputAction danceActLeft;
    public InputAction danceActRight;
    public InputAction dancingOn;


    protected Vector2 moveActVal;

    // cam references
    public CinemachineCamera carCam01;
    public CinemachineCamera carCam02;
    protected bool isSwitching;

    //rear cam references
    public Camera rearCam;
    public GameObject RimageRear;


    // Steering wheel
    public Transform steeringWheel;
    public float steeringWheelRange = 180f;

    //skid marks
    //float minSkidVelocity = 6f;
    [SerializeField] private TrailRenderer[] skidMarks=new TrailRenderer[2];

    bool isSkidding;


    // Dancing settings

    public bool isDancing;
    private float danceSuspensionFront=.3f;
    private float danceSuspensionBack = .3f;

    private float danceSuspensionLeft = .3f;
    private float danceSuspensionRight = .3f;




    // car engine sound ************

    protected CarAudio audio;




    //****************

    // brake and reverse lights

    public Material brakeMaterial;
    public Color brakingColor;
    public Color reverseColor;
    private float intensityWhenBraking = 8f;
    private float intensityWhenNotBraking = 3f;
    //protected LightController lightController;

    // references to scripts
    public UiManager uiManager;








    protected virtual void Awake()
    {
        moveAction = inputActions.FindAction("Car/Move");
        brakeAction = inputActions.FindAction("Break");
        gearAction = inputActions.FindAction("GearSwitch");
        camSwitchAction = inputActions.FindAction("CamSwitch");


        //dance actions
        dancingOn = inputActions.FindAction("isDancing");
        danceActUp = inputActions.FindAction("DanceUp");
        danceActDown = inputActions.FindAction("DanceDown");
        danceActLeft = inputActions.FindAction("DanceL");
        danceActRight = inputActions.FindAction("DanceR");
    }

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;
        wheels = GetComponentsInChildren<WheelControl>();

        //carAudio=GetComponent<AudioSource>();
        audio = GetComponent<CarAudio>();
       // lightController=GetComponentInChildren<LightController>();
    }

    protected virtual void OnEnable() => inputActions.FindActionMap("Car").Enable();
    protected virtual void OnDisable() => inputActions.FindActionMap("Car").Disable();


    protected virtual void Update()
    {

        // Engine sound***********

        audio.UpdateSound();

        //light setup
       // lightController.UpdateLight();

        // dancing mod on or off

        if(dancingOn.WasPerformedThisFrame())
        {
            isDancing = !isDancing;
            uiManager.DancingKeysOnAndOff(isDancing);
        }


        if (!isDancing)
        {
            rigidBody.constraints=RigidbodyConstraints.None;

           // moving
           moveActVal = moveAction.ReadValue<Vector2>();
            vInput = moveActVal.y > 0 ? 1f : 0f;
            hInput = moveActVal.x;

            // breaking
            isBraking = brakeAction.IsPressed();


            // skid marcks

            //var speedNowForSkid = rigidBody.linearVelocity;

            if (isBraking)
            {
                // enabling skidmarks
                // isSkidding = true ;
                ToggleSkidMarks(isBraking);

                

                


            }
            else
            {
                ToggleSkidMarks(isBraking);

                

            }

            // cam switch
            if (camSwitchAction.WasPressedThisFrame())
            {
                isSwitching = !isSwitching;
                //Debug.Log("switching cam "+isSwitching);

                if (isSwitching)
                {
                    carCam01.Priority = 0;
                    carCam02.Priority = 1;

                }
                else
                {
                    carCam02.Priority = 0;
                    carCam01.Priority = 1;


                }
            }

            // Gear actions
            if (gearAction.WasPerformedThisFrame())
            {

                isForwardGear = !isForwardGear;
                GameManager.instance.TriggerGearChangingUi(isForwardGear);
                // Debug.Log("forward gear "+isForwardGear);

            }

            // brake light settings and reverse light settings


            BrakeAndReverseLights();

            // rearview cam

            if (isSwitching)
            {
                if (!isForwardGear)
                {
                    RimageRear.SetActive(true);
                }
                else RimageRear.SetActive(false);


            }
            else RimageRear.SetActive(false);

        }else if (isDancing)
        {
            rigidBody.constraints=RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ;
            DancingMode();
        }

        


    }

    protected virtual void FixedUpdate()
    {
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);


        // Steering wheel rotation
        //float targetAngle = hInput * steeringWheelRange;
        //targetAngle = Mathf.Clamp(targetAngle, -steeringWheelRange, steeringWheelRange);

        //float currentZAngle = steeringWheel.localEulerAngles.z;
        //float newZ = Mathf.LerpAngle(currentZAngle, targetAngle, Time.deltaTime * 5f);
        //steeringWheel.localRotation=Quaternion.Euler(0,0,newZ);



        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isBraking)
            {
                wheel.WheelCollider.motorTorque = 0;
                ApplyBrakes();
            }
            else if (vInput > 0)
            {
                if (wheel.motorized)
                {
                    float dir = isForwardGear ? 1 : -1;
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque * dir;
                }
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = 0f;
            }
        }
    }

    public virtual void ApplyBrakes()
    {
        foreach (var wheel in wheels)
        {
            wheel.WheelCollider.brakeTorque=brakeTorque;
        }
    }



    private void ToggleSkidMarks(bool toggle)
    {

        if( wheels[2].WheelCollider.isGrounded)
        {
            foreach (var skidMark in skidMarks)
            {
                skidMark.emitting = toggle;
            }
        }

        
    }

    private void DancingMode()
    {

        if (wheels.All(w => w.WheelCollider.isGrounded))
        {
            if (danceActUp.IsPressed())
            {
                danceSuspensionFront = .9f;
                //front wheels
                wheels[0].WheelCollider.suspensionDistance = danceSuspensionFront;
                wheels[1].WheelCollider.suspensionDistance = danceSuspensionFront;
            }
            else if (danceActDown.IsPressed())
            {
                danceSuspensionBack = .9f;
                wheels[2].WheelCollider.suspensionDistance = danceSuspensionBack;
                wheels[3].WheelCollider.suspensionDistance = danceSuspensionBack;

            }
            else if (danceActLeft.IsPressed())
            {
                danceSuspensionLeft = .9f;
                wheels[0].WheelCollider.suspensionDistance = danceSuspensionLeft;
                wheels[2].WheelCollider.suspensionDistance = danceSuspensionLeft;
            }
            else if (danceActRight.IsPressed())
            {
                danceSuspensionRight = .9f;
                wheels[1].WheelCollider.suspensionDistance = danceSuspensionRight;
                wheels[3].WheelCollider.suspensionDistance = danceSuspensionRight;
            }

            else
            {
                wheels[0].WheelCollider.suspensionDistance = .3f;
                wheels[1].WheelCollider.suspensionDistance = .3f;
                wheels[2].WheelCollider.suspensionDistance = .3f;
                wheels[3].WheelCollider.suspensionDistance = .3f;
            }
        }
        
        
  
    }



    public void BrakeAndReverseLights()
    {
        if (!isForwardGear)
        {
            brakeMaterial.SetColor("_EmissionColor", reverseColor * intensityWhenBraking);
            brakeMaterial.EnableKeyword("_EMISSION");
        }
        else if (isForwardGear && !isBraking)
        {
            brakeMaterial.SetColor("_EmissionColor", brakingColor * intensityWhenNotBraking);
            brakeMaterial.EnableKeyword("_EMISSION");
        }
        else if (isBraking)
        {
            brakeMaterial.SetColor("_EmissionColor", brakingColor * intensityWhenBraking);
            brakeMaterial.EnableKeyword("_EMISSION");
        }
    }









}
