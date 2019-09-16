using System;
using System.Collections.Generic;
using System.Linq;

public class Enemy
{
    private string m_enemyName;
    private ushort m_enemyCurrentHealth;
    private ushort m_enemyMaxHealth;
    private Dictionary<ushort, Attack> m_enemyMoveset;
    private GameState.ElementType      m_enemyType;

    public Enemy(string name, GameState.ElementType type, ushort maxHealth)
    {
        m_enemyName          = name;
        m_enemyType          = type;
        m_enemyCurrentHealth = maxHealth;
        m_enemyMaxHealth     = maxHealth;
        m_enemyMoveset       = new Dictionary<ushort, Attack>();

        CreateRandomMoveset();
    }

    public void DecreaseHealth(ushort damage)
    {
        if (m_enemyCurrentHealth < damage)
        {
            m_enemyCurrentHealth = 0;
        }

        else
        {
            m_enemyCurrentHealth -= damage;
        }
    }

    public string GetName()
    {
        return m_enemyName;
    }

    public GameState.ElementType GetElementType()
    {
        return m_enemyType;
    }

    public ushort GetCurrentHealth()
    {
        return m_enemyCurrentHealth;
    }

    public ushort GetMaxHealth()
    {
        return m_enemyMaxHealth;
    }

    public Dictionary<ushort, Attack> GetMoveset()
    {
        return m_enemyMoveset;
    }

    private void CreateRandomMoveset()
    {
        Random          random        = new Random();
        HashSet<ushort> randomNumbers = new HashSet<ushort>();

        for (ushort i = 0; i < 4; i++)
        {
            while (!randomNumbers.Add((ushort)random.Next(1, 10))) ;
        }

        List<ushort> randomNumbersList = randomNumbers.ToList();

        m_enemyMoveset.Add(001, GameState.GetAttackListAll()[randomNumbersList[0]]);
        m_enemyMoveset.Add(002, GameState.GetAttackListAll()[randomNumbersList[1]]);
        m_enemyMoveset.Add(003, GameState.GetAttackListAll()[randomNumbersList[2]]);
        m_enemyMoveset.Add(004, GameState.GetAttackListAll()[randomNumbersList[3]]);
    }
}
