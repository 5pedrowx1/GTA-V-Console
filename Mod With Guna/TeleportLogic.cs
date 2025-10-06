using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;

namespace Mod_With_Guna
{
    public class TeleportLogic
    {
        private readonly Vector3[] popularLocations = new Vector3[]
        {
            new Vector3(-275.0f, -967.0f, 31.0f),      // Centro de Los Santos
            new Vector3(-1042.0f, -2746.0f, 21.0f),    // Aeroporto Internacional
            new Vector3(-1850.0f, -1248.0f, 8.0f),     // Pier de Vespucci
            new Vector3(501.0f, 5604.0f, 797.0f),      // Monte Chiliad
            new Vector3(1024.0f, 2671.0f, 40.0f),      // Vinewood Hills
            new Vector3(-388.0f, 6050.0f, 31.0f),      // Paleto Bay
            new Vector3(-2360.0f, 3249.0f, 32.0f),     // Fort Zancudo
            new Vector3(1681.0f, 3729.0f, 34.0f),      // Sandy Shores
            new Vector3(-1527.0f, -86.0f, 54.0f),      // Praia de Del Perro
            new Vector3(-75.0f, -818.0f, 326.0f)       // Maze Bank Tower
        };

        public void TeleportToLocation(int locationIndex)
        {
            if (locationIndex >= 0 && locationIndex < popularLocations.Length)
            {
                Vector3 destination = popularLocations[locationIndex];

                if (Game.Player.Character.IsInVehicle())
                {
                    Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                    vehicle.Position = destination;
                }
                else
                {
                    Game.Player.Character.Position = destination;
                }

                Notification.Show("~g~Teleportado com Sucesso!");
            }
        }

        public void TeleportToWaypoint()
        {
            // Verificar se existe um waypoint ativo
            if (!Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE))
            {
                Notification.Show("~r~Nenhum Waypoint Definido!");
                return;
            }

            // Obter coordenadas do waypoint
            Vector3 waypointPos = Function.Call<Vector3>(Hash.GET_BLIP_INFO_ID_COORD,
                Function.Call<int>(Hash.GET_FIRST_BLIP_INFO_ID, 8)); // 8 = waypoint blip

            // Obter a altura do terreno
            float groundZ = 0f;
            bool foundGround = false;

            // Requisitar colisão
            Function.Call(Hash.REQUEST_COLLISION_AT_COORD, waypointPos.X, waypointPos.Y, waypointPos.Z);
            Script.Wait(100);

            // Tentar obter altura do chão
            unsafe
            {
                float z;
                foundGround = Function.Call<bool>(Hash.GET_GROUND_Z_FOR_3D_COORD,
                    waypointPos.X, waypointPos.Y, 1000f, &z, false);
                if (foundGround)
                {
                    groundZ = z;
                }
                else
                {
                    groundZ = waypointPos.Z;
                }
            }

            Vector3 destination = new Vector3(waypointPos.X, waypointPos.Y, groundZ + 1.0f);

            if (Game.Player.Character.IsInVehicle())
            {
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                vehicle.Position = destination;
            }
            else
            {
                Game.Player.Character.Position = destination;
            }

            Notification.Show("~g~Teleportado para o Waypoint!");
        }

        public void TeleportVehicleToPlayer()
        {
            Vehicle lastVehicle = Game.Player.LastVehicle;

            if (lastVehicle == null || !lastVehicle.Exists())
            {
                Notification.Show("~r~Nenhum Veículo Encontrado!");
                return;
            }

            Vector3 playerPos = Game.Player.Character.Position;
            Vector3 spawnPos = playerPos + Game.Player.Character.ForwardVector * 5f;

            lastVehicle.Position = spawnPos;
            lastVehicle.PlaceOnGround();

            Notification.Show("~g~Veículo Teleportado!");
        }
    }
}