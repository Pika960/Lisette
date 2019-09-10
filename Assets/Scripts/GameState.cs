using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static Dictionary<string, int>                      m_playerInventory;
    private static Dictionary<string, Vector3>                  m_playerPositions;
    private static Dictionary<string, Quaternion>               m_playerRotations;
    private static Dictionary<string, Dictionary<string, bool>> m_triggeredEvents;

    static GameState()
    {
        m_playerInventory = new Dictionary<string, int>();
        m_playerPositions = new Dictionary<string, Vector3>();
        m_playerRotations = new Dictionary<string, Quaternion>();
        m_triggeredEvents = new Dictionary<string, Dictionary<string, bool>>();
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
}
