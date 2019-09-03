using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static Dictionary<string, Dictionary<string, bool>> m_triggeredEvents;
    private static TextAsset m_dialogResource;
    private static Vector3 m_playerPosition;

    static GameState()
    {
        m_triggeredEvents = new Dictionary<string, Dictionary<string, bool>>();
    }

    public static TextAsset DialogResource
    {
        get { return m_dialogResource; }
        set { m_dialogResource = value; }
    }

    public static Vector3 PlayerPosition
    {
        get { return m_playerPosition; }
        set { m_playerPosition = value; }
    }

    public static bool checkIfTriggered(string sceneName, string eventName)
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
}
