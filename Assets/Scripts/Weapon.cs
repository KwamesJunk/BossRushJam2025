using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponInfo weaponInfo;
    [SerializeField] HitBox _hitBox;
    [SerializeField] MeshFilter _meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        Equip();
    }

    public void Equip()
    {
        _meshFilter.mesh = weaponInfo.mesh;
        _hitBox.transform.localPosition = weaponInfo.hitBoxPosition;
        _hitBox.transform.localScale = weaponInfo.hitBoxSize;
    }
}
