using UnityEngine;

public class NoChildren : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0) Destroy(this);
    }
}
