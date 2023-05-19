using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFillingClientSimulation : MonoBehaviour
{
    public StationFilling Station;

    [Space]
    [SerializeField] private StationFillingClient client;
    private List<StationFillingClient> ClientPool = new List<StationFillingClient>();

    [Space]
    [SerializeField] private int parkingDotIndex;
    [SerializeField] private WayInfo Way;

    private List<Transform> Dots => Way.Dots;
    public Transform GetDotByIndex(int index) => Dots[index];
    public int DotsCount => Dots.Count;
    private Vector3 SpawnPoint => Dots[0].position;
    private Vector3 DirectionPoint => Dots[1].position;
    public int ParkingIndex => parkingDotIndex;
    private Vector3 rotation
    {
        get
        {
            Vector3 direction = (DirectionPoint - SpawnPoint).normalized;
            return Quaternion.LookRotation(direction).eulerAngles;
        }
    } 

    [Space]
    [SerializeField] private float distanceToCarForNewSpawn = 5f;

    Transform car;

    Coroutine coroutine;

    void Start()
    {
        StartWork();
    }

    public void StartWork()
    {
        if(coroutine == null) coroutine = StartCoroutine(Work());
    }

    public void StopWork()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Work()
    {
        yield return null;
        bool existInPool = false;
        StationFillingClient clnt = null;

        while(true)
        {
            existInPool = false;

            foreach(StationFillingClient cl in ClientPool)
            {
                clnt = cl;
                if(!clnt.Active)
                {
                    existInPool = true;
                    clnt.On(SpawnPoint, rotation);
                    ClientPool.Add(clnt);

                    car = clnt.transform;
                    break;
                }
            }

            if(!existInPool)
            {
                clnt = Instantiate(client);
                clnt.simulation = this;
                clnt.On(SpawnPoint, rotation);

                ClientPool.Add(clnt);

                car = clnt.transform;
            }

            if(clnt != null) clnt.SetMotor(1);

            yield return new WaitUntil(() => 
                Vector3.Distance(car.position, SpawnPoint) > distanceToCarForNewSpawn || !car.gameObject.activeSelf
            );
        }
    }
}
