using UnityEngine;
using UnityEngine.EventSystems;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
