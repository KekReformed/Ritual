using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public bool active;
    [SerializeField] float speed;
    RectTransform _rectTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        PlayerManager.Instance.transform.position = new Vector3(transform.position.x, 500, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(!active) return;
        
        if(_rectTransform.localPosition.y < 1499)transform.position = new Vector3(transform.position.x, transform.position.y + 1 * speed * Time.deltaTime, 0);
    }
}
