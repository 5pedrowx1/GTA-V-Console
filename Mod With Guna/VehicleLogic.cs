using GTA;
using GTA.Native;
using GTA.UI;

namespace Mod_With_Guna
{
    public class VehicleLogic
    {
        private bool invincibleVehicleActive = false;

        public void ToggleInvincibleVehicle(bool enabled)
        {
            invincibleVehicleActive = enabled;

            if (Game.Player.Character.IsInVehicle())
            {
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;

                if (enabled)
                {
                    vehicle.IsInvincible = true;
                    Function.Call(Hash.SET_ENTITY_INVINCIBLE, vehicle, true);
                    Function.Call(Hash.SET_ENTITY_PROOFS, vehicle, true, true, true, true, true, true, true, true);
                    Notification.Show("~g~Veículo Indestrutível Ativado");
                }
                else
                {
                    vehicle.IsInvincible = false;
                    Function.Call(Hash.SET_ENTITY_INVINCIBLE, vehicle, false);
                    Function.Call(Hash.SET_ENTITY_PROOFS, vehicle, false, false, false, false, false, false, false, false);
                    Notification.Show("~r~Veículo Indestrutível Desativado");
                }
            }
            else
            {
                Notification.Show("~r~Você precisa estar em um veículo!");
            }
        }

        public void RepairVehicle()
        {
            if (Game.Player.Character.IsInVehicle())
            {
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                vehicle.Repair();
                vehicle.DirtLevel = 0f;
                vehicle.PetrolTankHealth = 1000f;
                Notification.Show("~g~Veículo Reparado com Sucesso");
            }
            else
            {
                Notification.Show("~r~Você precisa estar em um veículo!");
            }
        }

        public void SetMaxSpeed(int speedMultiplier)
        {
            if (Game.Player.Character.IsInVehicle())
            {
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                float multiplier = speedMultiplier / 100f;

                // Modificar a velocidade máxima do veículo
                float baseSpeed = 50f; // Velocidade base padrão
                vehicle.MaxSpeed = baseSpeed * multiplier;

                // Também podemos usar o modificador de velocidade nativo
                Function.Call((Hash)0x93A3996368C94158, vehicle, multiplier); // _SET_VEHICLE_ENGINE_POWER_MULTIPLIER
            }
        }

        public void Update()
        {
            if (invincibleVehicleActive && Game.Player.Character.IsInVehicle())
            {
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                vehicle.Health = vehicle.MaxHealth;
                vehicle.BodyHealth = 1000f;
                vehicle.EngineHealth = 1000f;
            }
        }
    }
}