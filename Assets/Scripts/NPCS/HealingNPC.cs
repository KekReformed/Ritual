using UnityEngine;

public class HealingNPC : NPC
{
    protected override void Start()
    {
        base.Start();
        OnDialogueEnd.AddListener(ResetHP);
    }
    
    protected override void Update()
    {
        base.Update();
        float healthPercent = PlayerManager.Health / PlayerManager.MaxHealth;
        if (healthPercent == 1) dialogue = "I can heal you if your hurt, but doesn't look like you need me just yet";
        if (InRange(healthPercent, 0.8f, .99f)) dialogue = "You know this healing isn't cheap right? Here's some healing but you should only come when you really need it!";
        if (InRange(healthPercent, 0.6f, 0.8f)) dialogue = "Looks like you gave those guys what for! Here's some healing";
        if (InRange(healthPercent, 0.4f, 0.6f)) dialogue = "Got beat up huh? Take this healing and show those Christians what for!";
        if (InRange(healthPercent, 0.2f, 0.4f)) dialogue = "You might want to start being more careful, those wounds are pretty serious! No matter here's some healing";
        if (InRange(healthPercent, 0, 0.2f)) dialogue = "Oh my goodness, Be careful out there! I heal the living not raise not raise the dead!";
    }

    bool InRange(float number,float low, float high)
    {
        return number >= low && number <= high;
    }

    void ResetHP()
    {
        float heal = PlayerManager.MaxHealth - PlayerManager.Health;
        PlayerManager.Instance.Damage(-heal);
    }
}
