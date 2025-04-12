using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Zap", menuName = "Spells/Active/Zap")]
public class ZapSpell : ActiveSpell
{
    [SerializeField] LayerMask targetableObjects;
    [SerializeField] GameObject zapPrefab;
    [SerializeField] float lifetime;
    [SerializeField] float damage;
    [SerializeField] float range;
    
    public override bool Use()
    {
        if (!base.Use()) return false;

        float trueRange = range + Vector3.Distance(Camera.main.transform.position, PlayerManager.Instance.transform.position);
        
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray targetRay = Camera.main.ScreenPointToRay(screenCentre);
        RaycastHit hit;

        Ray ray;
        
        //Render the graphical effects
        GameObject zapObject = Instantiate(zapPrefab, PlayerManager.Instance.transform.position, quaternion.identity);
        zapObject.GetComponent<HitscanSpellMono>().Setup(lifetime);
            
        LineRenderer lr = zapObject.GetComponent<LineRenderer>();
        
        
        //Get the target location from the camera
        if (Physics.Raycast(targetRay, out hit, trueRange, targetableObjects))
        {
            ray = new Ray(PlayerManager.Instance.transform.position, (hit.point - PlayerManager.Instance.transform.position).normalized);
        }
        else
        {
            Vector3 endpoint = targetRay.origin + (targetRay.direction * trueRange);
            
            lr.positionCount = 2;
            Vector3[] positions = {PlayerManager.Instance.transform.position, endpoint};
            
            lr.SetPositions(positions);
            
            return true;
        }
        
        if (Physics.Raycast(ray, out hit, 10000f,targetableObjects))
        {
            lr.positionCount = 2;
            Vector3[] positions = {PlayerManager.Instance.transform.position, hit.point};
            
            lr.SetPositions(positions);
            
            //Deal the damage
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            if (damageable == null) return true;
            
            damageable.Damage(damage);
        }
        else
        {
            Debug.LogError("Zap attempted to hit object that should've been in range but wasn't!");
        }
        
        return true;
    }
}
