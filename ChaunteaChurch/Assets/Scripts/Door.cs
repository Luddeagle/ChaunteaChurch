using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Door : MonoBehaviour {

    public float m_rotateAmount = 80;
    float m_doorOpenTime;
    Quaternion m_startRot, m_endRot;
    float m_rotateSpeed = 150.0f;
    public bool m_otherDir = false;
    public bool m_openOnce = false;

    private void Awake()
    {
        m_startRot = transform.rotation;
        float amount = m_otherDir ? -m_rotateAmount : m_rotateAmount;
        m_endRot = transform.rotation * Quaternion.Euler(new Vector3(0, amount, 0));
    }

    private void Update()
    {
        if (m_doorOpenTime > 0)
        {
            m_doorOpenTime -= Time.deltaTime;
            Quaternion prevQuat = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_endRot, m_rotateSpeed * Time.deltaTime);

            if (m_openOnce)
            {
                if (prevQuat == transform.rotation)
                    Destroy(this); // We done with opening
            }
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_startRot, m_rotateSpeed * Time.deltaTime);
        }
    }

    public void Open() { m_doorOpenTime = 5.0f; }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            m_doorOpenTime = 5.0f;
        }
    }
}
