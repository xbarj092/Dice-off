using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PredefinedDiceFallStrategy : IEventStrategy
{
    private Dice _diceToDrop;

    public void Execute()
    {
        if (_diceToDrop != null)
        {
            DropDice(_diceToDrop);
        }
    }

    public void SelectRandomDiceForNextRound()
    {
        List<Dice> allDices = DiceManager.Instance.GetAllDices();
        List<GridNode> playerNodes = new();

        foreach (PlayerInput player in GameManager.Instance.Players)
        {
            if (player.GridNode != null)
            {
                playerNodes.Add(player.GridNode);
            }
        }

        List<Dice> availableDices = allDices.Where(dice => !playerNodes.Contains(dice.GridNode)).ToList();

        if (availableDices.Count > 0)
        {
            _diceToDrop = availableDices[Random.Range(0, availableDices.Count)];
            _diceToDrop.DiceEffectPlayer.PlayNextRoundFallFX();
        }
        else
        {
            Debug.LogWarning("No available dice to drop that are not under players.");
        }
    }

    private void DropDice(Dice dice)
    {
        dice.DecreaseValue(6);
    }
}
