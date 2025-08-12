using UnityEngine;
using UnityEngine.UI;

public class CarSelectionMenu : MonoBehaviour
{

    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button playButton;
    [SerializeField] private string[] cars;

    private int unlockedLevel;

    private int currentCar=0;


    private void Awake()
    {
        SelectCarFromList(0);

    }

    private void Start()
    {
        playButton.onClick.AddListener(()=> GameManager.instance.SelectCar(cars[currentCar]));
        unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
    }



    private void SelectCarFromList(int index)
    {
        int count = transform.childCount;
        prevButton.interactable = (index > 0);
        nextButton.interactable = (index != count - 1);

        

        for (int i = 0; i < count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i== index);  
        }

        if(unlockedLevel<5)
        {
            if(index>=1) playButton.interactable = false;
            else playButton.interactable = true;
        }
    }

    

    public void ChangeCar(int change)
    {
        currentCar += change;
        SelectCarFromList(currentCar);
    }
}
