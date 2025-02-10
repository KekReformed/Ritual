using System;
using UnityEngine;

[Serializable]
public class BasicSpell : Spell
{
    [SerializeField] GameObject fireball;
    [SerializeField] private Transform aimpoint;

    public override void Use()
    {
        //Rotate player towards spell direction
        GameObject fireballObject;
        float targetAngle = Mathf.Atan2(Vector3.forward.x, Vector3.forward.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
        fireballObject = GameObject.Instantiate(fireball,PlayerManager.Movement.transform.position,Quaternion.identity);
        fireballObject.GetComponent<Rigidbody>().linearVelocity = (aimpoint.position - PlayerManager.Instance.transform.position).normalized * 2;
            
        PlayerManager.SetMana(PlayerManager.Instance.Mana - 20);
    }
}
