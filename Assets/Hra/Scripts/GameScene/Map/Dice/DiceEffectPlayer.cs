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
        Material[] materials = new Material[_meshRenderer.materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = _nextRoundFallMat;
        }

        _meshRenderer.materials = materials;
    }
}
