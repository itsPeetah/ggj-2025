using System.Collections;
using UnityEngine;

public class Squisher : MonoBehaviour
{
    private enum State
    {
        Neutral,
        Jump_SquishDown,
        Jump_SquishUp,
        Jump_Flying,
        Land,
        
    };
    public Transform m_Visuals;
    private State m_State = State.Neutral;
    private Vector3 m_InitScale;

    private float m_StateTime;
    private float m_Timeleft;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Visuals == null) { return; }
        switch (m_State)
        {
            case State.Neutral:
                {
                    break;
                }
            case State.Jump_SquishDown:
                {
                    break;
                }
            case State.Jump_SquishUp:
                {
                    break;
                }
            case State.Jump_Flying:
                {
                    break;
                }
            case State.Land:
                {
                    break;
                }
        }
    }

    public void JumpStart(Transform visuals)
    {
        if (m_Visuals == null)
        {
            m_Visuals = visuals;
            m_InitScale = visuals.localScale;
        }
        NextState(State.Jump_SquishDown, 0.2f);
        // squish down + out
        // squishin + up
        // rot
    }

    private void NextState(State state, float withDuration)
    {
        m_State = state;
        m_StateTime = withDuration;
        m_Timeleft = withDuration;
    }

    public void JumpEnd()
    {
        m_State = State.Land;
        // in reverse
        // squish down + out
        // squishin + up
        // rot
    }

    public void ToNeutral()
    {
        m_Visuals.localScale = m_InitScale;
        //m_Visuals.localRotation.Set(0, 0, 0);
    }
}
