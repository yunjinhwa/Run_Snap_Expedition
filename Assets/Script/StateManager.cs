using JetBrains.Annotations;
using Unity.VisualScripting;

public sealed class StateManager
{
    private static readonly StateManager instance = new StateManager();
    public static StateManager Instance => instance;

    private int hp;
    private int score;
    private int totalDistance;

    public int HP => hp;
    public int Score => score;
    public int TotalDistance => totalDistance;

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

    /*------------------------------------------------------ Add methods ------------------------------------------------------*/
    public void AddHP(int amount)
    {
        hp += amount;
    }


    public void AddScore(int amount)
    {
        score += amount;
    }

    public void AddDistance(int amount)
    {
        totalDistance += amount;
    }


    /*------------------------------------------------------ Minus methods ------------------------------------------------------*/
    public void MinusHP(int amount)
    {
        hp -= amount;
    }

    public void MinusScore(int amount)
    {
        score -= amount;
    }

    public void MinusDistance(int amount)
    {
        totalDistance -= amount;
    }
}