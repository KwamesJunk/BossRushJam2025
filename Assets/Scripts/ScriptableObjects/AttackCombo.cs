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
        public string animationName;
        public float transitionDuration;
        public float minDuration;
        public float damage;
        public Vector3 hitBoxPosition;
        public Vector3 hitBoxSize;
        public float hitBoxActivationTime;
        public float hitBoxDeactivationTime;
        // public float Knockback
        // public Vector3 KnockbackDirection
    }

    [SerializeField] List<AttackInfo> _attacks;
    public List<AttackInfo> Attacks => _attacks;
}