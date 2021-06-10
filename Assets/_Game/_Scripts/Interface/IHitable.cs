using UnityEngine;

public interface IHitable
{
    void Hit(RaycastHit hit, int damage = 1);
}
