using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addRoom : MonoBehaviour
{
    private roomTemplates templates;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<roomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
