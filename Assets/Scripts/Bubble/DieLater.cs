using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieLater : MonoBehaviour
{
    public float m_InSeconds = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_InSeconds -= Time.deltaTime;
        if (m_InSeconds <= 0)
        {
            Object.Destroy(transform.gameObject);
        }
    }
}
