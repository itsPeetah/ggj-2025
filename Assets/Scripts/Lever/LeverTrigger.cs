using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour
{
    public float onDuration = 10f;
    
    // Colors to indicate off/on states
    private Color offColor = Color.white;
    private Color onColor = Color.green;
    
    private bool isOn = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Grab the SpriteRenderer to change color
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOn)
        {
            // Check if collided object is on "Player", "Bubble" or Enemy layer
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
                collision.gameObject.layer == LayerMask.NameToLayer("Bubble")|| collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                StartCoroutine(LeverRoutine());
            }
        }
    }

    private IEnumerator LeverRoutine()
    {
        // Turn lever ON
        isOn = true;
        if (spriteRenderer != null)
            spriteRenderer.color = onColor;

        Debug.Log("Lever turned ON!");

        // Stay on for 10 seconds
        yield return new WaitForSeconds(onDuration);

        // Turn lever OFF
        isOn = false;
        if (spriteRenderer != null)
            spriteRenderer.color = offColor;

        Debug.Log("Lever turned OFF.");
    }
}
