using UnityEngine;

public class DiceEffectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _fallFX;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _nextRoundFallMat;

    public void PlayFallFX()
    {
        _fallFX.SetActive(true);
    }

    public void PlayNextRoundFallFX()
    {
        _meshRenderer.material = _nextRoundFallMat;
    }
}
