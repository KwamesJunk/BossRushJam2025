using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnabler : MonoBehaviour
{
    [SerializeField] HitBox _hitBox;

    private void OnEnable()
    {
        _hitBox.enabled = true;
    }
    private void OnDisable()
    {
        _hitBox.enabled = false;
    }
}
