using UnityEngine;
using TMPro;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected float productionInterval;
    [SerializeField] protected float currentResourceCount;
    [SerializeField] protected float maxResourceCount;

    [SerializeField] protected float redWarehouseStoreageCount;
    [SerializeField] protected float greenWarehouseStoreageCount;
    [SerializeField] protected float maxRedWarehouseStorage;
    [SerializeField] protected float maxGreenWarehouseStorage;

    [SerializeField] protected float produceTimeElapsed;
    [SerializeField] protected float gettingTimeElapsed;

    [SerializeField] protected RedBuilding redBuilding;
    [SerializeField] protected GreenBuilding greenBuilding;
    [SerializeField] protected BlueBuilding blueBuilding;
    [SerializeField] protected TextMeshPro resourceDisplay;

    protected string resourceColor;
    protected string resourceType;

    [SerializeField] protected Transform resSpawnPoint;
    [SerializeField] protected Transform redResStorePoint;
    [SerializeField] protected Transform greenResStorePoint;

    protected List<GameObject> displayResource;

    
    protected virtual void Update()
    {
        ProduceTimeElapse();
        ResourceDisplay();
        GetResourceTimeElapse();
    }
    
    private void ProduceTimeElapse()
    {
        produceTimeElapsed += Time.deltaTime;

        if (produceTimeElapsed >= productionInterval)
        {
            ProduceResource();
            produceTimeElapsed = 0f;
        }
    }
    private void GetResourceTimeElapse()
    {
        gettingTimeElapsed += Time.deltaTime;

        if (gettingTimeElapsed >= productionInterval)
        {
            GetResource();
            gettingTimeElapsed = 0f;
        }
    }

    private void ResourceDisplay()
    {
        if (resourceDisplay != null)
        {
            resourceDisplay.text = "<color=" + resourceColor + ">" + resourceType + "</color> resource: "
                + displayResource.Count.ToString("F2")
                + "\nTime elapsed: " + gettingTimeElapsed.ToString("F2");
        }
    }
    public abstract void GetResource();
    public abstract TextMeshPro FindTMPInScene();
    public abstract void ProduceResource();
}
