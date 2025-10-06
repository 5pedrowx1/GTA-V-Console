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
                Notification.Show("~g~God Mode Ativado");
            }
            else
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, Game.Player, false);
                Game.Player.Character.IsInvincible = false;
                Notification.Show("~r~God Mode Desativado");
            }
        }

        public void ToggleInfiniteStamina(bool enabled)
        {
            infiniteStaminaActive = enabled;

            if (enabled)
            {
                Function.Call(Hash.RESET_PLAYER_STAMINA, Game.Player);
                Notification.Show("~g~Stamina Infinita Ativada");
            }
            else
            {
                Notification.Show("~r~Stamina Infinita Desativada");
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
            Notification.Show("~g~Jogador Curado Completamente");
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