using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image Bar;
    public Text Name;
    public Text Health;

    public float maxHealth;
    public float hp;

    public float attackSpeedEnemy;
    public int minDamageEnemy;
    public int maxDamageEnemy;

    private void Start()
    {
        Bar = GetComponent<Image>();
        hp = maxHealth;

        InvokeRepeating("Attack", 0.0f, attackSpeedEnemy);
    }

    private void Update()
    {
        Bar.fillAmount = hp / maxHealth;
        Health.text = hp.ToString();
    }

    void Attack()
    {
        hp -= Random.Range(minDamageEnemy, maxDamageEnemy);

        if (hp < 0)
        {
            Debug.Log(Name.text + " Win");
            CancelInvoke();
        }
    }
}
