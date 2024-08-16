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
            animator.SetBool("Move", true);
        }

        public void EndMoveAnime()
        {
            animator.SetBool("Move", false);
        }

        public void ChangeStunAnime()
        {
            bool StunState = animator.GetBool("Stun");
            animator.SetBool("Stun", !StunState);
        }

        public void AttackAnime(int Num)
        {
            Debug.Log("AttackType:" + Num);
            animator.SetInteger("AttackType", Num);
            animator.SetTrigger("Attack");
        }

        public void DamageAnime()
        {
            animator.SetTrigger("Damage");
        }

        public IEnumerator DieAnime()
        {
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