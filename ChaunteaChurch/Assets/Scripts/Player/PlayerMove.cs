using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [Header("Move Variables")]
    public float m_maxMoveSpeed = 5f;
    public float m_timeToReachMaxSpeed = 0.1f;
    public float m_jumpStr = 6.5f;
    public float m_ladderStr = 4.0f;

    private Vector3 m_lastStepLocation;
    private Vector3 m_refVelocity;
    private Vector3 m_currentVelocity;
    private CharacterController m_moveController;
    private float m_yVel;

    private float m_jumpCD;
    private Vector3 m_pushDirection;
    private bool m_meGrounded;
    private float m_lastJumpTime;

    [Header("Footstep variables")]
    private float lastTimeSinceStep, m_baseStepPitch;
    public float timeBetweenStep;
    AudioSource m_audioStep;
    public float m_minDistBeforeStep = 0.3f;

    float m_gracePeriod;
    public bool m_canMove = true;

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
        if (!m_canMove)
            return;

        if (m_gracePeriod > 0)
            m_gracePeriod -= Time.deltaTime;

        UpdateMovement();
        JumpLogic();
        GroundLogic();

        if (m_meGrounded)
            m_gracePeriod = 0.3f;
    }

    void GroundLogic()
    {
        if (m_moveController.isGrounded)
            m_meGrounded = true;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, m_moveController.height * 0.6f))
        {
            m_meGrounded = true;
        }
        else
            m_meGrounded = false;
    }

    void LateUpdate()
    {
        FootStepSoundLogic();
    }

    public void ApplyLadderClimb()
    {
        m_currentVelocity.y = m_ladderStr;
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
        if (m_jumpCD > 0)
            m_jumpCD -= Time.deltaTime;

        if (!m_meGrounded && m_gracePeriod <= 0)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            m_currentVelocity.y = m_jumpStr;
            m_jumpCD = 0.3f;
            m_lastJumpTime = Time.time;
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
        if (m_moveController.isGrounded && Time.time - m_lastJumpTime > 0.2f)
            m_yVel = 0;

        m_yVel -= 20.0f * Time.deltaTime;
        m_currentVelocity.y = m_yVel;

        // Update changes
        m_moveController.Move(m_currentVelocity * Time.deltaTime);
    }
}
