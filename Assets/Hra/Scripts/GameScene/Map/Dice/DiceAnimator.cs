using UnityEngine;

public class DiceAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAnimation(DiceAnimations animation)
    {
        _animator.StopPlayback();
        _animator.Play(animation.ToString());
    }
}
