using UnityEngine;

public class CarAudio : MonoBehaviour
{
    [SerializeField] private float minSpeedForSound;
    [SerializeField] private float maxSpeedForSound;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    public AudioSource carAudio;
    public Rigidbody rigidBody;

    void Start()
    {
        //carAudio = GetComponent<AudioSource>();
        //rigidBody = GetComponent<Rigidbody>();
    }

    public void UpdateSound()
    {
        float currentSpeed = rigidBody.linearVelocity.magnitude;
        float pitchFromCar = currentSpeed / 50f;
        if (currentSpeed < minSpeedForSound)
            carAudio.pitch = minPitch;
        else if (currentSpeed > maxSpeedForSound)
            carAudio.pitch = maxPitch;
        else
            carAudio.pitch = minPitch + pitchFromCar;
    }
}