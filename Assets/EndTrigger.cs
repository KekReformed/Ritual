using UnityEngine;
using UnityEngine.Events;

public class EndTrigger : MonoBehaviour
{
    public UnityEvent endTrigger;
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) endTrigger.Invoke();
    }
}
