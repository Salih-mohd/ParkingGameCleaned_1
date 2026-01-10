using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    [Header("Fuel datas")]
    public Slider fuelBar;
    public float totalTime;
    public float fuelSpeed;
    public bool isWin;

    private float currentTime;
    private bool isSubscribed;
    //private GameManager gameManager;

  

    private void OnEnable()
    {
        currentTime = 3;

        TrySubscribe();


    }

    private void Start()
    {
        TrySubscribe();
    }

    private void OnDisable()
    {
        GameManager.instance.onLevelWin.RemoveListener(BoolChanger);
        GameManager.instance.OnMission.RemoveListener(BoolChanger);
    }


    private void Update()
    {

        if (!isWin)
        {
            if (Time.time > currentTime && fuelBar.value >= 0)
            {
                if (fuelBar.value <= 0) GameManager.instance.TriggerLose();

                fuelBar.value -= fuelSpeed * Time.deltaTime;
                currentTime = Time.time + 1;
            }
        }

        
    }
    private void BoolChanger()
    {
        isWin = true;
    }

    private void TrySubscribe()
    {
        if(!isSubscribed && GameManager.instance!= null)
        {
            GameManager.instance.onLevelWin.AddListener(BoolChanger);
            GameManager.instance.OnMission.AddListener(BoolChanger);
            isSubscribed = true;
        }
    }
}
