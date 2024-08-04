using System;
using System.Collections;
using UnityEngine;

public class TutorialShopAction : TutorialAction
{
    [Header("TextPositions")]
    [SerializeField] private Transform _defaultTransform;
    [SerializeField] private Transform _shopClickTransform;
    [SerializeField] private Transform _weaponClickTransform;
    [SerializeField] private Transform _elementClickTransform;
    [SerializeField] private Transform _elementSlotClickTransform;
    [SerializeField] private Transform _afterPurchaseTransform;
    [SerializeField] private Transform _loadoutClickTransform;
    [SerializeField] private Transform _elementLoadoutClickTransform;
    [SerializeField] private Transform _statsLoadoutClickTransform;
    [SerializeField] private Transform _postLoadoutTransform;

    public override void StartAction()
    {
        _tutorialPlayer.SetTextPosition(_shopClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        ScreenEvents.OnGameScreenOpened += OnScreenOpened1;
    }

    private void OnScreenOpened1(GameScreenType type)
    {
        if (type == GameScreenType.Upgrades)
        {
            OnShopOpened();
        }
    }

    private void OnShopOpened()
    {
        ScreenEvents.OnGameScreenOpened -= OnScreenOpened1;
    }

    public override void Exit()
    {
    }
}
