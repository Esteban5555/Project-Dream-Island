using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public int currentMaxHealth;

    public bool Sword;
    public bool RubberRing;
    public bool Lamp;

    public int coins;

    public PlayerData(int health, int maxHealth, bool sword, bool RubberRing, bool Lamp, int coins) {
        currentHealth = health;
        currentMaxHealth = maxHealth;

        Sword = sword;
        this.RubberRing = RubberRing;
        this.Lamp = Lamp;

        this.coins = coins;

    }
}
