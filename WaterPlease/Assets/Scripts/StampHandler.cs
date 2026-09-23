using UnityEngine;

public class StampHandler : MonoBehaviour
{
    public enum StampType { Approve, Deny}

    [SerializeField] public StampType stampType;
}
