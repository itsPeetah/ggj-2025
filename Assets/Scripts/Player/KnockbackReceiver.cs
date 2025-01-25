using System.Collections;
using UnityEngine;

public class KnockbackReceiver : MonoBehaviour
{
    private CharacterMovement movement;
    private Rigidbody2D rbody;

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        rbody = GetComponent<Rigidbody2D>();
    }

    public void GiveKnockback(Vector2 force, float time)
    {
        Debug.Log("KB!!");
        StartCoroutine(DoDisableMovement(force, time));
    }

    public IEnumerator DoDisableMovement(Vector2 force, float time)
    {
        movement.enabled = false;
        rbody.AddForce(force);
        yield return new WaitForSeconds(time);
        movement.enabled = true;
    }


}
