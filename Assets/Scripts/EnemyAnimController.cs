using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemyScript;
    private bool _isAttacking;
    private bool _isMoving;
    private bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        _isAttacking = _enemyScript.IsAttacking;
        _isMoving = _enemyScript.IsMoving;
        _isDead = _enemyScript.IsDead;

        _animator.SetBool("isAttacking", _isAttacking);
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("isDead", _isDead);
    }
}
