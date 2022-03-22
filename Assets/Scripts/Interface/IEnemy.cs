/// <summary>
/// Enemy Interface
/// </summary>
public interface IEnemy
{
    /// <summary>
    /// Enemy Trace VIP
    /// </summary>
    void Trace();
    /// <summary>
    /// Enemy Attack VIP/Player
    /// </summary>
    void Attack();
    /// <summary>
    /// Enemy damaged
    /// </summary>
    void Damaged();
    /// <summary>
    /// Enemy Death
    /// </summary>
    void Death();
}
