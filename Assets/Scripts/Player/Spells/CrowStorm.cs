using UnityEngine;

[CreateAssetMenu(fileName = "Crow Storm", menuName = "Spells/Active/Crow storm")]
public class CrowStorm : ActiveSpell
{
    [SerializeField] GameObject prefab;

    public override bool Use()
    {
        if (!base.Use()) return false;

        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(Vector3.forward);
        
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        
        Instantiate(prefab, PlayerManager.Instance.transform.position + Vector3.up * 2, PlayerManager.Instance.transform.rotation);

        return true;
    }
}
