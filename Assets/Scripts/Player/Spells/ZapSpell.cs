using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Zap", menuName = "Spells/Active/Zap")]
public class ZapSpell : ActiveSpell
{
    [SerializeField] LayerMask targetableObjects;
    [SerializeField] GameObject zapPrefab;
    [SerializeField] float lifetime;
    [SerializeField] float damage;
    
    public override bool Use()
    {
        if (!base.Use()) return false;

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray targetRay = Camera.main.ScreenPointToRay(screenCentre);
        RaycastHit hit;

        Physics.Raycast(targetRay, out hit, 10000f, targetableObjects);
        
        Ray ray = new Ray(PlayerManager.Instance.transform.position, (hit.point - PlayerManager.Instance.transform.position).normalized);
        
        if (Physics.Raycast(ray, out hit, 10000f,targetableObjects))
        {
            //Render the graphical effects
            GameObject zapObject = Instantiate(zapPrefab, PlayerManager.Instance.transform.position, quaternion.identity);
            zapObject.GetComponent<ZapSpellMono>().Setup(lifetime);
            
            LineRenderer lr = zapObject.GetComponent<LineRenderer>();
            
            lr.positionCount = 2;
            Vector3[] positions = {PlayerManager.Instance.transform.position, hit.point};
            
            lr.SetPositions(positions);
            
            //Deal the damage
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            if (damageable == null) return true;
            
            damageable.Damage(damage);
        }
        
        return true;
    }
}
