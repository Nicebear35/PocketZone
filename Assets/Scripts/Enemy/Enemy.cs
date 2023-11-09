using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private AttackDetector _attackDetector;

    public event Action IsDamaged;

    private Coroutine _attackCoroutine;
    private LootingSystem _lootingSystem;
    private Transform _target;
    private float _currentHealth;
    private NavMeshAgent _navmeshAgent;
    private WaitForSeconds _attackCooldown;


    public void Initialize(Transform target, LootingSystem lootingSystem)
    {
        _lootingSystem = lootingSystem;
        _target = target;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _navmeshAgent.updateRotation = false;
        _navmeshAgent.updateUpAxis = false;
        _attackCooldown = new WaitForSeconds(1f);
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
        _attackDetector.IsPlayerFound += Attack;
        _attackDetector.IsPlayerLost += Attack;
    }

    private void OnDisable()
    {
        _attackDetector.IsPlayerFound -= Attack;
        _attackDetector.IsPlayerLost -= Attack;
    }

    private void Attack(Player player)
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }

        _attackCoroutine = StartCoroutine(DamagePlayer(player));

    }

    private IEnumerator DamagePlayer(Player player)
    {
        while (_attackDetector.CanAttackPlayer)
        {
            player.TakeDamage(_damage);
            yield return _attackCooldown;
        }
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
        Destroy(gameObject, 0.2f);
    }
}
