using System;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    ResourceBar _passiveManaBar;
    InputAction _radialPrimaryOpen;
    InputAction _radialSecondaryOpen;
    InputAction _openSpellTree;
    Canvas _canvas;
    
    public bool talking;

    public GameObject currentTextbox;

    [SerializeField] SpellSlot primarySpellSlot;
    [SerializeField] SpellSlot secondarySpellSlot;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Text manaRegenText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject textbox;
    [SerializeField] GameObject[] objectsToHideInDialogue;
    [SerializeField] GameObject spellTree;
    [SerializeField] Button learnButton;
    public TMP_Text skillPointCost;
    public TMP_Text currentSkillPoints;
    
    public TMP_Text spellTreeDescription;
    public TMP_Text spellTreeTitle;

    public Spell selectedSpell;
    public GameObject UISelection;

    public TitleUI title;
    
    public static SpellSlot PrimarySpellSlot;
    public static SpellSlot SecondarySpellSlot;
    public static TMP_Text ManaText;
    public static TMP_Text ManaRegenText;
    public static TMP_Text HealthText;
    public static RadialUI RadialUI;
    public static List<ResourceBar> ResourceBarList = new List<ResourceBar>();
    public static Dictionary<string, ResourceBar> ResourceBars = new Dictionary<string,ResourceBar>();
    public static UIManager Instance;

    CinemachineInputAxisController _cameraInput;


    void Awake()
    {
        PrimarySpellSlot = primarySpellSlot;
        SecondarySpellSlot = secondarySpellSlot;

        _cameraInput = GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>();
        
        _canvas = GetComponent<Canvas>();
        
        ManaText = manaText;
        ManaRegenText = manaRegenText;
        HealthText = healthText;
        
        RadialUI = GetComponentInChildren<RadialUI>();
        
        RadialUI.gameObject.SetActive(true);
        RadialUI.Setup();
        RadialUI.gameObject.SetActive(false);
        
        if (Instance != null)
        {
            Debug.LogError("Multiple UI managers! there should only be 1!");
            return;
        }

        title = GetComponentInChildren<TitleUI>();
        Instance = this;
    }
    void Start()
    {
        foreach (ResourceBar bar in ResourceBarList)
        {
            ResourceBars[bar.resourceString] = bar;
        }
        
        _radialPrimaryOpen = PlayerManager.PlayerInput.actions.FindAction("SelectPrimarySpell");
        _radialSecondaryOpen = PlayerManager.PlayerInput.actions.FindAction("SelectSecondarySpell");
        _openSpellTree = PlayerManager.PlayerInput.actions.FindAction("Open Spell Tree");
        
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SpellTreeSlot slot = null;
        if (UISelection != null) slot = UISelection.GetComponent<SpellTreeSlot>();
        if (UISelection != null && slot.cost <= PlayerManager.Instance.skillPoints && !slot.learnt) learnButton.interactable = true;
        else learnButton.interactable = false;
        
        if ((_radialPrimaryOpen.triggered || _radialSecondaryOpen.triggered) && !talking)
        {
            RadialUI.gameObject.SetActive(true);
            DisableMouseLook();
            RadialUI.Open();
        }

        if (_radialPrimaryOpen.WasReleasedThisFrame())
        {
            RadialUI.GetSpell();
            if (_radialSecondaryOpen.ReadValue<float>() == 0)
            {
                RadialUI.Close();
                RadialUI.gameObject.SetActive(false);
            }
            EnableMouseLook();
        }
        
        if (_radialSecondaryOpen.WasReleasedThisFrame())
        {
            RadialUI.GetSpell(true);
            if (_radialPrimaryOpen.ReadValue<float>() == 0)
            {
                RadialUI.Close();
                RadialUI.gameObject.SetActive(false);
            }
            EnableMouseLook();
        }

        if (_openSpellTree.WasReleasedThisFrame() && (!talking || spellTree.activeSelf))
        {
            if(spellTree.activeSelf) CloseSpelltree();
            else OpenSpelltree();
        }
    }

    public static void SetManaUI(float mana, float maxMana, float passiveManaUsage)
    {
        ManaText.SetText($"{Mathf.Round(mana)}/{Mathf.Round(maxMana)}");
        
        ResourceBars["Mana"].SetScale(mana / (maxMana + passiveManaUsage) + passiveManaUsage / (maxMana + passiveManaUsage));
        ResourceBars["PassiveMana"].UpdateResource(passiveManaUsage, maxMana + passiveManaUsage);
    }

    public void CreateNewDialogue(string text)
    {
        currentTextbox = Instantiate(textbox, _canvas.transform, false);
        currentTextbox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-254, 362);
        currentTextbox.GetComponentInChildren<TMP_Text>().SetText(text);
        
    }

    public void ClearDialogue()
    {
        if(currentTextbox == null) return;
        Destroy(currentTextbox);
    }

    public void HideUI()
    {
        for (int i = 0; i < objectsToHideInDialogue.Length; i++)
        {
            GameObject UIElement = objectsToHideInDialogue[i];
            
            UIElement.SetActive(false);
        }
    }
    
    public void UnhideUI()
    {
        
        for (int i = 0; i < objectsToHideInDialogue.Length; i++)
        {
            GameObject UIElement = objectsToHideInDialogue[i];
            
            UIElement.SetActive(true);
        }
    }

    void DisableMouseLook()
    {
        _cameraInput.enabled = false;
    }
    
    void EnableMouseLook()
    {
        _cameraInput.enabled = true;
    }

    void OpenSpelltree()
    {
        spellTree.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        DisableMouseLook();
        
        PlayerManager.Movement.enabled = false;
        PlayerManager.PlayerSpellcasting.enabled = false;
        PlayerManager.Movement.animator.SetFloat("Move", 0);
        
        currentSkillPoints.SetText($"Current Skill Points: {PlayerManager.Instance.skillPoints.ToString()}");
        
        HideUI();
    }
    
    void CloseSpelltree()
    {
        spellTree.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        EnableMouseLook();
        
        PlayerManager.Movement.enabled = true;
        PlayerManager.PlayerSpellcasting.enabled = true;
        
        spellTreeTitle.SetText("");
        spellTreeDescription.SetText("");
        skillPointCost.SetText("");
        
        UnhideUI();
    }

    public void LearnSpell()
    {
        SpellTreeSlot spellTreeSlot = UISelection.GetComponent<SpellTreeSlot>();
        if(spellTreeSlot.cost > PlayerManager.Instance.skillPoints || spellTreeSlot.learnt) return;
        
        spellTreeSlot.EnableDependents();
        PlayerManager.Instance.skillPoints -= spellTreeSlot.cost;
        currentSkillPoints.SetText($"Current Skill Points: {PlayerManager.Instance.skillPoints.ToString()}");
        spellTreeSlot.learnt = true;

        if (spellTreeSlot.upgrade)
        {
            PlayerManager.Instance.Upgrades.Add(spellTreeSlot.upgradeName);
            return;
        }
        
        for (int i = 0; i < RadialUI.SpellList.Length; i++)
        {
            if (RadialUI.SpellList[i] != null) continue;
            
            RadialUI.SpellList[i] = selectedSpell;
            RadialUI.gameObject.SetActive(true);
            GameObject PizzaSlice = GameObject.Find($"Pizza Slice {i+1}");
            
            Image image = PizzaSlice.transform.GetChild(0).GetComponent<Image>();
            image.sprite = selectedSpell.icon;
            image.SetNativeSize();
            
            RadialUI.gameObject.SetActive(false);
            break;
        }
    }
}
