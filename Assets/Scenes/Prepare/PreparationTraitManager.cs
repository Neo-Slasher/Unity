using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationTraitManager : MonoBehaviour
{
    public Player dummyPlayer;

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
            dummyPlayer.maxHp += value;
        }
        else if (type == EffectType.moveSpeed) {
            dummyPlayer.moveSpeed += value;
        }
        else if (type == EffectType.attackPower) {
            dummyPlayer.attackPower += value;
        }
        else if (type == EffectType.attackSpeed) {
            dummyPlayer.attackSpeed += value;
        }
        else if (type == EffectType.attackRange) {
            dummyPlayer.attackRange += value;
        }
        else if (type == EffectType.startMoney) {
            dummyPlayer.startMoney += (int)value;
        }
        else if (type == EffectType.earnMoney) {
            dummyPlayer.earnMoney += (float)value;
        }
        else if (type == EffectType.shopSlot) {
            dummyPlayer.shopSlot += (int)value;
        }
        else if (type == EffectType.itemSlot) {
            dummyPlayer.itemSlot += (int)value;
        }
        else if (type == EffectType.shopMinRank) {
            dummyPlayer.shopMinRank += (int)value;
        }
        else if (type == EffectType.shopMaxRank) {
            dummyPlayer.shopMaxRank += (int)value;
        }
        else if (type == EffectType.dropRank) {
            dummyPlayer.dropRank += (int)value;
        }
        else if (type == EffectType.dropRate) {
            dummyPlayer.dropRate += value;
        }
        else if (type == EffectType.healByHit) {
            dummyPlayer.healByHit += value;
        }
        else if (type == EffectType.hpRegen) {
            dummyPlayer.hpRegen += value;
        }
        else if (type == EffectType.dealOnMax) {
            dummyPlayer.dealOnMaxHp += value;
        }
        else if (type == EffectType.dealOnHp) {
            dummyPlayer.dealOnCurHp += value;
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
        GameManager.instance.player.maxHp += dummyPlayer.maxHp;
        GameManager.instance.player.moveSpeed += dummyPlayer.moveSpeed;
        GameManager.instance.player.attackPower += dummyPlayer.attackPower;
        GameManager.instance.player.attackSpeed += dummyPlayer.attackSpeed;
        GameManager.instance.player.attackRange += dummyPlayer.attackRange;
        GameManager.instance.player.startMoney += dummyPlayer.startMoney;
        GameManager.instance.player.earnMoney += dummyPlayer.earnMoney;
        GameManager.instance.player.shopSlot += dummyPlayer.shopSlot;
        GameManager.instance.player.itemSlot += dummyPlayer.itemSlot;
        GameManager.instance.player.shopMinRank += dummyPlayer.shopMinRank;
        GameManager.instance.player.shopMaxRank += dummyPlayer.shopMaxRank;
        GameManager.instance.player.dropRank += dummyPlayer.dropRank;
        GameManager.instance.player.dropRate += dummyPlayer.dropRate;
        GameManager.instance.player.healByHit += dummyPlayer.healByHit;
        GameManager.instance.player.hpRegen += dummyPlayer.hpRegen;
        GameManager.instance.player.dealOnMaxHp += dummyPlayer.dealOnMaxHp;
        GameManager.instance.player.dealOnCurHp += dummyPlayer.dealOnCurHp;
    }
}
