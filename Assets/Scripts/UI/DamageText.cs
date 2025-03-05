using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageText : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] float maxLifetime;
    [SerializeField] float speed;
    float _lifetime;
    
    public void Setup(float damage)
    {
        text = GetComponent<TMP_Text>();
        text.SetText(damage.ToString());
        _lifetime = maxLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        text.color = StaticUtils.fadeColor(text.color, _lifetime, maxLifetime);
        transform.LookAt(Camera.main.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        
        
        _lifetime -= Time.deltaTime;
        
        if (_lifetime < 0) Destroy(gameObject);
    }
}
