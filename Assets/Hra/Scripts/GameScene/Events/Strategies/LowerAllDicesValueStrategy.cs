public class LowerAllDicesValueStrategy : IEventStrategy
{
    public void Execute()
    {
        foreach (Dice dice in DiceManager.Instance.GetAllDices())
        {
            dice.DecreaseValue(1);
        }
    }
}
