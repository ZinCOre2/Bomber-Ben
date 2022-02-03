using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKiller : MonoBehaviour
{
    [SerializeField] private Camera camera;
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) { return; }

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (!Physics.Raycast(ray, out var hit)) { return; }

        var rb = hit.transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForceAtPosition((hit.point - transform.position).normalized * 10f, hit.point, ForceMode.Impulse);
        }

        var tnt = rb.gameObject.GetComponent<TNT>();
        if (tnt != null)
        {
            tnt.Badabum();
        }
    }
}
