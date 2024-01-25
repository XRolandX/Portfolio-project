using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public GameObject prefab;
    
    void Awake()
    {
        
        for (int i = 0; i < 10000; i++)
        {
            

            Vector3 position = new(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
            Quaternion rotation = Quaternion.identity;

            Instantiate(prefab, position, rotation);
        }


    }

}
