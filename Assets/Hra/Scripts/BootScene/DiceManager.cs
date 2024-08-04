using System.Collections.Generic;

public class DiceManager : MonoSingleton<DiceManager>
{
    private List<Dice> _dices = new();

    public void RegisterDice(Dice dice)
    {
        _dices.Add(dice);
    }

    public void UnregisterDice(Dice dice)
    {
        if (_dices.Contains(dice))
        {
            _dices.Remove(dice);
        }
    }

    public IEnumerable<Dice> GetAllDices()
    {
        return _dices;
    }
}
