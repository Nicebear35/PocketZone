using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const string IsWalking = "IsWalking";

    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Image _healthBar;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private LootingDetector _lootingDetector;
    [SerializeField] private LootingSystem _lootingSystem;

    private Enemy _enemyToShoot;
    private Weapon _currentWeapon;
    private Vector2 _moveVector;
    private Coroutine _shootCoroutine;
    private float _currentHealth;
    private List<Enemy> _enemies;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _moveVector = Vector2.zero;
        _currentWeapon = _weaponList[0];
        _healthBar.fillAmount = _currentHealth;
        _enemies = new List<Enemy>();
    }

    private void OnEnable()
    {
        _enemyDetector.OnEnemyDetected += AddEnemyToList;
        _enemyDetector.OnEnemyLost += DeleteEnemyFromList;
        _lootingSystem.OnInventoryItemFound += AddInventoryItem;
    }

    private void OnDisable()
    {
        _enemyDetector.OnEnemyDetected -= AddEnemyToList;
        _enemyDetector.OnEnemyLost -= DeleteEnemyFromList;
        _lootingSystem.OnInventoryItemFound -= AddInventoryItem;
    }

    private void Update()
    {
        _moveVector.x = _joystick.Horizontal;
        _moveVector.y = _joystick.Vertical;

        if (_moveVector.x != 0 || _moveVector.y != 0)
        {
            _animator.SetBool(IsWalking, true);
            _playerRigidbody.velocity = _moveVector.normalized * _moveSpeed;
        }
        else
        {
            _animator.SetBool(IsWalking, false);
            _playerRigidbody.velocity = Vector2.zero;
        }
    }

    private void AddEnemyToList(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    private void DeleteEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.fillAmount = _currentHealth;
    }

    private void AddInventoryItem(InventoryItem item)
    {
        _inventory.TryToAddItem(item);
    }

    public void Shoot()
    {
        if (_enemies.Count > 0)
        {
            _enemyToShoot = _enemies[0];

            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }

            _shootCoroutine = StartCoroutine(_currentWeapon.Shoot(_currentWeapon, _enemyToShoot));
        }
        else
        {
            _enemyToShoot = null;
        }
    }
}
