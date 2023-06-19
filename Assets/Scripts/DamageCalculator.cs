using UnityEngine;

public class DamageCalculator : MonoBehaviour
{ 
    public float value { get; private set; }
    public float upgradeAmount { get; private set; }
    public float upgradeCost { get; private set; }
    public int level { get; private set; }
    public DamageCalculator flatDamage { get; private set; }
    public DamageCalculator critDamage { get; private set; }
    public DamageCalculator critDamageChance { get; private set; }
    public DamageCalculator damageSpread { get; private set; }
    public DamageCalculator spreadPercent { get; private set; }
    public DamageCalculator damageMultiplier { get; private set; }
    public DamageCalculator moneyMultiplier { get; private set; }
    public DamageCalculator critMoney { get; private set; }
    public DamageCalculator critMoneyChance { get; private set; }
        
    public void SetDamageParameter(DamageCalculator parameter, float value)
    {
        parameter.value = value;
    }
    
    public float Damage(float flatDamage, float critDamage, float critDamageChance, float damageSpread, float spreadPercent, float damageMultiplier)
    {
        bool isCrit;

        isCrit = Random.Range(0, 100) <= critDamageChance;

        float damage = (flatDamage + flatDamage * critDamage * critDamageChance) * damageMultiplier;

        damageSpread = Random.Range(0.0f, damage * spreadPercent);

        int spread = Random.Range(-1, 1); //если -1, то damageSpread вычитается из damage, если 1, то наоборот

        damage = damage + damageSpread * spread;

        return damage;
    }
}
