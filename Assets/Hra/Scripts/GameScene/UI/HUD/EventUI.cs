using AYellowpaper.SerializedCollections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<EventType, Sprite> _eventIcons = new();
    [SerializeField] private Image _eventImage;
    [SerializeField] private TMP_Text _eventCount;

    public void UpdateEventCountText(int eventCount)
    {
        _eventCount.text = eventCount.ToString();
    }

    public void SetUpEvent(EventType eventType)
    {
        _eventImage.sprite = _eventIcons[eventType];
    }

    public void DisableText()
    {
        _eventCount.gameObject.SetActive(false);
    }
}
