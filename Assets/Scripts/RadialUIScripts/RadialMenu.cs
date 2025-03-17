using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IRadialMenu
{
    void Close();
    void Open();
    void Toggle();
}

public class RadialMenu : MonoBehaviour
{

    [SerializeField]
    GameObject EntryPrefab;
    [SerializeField]
    float Radius = 300f;

    [SerializeField]
    List<Texture> Icons;

    List<RadialMenuEntry> Entries;
    void Start()
    {
        Entries = new List<RadialMenuEntry>();
    }

    void AddEntry(string pLabel, Texture pIcon)
    {
        GameObject entry = Instantiate(EntryPrefab, transform);

        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);

        Entries.Add(rme);
    }

    public void Open()
    {
        for (int i = 0; i < 4; i++)
        {
            AddEntry("Button" + i.ToString(), Icons[i]);
        }
        Rearrange();
    }
    public void Close()
    {
        for (int i = 0; i < 4; i++)
        {
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            GameObject entry = Entries[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete =
            delegate ()
            {
                Destroy(entry);
            };

        }
         Entries.Clear();
    }
    public void Toggle()
    {
        if (Entries.Count == 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Rearrange()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / Entries.Count;
        for (int i = 0; i < Entries.Count; i++)
        {
            float angle = radiansOfSeparation * i;
            float x = Mathf.Sin(angle) * Radius;
            float y = Mathf.Cos(angle) * Radius;

            RectTransform rectTransform = Entries[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(x, y, 0);

            // Apply rotation to each entry
            rectTransform.rotation = Quaternion.Euler(0, 0, -angle * Mathf.Rad2Deg);
        }
    }

   }
