using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Vector3 m_Direction = Vector3.zero;

    private float m_UntilEnableCollider = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_Direction;

        if (m_UntilEnableCollider > 0)
        {
            m_UntilEnableCollider -= Time.deltaTime;
            if (m_UntilEnableCollider <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Absorb(collision.gameObject);
        }
    }

    public void Init(Vector3 direction)
    {
        m_Direction = direction;
    }

    private void Absorb(GameObject obj)
    {
        if (transform.localScale.x < obj.transform.localScale.x) { return; }

        var growBy = obj.transform.localScale *= 0.3f;
        transform.localScale += growBy;

        Destroy(obj);
    }
}
