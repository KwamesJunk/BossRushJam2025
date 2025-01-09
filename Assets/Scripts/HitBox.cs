using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] GameObject _owner;
    [SerializeField] int _damage;
    [SerializeField] DamageType _damageType;
    [SerializeField] BoxCollider _boxCollider;
    //[SerializeField] ParticleSystem _particleSystem;
    [SerializeField] bool _showHitBox;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(_showHitBox);
       // _particleSystem?.Stop();
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

    // public void SetSizeAndPosition(Vector3 localPosition, Vector3 size) {
    //     _boxCollider.center = localPosition;
    //     _boxCollider.size = size;
    // }

    private void OnEnable()
    {
        Debug.Log("Enabled!");
    }

    private void OnDisable()
    {
        Debug.Log("Disabled!");
    }
}
