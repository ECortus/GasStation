using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTrucksController : MonoBehaviour
{
    [SerializeField] private GasTruck gasTruck;
    public Tanker tanker;
    public WayInfo way;
    public int ParkingIndex = 0;

    [Space]
    public TankerBarrel barrel;

    private List<GasTruck> TruckPool = new List<GasTruck>();

    private Vector3 SpawnPoint => way.Dots[0].position;
    private Vector3 SpawnRot => way.Dots[0].eulerAngles;

    [HideInInspector] public bool Called = false;

    public void CallToFill()
    {
        if(Called) return;

        GasTruck truck = null;
        bool existInPool = false;

        if(TruckPool.Count > 0)
        {
            foreach(GasTruck trck in TruckPool)
            {
                truck = trck;
                if(!truck.Active)
                {
                    existInPool = true;
                    truck.On(this, SpawnPoint, SpawnRot);
                    TruckPool.Add(truck);
                    break;
                }
            }
        }

        if(!existInPool)
        {
            truck = Instantiate(gasTruck);
            truck.On(this, SpawnPoint, SpawnRot);

            TruckPool.Add(truck);
        }

        if(truck != null) truck.SetMotor(1);
    }
}
