using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LowerRandomDicesValueStrategy : IEventStrategy
{
    private int _amount;

    public LowerRandomDicesValueStrategy(int amount)
    {
        _amount = amount;
    }

    public void Execute()
    {
        List<Dice> dices = DiceManager.Instance.GetAllDices().OrderBy(d => Random.value).Take(_amount).ToList();
        foreach (Dice dice in dices)
        {
            dice.DecreaseValue(1);
        }
    }
}
