using UnityEngine;

namespace CoreGameplay.Hero
{
    public class HeroAnimationController
    {
        #region const

        private const string RUN_STATE = "Run";

        #endregion

        #region fields

        private readonly Animator _animator;
        private readonly float _heroAnimationSpeed;
        private static readonly int IsFly = Animator.StringToHash("IsFly");
        private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");

        #endregion


        #region constructor

        public HeroAnimationController(Animator animator, float heroAnimationSpeed)
        {
            _animator = animator;
            _heroAnimationSpeed = heroAnimationSpeed;
        }

        #endregion

        #region public methods

        public void SetSpeed(float value)
        {
            _animator.speed = value * _heroAnimationSpeed;
        }

        public void Fly(bool value)
        {
            _animator.SetBool(IsFly, value);
        }

        public void OnJump()
        {
            _animator.SetTrigger(JumpTrigger);
        }

        public void Reset()
        {
            _animator.Play(RUN_STATE);
            _animator.speed = 1f;
        }

        #endregion
    }
}