using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperInteract : Interactable {
    [TextArea(2, 5)]
    public string message;
    bool meOn = false;
    PlayerController pc;

    public override bool Interact(PlayerController _playerController)
    {
        pc = _playerController;
        _playerController.StopMe();

        meOn = true;
        PlayerUI.instance.m_paper.SetActive(true);
        PlayerUI.instance.m_paperText.text = message;

        return true;
    }

    private void Update()
    {
        if (!meOn)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            pc.StartMe();
            PlayerUI.instance.m_paper.SetActive(false);
            meOn = false;
        }
    }
}
