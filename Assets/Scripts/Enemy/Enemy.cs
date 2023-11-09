using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _healthBar;

    public event Action IsDamaged;

    private LootingSystem _lootingSystem;
    private Transform _target;
    private float _currentHealth;
    private NavMeshAgent _navmeshAgent;

    public void Initialize(Transform target, LootingSystem lootingSystem)
    {
        _lootingSystem = lootingSystem;
        _target = target;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _navmeshAgent.updateRotation = false;
        _navmeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (_target)
        {
            _navmeshAgent.SetDestination(_target.position);
        }
    }

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.fillAmount = _currentHealth / _maxHealth;

        if (_currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Instantiate(_lootingSystem.ChooseRandomItem(), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
