using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTorch : MonoBehaviour
{
    public GameObject torchParent;
    public GameObject torch;

    private void Start()
    {
        int amaount = Random.Range(2, 4);

        for (int i = 0; i < amaount; i++)
        {
            int rand = Random.Range(0, 8);
            Instantiate(torch, new Vector3(torchParent.transform.GetChild(rand).position.x, torchParent.transform.GetChild(rand).position.y, torch.transform.position.z), Quaternion.identity);

        }
    }
}
