using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpellcasting : MonoBehaviour
{
    
    //Currently using an array, this should only ever be of size 2, if you can think of a better data structure please tell me because im SURE theres a beter way
    public Spell[] Spells;
    
    
    private InputAction _attackAction;
    private InputAction _altAttackAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackAction = PlayerManager.PlayerInput.actions.FindAction("Attack");
        _altAttackAction = PlayerManager.PlayerInput.actions.FindAction("AltAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackAction.triggered) Spells[0].Use();
        if (_altAttackAction.triggered) Spells[1].Use();
    }
}
