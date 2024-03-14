using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHealth = 100.0f;
    private float _value;
    private Hurtbox hurtbox;

    [SerializeField] private bool reloadOnDeath = false;

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
            if (reloadOnDeath)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            Destroy(gameObject);
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
