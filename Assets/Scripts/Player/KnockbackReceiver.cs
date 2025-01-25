using System.Collections;
using UnityEngine;

public class KnockbackReceiver : MonoBehaviour
{
    private CharacterMovement movement;
    private Rigidbody2D rbody;

    public Vector2 knockbackForce = new Vector2(4, 4);
    public float knockbackDuration = 0.5f;
    public bool hasJustReceivedKnockback;

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        rbody = GetComponent<Rigidbody2D>();
    }

    public void GiveKnockback(Vector2 from)
    {
        if (hasJustReceivedKnockback) return;

        Vector2 force = knockbackForce;
        if (from.x > transform.position.x)
            force.x *= -1;

        StartCoroutine(DoDisableMovement(force, knockbackDuration));
    }

    public IEnumerator DoDisableMovement(Vector2 force, float time)
    {
        bool previousEnabled = movement.enabled;
        hasJustReceivedKnockback = true;
        movement.enabled = false;
        rbody.AddForce(force);
        yield return new WaitForSeconds(time);
        movement.enabled = previousEnabled;
        hasJustReceivedKnockback = false;
    }


}
