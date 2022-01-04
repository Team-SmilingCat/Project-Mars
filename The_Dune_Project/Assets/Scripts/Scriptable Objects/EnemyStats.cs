using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Create Stats/enemy stats")]
    public class EnemyStats : EntityStats
    {
        public int damage;
        public int moveSpeed;
    }
}