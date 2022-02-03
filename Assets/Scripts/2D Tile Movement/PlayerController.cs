using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private int bombCount = 1;

    private bool _isMovement;
    private int _bombsLeft;

    private void Start()
    {
        _bombsLeft = bombCount;
    }
    
    private void Update()
    {
        if (_isMovement)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayerTo(Vector2.left);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayerTo(Vector2.right);
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovePlayerTo(Vector2.up);
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MovePlayerTo(Vector2.down);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_bombsLeft <= 0) { return; }
            
            Bomb bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bomb.SetOwner(this);
            _bombsLeft--;
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     var obj = RaycastFromCamera();
        //     if (obj != null && obj.CompareTag("Explosive"))
        //     {
        //         Destroy(obj);
        //         
        //         var colliders = Physics2D.OverlapCircleAll(obj.transform.position, blastRadius, blastMask);
        //
        //         foreach (var col in colliders)
        //         {
        //             Destroy(col.gameObject);
        //         }
        //     }
        // }
    }

    public void RefreshBomb()
    {
        if (_bombsLeft >= bombCount) return;

        _bombsLeft++;
    }
    
    private void MovePlayerTo(Vector2 dir)
    {
        if (Raycast(dir)) { return; }

        _isMovement = true;

        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 0.3f).OnComplete(() =>
        {
            _isMovement = false;
        });
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, 
            dir, 1f, collisionMask);
        return hit.collider != null;
    }

    // private GameObject RaycastFromCamera()
    // {
    //     var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //     return hit.collider != null ? hit.collider.gameObject : null;
    // }
}
