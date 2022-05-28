using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private GameEvent OnCombatEnd;
    [SerializeField] private HeroData heroData;
    [SerializeField] private Animator animator;

    private EnemyObject _enemyObject;
    private bool _combatStarted;
    private int _enemyHealth;
    private float _timeEnemy;
    private float _timeHero;

    private void Start()
    {
        _enemyObject = (EnemyObject)GetComponent<Structure>().StructureObject;
        _enemyHealth = _enemyObject.maxHealth;
    }

    public void StartCombat()
    {
        _combatStarted = true;
    }

    private void Update()
    {
        if (!_combatStarted) return;
        HeroCycle();
        EnemyCycle();
    }

    private void HeroCycle()
    {
        if (_timeHero > 0) _timeHero -= Time.deltaTime;
        else
        {
            _enemyHealth -= heroData.currentDamage;
            _timeHero = heroData.currentAttackSpeed;
            if (_enemyHealth <= 0) EnemyDied();
        }
    }

    private void EnemyCycle()
    {
        if (_timeEnemy > 0) _timeEnemy -= Time.deltaTime;
        else
        {
            animator.Play("Attack");
            heroData.currentHealth -= _enemyObject.baseDamage;
            _timeEnemy = _enemyObject.attackSpeed;
            if (heroData.currentHealth <= 0) HeroDied();
        }
    }

    private void HeroDied()
    {
        Debug.Log("Welp! Hero is dead. Now what?");
        _combatStarted = false;
    }

    private void EnemyDied()
    {
        OnCombatEnd.Raise();
        Destroy(gameObject);
    }
}
