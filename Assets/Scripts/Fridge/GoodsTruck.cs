using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsTruck : MonoBehaviour
{
    [SerializeField] private GoodsStorage storage;

    [Header("Par-s:")]
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private int maxAmount = 16;

    Coroutine coroutine;

    void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        if(coroutine == null) coroutine = StartCoroutine(Spawning());
    }

    public void StopSpawn()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Spawning()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        while(true)
        {
            if(storage.Amount > maxAmount - 1)
            {
                yield return new WaitUntil(() => storage.Amount < maxAmount);
            }
            storage.Add();

            yield return wait;
        }
    }
}
