using GTA;
using GTA.Native;
using GTA.UI;
using System.Linq;

namespace Mod_With_Guna
{
    public class ChaosLogic
    {
        private bool pedsCrazyActive = false;
        private bool totalChaosActive = false;

        public void TogglePedsCrazy(bool enabled)
        {
            pedsCrazyActive = enabled;

            if (enabled)
            {
                Function.Call(Hash.SET_RIOT_MODE_ENABLED, true);
                Notification.Show("~r~Peds Agressivos Ativado - CUIDADO!");
            }
            else
            {
                Function.Call(Hash.SET_RIOT_MODE_ENABLED, false);
                Notification.Show("~g~Peds Agressivos Desativado");
            }
        }

        public void ExplodeNearbyVehicles()
        {
            Vehicle[] nearbyVehicles = World.GetNearbyVehicles(Game.Player.Character.Position, 100f);
            int count = 0;

            foreach (Vehicle vehicle in nearbyVehicles)
            {
                if (vehicle != Game.Player.Character.CurrentVehicle && vehicle.Exists())
                {
                    vehicle.Explode();
                    count++;
                }
            }

            Notification.Show($"~r~{count} veículos explodidos!");
        }

        public void ToggleTotalChaos()
        {
            totalChaosActive = !totalChaosActive;

            if (totalChaosActive)
            {
                // Ativar modo caos
                Function.Call(Hash.SET_RIOT_MODE_ENABLED, true);
                Function.Call(Hash.SET_CREATE_RANDOM_COPS, false);

                // Fazer peds atacarem uns aos outros
                Ped[] nearbyPeds = World.GetNearbyPeds(Game.Player.Character.Position, 100f);
                foreach (Ped ped in nearbyPeds)
                {
                    if (ped != Game.Player.Character && ped.Exists())
                    {
                        Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);
                        Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 5, true);

                        // Dar armas aos peds
                        ped.Weapons.Give(WeaponHash.Pistol, 1000, true, true);

                        // Fazer ped atacar outro ped aleatório
                        if (nearbyPeds.Length > 1)
                        {
                            Ped target = nearbyPeds.OrderBy(x => System.Guid.NewGuid()).FirstOrDefault(x => x != ped);
                            if (target != null)
                            {
                                ped.Task.FightAgainst(target);
                            }
                        }
                    }
                }

                Notification.Show("~r~MODO CHAOS TOTAL ATIVADO!");
            }
            else
            {
                Function.Call(Hash.SET_RIOT_MODE_ENABLED, false);
                Function.Call(Hash.SET_CREATE_RANDOM_COPS, true);
                Notification.Show("~g~Modo Chaos Total Desativado");
            }
        }

        public void Update()
        {
            if (totalChaosActive)
            {
                // Manter o caos ativo
                Function.Call(Hash.SET_RIOT_MODE_ENABLED, true);
            }
        }
    }
}