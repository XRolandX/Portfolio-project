public class BlueBuilding : Building
{
    private readonly float blueResProdInterval = 4.77f;
    private readonly float blueResMaxStore = 5f;

    private readonly float maxRedWarehouseCount = 5f;
    private readonly float maxGreenWarehouseCount = 5f;

    protected string blueColor = "blue";
    protected string blueResourceType = "Blue";

    private void Start()
    {
        productionInterval = blueResProdInterval;
        maxResourceCount = blueResMaxStore;

        maxRedWarehouseStorage = maxRedWarehouseCount;
        maxGreenWarehouseStorage = maxGreenWarehouseCount;

        resourceColor = blueColor;
        resourceType = blueResourceType;
        resourceCountDisplay = ResourceManager.Instance.blueResources;
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
        }
    }

    public override void GetResource()
    {
        if (ResourceManager.Instance.blueGreenWarehouse.Count < maxGreenWarehouseStorage && ResourceManager.Instance.greenResources.Count > 0)
        {
            ResourceManager.Instance.GreenBlueTransition = true;
            ResourceManager.Instance.GetLatestResource(greenResStorePoint.transform, ResourceManager.Instance.greenResources, ResourceManager.Instance.blueGreenWarehouse);
        }
        if (ResourceManager.Instance.blueRedWarehouse.Count < maxRedWarehouseStorage && ResourceManager.Instance.redResources.Count > 0)
        {
            ResourceManager.Instance.RedBlueTransition = true;
            ResourceManager.Instance.GetLatestResource(redResStorePoint.transform, ResourceManager.Instance.redResources, ResourceManager.Instance.blueRedWarehouse);
        }
    }
}
