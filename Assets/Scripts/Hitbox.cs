using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [System.Serializable]
    public struct Properties {
        public float damage;
        public float knocback;
    }

    [SerializeField] private Properties _properties;
    public Properties properties {
        get { return _properties; }
    }
}