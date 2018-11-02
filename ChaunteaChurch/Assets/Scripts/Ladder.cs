using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    PlayerMove m_player;
    float m_waitTime;

    private void OnTriggerEnter(Collider c)
    {
        if (c.transform.CompareTag("Player"))
        {
            m_player = c.transform.GetComponent<PlayerMove>();
            m_waitTime = 0.3f;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.transform.CompareTag("Player")) m_player = null;
    }

    private void Update()
    {
        if (m_waitTime > 0)
            m_waitTime -= Time.deltaTime;

        if (m_player && m_waitTime <= 0)
            m_player.ApplyLadderClimb();
    }
}
