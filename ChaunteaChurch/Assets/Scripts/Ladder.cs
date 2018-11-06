using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    PlayerMove m_player;

    private void OnTriggerEnter(Collider c)
    {
        if (c.transform.CompareTag("Ladder"))
        {
            m_player = c.transform.parent.GetComponent<PlayerMove>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_player = null;
    }

    private void Update()
    {
        if (m_player && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f) m_player.ApplyLadderClimb();
    }
}
