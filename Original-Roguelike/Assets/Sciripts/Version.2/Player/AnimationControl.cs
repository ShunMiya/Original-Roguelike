using UnityEngine;
using System.Collections;

namespace Anime
{
    public class AnimationControl : MonoBehaviour
    {

        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }


        public void StartMoveAnime()
        {
            animator.speed = GameRule.AnimeSpeed;
            animator.SetBool("Move", true);
        }

        public void EndMoveAnime()
        {
            animator.speed = GameRule.AnimeSpeed;
            animator.SetBool("Move", false);
        }

        public void ChangeStunAnime()
        {
            animator.speed = GameRule.AnimeSpeed;
            bool StunState = animator.GetBool("Stun");
            animator.SetBool("Stun", !StunState);
        }

        public void AttackAnime(int Num)
        {
            animator.speed = GameRule.AnimeSpeed;
            Debug.Log("AttackType:" + Num);
            animator.SetInteger("AttackType", Num);
            animator.SetTrigger("Attack");
        }

        public void DamageAnime()
        {
            animator.speed = GameRule.AnimeSpeed;
            animator.SetTrigger("Damage");
        }

        public IEnumerator DieAnime()
        {
            animator.speed = GameRule.AnimeSpeed;
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            animator.SetTrigger("Die");

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            while (!stateInfo.IsName("Die"))
            {
                yield return null;
                stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            }

            while (stateInfo.IsName("Die") && stateInfo.normalizedTime < 1.0f)
            {
                yield return null;
                stateInfo = animator.GetCurrentAnimatorStateInfo(0); 
            }
        }
    }

}