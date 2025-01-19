using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStats : MonoBehaviour
{
    [SerializeField]private int _currentHp;
    public int CurrentHp => _currentHp;
    public int maxHP = 100;
    //public int Def;
    // weaknesses
    // strengths
    // ??

    public event Action<int, Vector3> OnTakeDamage;
    public event Action OnDead;
    public event Action<int, int> OnHpChange;

    private void Start()
    {
        _currentHp = maxHP;
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        int oldHp = _currentHp;

        _currentHp -= damage;
        if (_currentHp <= 0 ) {
            _currentHp = 0;

            OnHpChange(oldHp, 0);
            OnDead?.Invoke();
            return;
        }
        
        OnTakeDamage?.Invoke(damage, direction);
        OnHpChange?.Invoke(oldHp, _currentHp);
    }

    public void SetCurrentHP(int currentHP)
    {
        int oldHP = _currentHp;
        _currentHp = currentHP;

        OnHpChange?.Invoke(oldHP, currentHP);
    }

}
