using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Storage : MonoBehaviour
{
    public int Amount => Models.Count;

    [Header("Storage par-s: ")]
    [SerializeField] private ModelController prefab;

    [Space]
    [SerializeField] private Vector3 space = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector2Int sizeOfStorage = new Vector2Int(3, 3);

    [Space]
    [SerializeField] private Transform storageSpawn;
    [SerializeField] private Transform storagePlace;

    public List<ModelController> Models = new List<ModelController>();
    private List<ModelController> ModelsPool = new List<ModelController>();

    ModelController model;

    public void Add()
    {
        if(ModelsPool.Count == 0)
        {
            model = Instantiate(prefab, storageSpawn);
        }
        else
        {
            model = ModelsPool[0];
            ModelsPool.Remove(model);
        }

        Models.Add(model);
        model.Enable(this, storagePlace, CalculatePosition(), CalculateRotation(), 
            new Vector3(Random.Range(-30f, 30f), Random.Range(-90f, 90f), Random.Range(-30f, 30f))
        );
    }
    
    public void AddToPool(ModelController mdlC)
    {
        ModelsPool.Add(mdlC);
    }

    public ModelController GetLastModel()
    {
        return Models[Models.Count - 1];
    }

    public void Reduce(ModelController mdlC)
    {
        Models.Remove(mdlC);
    }

    public void ReduceInArriveAll()
    {
        for(int i = 0; i < Models.Count; )
        {
            ReduceInArrive();
        }
    }

    public void ReduceInArrive()
    {
        model = Models[Models.Count - 1];
        Models.Remove(model);

        model.DisableInArrive(Player.Instance.Transform);
    }

    Vector3 CalculatePosition()
    {
        Vector3 center = storagePlace.localPosition;
        
        int mod = (sizeOfStorage.x - 1) % 2;

        Vector3 pos;

        int height = (Models.Count - 1) / (sizeOfStorage.x * sizeOfStorage.y);
        int index = (Models.Count - 1) % (sizeOfStorage.x * sizeOfStorage.y);

        int row = index / sizeOfStorage.x - (sizeOfStorage.x / 2);
        int column = index % sizeOfStorage.y - (sizeOfStorage.y / 2);
        
        pos = new Vector3(
            space.x * column,
            space.y * height,
            -space.z * row
        );

        pos += new Vector3(
            space.x * mod / 2f,
            0f,
            space.z * mod / 2f
        );

        return pos;
    }

    Vector3 CalculateRotation()
    {
        return storagePlace.localEulerAngles;
    }
}
