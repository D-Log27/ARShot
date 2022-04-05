/// <summary>
/// Player Interface
/// </summary>
public interface IPlayer 
{
    /// <summary>
    /// player attack
    /// </summary>
    void Skill();

    /// <summary>
    /// Under attack
    /// </summary>
    void UnderAttack();
    /// <summary>
    /// Kill enemy
    /// </summary>
    void Kill();
    /// <summary>
    /// Player Dead
    /// </summary>
    void Death();

    /// <summary>
    /// Heal player
    /// </summary>
    /// <param name="point">Healing ammount. default is 10</param>
    void Heal(int point = 10);
}
