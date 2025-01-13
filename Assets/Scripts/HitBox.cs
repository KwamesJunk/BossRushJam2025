using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HitBox : MonoBehaviour
{
    [SerializeField] GameObject _owner;
    [SerializeField] int _damage;
    [SerializeField] DamageType _damageType;
    private BoxCollider _boxCollider;
    //[SerializeField] ParticleSystem _particleSystem;
    [SerializeField] bool _showHitBox;
    [SerializeField] GameObject _debugView;

    private void Start()
    {
        //transform.GetChild(0).gameObject.SetActive(_showHitBox);
       _boxCollider = GetComponent<BoxCollider>();
        _debugView.SetActive(_showHitBox);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _owner) return;
        

        if (other.TryGetComponent<LifeStats>(out var lifeStats))
        {
            lifeStats.TakeDamage(_damage);
            Debug.Log($"Just hit {other.name} for {_damage}!");

           // if (_particleSystem != null) { 
                //rticleSystem.transform.position = other.ClosestPoint(transform.position);
               // Instantiate(_particleSystem, other.ClosestPoint(transform.position), Quaternion.identity).Play();
                //_particleSystem.Play();
                //Debug.Log("Particles!");
           // }
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled!");
    }

    private void OnDisable()
    {
        Debug.Log("Disabled!");
    }
}
