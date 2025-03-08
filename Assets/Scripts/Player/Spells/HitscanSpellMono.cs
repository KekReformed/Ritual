using UnityEngine;

public class HitscanSpellMono : MonoBehaviour
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
        Color color = StaticUtils.fadeColor(_lr.startColor, _lifetime, _maxLifetime);
        
        _lr.startColor = color;
        _lr.endColor = color;
        
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0) Destroy(gameObject);
    }
}
