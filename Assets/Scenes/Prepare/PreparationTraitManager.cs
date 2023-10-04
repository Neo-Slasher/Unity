using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationTraitManager : MonoBehaviour
{
    public Player statusByTrait;

    public enum EffectType {
        none, hp, moveSpeed, attackPower, attackSpeed,
        attackRange, startMoney, earnMoney, shopSlot,
        itemSlot, shopMinRank, shopMaxRank, dropRank,
        dropRate, healByHit, hpRegen, dealOnMax, dealOnHp,
        active
    }

    public void activeTrait(Trait trait) {
        TraitParseAndApply(trait);
    }

    public void TraitParseAndApply(Trait trait) {
        if (trait.effectType1 != 0)
            applyTrait((EffectType)trait.effectType1, trait.effectValue1, trait.effectMulti1);
        if (trait.effectType2 != 0)
            applyTrait((EffectType)trait.effectType2, trait.effectValue2, trait.effectMulti2);
        if (trait.effectType3 != 0)
            applyTrait((EffectType)trait.effectType3, trait.effectValue3, trait.effectMulti3);
        if (trait.effectType4 != 0)
            applyTrait((EffectType)trait.effectType4, trait.effectValue4, trait.effectMulti4);
    }

    private void applyTrait(EffectType type, double value, bool multi) {
        if (type == EffectType.hp) {
            statusByTrait.maxHp += value;
        }
        else if (type == EffectType.moveSpeed) {
            statusByTrait.moveSpeed += value;
        }
        else if (type == EffectType.attackPower) {
            statusByTrait.attackPower += value;
        }
        else if (type == EffectType.attackSpeed) {
            statusByTrait.attackSpeed += value;
        }
        else if (type == EffectType.attackRange) {
            statusByTrait.attackRange += value;
        }
        else if (type == EffectType.startMoney) {
            statusByTrait.startMoney += (int)value;
        }
        else if (type == EffectType.earnMoney) {
            statusByTrait.earnMoney += (float)value;
        }
        else if (type == EffectType.shopSlot) {
            statusByTrait.shopSlot += (int)value;
        }
        else if (type == EffectType.itemSlot) {
            statusByTrait.itemSlot += (int)value;
        }
        else if (type == EffectType.shopMinRank) {
            statusByTrait.shopMinRank += (int)value;
        }
        else if (type == EffectType.shopMaxRank) {
            statusByTrait.shopMaxRank += (int)value;
        }
        else if (type == EffectType.dropRank) {
            statusByTrait.dropRank += (int)value;
        }
        else if (type == EffectType.dropRate) {
            statusByTrait.dropRate += value;
        }
        else if (type == EffectType.healByHit) {
            statusByTrait.healByHit += value;
        }
        else if (type == EffectType.hpRegen) {
            statusByTrait.hpRegen += value;
        }
        else if (type == EffectType.dealOnMax) {
            statusByTrait.dealOnMaxHp += value;
        }
        else if (type == EffectType.dealOnHp) {
            statusByTrait.dealOnCurHp += value;
        }
    }


    public void unactiveTrait(Trait trait) {
        TraitParseAndDisapply(trait);
    }

    public void TraitParseAndDisapply(Trait trait) {
        if (trait.effectType1 != 0)
            disapplyTrait((EffectType)trait.effectType1, trait.effectValue1, trait.effectMulti1);
        if (trait.effectType2 != 0)
            disapplyTrait((EffectType)trait.effectType2, trait.effectValue2, trait.effectMulti2);
        if (trait.effectType3 != 0)
            disapplyTrait((EffectType)trait.effectType3, trait.effectValue3, trait.effectMulti3);
        if (trait.effectType4 != 0)
            disapplyTrait((EffectType)trait.effectType4, trait.effectValue4, trait.effectMulti4);
    }

    private void disapplyTrait(EffectType type, double value, bool multi) {
        applyTrait(type, -value, multi);  
    }

    public void SaveTrait() {
        GameManager.instance.player.maxHp += statusByTrait.maxHp;
        GameManager.instance.player.moveSpeed += statusByTrait.moveSpeed;
        GameManager.instance.player.attackPower += statusByTrait.attackPower;
        GameManager.instance.player.attackSpeed += statusByTrait.attackSpeed;
        GameManager.instance.player.attackRange += statusByTrait.attackRange;
        GameManager.instance.player.startMoney += statusByTrait.startMoney;
        GameManager.instance.player.earnMoney += statusByTrait.earnMoney;
        GameManager.instance.player.shopSlot += statusByTrait.shopSlot;
        GameManager.instance.player.itemSlot += statusByTrait.itemSlot;
        GameManager.instance.player.shopMinRank += statusByTrait.shopMinRank;
        GameManager.instance.player.shopMaxRank += statusByTrait.shopMaxRank;
        GameManager.instance.player.dropRank += statusByTrait.dropRank;
        GameManager.instance.player.dropRate += statusByTrait.dropRate;
        GameManager.instance.player.healByHit += statusByTrait.healByHit;
        GameManager.instance.player.hpRegen += statusByTrait.hpRegen;
        GameManager.instance.player.dealOnMaxHp += statusByTrait.dealOnMaxHp;
        GameManager.instance.player.dealOnCurHp += statusByTrait.dealOnCurHp;
    }
}
