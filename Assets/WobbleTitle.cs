using UnityEngine;

public class TitleWobble : MonoBehaviour
{
    // How fast the text moves
    public float speed = 5f; 
    // How far the text moves
    public float amplitude = 10f; 

    private Vector3 _originalPosition;

    void Start()
    {
        // Store the original position of the text at the start
        _originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate a new vertical offset using a sine wave
        float wobbleOffset = Mathf.Sin(Time.time * speed) * amplitude;

        // Apply the offset to the GameObject's local position
        transform.localPosition = _originalPosition + new Vector3(0f, wobbleOffset, 0f);
    }
}
