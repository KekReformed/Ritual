using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [HideInInspector] public Image image;
    [HideInInspector] public float scale;
    
    public float startingResourceCount = 100;
    public string resourceString;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = GetComponent<Image>();
        UIManager.ResourceBarList.Add(this);
        if(resourceString == "") Debug.LogError("Empty resource bar string!");
    }

    // Update is called once per frame
    public void UpdateResource(float resource, float resourceMax)
    {
        scale = resource / resourceMax;

        image.fillAmount = scale;
    }

    public void SetScale(float scale)
    {
        image.fillAmount = scale;
    }
}
