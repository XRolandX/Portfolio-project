public class GreenBuilding : Building
{
    private readonly float greenResProdInterval = 2.77f;
    private readonly float greenResMaxStore = 5f;
    private readonly float redStorageCountMax = 5f;

    protected string greenColor = "green";
    protected string greenResourceType = "Green";

    private void Start()
    {
        productionInterval = greenResProdInterval;
        maxResourceCount = greenResMaxStore;
        maxRedWarehouseStorage = redStorageCountMax;

        resourceColor = greenColor;
        resourceType = greenResourceType;
        resourceCountDisplay = ResourceManager.Instance.greenResources;
    }

    public override void ProduceResource()
    {

        if (ResourceManager.Instance.greenResources.Count < maxResourceCount && ResourceManager.Instance.RedGreenTransition && ResourceManager.Instance.greenRedWarehouse.Count > 0)
        {
            ResourceManager.Instance.RedGreenTransition = false;
            Destroy(ResourceManager.Instance.greenRedWarehouse[^1]);
            ResourceManager.Instance.greenRedWarehouse.RemoveAt(ResourceManager.Instance.greenRedWarehouse.Count - 1);
            ResourceManager.Instance.ResourceInstance(ResourceManager.Instance.greenResourcePrefab, resSpawnPoint.transform, ResourceManager.Instance.greenResources);
        }
    }

    public override void GetResource()
    {
        if (ResourceManager.Instance.greenRedWarehouse.Count < maxRedWarehouseStorage && ResourceManager.Instance.redResources.Count > 0)
        {
            ResourceManager.Instance.RedGreenTransition = true;
            ResourceManager.Instance.GetLatestResource(redResStorePoint.transform, ResourceManager.Instance.redResources, ResourceManager.Instance.greenRedWarehouse);
        }

    }
}
