using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private PlayerAnimation _playerAnim;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
    }
    public void TryMove(float directionX, float directionY)
    { 
        _rb.velocity = new Vector2(directionX * _moveSpeed, directionY * _moveSpeed);
        PlayPlayerAnimation(directionX, directionY);
    }
    private void PlayPlayerAnimation(float x, float y)
    {
        if (x != 0 || y != 0)
            _playerAnim.PlayWalkAnim(true);
        else 
            _playerAnim.PlayWalkAnim(false);
    }


}
