using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeFly : MonoBehaviour {

    public Rigidbody m_myBody;
    public Vector3 m_flyVel;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Fly();
        Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Fly();
    }

    void Fly()
    {
        m_myBody.isKinematic = false;
        Destroy(GetComponent<Collider>());
        var col = gameObject.AddComponent<MeshCollider>();
        col.convex = true;

        m_myBody.AddForce(m_flyVel, ForceMode.VelocityChange);
        m_myBody.AddTorque(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode.VelocityChange);
    }
}
