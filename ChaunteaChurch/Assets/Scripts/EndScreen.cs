using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndGame());
            other.GetComponent<PlayerController>().StopMe();
        }
    }

    IEnumerator EndGame()
    {
        Destroy(GetComponent<Collider>());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PlayerUI.instance.m_end.SetActive(true);

        var allColourStuffs = PlayerUI.instance.m_end.GetComponentsInChildren<MaskableGraphic>();

        foreach (MaskableGraphic g in allColourStuffs)
        {
            g.color = new Color(g.color.r, g.color.g, g.color.b, 0);
        }

        while (allColourStuffs[0].color.a < 1)
        {
            foreach (MaskableGraphic g in allColourStuffs)
            {
                g.color = new Color(g.color.r, g.color.g, g.color.b, g.color.a + Time.deltaTime * 2);
                yield return null;
            }
        }


        yield return null;
    }
}
