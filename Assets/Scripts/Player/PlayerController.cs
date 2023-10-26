using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private const string IsWalking = "IsWalking";

    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _inventoryButton;

    private Weapon _currentWeapon;
    [SerializeField]private Enemy _enemyToShoot;
    private Vector2 _moveVector;
    private Coroutine _shootCoroutine;

    private void Start()
    {
        _moveVector = Vector2.zero;
        _currentWeapon = _weaponList[0];
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

    public void Shoot()
    {
        if (_shootCoroutine == null)
        {
            _shootCoroutine = StartCoroutine(_currentWeapon.Shoot(_currentWeapon, _enemyToShoot));
            _shootCoroutine = null;
        }
    }
}
