using UnityEngine;

public class DiceEffectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _fallFX;

    public void PlayFallFX()
    {
        _fallFX.SetActive(true);
    }
}
