using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public GameObject prefab;
    
    void Awake()
    {
        
        for (int i = 0; i < 10000; i++)
        {
            float random = Random.Range(1f, 3f);
            prefab.transform.localScale = new Vector3(random, random, random);
            Vector3 position = new(Random.Range(10f, 100f), Random.Range(10f, 100f), Random.Range(10f, 100f));
            Quaternion rotation = Quaternion.identity;

            Instantiate(prefab, position, rotation);
        }


    }

}
