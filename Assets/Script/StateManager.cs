using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public sealed class StateManager : MonoBehaviour
{
    private static readonly StateManager instance = new StateManager();
    public static StateManager Instance => instance;

    private int hp;
    private int score;
    private int totalDistance;
    private int hitCount;

    public int HP => hp;
    public int Score => score;
    public int TotalDistance => totalDistance;
    public int HitCount => hitCount;

    private StateManager() { }

    public void Reset(int hp)
    {
        this.hp = hp;
        score = 0;
        totalDistance = 0;
    }

    public void SetHP(int value)
    {
        hp = value;
    }
    public void SetScore(int value) {
        score = value;
    }

    public void SetTotalDistance(int value) {
        totalDistance = value;
    }

    public void SetHitCount(int value) {
        hitCount = value;
    }

    /*------------------------------------------------------ Add methods ------------------------------------------------------*/
    public void AddHP(int amount)
    {
        hp += amount;
        Debug.Log("HP added: " + amount + ", Current HP: " + hp);
    }


    public void AddScore(int amount)
    {
        score += amount;
            Debug.Log("Score added: " + amount + ", Current Score: " + score);
    }

    public void AddDistance(int amount)
    {
        totalDistance += amount;
    }

    public void AddHitCount(int amount)
    {
        hitCount += amount;
    }

    /*------------------------------------------------------ Minus methods ------------------------------------------------------*/
    public void MinusHP(int amount)
    {
        hp -= amount;
        Debug.Log("HP lost: " + amount + ", Current HP: " + hp);
    }

    public void MinusScore(int amount)
    {
        score -= amount;
        Debug.Log("Score lost: " + amount + ", Current Score: " + score);
    }

    public void MinusDistance(int amount)
    {
        totalDistance -= amount;
    }
}