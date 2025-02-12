using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpellcasting : MonoBehaviour
{
    
    //Currently using an array, this should only ever be of size 2, if you can think of a better data structure please tell me because im SURE theres a beter way
    public Spell MainSpell;
    public Spell AltSpell;
    
    InputAction _attackAction;
    InputAction _altAttackAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackAction = PlayerManager.PlayerInput.actions.FindAction("Attack");
        _altAttackAction = PlayerManager.PlayerInput.actions.FindAction("AltAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackAction.triggered) CastSpell(MainSpell);
        if (_altAttackAction.triggered) CastSpell(AltSpell);
    }

    void CastSpell(Spell spell)
    {
        if (spell.cost > PlayerManager.Mana) return;


        if (!TimerManager.Timers.ContainsKey(spell.name))
        {
            TimerManager.AddTimer(new Timer(spell.name,spell.cooldown));
            TimerManager.ResetTimer(spell.name);
            
            spell.Use();
        }
        
        if (!TimerManager.CheckTimer(spell.name)) return;
        
        TimerManager.ResetTimer(spell.name);
        spell.Use();
    }
}
