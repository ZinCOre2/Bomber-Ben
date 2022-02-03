using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private float radius = 5f;  
    public void Badabum()
    {
        Destroy(this.gameObject);
        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var cldr in colliders)
        {
            if (cldr.attachedRigidbody == null)
            {
                continue;
            }
            var dir = cldr.transform.position - transform.position;
            var dist = dir.magnitude;

            var k = dist / radius;          
            cldr.attachedRigidbody.AddForce(k * 40f * dir.normalized, ForceMode.Impulse);
        }
    }
}

