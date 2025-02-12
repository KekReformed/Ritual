using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public float cost;
    [Tooltip("Cooldown in seconds")] public float cooldown;

    //Returns a bool corresponding if we can cast the spell or not, use later for things like stuns
    public virtual void Use()
    {
        PlayerManager.SetMana(PlayerManager.Mana - cost);
    }

    public void RotateTowardsCameraDir()
    {
        float targetAngle = Mathf.Atan2(Vector3.forward.x, Vector3.forward.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
}
