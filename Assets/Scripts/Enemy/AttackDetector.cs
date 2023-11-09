using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackDetector : MonoBehaviour
{
    public event UnityAction<Player> IsPlayerFound;
    public event UnityAction<Player> IsPlayerLost;

    private bool _canAttackPlayer;

    public bool CanAttackPlayer => _canAttackPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _canAttackPlayer = true;
            IsPlayerFound.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _canAttackPlayer = false;
            IsPlayerLost.Invoke(player);
        }
    }
}
