using UnityEngine;

public class ZapSpellMono : MonoBehaviour
{
    float _lifetime;
    float _maxLifetime;
    LineRenderer _lr;
    
    public void Setup(float maxLifetime)
    {
        _maxLifetime = maxLifetime;
        _lifetime = _maxLifetime;
        _lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = new Color(_lr.startColor.r,_lr.startColor.g,_lr.startColor.b, _lifetime / _maxLifetime);
        
        _lr.startColor = color;
        _lr.endColor = color;
        
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0) Destroy(gameObject);
    }
}
