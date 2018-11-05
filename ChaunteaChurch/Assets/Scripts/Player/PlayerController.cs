using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerGrab))]
public class PlayerController : MonoBehaviour {

    PlayerGrab m_playerHands;
    PlayerMove m_playerMove;
    PlayerCameraController m_cameraController;

    private void Awake()
    {
        m_playerHands = GetComponent<PlayerGrab>();
        m_playerMove = GetComponent<PlayerMove>();
        m_cameraController = GetComponent<PlayerCameraController>();
    }

    public void StartMe()
    {
        m_playerHands.m_canMove = m_playerMove.m_canMove = m_cameraController.m_canMove = true;
    }

    public void StopMe()
    {
        m_playerHands.m_canMove = m_playerMove.m_canMove = m_cameraController.m_canMove = false;
    }
}
