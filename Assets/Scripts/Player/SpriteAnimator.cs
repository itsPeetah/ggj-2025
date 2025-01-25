using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public List<Sprite> m_Sprites = new List<Sprite>();
    public float m_Speed = 0.1f;

    private int m_CurrentFrame = 0;
    private float m_NextIn = 0.0f;
    private bool m_Stopped = true;

    // Use this for initialization
    void Start()
    {
        m_NextIn = m_Speed;
    }

    public void SetPaused(bool paused)
    {
        m_Stopped = paused;   
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Stopped) { return; }

        m_NextIn -= Time.deltaTime;
        if (m_NextIn < 0)
        {
            m_NextIn = m_Speed;
            PlayNext();
        }
    }

    private void PlayNext()
    {
        if (m_CurrentFrame < m_Sprites.Count)
        {
            GetComponent<SpriteRenderer>().sprite = m_Sprites[m_CurrentFrame];
        }

        m_CurrentFrame = (m_CurrentFrame + 1) % m_Sprites.Count;
    }
}
