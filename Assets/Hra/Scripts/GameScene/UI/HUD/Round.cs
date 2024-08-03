using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    [SerializeField] private Image _activeImage;

    public void SetRoundActive()
    {
        _activeImage.enabled = true;
    }
}
