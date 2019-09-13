public class Attack
{
    private string m_attackName;
    private ushort m_attackDamage;
    private GameState.ElementType m_attackType;

    public Attack(string name, GameState.ElementType type, ushort damage)
    {
        m_attackName   = name;
        m_attackType   = type;
        m_attackDamage = damage;
    }

    public string GetAttackName()
    {
        return m_attackName;
    }

    public GameState.ElementType GetAttackType()
    {
        return m_attackType;
    }

    public ushort GetAttackDamage()
    {
        return m_attackDamage;
    }
}
