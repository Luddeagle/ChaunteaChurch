using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float m_rotateAmount;
    float m_doorOpenTime;
    Quaternion m_startRot, m_endRot;
    float m_rotateSpeed = 90.0f;

    private void Awake()
    {
        m_startRot = transform.rotation;
        m_endRot = transform.rotation * Quaternion.Euler(new Vector3(0, m_rotateAmount, 0));
    }

    private void Update()
    {
        if (m_doorOpenTime > 0)
        {
            m_doorOpenTime -= Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_endRot, m_rotateAmount * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_startRot, m_rotateAmount * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            m_doorOpenTime = 5.0f;
        }
    }
}
