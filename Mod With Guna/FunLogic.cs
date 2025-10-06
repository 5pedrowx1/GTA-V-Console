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
                Notification.PostTicker("~b~Gravidade da Lua Ativada!", true);
            }
            else
            {
                Function.Call(Hash.SET_GRAVITY_LEVEL, 1); // 1 = gravidade normal
                Notification.PostTicker("~g~Gravidade Normal Restaurada", true);
            }
        }

        public void ToggleSlowMotion()
        {
            slowMotionActive = !slowMotionActive;

            if (slowMotionActive)
            {
                Game.TimeScale = 0.5f; // Metade da velocidade normal
                Notification.PostTicker("~b~Câmera Lenta Ativada!", true);
            }
            else
            {
                Game.TimeScale = 1.0f; // Velocidade normal
                Notification.PostTicker("~g~Velocidade Normal Restaurada", true);
            }
        }

        public void ActivateRagdoll()
        {
            if (Game.Player.Character.IsAlive)
            {
                Game.Player.Character.Ragdoll(5000); // 5 segundos de ragdoll
                Notification.PostTicker("~y~Modo Ragdoll Ativado!", true);
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