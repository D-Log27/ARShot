/// <summary>
/// Player Gun Interface
/// </summary>
public interface IPlayerGun : IPlayer
{
    /// <summary>
    /// Gun Reload
    /// </summary>
    void Reload();

    /// <summary>
    /// player attack
    /// </summary>
    void Attack();
}
