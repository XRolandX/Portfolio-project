using UnityEngine;
using TMPro;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected float resourceProductionRate;
    [SerializeField] protected float currentResourceCount;
    [SerializeField] protected float timeElapsed;
    [SerializeField] protected float productionInterval;
    [SerializeField] protected float maxResourceCount;

    [SerializeField] protected RedBuilding redBuilding;
    [SerializeField] protected GreenBuilding greenBuilding;
    [SerializeField] protected BlueBuilding blueBuilding;

    [SerializeField] protected TextMeshPro resourceDisplay;
    protected string resourceColor;
    protected string resourceType;

    protected virtual void Update()
    {
        TimeElapse();

        ResourceDisplay();

    }

    private void Awake()
    {
        redBuilding = FindObjectOfType<RedBuilding>();
        greenBuilding = FindObjectOfType<GreenBuilding>();
        blueBuilding = FindObjectOfType<BlueBuilding>();
        resourceDisplay = FindTMPInScene();
    }

    

    private void TimeElapse()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= productionInterval)
        {
            ProduceResource();
            timeElapsed = 0f;
        }
    }
    private void ResourceDisplay()
    {
        if (resourceDisplay != null)
        {
            resourceDisplay.text = "<color="+resourceColor+">"+resourceType+"</color> resource: " 
                + ResourceManager.Instance.CurrentIndex.ToString("F2")
                + "\nTime elapsed: " + timeElapsed.ToString("F2");
        }
    }


    protected virtual void GetResource()
    {

    }
    protected virtual void ProduceResource()
    {

    }

    public abstract TextMeshPro FindTMPInScene();
}