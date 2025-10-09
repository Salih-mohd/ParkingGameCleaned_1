using System.Linq;
using UnityEngine;

public class ParkingDetector : MonoBehaviour
{

    public bool isParking;
    public bool isPicking;

    public Color outSideColor;
    public Color inSideColor;

    //public UiManager uiManager;

    private WheelCollider[] wheelColliders;
    public GameObject[] carPrefabs;
    Bounds parkingBound;
    public GameObject[] parkingEffects;

    public Collider parkingZone;
    private bool isInside;

    // 00FFEC parked color
    // E9FF00 un parked color 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isParking)
        {
           
            parkingBound = parkingZone.bounds;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        //if (isParking)
        //{
        //    CheckingAllWheels();
        //}       
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (isParking)
        {
            
            if (other.gameObject.CompareTag("Car"))
            {
                wheelColliders = other.gameObject.GetComponentsInChildren<WheelCollider>();
                CheckingAllWheels();
            }
        }



    }

    private void CheckingAllWheels()
    {
        
        isInside = true;

        foreach (var wheel in wheelColliders)
        {
            Vector3 wheelPos;
            Quaternion wheelRot;

            wheel.GetWorldPose(out wheelPos, out wheelRot);

            if(!parkingBound.Contains(wheelPos))
            {
                isInside = false;
            }
        }

        if(isInside)
        {
            ColorChange(inSideColor);
            Invoke("AboutToWin", 5);
            


        }
        else
        {
            ColorChange(outSideColor);
            CancelInvoke("AboutToWin");
            

        }
    }

    //private void GettingTheWheels()
    //{
    //    foreach (GameObject car in carPrefabs)
    //    {
    //        if (car.activeInHierarchy)
    //        {
    //            wheelColliders = car.GetComponentsInChildren<WheelCollider>();

    //        }

    //    }
    //}

    private void AboutToWin()
    {
        GameManager.instance.TriggerWin();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPicking)
        {
            if(other.gameObject.CompareTag("Car")) ColorChange(inSideColor);
            Invoke("TriggeringEvent", 5);
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (isPicking)
        {
            if (other.gameObject.CompareTag("Car")) ColorChange(outSideColor);
            CancelInvoke("TriggeringEvent");
        }
    }

    private void ColorChange(Color color)
    {
        foreach (var effect in parkingEffects)
        {
            var main = effect.GetComponent<ParticleSystem>().main;
            main.startColor = color;
        }
    }

    private void TriggeringEvent()
    {
        MissionEventManager.instance.TriggerMission();
    }

    



}
