using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    private CharacterMovement movement;
    private new Transform transform;

    [Header("Knockback")]
    public LayerTrigger playerDetector;
    public Vector2 knockbackForce = new Vector2(4, 4);
    public float knockbackDuration = 0.5f;

    [Header("Path")]
    public BoxCollider2D patrolArea;
    public bool facingLeft = false;
    public float endOfPathWaitDuration = 1f;


    private Vector3 boundsMin;
    private Vector3 boundsMax;
    private bool isMoving;

    private void OnEnable()
    {
        playerDetector.onTriggerEnter += HandlePlayerEnter;
    }

    private void OnDisable()
    {
        playerDetector.onTriggerEnter -= HandlePlayerEnter;
    }

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        transform = gameObject.transform;

        patrolArea.transform.parent = null;

        Bounds b = patrolArea.bounds;
        boundsMin = b.min;
        boundsMax = b.max;

        patrolArea.gameObject.SetActive(false);

        SetMovementDirection(facingLeft ? Vector2.left : Vector2.right);
    }

    private void Update()
    {
        if (!isMoving) return;

        bool overshootLeft = facingLeft && transform.position.x <= boundsMin.x;
        bool overshootRight = !facingLeft && transform.position.x >= boundsMax.x;

        if (overshootLeft || overshootRight)
        {
            isMoving = false;
            StartCoroutine(DoFlip());
        }

    }

    public void SetMovementDirection(Vector2 dir)
    {
        isMoving = dir.sqrMagnitude > 0;
        movement.currentMoveInput = dir;
    }

    private IEnumerator DoFlip()
    {
        yield return new WaitForSeconds(endOfPathWaitDuration);
        Flip();
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        SetMovementDirection(facingLeft ? Vector2.left : Vector2.right);
    }

    public void HandlePlayerEnter(Collider2D player)
    {
        if (player.TryGetComponent(out Damageable dm))
        {
            dm.TakeDamage();
        }

        if (player.TryGetComponent(out KnockbackReceiver kb))
        {
            Vector2 force = knockbackForce;
            if (transform.position.x > player.transform.position.x)
            {
                force.x *= -1;
            }
            kb.GiveKnockback(force, knockbackDuration);
            Flip();
        }
    }
}
