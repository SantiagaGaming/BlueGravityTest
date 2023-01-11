using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb;
    //private Player _player;
    private void Awake()
    {
      //  _player = GetComponent<Player>();
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void TryMove(float directionX, float directionY)
    { 
        _rb.velocity = new Vector2(directionX * _moveSpeed, directionY * _moveSpeed);
        //PlayAnim(directionX);
    }

    //private void PlayAnim(float x)
    //{
    //    if (x > 0)
    //    { 
    //        _player.PlayRightMovingAnim();
    //    }
    //    else if(x<0)
    //    {
    //        _player.PlayLeftMovingAnim();
    //    }
    //    else if(x==0)
    //    {
    //        _player.PlayIdleAnimation();
    //    }
    //}
}
