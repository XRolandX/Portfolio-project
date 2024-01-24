using TMPro;
using UnityEngine;

public class BlueBuilding : Building
{
    
    

    private void Start()
    {
        currentStorageCount = 0f;
        currentStorageCount2 = 0f;
        maxResourceStorage = 5f;
        currentStorageCount = 0f;
        maxResourceCount = 5f;
        resourceProductionRate = 2f;
        productionInterval = 4.77f;
        resourceColor = "blue";
        resourceType = "Blue";
        displayResource = ResourceManager.Instance.blueResources;
    }

    

    
    public override void ProduceResource()
    {
        if (ResourceManager.Instance.blueGreenWarehouse.Count > 0 && ResourceManager.Instance.blueRedWarehouse.Count > 0 && ResourceManager.Instance.blueResources.Count < maxResourceCount
            && ResourceManager.Instance.GreenBlueTransition && ResourceManager.Instance.RedBlueTransition)
        {
            ResourceManager.Instance.GreenBlueTransition = false;
            ResourceManager.Instance.RedBlueTransition = false;
            Destroy(ResourceManager.Instance.blueGreenWarehouse[^1]);
            Destroy(ResourceManager.Instance.blueRedWarehouse[^1]);
            ResourceManager.Instance.blueGreenWarehouse.RemoveAt(ResourceManager.Instance.blueGreenWarehouse.Count - 1);
            ResourceManager.Instance.blueRedWarehouse.RemoveAt(ResourceManager.Instance.blueRedWarehouse.Count - 1);

            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.blueResourcePrefab, resSpawnPoint.transform, ResourceManager.Instance.blueResources);
            currentResourceCount = ResourceManager.Instance.blueResources.Count;
        }
    }

    public override void GetResource()
    {
        if (ResourceManager.Instance.blueGreenWarehouse.Count < maxResourceStorage && ResourceManager.Instance.greenResources.Count > 0)
        {
            ResourceManager.Instance.GreenBlueTransition = true;
            ResourceManager.Instance.GetLatestResource(resStorePoint.transform, ResourceManager.Instance.greenResources, ResourceManager.Instance.blueGreenWarehouse);
            currentStorageCount = ResourceManager.Instance.blueGreenWarehouse.Count;
        }
        if (ResourceManager.Instance.blueRedWarehouse.Count < maxResourceStorage && ResourceManager.Instance.redResources.Count > 0)
        {
            ResourceManager.Instance.RedBlueTransition = true;
            ResourceManager.Instance.GetLatestResource(resStorePoint2.transform, ResourceManager.Instance.redResources, ResourceManager.Instance.blueRedWarehouse);
            currentStorageCount2 = ResourceManager.Instance.blueRedWarehouse.Count;
        }
    }

    public override TextMeshPro FindTMPInScene()
    {
        return GameObject.FindGameObjectWithTag("Blue TMP").GetComponent<TextMeshPro>();
    }

    

    
    
}
