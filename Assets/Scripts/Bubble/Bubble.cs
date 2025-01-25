using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : PoolableObject
{
    private Vector3 m_Direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_Direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Absorb(collision.gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void Init(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        m_Direction = direction;
    }

    private void Absorb(GameObject obj)
    {
        if (transform.localScale.x < obj.transform.localScale.x) { return; }

        var growBy = obj.transform.localScale *= 0.3f;
        transform.localScale += growBy;

        Disable();
    }
}
