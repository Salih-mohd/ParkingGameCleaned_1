using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DancingController : MonoBehaviour
{
    [Header("Dancing references")]
    public GameObject ShowInfo;
    public AudioClip[] audioClips;
    public AudioSource shoutingAudioSourse;
    public AudioSource bgMusicAudioSource;
    public GameObject[] characters;
    public GameObject parkingSpot;

    private int count = 0;
    private GameManager gameManager;
    private bool gameOver;
    

    private void Start()
    {
        ShowInfo.SetActive(false);
        gameManager=FindAnyObjectByType<GameManager>();        
    }

    public void DanceTime()
    {
        gameManager.OnMission.Invoke();
        parkingSpot.SetActive(false);
        bgMusicAudioSource.volume = .3f;
        bgMusicAudioSource.Play();
        ShowInfo.SetActive(true);
        //Debug.Log("instruction is on");
        StartCoroutine(TickTock());
        

    }

    IEnumerator TickTock()
    {
        yield return new WaitForSeconds(3);
        ShowInfo.SetActive(false);
        parkingSpot.SetActive(false );
        //Debug.Log("instruction is offf");
        StartCoroutine(WinningGame());
    }

    IEnumerator WinningGame()
    {
        Debug.Log("inside winning game coroutine");
        yield return new WaitForSeconds(10);
        gameOver = true;
        GameManager.instance.TriggerWin();
    }

    
    
}
