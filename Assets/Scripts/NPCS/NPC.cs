using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    public UnityEvent OnDialogueEnd;
    
    InputAction _use;
    bool canTalk;

    [SerializeField] [TextArea(5,10)] protected string dialogue;
    [SerializeField] [TextArea(5,10)] string questCompleteDialogue;
    [SerializeField] GameObject dialogueIcon;
    [SerializeField] GameObject dialogueCam;
    [SerializeField] Quest quest;
    Vector3 _originalRotation;
    bool _talking;
    bool _confirmedComplete;
    bool _questAdded;

    Animator _animator;

    public bool questComplete = false;
    
    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _originalRotation = transform.rotation.eulerAngles;
        _use = PlayerManager.PlayerInput.actions.FindAction("Use");
        if(quest != null) quest.questGiver = this;
    }
    void OnTriggerStay(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) != "Player") return;
        
        dialogueIcon.SetActive(true);
        canTalk = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) != "Player") return;
        
        dialogueIcon.SetActive(false);
        canTalk = false;
        
        StopTalking();
    }

    protected virtual void Update()
    {
        if(!canTalk) return;

        if (_use.triggered)
        {
            if (!_talking)
            {
                StartTalking();
            }
            else
            {
                StopTalking();
                if (quest.name != "" && !_questAdded)
                {
                    QuestManager.Quests.Add(quest.name,quest);
                    quest.QuestStart();
                    _questAdded = true;
                }
                if (questComplete)
                {
                    QuestManager.Quests.Remove(quest.name);
                    UIManager.Instance.title.SetTitle($"Gained {quest.skillPointReward} skill points!",2f, Color.green);
                }
            }
        }
    }

    void StopTalking()
    {
        _animator.SetBool("Talking", false);
        _talking = false;
        UIManager.Instance.ClearDialogue();
        
        UIManager.Instance.talking = false;
                
        PlayerManager.Movement.enabled = true;
        PlayerManager.PlayerSpellcasting.enabled = true;
        dialogueCam.SetActive(false);
        UIManager.Instance.UnhideUI();
        
        if (!PlayerManager.Instance.completedAQuest && _confirmedComplete)
        {
            UIManager.Instance.title.SetTitle("Quests reward Skill points that can be spent in the Spell tree (TAB)", 5f, Color.cyan);
            PlayerManager.Instance.completedAQuest = true;
        }

        transform.rotation = Quaternion.Euler(_originalRotation);
        OnDialogueEnd.Invoke();
    }

    void StartTalking()
    {
        _animator.SetBool("Talking", true);
        UIManager.Instance.talking = true;
        
        if (quest.name == "" || !questComplete) UIManager.Instance.CreateNewDialogue(dialogue);
        else if (questComplete)
        {
            if (!_confirmedComplete)
            {
                UIManager.Instance.CreateNewDialogue(questCompleteDialogue);
                PlayerManager.Instance.skillPoints += quest.skillPointReward;
            }
            _confirmedComplete = true;
        }
        _talking = true;
                
        PlayerManager.Movement.enabled = false;
        PlayerManager.PlayerSpellcasting.enabled = false;
        dialogueCam.SetActive(true);
        
        //Make player look at NPC
        PlayerManager.Instance.transform.LookAt(transform.position);
        Vector3 playerRotation = PlayerManager.Instance.transform.rotation.eulerAngles;
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0,playerRotation.y,0);
        
        transform.LookAt(PlayerManager.Instance.transform);
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
                
        UIManager.Instance.HideUI();
        PlayerManager.Movement.animator.SetFloat("Move", 0);
    }
}
