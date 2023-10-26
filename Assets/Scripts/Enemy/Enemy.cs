using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    private void Update()
    {
        _rigidbody.velocity = (_player.transform.position - transform.position) * _moveSpeed;
    }

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    private void MoveToPlayer()
    {
        if (_player != null)
        {
            _rigidbody.velocity = transform.position - _player.transform.position;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
