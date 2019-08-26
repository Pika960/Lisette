using UnityEngine;

public static class GameState
{
    private static TextAsset m_dialogResource;
    private static Vector3   m_playerPosition;

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
}
