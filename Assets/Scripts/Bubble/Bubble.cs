using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static float m_Speed = 0.05f;
    private Vector3 m_Direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_Direction;
    }

    public void Init(Vector3 direction)
    {
        m_Direction = direction * m_Speed;
    }
}
