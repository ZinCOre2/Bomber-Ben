using System;
using System.Security.Cryptography;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionDelay = 2.5f;
    [SerializeField] private int blastPower = 2;
    [SerializeField] private LayerMask blastMask;
    [SerializeField] private GameObject blastEffectPrefab;
    
    private float _timeLeft;
    private PlayerController _owner;

    public void SetOwner(PlayerController owner)
    {
        _owner = owner;
    }
    
    private void Start()
    {
        _timeLeft = explosionDelay;
    }
    
    private void Update()
    {
        if (_timeLeft < 0f)
        {
            Explode();
            return;
        }

        _timeLeft -= Time.deltaTime;
    }

    
    private void Explode()
    {
        int i;

        var collider = Physics2D.OverlapCircle(transform.position, 0.4f);

        if (collider != null)
        {
            BlastHitChecker(collider);
        }

        var blastEffect = Instantiate(blastEffectPrefab, transform.position, Quaternion.identity);
        Destroy(blastEffect.gameObject, 1.5f);
        
        
        for (i = 1; i < blastPower + 1; i++)
        {
            collider = Physics2D.OverlapCircle(transform.position + Vector3.up * i, 0.4f);

            if (collider != null)
            {
                // Returns true if blast can pass further. If blocked by wall, it stops - returns false.
                if (!BlastHitChecker(collider)) { break; }
            }

            blastEffect = Instantiate(blastEffectPrefab, transform.position + Vector3.up * i, Quaternion.identity);
            Destroy(blastEffect.gameObject, 1.5f);
        }
        
        for (i = 1; i < blastPower + 1; i++)
        {
            collider = Physics2D.OverlapCircle(transform.position - Vector3.up * i, 0.4f);

            if (collider != null)
            {
                // Returns true if blast can pass further. If blocked by wall, it stops - returns false.
                if (!BlastHitChecker(collider)) { break; }
            }

            blastEffect = Instantiate(blastEffectPrefab, transform.position - Vector3.up * i, Quaternion.identity);
            Destroy(blastEffect.gameObject, 1.5f);
        }
        
        for (i = 1; i < blastPower + 1; i++)
        {
            collider = Physics2D.OverlapCircle(transform.position + Vector3.right * i, 0.4f);

            if (collider != null)
            {
                // Returns true if blast can pass further. If blocked by wall, it stops - returns false.
                if (!BlastHitChecker(collider)) { break; }
            }

            blastEffect = Instantiate(blastEffectPrefab, transform.position + Vector3.right * i, Quaternion.identity);
            Destroy(blastEffect.gameObject, 1.5f);
        }
        
        for (i = 1; i < blastPower + 1; i++)
        {
            collider = Physics2D.OverlapCircle(transform.position - Vector3.right * i, 0.4f);

            if (collider != null)
            {
                // Returns true if blast can pass further. If blocked by wall, it stops - returns false.
                if (!BlastHitChecker(collider)) { break; }
            }

            blastEffect = Instantiate(blastEffectPrefab, transform.position - Vector3.right * i, Quaternion.identity);
            Destroy(blastEffect.gameObject, 1.5f);
        }
        
        _owner.RefreshBomb();
        
        Destroy(gameObject);
    }

    private bool BlastHitChecker(Collider2D collider)
    {
        if (Math.Abs(Mathf.Pow(2,collider.gameObject.layer) - LayerMask.GetMask("Indestructible")) < 0.01f)
        {
            return false;
        }
        
        // Returns true if blast can pass further. If blocked by wall, it stops - returns false.
        if (collider.gameObject.TryGetComponent(out Bomb bomb)) 
        {
            return true;
        }
        
        if (collider.gameObject.TryGetComponent(out PlayerController player) || collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Destroy(collider.gameObject);
            return true;
        }

        Destroy(collider.gameObject);
        
        return false;
    }
}