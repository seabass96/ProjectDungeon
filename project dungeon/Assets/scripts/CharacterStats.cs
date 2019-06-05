using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float health { get; set; } = 20;
    public float Stamina { get; set; } = 3;
    public float Strength { get; set; } = 3;
    public float Magic { get; set; } = 3;
    public float Luck { get; set; } = 3;


   


}
