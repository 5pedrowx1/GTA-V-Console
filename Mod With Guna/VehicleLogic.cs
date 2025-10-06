using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;

namespace Mod_With_Guna
{
    public class VehicleLogic
    {
        private bool invincibleVehicleActive = false;
        private Vehicle trackedVehicle = null;

        private bool shouldRepair = false;
        private bool shouldToggleInvincible = false;
        private bool invincibleTargetState = false;

        public void ToggleInvincibleVehicle(bool enabled)
        {
            shouldToggleInvincible = true;
            invincibleTargetState = enabled;
        }

        public void RepairVehicle()
        {
            shouldRepair = true;
        }

        public void SetMaxSpeed(int speedMultiplier)
        {
            if (!Game.Player.Character.IsInVehicle())
            {
                return;
            }

            Vehicle vehicle = Game.Player.Character.CurrentVehicle;

            if (vehicle == null || !vehicle.Exists())
            {
                return;
            }

            speedMultiplier = Math.Max(10, Math.Min(500, speedMultiplier));
            float multiplier = speedMultiplier / 100f;

            try
            {
                float baseSpeed = 50f;
                vehicle.MaxSpeed = baseSpeed * multiplier;
                Function.Call((Hash)0x93A3996368C94158, vehicle.Handle, multiplier);
            }
            catch
            {
                // Ignora erros
            }
        }


        public void Update()
        {
            try
            {
                // Processa toggle de invencibilidade
                if (shouldToggleInvincible)
                {
                    shouldToggleInvincible = false;
                    ProcessToggleInvincible(invincibleTargetState);
                }

                // Processa reparo
                if (shouldRepair)
                {
                    shouldRepair = false;
                    ProcessRepair();
                }

                // Mantém veículo invencível
                if (invincibleVehicleActive && trackedVehicle != null && trackedVehicle.Exists())
                {
                    if (Game.Player.Character.IsInVehicle() &&
                        Game.Player.Character.CurrentVehicle == trackedVehicle)
                    {
                        trackedVehicle.Health = trackedVehicle.MaxHealth;
                        trackedVehicle.BodyHealth = 1000f;
                        trackedVehicle.EngineHealth = 1000f;
                    }
                    else
                    {
                        invincibleVehicleActive = false;
                        trackedVehicle = null;
                    }
                }
            }
            catch
            {
                invincibleVehicleActive = false;
                trackedVehicle = null;
            }
        }


        private void ProcessToggleInvincible(bool enabled)
        {
            if (!Game.Player.Character.IsInVehicle())
            {
                Notification.PostTicker("~r~Você precisa estar em um veículo!", true);
                invincibleVehicleActive = false;
                return;
            }

            Vehicle vehicle = Game.Player.Character.CurrentVehicle;

            if (vehicle == null || !vehicle.Exists())
            {
                Notification.PostTicker("~r~Veículo inválido!", true);
                invincibleVehicleActive = false;
                return;
            }

            invincibleVehicleActive = enabled;

            if (enabled)
            {
                trackedVehicle = vehicle;
                vehicle.IsInvincible = true;
                Function.Call(Hash.SET_ENTITY_INVINCIBLE, vehicle.Handle, true);
                Function.Call(Hash.SET_ENTITY_PROOFS, vehicle.Handle, true, true, true, true, true, true, true, true);
                Notification.PostTicker("~g~Veículo Indestrutível Ativado", true);
            }
            else
            {
                trackedVehicle = null;
                vehicle.IsInvincible = false;
                Function.Call(Hash.SET_ENTITY_INVINCIBLE, vehicle.Handle, false);
                Function.Call(Hash.SET_ENTITY_PROOFS, vehicle.Handle, false, false, false, false, false, false, false, false);
                Notification.PostTicker("~r~Veículo Indestrutível Desativado", true);
            }
        }


        private void ProcessRepair()
        {
            if (!Game.Player.Character.IsInVehicle())
            {
                Notification.PostTicker("~r~Você precisa estar em um veículo!", true);
                return;
            }

            Vehicle vehicle = Game.Player.Character.CurrentVehicle;

            if (vehicle == null || !vehicle.Exists())
            {
                Notification.PostTicker("~r~Veículo inválido!", true);
                return;
            }

            vehicle.Repair();
            vehicle.DirtLevel = 0f;
            vehicle.PetrolTankHealth = 1000f;
            Notification.PostTicker("~g~Veículo Reparado com Sucesso", true);
        }
    }
}