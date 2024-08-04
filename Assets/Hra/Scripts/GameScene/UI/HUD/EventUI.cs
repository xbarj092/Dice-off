using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<EventType, Sprite> _eventIcons = new();
    [SerializeField] private Image _eventImage;

    public void SetUpEvent(EventType eventType)
    {
        _eventImage.sprite = _eventIcons[eventType];
    }
}
