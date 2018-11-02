using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerGrab))]
public class PlayerController : MonoBehaviour {

    public PlayerCanvas m_playerCanvas;
    PlayerGrab m_playerHands;
    PlayerMove m_playerMove;
    PlayerCameraController m_cameraController;

    private void Awake()
    {
        m_playerHands = GetComponent<PlayerGrab>();
        m_playerMove = GetComponent<PlayerMove>();
        m_cameraController = GetComponent<PlayerCameraController>();
    }

    private void Update()
    {

    }
}
