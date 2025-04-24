using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Rapid Age", menuName = "Spells/Active/Rapid Age")]
public class RapidAge : ActiveSpell
{
    [SerializeField] GameObject prefab;

    public override bool Use()
    {
        if (!base.Use()) return false;

        Instantiate(prefab, PlayerManager.Instance.transform.position, quaternion.identity);

        return true;
    }
}
