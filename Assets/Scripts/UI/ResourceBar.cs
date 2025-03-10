using UnityEngine;
using UnityEngine.Serialization;

public class ResourceBar : MonoBehaviour
{
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public float scale;
    
    public float startingResourceCount = 100;
    public string resourceString;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        UIManager.ResourceBarList.Add(this);
        if(resourceString == "") Debug.LogError("Empty resource bar string!");
    }

    // Update is called once per frame
    public void UpdateResource(float resource, float offset = 0)
    {
        scale = resource / startingResourceCount;
        
        rect.localScale = new Vector3(scale, rect.localScale.y, rect.localScale.z);
        rect.localPosition = new Vector3(-(rect.rect.width * (scale / 2) - rect.rect.width / 2) - offset, rect.localPosition.y, rect.localPosition.z);
    }
}
