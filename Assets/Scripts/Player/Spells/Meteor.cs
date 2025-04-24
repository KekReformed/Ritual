using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Meteor", menuName = "Spells/Active/Meteor")]
public class Meteor : ActiveSpell
{
    [SerializeField] float range;
    [SerializeField] LayerMask targetableObjects;
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] float damage;
    [SerializeField] float speed = 1;
    
    public override bool Use()
    {
        if (!base.Use()) return false;
        
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(Vector3.forward);
        
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        
        float trueRange = range + Vector3.Distance(Camera.main.transform.position, PlayerManager.Instance.transform.position);
        
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray targetRay = Camera.main.ScreenPointToRay(screenCentre);
        RaycastHit hit;

        Ray ray;
        Vector3 targetPoint;
        
        //Get the target location from the camera, if fail do it at max range
        if (Physics.Raycast(targetRay, out hit, trueRange, targetableObjects))
        {
            ray = new Ray(PlayerManager.Instance.transform.position, (hit.point - PlayerManager.Instance.transform.position).normalized);
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = targetRay.origin + (targetRay.direction * trueRange);
            
        }

        if(targetPoint.y > PlayerManager.Instance.transform.position.y) targetPoint.y = PlayerManager.Instance.transform.position.y;
        
        GameObject prefab = Instantiate(meteorPrefab,  PlayerManager.Instance.transform.position + Vector3.up * 3, quaternion.identity);
        MeteorMono mono = prefab.GetComponent<MeteorMono>();
        
        mono.target = targetPoint;
        mono.damage = damage;
        
        prefab.GetComponent<Rigidbody>().linearVelocity = (targetPoint - prefab.transform.position).normalized * speed * Time.deltaTime;
        
        return true;
    }
}
