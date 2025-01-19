using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStateMachine : StateMachine
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
        lifeStats.OnTakeDamage += OnTakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTakeDamage(int damage, Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(-direction);
        charAnimator.Play("TakeDamage");
    }
}
