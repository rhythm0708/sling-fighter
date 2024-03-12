using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHealth = 100.0f;
    private float _value;
    private Hurtbox hurtbox;

    public float value 
    {
        get { return _value;}
    }

    public float ratio 
    {
        get { return _value / maxHealth;}
    }

    public void Damage(float amount) 
    {
        _value -= amount;
        if (_value < 0.0f)
        {
            _value = 0.0f;
        }
    }

    void Start()
    {
        _value = maxHealth;
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);
    }

    void OnHurt(Collider collider, Hitbox.Properties properties, Vector3 direction) 
    {
        Damage(properties.damage);
    }
}
