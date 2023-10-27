using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform _bulletContainer;
    [SerializeField] Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _weaponIndex;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _damage;

    private WaitForSeconds _shootingSpeed;
    private WaitForSeconds _reloadTime;
    private bool _isReloaded;
    private int _currentAmmo;
    private Coroutine _reloadCoroutine;

    private void Start()
    {
        _shootingSpeed = new WaitForSeconds(0.3f);
        _reloadTime = new WaitForSeconds(1.5f);
        _isReloaded = true;
        _currentAmmo = _maxAmmo;
    }

    public IEnumerator Shoot(Weapon weapon, Enemy enemy)
    {
        if (_isReloaded)
        {
            Debug.Log("стреляем");
            var bullet = Instantiate(_bulletPrefab, _shootPoint.transform.position, Quaternion.identity, _bulletContainer);
            Rigidbody2D bulletRidgidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRidgidbody.velocity = (enemy.transform.position - transform.position) * _bulletSpeed;
            _currentAmmo--;

            if (_currentAmmo <= 0)
            {
                _isReloaded = false;
            }

        }
        else
        {
            _reloadCoroutine = StartCoroutine(Reload());
        }

        yield return weapon._shootingSpeed;
    }

    private IEnumerator Reload()
    {
        yield return _reloadTime;
        _isReloaded = true;
    }
}