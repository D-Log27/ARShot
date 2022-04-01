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
}
