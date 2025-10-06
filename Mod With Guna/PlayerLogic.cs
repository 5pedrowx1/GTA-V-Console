using GTA;
using GTA.Native;
using GTA.UI;

namespace Mod_With_Guna
{
    public class PlayerLogic
    {
        private bool godModeActive = false;
        private bool infiniteStaminaActive = false;

        public void ToggleGodMode(bool enabled)
        {
            godModeActive = enabled;

            if (enabled)
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, Game.Player, true);
                Game.Player.Character.IsInvincible = true;
                Notification.PostTicker("~g~God Mode Ativado", true);
            }
            else
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, Game.Player, false);
                Game.Player.Character.IsInvincible = false;
                Notification.PostTicker("~r~God Mode Desativado", true);
            }
        }

        public void ToggleInfiniteStamina(bool enabled)
        {
            infiniteStaminaActive = enabled;

            if (enabled)
            {
                Function.Call(Hash.RESET_PLAYER_STAMINA, Game.Player);
                Notification.PostTicker("~g~Stamina Infinita Ativada", true);
            }
            else
            {
                Notification.PostTicker("~r~Stamina Infinita Desativada", true);
            }
        }

        public void SetHealth(int health)
        {
            if (Game.Player.Character.IsAlive)
            {
                Game.Player.Character.Health = health;
            }
        }

        public void HealPlayer()
        {
            Game.Player.Character.Health = Game.Player.Character.MaxHealth;
            Game.Player.Character.Armor = 100;
            Notification.PostTicker("~g~Jogador Curado Completamente", true);
        }

        public void Update()
        {
            if (infiniteStaminaActive)
            {
                Function.Call(Hash.RESET_PLAYER_STAMINA, Game.Player);
            }

            if (godModeActive)
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
            }
        }
    }
}