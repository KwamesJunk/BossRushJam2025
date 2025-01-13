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

    public event Action<int> OnTakeDamage;
    public event Action OnDead;

    private void Start()
    {
        _currentHp = maxHP;
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (damage <= 0 ) {
            damage = 0;

            OnDead?.Invoke();
            return;
        }
        
        OnTakeDamage?.Invoke(damage);
    }
}
