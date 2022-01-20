using UnityEngine;
using System.Collections;
using Scriptable_Objects;

namespace Fight
{
    public class Fighter : MonoBehaviour
    {
        [Header("fighter stats info")]
        [SerializeField] protected EntityStats stats;
        [SerializeField] protected bool isHittable = true;
        [SerializeField] protected Animator animator;
        
        [SerializeField] private Transform curTarget; // TODO

        [HideInInspector] public int curHealth { get; protected set; }
        [HideInInspector] public bool inCombat { get; protected set; }

        private void Awake()
        {
            Init();
        }
        
        protected virtual void Init()
        {
            inCombat = false;
            isHittable = true;
            curHealth = stats.maxHealth;
            if (!animator)
            {
                animator = GetComponent<Animator>();
            }
        }
        
        public virtual void GainHealth(int healthBonus)
        {
            curHealth += healthBonus;
            Mathf.Clamp(curHealth, 0, stats.maxHealth);
        }

        public virtual void TakeDamage(int damage)
        {
            if (!isHittable)
                return;
            
            if (curHealth <= 0)
            {
                KillEntity();
                return;
            }

            inCombat = true;
            curHealth -= damage;
            Mathf.Clamp(curHealth, 0, stats.maxHealth);
            Debug.Log(name + " Health: " + curHealth);
            
            if (animator)
            {
                // TODO: animator.Play("hit");
            }

            StartCoroutine(Timer(5f));
        }

        protected virtual void KillEntity()
        {
            inCombat = false;
            isHittable = false; //also prevents repeatedly calling this func
            
            CharacterController charController = GetComponent<CharacterController>();
            if (charController)
            {
                charController.enabled = false;
            }
        }

        private IEnumerator Timer(float time)
        {
            yield return new WaitForSeconds(time);
            inCombat = false;
        }
    }
}