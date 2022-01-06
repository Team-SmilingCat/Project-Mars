using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Create Stats/player stats")]
    public class PlayerStats : EntityStats
    {
        [Header("player Stats")] 
        public int numBullets;
    }
}
