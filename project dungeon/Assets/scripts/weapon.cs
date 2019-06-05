using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class weapon: ScriptableObject
{
    public enum swingType {  SINGLE, AUTO, SPEAR };
    public swingType type;
    public string weaponName;
    public string description;
    public Sprite artwork;
    public float attack = 10;
    public GameObject projectile;
    public Vector3 position;
    public float rotation;
    public Vector3 scale;   
}
