using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static string m_playerName;
    private static ushort m_playerCurrentHealth;
    private static ushort m_playerMaxHealth;
    private static Enemy  m_currentEnemy;

    private static Dictionary<ushort, Attack>                   m_attackList;
    private static Dictionary<ushort, Attack>                   m_playerAttackList;
    private static Dictionary<string, int>                      m_playerInventory;
    private static Dictionary<string, Vector3>                  m_playerPositions;
    private static Dictionary<string, Quaternion>               m_playerRotations;
    private static Dictionary<string, Dictionary<string, bool>> m_triggeredEvents;

    public enum ElementType
    {
        Fire,
        Ice,
        Lightning,
        Earth,
        Wind,
        Water,
        Holy,
        Dark,
        Poison,
    };

    static GameState()
    {
        m_currentEnemy        = null;
        m_playerAttackList    = new Dictionary<ushort, Attack>();
        m_playerName          = "Lisette";
        m_playerCurrentHealth = 420;
        m_playerMaxHealth     = 420;

        InitAttackList();
        ResetGameState();

        m_playerAttackList.Add(001, m_attackList[002]);
        m_playerAttackList.Add(002, m_attackList[003]);
        m_playerAttackList.Add(003, m_attackList[005]);
        m_playerAttackList.Add(004, m_attackList[008]);
    }

    private static void InitAttackList()
    {
        m_attackList = new Dictionary<ushort, Attack>();

        m_attackList.Add(001, new Attack("Antigravity",   ElementType.Dark,      140));
        m_attackList.Add(002, new Attack("Aqua",          ElementType.Water,      80));
        m_attackList.Add(003, new Attack("Blizzard",      ElementType.Ice,       120));
        m_attackList.Add(004, new Attack("Fire",          ElementType.Fire,       80));
        m_attackList.Add(005, new Attack("Nimbus",        ElementType.Holy,      140));
        m_attackList.Add(006, new Attack("Poison Breath", ElementType.Poison,     80));
        m_attackList.Add(007, new Attack("Quake",         ElementType.Earth,      80));
        m_attackList.Add(008, new Attack("Thunder",       ElementType.Lightning, 120));
        m_attackList.Add(009, new Attack("Tornado",       ElementType.Wind,      100));
    }

    public static string GetPlayerName()
    {
        return m_playerName;
    }

    public static ushort GetPlayerCurrentHealth()
    {
        return m_playerCurrentHealth;
    }

    public static void DecreasePlayerHealth(ushort damage)
    {
        if (m_playerCurrentHealth < damage)
        {
            m_playerCurrentHealth = 0;
        }

        else
        {
            m_playerCurrentHealth -= damage;
        }
    }

    public static void RefreshPlayerHealth()
    {
        m_playerCurrentHealth = m_playerMaxHealth;
    }

    public static ushort GetPlayerMaxHealth()
    {
        return m_playerMaxHealth;
    }

    public static Enemy GetCurrentEnemy()
    {
        return m_currentEnemy;
    }

    public static void SetCurrentEnemy(Enemy enemy)
    {
        m_currentEnemy = enemy;
    }

    public static Dictionary<ushort, Attack> GetAttackListAll()
    {
        return m_attackList;
    }

    public static Dictionary<ushort, Attack> GetAttackListPlayer()
    {
        return m_playerAttackList;
    }

    public static Dictionary<string, int> GetInventory()
    {
        return m_playerInventory;
    }

    public static Vector3 GetPlayerPosition(string sceneName)
    {
        if (m_playerPositions.ContainsKey(sceneName))
        {
            return m_playerPositions[sceneName];
        }

        return Vector3.zero;
    }

    public static void SetPlayerPosition(string sceneName, Vector3 value)
    {
        if (m_playerPositions.ContainsKey(sceneName))
        {
            m_playerPositions[sceneName] = value;
        }

        else
        {
            m_playerPositions.Add(sceneName, value);
        }
    }

    public static Quaternion GetPlayerRotation(string sceneName)
    {
        if (m_playerRotations.ContainsKey(sceneName))
        {
            return m_playerRotations[sceneName];
        }

        return Quaternion.Euler(Vector3.up * 180);
    }

    public static void SetPlayerRotation(string sceneName, Quaternion value)
    {
        if (m_playerRotations.ContainsKey(sceneName))
        {
            m_playerRotations[sceneName] = value;
        }

        else
        {
            m_playerRotations.Add(sceneName, value);
        }
    }

    public static bool CheckIfTriggered(string sceneName, string eventName)
    {
        if (m_triggeredEvents.ContainsKey(sceneName))
        {
            Dictionary<string, bool> eventList = m_triggeredEvents[sceneName];

            if (eventList.ContainsKey(eventName))
            {
                return true;
            }

            else
            {
                eventList.Add(eventName, false);

                return false;
            }
        }

        else
        {
            Dictionary<string, bool> newDictionary = new Dictionary<string, bool>();
            newDictionary.Add(eventName, false);
            m_triggeredEvents.Add(sceneName, newDictionary);

            return false;
        }
    }

    public static bool ManageInventory(string item, int amount)
    {
        if (m_playerInventory.ContainsKey(item))
        {
            if ((m_playerInventory[item] += amount) > 0)
            {
                m_playerInventory[item] += amount;
                return true;
            }

            else if ((m_playerInventory[item] += amount) == 0)
            {
                m_playerInventory.Remove(item);
                return true;
            }

            else
            {
                return false;
            }
        }

        else
        {
            if (amount > 0)
            {
                m_playerInventory.Add(item, amount);
                return true;
            }

            else
            {
                return false;
            }
        }
    }

    public static void ResetGameState()
    {
        m_playerInventory = new Dictionary<string, int>();
        m_playerPositions = new Dictionary<string, Vector3>();
        m_playerRotations = new Dictionary<string, Quaternion>();
        m_triggeredEvents = new Dictionary<string, Dictionary<string, bool>>();
    }
}
