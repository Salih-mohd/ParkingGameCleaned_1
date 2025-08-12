using UnityEngine;
using UnityEngine.Events;

public class MissionEventManager : MonoBehaviour
{

    public static MissionEventManager instance { get; private set; }

    public UnityEvent missionIsStarted = new UnityEvent();

    private void Awake()
    {
        instance = this;
    }

    public void TriggerMission()
    {
        missionIsStarted?.Invoke();
    }
}
