using System;
using Upgrades;

namespace Core
{
  public class Player
  {
    private int _diamonds_balance = 0;
    private int _money_balance = 0;

    public int diamondsBalance
    {
      get => _diamonds_balance;
      set
      {
        _diamonds_balance = value;
        diamondBalanceUpdated?.Invoke(_diamonds_balance);
      }
    }

    public int moneyBalance
    {
      get => _money_balance;
      set
      {
        _money_balance = value;
        moneyBalanceUpdated?.Invoke(_money_balance);
      }
    }

    public PlayerUpgrades playerUpgrades { get; private set; } = new PlayerUpgrades();

    public event Action<int> diamondBalanceUpdated;
    public event Action<int> moneyBalanceUpdated;

    public bool hasHelper { get; private set; }


    public bool trySpendMoney(int amount)
    {
      if (amount > moneyBalance)
        return false;

      moneyBalance -= amount;
      return true;
    }

    public bool trySpendDiamonds(int amount)
    {
      if (amount > diamondsBalance)
        return false;

      diamondsBalance -= amount;
      return true;
    }
  }
}