using Unity.Burst;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "ScriptableObjects/Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public string weaponName = "Weapon Name";
    public Mesh mesh;
    public AttackCombo combo;
    public Vector3 hitBoxPosition;
    public Vector3 hitBoxSize = Vector3.one;
}
