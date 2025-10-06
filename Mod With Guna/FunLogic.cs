using GTA;
using GTA.Native;
using GTA.UI;

namespace Mod_With_Guna
{
    public class FunLogic
    {
        private bool moonGravityActive = false;
        private bool slowMotionActive = false;

        public void ToggleMoonGravity()
        {
            moonGravityActive = !moonGravityActive;

            if (moonGravityActive)
            {
                Function.Call(Hash.SET_GRAVITY_LEVEL, 0); // 0 = gravidade da lua
                Notification.Show("~b~Gravidade da Lua Ativada!");
            }
            else
            {
                Function.Call(Hash.SET_GRAVITY_LEVEL, 1); // 1 = gravidade normal
                Notification.Show("~g~Gravidade Normal Restaurada");
            }
        }

        public void ToggleSlowMotion()
        {
            slowMotionActive = !slowMotionActive;

            if (slowMotionActive)
            {
                Game.TimeScale = 0.5f; // Metade da velocidade normal
                Notification.Show("~b~Câmera Lenta Ativada!");
            }
            else
            {
                Game.TimeScale = 1.0f; // Velocidade normal
                Notification.Show("~g~Velocidade Normal Restaurada");
            }
        }

        public void ActivateRagdoll()
        {
            if (Game.Player.Character.IsAlive)
            {
                Game.Player.Character.Ragdoll(5000); // 5 segundos de ragdoll
                Notification.Show("~y~Modo Ragdoll Ativado!");
            }
        }

        public void Update()
        {
            // Manter estados ativos se necessário
            if (moonGravityActive)
            {
                Function.Call(Hash.SET_GRAVITY_LEVEL, 0);
            }

            if (slowMotionActive)
            {
                Game.TimeScale = 0.5f;
            }
        }
    }
}