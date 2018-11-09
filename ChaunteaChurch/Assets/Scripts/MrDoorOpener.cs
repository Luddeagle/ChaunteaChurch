using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrDoorOpener : MonoBehaviour {

    public Collider[] m_doors;
    public Rigidbody[] locks;

    public void WeDone()
    {
        foreach (Collider d in m_doors) d.enabled = true;
        bool yes = true;
        foreach (Rigidbody rb in locks)
        {
            rb.isKinematic = false;
            rb.gameObject.layer = 0;
            rb.transform.SetParent(null);
            if (yes)
            {
                yes = false;
                var ye = rb.GetComponent<CandleHoleInteractable>();
                if (ye) Destroy(ye);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeDone();
            Destroy(gameObject);
        }
    }
}
