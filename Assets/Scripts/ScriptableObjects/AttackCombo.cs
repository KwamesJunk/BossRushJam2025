using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCombo", menuName = "ScriptableObjects/Attack Combo")]
public class AttackCombo : ScriptableObject
{
    [Serializable]
    public struct AttackInfo
    {
        public string AnimationName;
        public float TransitionDuration;
        public float MinNormalizedDuration;
        public float Damage;
        // public float Knockback
        // public Vector3 KnockbackDirection
    }

    [SerializeField] List<AttackInfo> _attacks;
    public List<AttackInfo> Attacks => _attacks;
}
