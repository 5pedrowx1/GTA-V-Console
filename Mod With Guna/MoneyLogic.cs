using GTA;
using GTA.Native;
using GTA.UI;
using System;

namespace Mod_With_Guna
{
    public class MoneyLogic
    {
        // Hashes das estatísticas de dinheiro para cada personagem
        private const string MICHAEL_CASH = "SP0_TOTAL_CASH";
        private const string FRANKLIN_CASH = "SP1_TOTAL_CASH";
        private const string TREVOR_CASH = "SP2_TOTAL_CASH";

        private string GetCurrentCharacterMoneyStatName()
        {
            // Detectar personagem atual
            Model playerModel = Game.Player.Character.Model;

            if (playerModel == new Model("player_zero")) // Michael
                return MICHAEL_CASH;
            else if (playerModel == new Model("player_one")) // Franklin
                return FRANKLIN_CASH;
            else if (playerModel == new Model("player_two")) // Trevor
                return TREVOR_CASH;

            return MICHAEL_CASH; // Padrão
        }

        public void AddMoney(int amount)
        {
            try
            {
                string statName = GetCurrentCharacterMoneyStatName();
                int currentMoney = GetCurrentMoney();
                int newMoney = currentMoney + amount;

                // Usar hash da stat
                int statHash = Game.GenerateHash(statName);

                // Definir novo valor
                Function.Call(Hash.STAT_SET_INT, statHash, newMoney, true);

                Notification.PostTicker($"~g~+${amount:N0}", true);
            }
            catch (Exception ex)
            {
                Notification.PostTicker($"~r~Erro: {ex.Message}", true);
            }
        }

        public void SetMoney(int amount)
        {
            try
            {
                string statName = GetCurrentCharacterMoneyStatName();
                int statHash = Game.GenerateHash(statName);

                Function.Call(Hash.STAT_SET_INT, statHash, amount, true);

                Notification.PostTicker($"~g~${amount:N0}", true);
            }
            catch (Exception ex)
            {
                Notification.PostTicker($"~r~Erro: {ex.Message}", true);
            }
        }

        public int GetCurrentMoney()
        {
            try
            {
                string statName = GetCurrentCharacterMoneyStatName();
                int statHash = Game.GenerateHash(statName);

                OutputArgument outArg = new OutputArgument();
                Function.Call(Hash.STAT_GET_INT, statHash, outArg, -1);

                return outArg.GetResult<int>();
            }
            catch
            {
                return 0;
            }
        }
    }
}