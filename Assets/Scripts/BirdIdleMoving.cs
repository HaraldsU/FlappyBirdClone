using UnityEngine;

public class BirdIdleMoving : MonoBehaviour
{
    public float amplitude = .5f; // Amplitude of the oscillation (vertical movement)
    public float frequency = 1.0f; // Frequency of the oscillation (oscillations per second)
    private Vector3 initialPosition;
    public UnityEngine.UI.Image black;
    public Animator anim;
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        // Calculate the vertical offset based on a sine wave
        float yOffset = Mathf.Sin(2 * Mathf.PI * frequency * Time.time) * amplitude;

        // Update the object's position
        transform.position = initialPosition + new Vector3(0, yOffset, 0);
    }
}
