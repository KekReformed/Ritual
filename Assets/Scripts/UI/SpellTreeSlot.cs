using UnityEngine;
using Button = UnityEngine.UI.Button;

public class SpellTreeSlot : MonoBehaviour
{
    [SerializeField] string spellName;
    [SerializeField] [TextArea(5,10)] string description;
    
    public GameObject[] dependents;
    public Spell spell;
    public bool upgrade;
    public bool learnt;
    public string upgradeName;
    
    public int cost;
    
    public void SetUi()
    {
        UIManager.Instance.spellTreeTitle.SetText(spellName);
        UIManager.Instance.spellTreeDescription.SetText(description);
        if (!upgrade) UIManager.Instance.selectedSpell = spell;

        UIManager.Instance.UISelection = gameObject;
        UIManager.Instance.skillPointCost.SetText($"{cost.ToString()} Skill Points");
    }

    public void EnableDependents()
    {
        for (int i = 0; i < dependents.Length; i++)
        {
            GameObject spellUI = dependents[i];

            spellUI.GetComponent<Button>().interactable = true;
        }
    }
}
