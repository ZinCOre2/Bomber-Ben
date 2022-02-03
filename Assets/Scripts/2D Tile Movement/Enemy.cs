using System;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Direction
    {
        Left, Right, Up, Down
    };
    
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private Direction startingDirection;
    
    private bool _isMovement;
    private Direction _currentDir;

    private void Start()
    {
        _currentDir = startingDirection;
    }

    private void Update()
    {
        if (_isMovement)
        {
            return;
        }

        switch (_currentDir)
        {
            case Direction.Left:
                MoveTo(Vector2.left);
                break;
            case Direction.Right:
                MoveTo(Vector2.right);
                break;
            case Direction.Up:
                MoveTo(Vector2.up);
                break;
            case Direction.Down:
                MoveTo(Vector2.down);
                break;
        }
    }
    private void MoveTo(Vector2 dir)
    {
        if (Raycast(dir))
        {
            switch (_currentDir)
            {
                case Direction.Left:
                    _currentDir = Direction.Right;
                    break;
                case Direction.Right:
                    _currentDir = Direction.Left;
                    break;
                case Direction.Up:
                    _currentDir = Direction.Down;
                    break;
                case Direction.Down:
                    _currentDir = Direction.Up;
                    break;
            }
            return;
        }

        _isMovement = true;

        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 1f).OnComplete(() => { _isMovement = false; });
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position,
            dir, 1f, collisionMask);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            Destroy(other.gameObject);
        }
    }
}
