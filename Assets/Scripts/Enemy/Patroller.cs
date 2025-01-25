using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    private CharacterMovement movement;
    private new Transform transform;

    [Header("Path")]
    public BoxCollider2D patrolArea;
    public bool facingLeft = false;
    public float endOfPathWaitDuration = 1f;


    private Vector3 boundsMin;
    private Vector3 boundsMax;
    private bool isMoving;

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
            StartCoroutine(Flip());
        }

    }

    public void SetMovementDirection(Vector2 dir)
    {
        isMoving = dir.sqrMagnitude > 0;
        movement.currentMoveInput = dir;
    }

    private IEnumerator Flip()
    {
        yield return new WaitForSeconds(endOfPathWaitDuration);
        facingLeft = !facingLeft;
        SetMovementDirection(facingLeft ? Vector2.left : Vector2.right);
    }
}
