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
    private GameManager gameManager;

    private void Start()
    {
        currentTime = 3;
        gameManager=FindAnyObjectByType<GameManager>();
        gameManager.onLevelWin.AddListener(BoolChanger);
        gameManager.OnMission.AddListener(BoolChanger);
    }


    private void Update()
    {

        if (!isWin)
        {
            if (Time.time > currentTime && fuelBar.value >= 0)
            {
                if (fuelBar.value <= 0) gameManager.TriggerLose();

                fuelBar.value -= fuelSpeed * Time.deltaTime;
                currentTime = Time.time + 1;
            }
        }

        
    }
    private void BoolChanger()
    {
        isWin = true;
    }
}
