using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    private CharacterMovement movement;
    private new Transform transform;

    [Header("Knockback")]
    public LayerTrigger playerDetector;

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

        bool overshootLeft = transform.position.x <= boundsMin.x;
        bool overshootRight = transform.position.x >= boundsMax.x;

        if ((overshootLeft && facingLeft) || (overshootRight && !facingLeft))
        {

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
        isMoving = false;
        SetMovementDirection(Vector2.zero);
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
            kb.GiveKnockback(transform.position);
        }
        if (
            transform.position.x > player.transform.position.x && facingLeft ||
            transform.position.x < player.transform.position.y && !facingLeft
        )
            Flip();
    }
}
