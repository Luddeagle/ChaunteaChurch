using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float m_maxMoveSpeed = 5f;
    public float m_timeToReachMaxSpeed = 0.1f;

    private Vector3 m_lastStepLocation;
    private Vector3 m_refVelocity;
    private Vector3 m_currentVelocity;
    private CharacterController m_moveController;
    private float m_yVel;

    private Vector3 m_pushDirection;

    private float lastTimeSinceStep, m_baseStepPitch;
    public float timeBetweenStep;
    AudioSource m_audioStep;
    public float m_minDistBeforeStep = 0.3f;

    float m_gracePeriod;

    private void Awake()
    {
        m_moveController = GetComponent<CharacterController>();
        m_currentVelocity = Vector3.zero;
        m_yVel = 0;
        m_audioStep = GetComponent<AudioSource>();
        if (m_audioStep)
            m_baseStepPitch = m_audioStep.pitch;
        m_lastStepLocation = transform.position;
    }

    void Update()
    {
        if (m_gracePeriod > 0)
            m_gracePeriod -= Time.deltaTime;

        UpdateMovement();
        JumpLogic();

        if (m_moveController.isGrounded)
            m_gracePeriod = 0.3f;
    }


    void LateUpdate()
    {
        FootStepSoundLogic();
    }

    void FootStepSoundLogic()
    {
        float distSqr = (transform.position - m_lastStepLocation).sqrMagnitude;
        if (distSqr > m_minDistBeforeStep)
        {
            if (Time.time >= lastTimeSinceStep + timeBetweenStep)
            {
                m_lastStepLocation = transform.position;
                lastTimeSinceStep = Time.time;
                if (m_audioStep)
                {
                    m_audioStep.Play();
                    m_audioStep.pitch = m_baseStepPitch + Random.Range(-0.1f, 0.1f);
                }
            }
        }
    }

    void JumpLogic()
    {
        if (!m_moveController.isGrounded && m_gracePeriod <= 0)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("JUMP");
            m_currentVelocity.y = 8.0f;
        }
    }

    void UpdateMovement()
    {
        // Get input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        xInput *= 0.75f;
        if(yInput < 0)
        {
            yInput *= 0.75f;
        }

        m_yVel = m_currentVelocity.y;
        m_currentVelocity.y = 0;
        // Get new velocity, rotated by camera rotation
        Vector3 targetVelocity = transform.rotation * new Vector3(xInput, 0, yInput);//.normalized;
        targetVelocity *= m_maxMoveSpeed;
        m_currentVelocity = Vector3.SmoothDamp(m_currentVelocity, targetVelocity, ref m_refVelocity, m_timeToReachMaxSpeed);

        // Apply gravity
        if (m_moveController.isGrounded)
            m_yVel = 0;

        m_yVel -= 20.0f * Time.deltaTime;
        m_currentVelocity.y = m_yVel;

        // Update changes
        m_moveController.Move(m_currentVelocity * Time.deltaTime);
    }
}
