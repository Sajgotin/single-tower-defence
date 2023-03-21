using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Canvas _healthBarCanvas;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] Transform _towerPos;
    [SerializeField] Turret _turretScript;
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    [SerializeField] int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }
    [SerializeField] int _health;
    public int Health { get { return _health; } }
    [SerializeField] int _bountyValue;
    public int BountyValue { get { return _bountyValue; } }
    private Vector2 _targetPos;
    private bool _deathSound;
    private bool _isAttacking;
    public bool IsAttacking { get { return _isAttacking; } }
    private bool _isMoving;
    public bool IsMoving { get { return _isMoving; } }
    private bool _isDead;
    public bool IsDead { get { return _isDead; } }

    // Start is called before the first frame update
    void Start()
    {
        _isMoving = true;
        _isAttacking = false;
        _isDead = false;

        _turretScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turret>();
        _healthBarCanvas = GameObject.FindGameObjectWithTag("Health Canvas").GetComponent<Canvas>();
        _healthBar.SetupHealthBar(_healthBarCanvas);
        GetTargetPosition();
        RotateTowardsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTower();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Tower" && !_isDead)
        {
            _isMoving = false;
            _isAttacking = true;
        }
    }

    void MoveTowardsTower()
    {
        if(_isMoving && !_isDead) transform.position = Vector2.MoveTowards(this.transform.position, _targetPos, _speed * Time.deltaTime);
    }

    void GetTargetPosition()
    {
        _towerPos = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
        _targetPos = new Vector2(_towerPos.position.x, transform.position.y);
    }

    void RotateTowardsTarget()
    {
        Vector2 direction = new Vector2(_targetPos.x - transform.position.x, _targetPos.y - transform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, rotation, 0);
    }

    public void HurtEnemy(int damage)
    {
        _health -= damage;
        _healthBar.UpdateHealthBar();
        if (_health <= 0)
        {
            _isMoving = false;
            _isAttacking = false;
            _isDead = true;
            Points.Instance.points += _bountyValue;
            UIManager.Instance.UpdateGoldValue();
            if(!_deathSound) SoundController.Instance.ActivateDeathSound();
            _deathSound = true;
        }     
    }

    void DealDamage()
    {
        GameManager.Instance.towerHealth -= (_damage * _turretScript.Armor);
        UIManager.Instance.UpdateTowerHealthSlider();
    }

    void PlayAttackSound()
    {
        SoundController.Instance.ActivateHitSound();
    }
}
