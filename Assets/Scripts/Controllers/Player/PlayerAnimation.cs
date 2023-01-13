using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
       _anim = GetComponent<Animator>();
    }
    public void PlayWalkAnim(bool value)
    {
        _anim.SetBool("Walking", value);
    }
}
