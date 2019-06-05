using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelStopperParent : MonoBehaviour
{
    public GameObject[] levelStoppers;

    private void Start()
    {
        StartCoroutine(cannon());
    }

    private IEnumerator cannon()
    {
        levelStoppers[0].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        levelStoppers[1].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        levelStoppers[2].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        levelStoppers[3].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        levelStoppers[4].SetActive(true);
    }
}
