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
        Land_Start,
        Land_End,

    };
    public Transform m_Visuals;
    private State m_State = State.Neutral;
    private Vector3 m_InitScale;

    private float m_StateTime = 1;
    private float m_Timeleft;

    // Use this for initialization
    void Start()
    {
        m_InitScale = m_Visuals.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timeleft -= Time.deltaTime;

        float progress = 1.0f - m_Timeleft / m_StateTime;

        switch (m_State)
        {
            case State.Neutral:
                {
                    break;
                }
            case State.Jump_SquishDown:
                {
                    float scaleY = m_InitScale.y * (1.0f - progress / 10.0f);
                    //float scaleX = m_InitScale.x * (1.0f + progress / 10.0f);
                    m_Visuals.localScale =  new Vector3(m_InitScale.x, scaleY, m_InitScale.z);

                    if (m_Timeleft < 0.0f) { ToState(State.Jump_SquishUp, 0.05f); }
                    break;
                }
            case State.Jump_SquishUp:
                {
                    //float scaleY = m_InitScale.y * (0.9f + progress / 5.0f);
                    //float scaleX = m_InitScale.x * (1.1f - progress / 10.0f);
                    //m_Visuals.localScale = new Vector3(scaleX, scaleY, m_InitScale.z);

                    if (m_Timeleft < 0.0f) { ToState(State.Jump_Flying, 0.05f); }
                    break;
                }
            case State.Jump_Flying:
                {

                    if (m_Timeleft < 0.0f) { ToNeutral(); ToState(State.Neutral, 0.1f); }
                    break;
                }
            case State.Land_Start:
                {
                    float scaleY = m_InitScale.y * (1.0f - progress / 10.0f);
                    float scaleX = m_InitScale.x * (1.0f + progress / 10.0f);
                    m_Visuals.localScale = new Vector3(scaleX, scaleY, m_InitScale.z);

                    if (m_Timeleft < 0.0f) { ToNeutral(); ToState(State.Land_End, 0.05f); }
                    break;
                }
            case State.Land_End:
                {
                    float scaleY = m_InitScale.y * (0.9f + progress / 10.0f);
                    float scaleX = m_InitScale.x * (1.1f - progress / 10.0f);
                    m_Visuals.localScale = new Vector3(scaleX, scaleY, m_InitScale.z);

                    if (m_Timeleft < 0.0f) { ToNeutral(); ToState(State.Neutral, 0.1f); }
                    break;
                }
        }

    }

    public void JumpStart()
    {
        ToState(State.Jump_SquishDown, 0.05f);
        // squish down + out
        // squishin + up
        // rot
    }

    private void ToState(State state, float withDuration)
    {
        m_State = state;
        m_StateTime = withDuration;
        m_Timeleft = withDuration;
    }

    public void JumpEnd()
    {
        ToState(State.Land_Start, 0.05f);
        // in reverse
        // squish down + out
        // squishin + up
        // rot
    }

    public void ToNeutral()
    {
        m_Visuals.localScale = m_InitScale;
        //m_Visuals.localRotation.SetAxisAngle(Vector3.forward, 0);
    }
}
