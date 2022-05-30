using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private GameEvent OnCombatEnd;
    [SerializeField] private GameEvent OnHeroDied;
    [SerializeField] private Animator animator;

    private EnemyObject _enemyObject;
    private HeroData _heroData;
    private Animator _heroAnimator;
    private bool _combatStarted;
    private int _enemyHealth;
    public HP_BAR _enemyHPBar;
    private float _timeEnemy;
    private float _timeHero;

    private int _enemyDamage;

    private void Start()
    {
        _enemyObject = (EnemyObject)GetComponent<Structure>().StructureObject;
        _enemyHealth = _enemyObject.maxHealth;
        _enemyHPBar.SetMaxHealth(_enemyObject.maxHealth);
        _heroData = _enemyObject.heroData;
        _enemyDamage = _enemyObject.baseDamage + 2 * GameManager.instance.enemyLevel;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Hero"))
        {
            _combatStarted = true;
            _heroAnimator = col.GetComponent<Animator>();
        }
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
            _heroAnimator.Play("Attack");
            _enemyHealth -= _heroData.currentDamage;
            _enemyHPBar.SetHealth(_enemyHealth);
            _timeHero = _heroData.currentAttackSpeed;
            if (_enemyHealth <= 0) EnemyDied();
        }
    }

    private void EnemyCycle()
    {
        if (_timeEnemy > 0) _timeEnemy -= Time.deltaTime;
        else
        {
            animator.Play("Attack");
            _heroData.GetDamage(_enemyDamage);
            _timeEnemy = _enemyObject.attackSpeed;
            if (_heroData.currentHealth <= 0) HeroDied();
        }
    }

    private void HeroDied()
    {
        _combatStarted = false;
        OnHeroDied.Raise();
    }

    private void EnemyDied()
    {
        _heroData.UpdateExperience(_enemyObject.CurrentExperienceWhenKilled);
        _heroData.money += _enemyObject.DroppedMoney;
        GameManager.instance.UpdateMoney();
        OnCombatEnd.Raise();
        Destroy(gameObject);
    }
}