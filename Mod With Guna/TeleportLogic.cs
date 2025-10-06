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

        // Variáveis para controlar o teleporte assíncrono
        private bool shouldTeleport = false;
        private int teleportStage = 0;
        private Vector3 waypointPos;
        private int waitTimer = 0;

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

                Notification.PostTicker("~g~Teleportado com Sucesso!", true);
            }
        }

        // Método chamado pelo botão (não usa Wait)
        public void TeleportToWaypoint()
        {
            if (!Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE))
            {
                Notification.PostTicker("~r~Nenhum Waypoint Definido!", true);
                return;
            }

            // Ativa o processo de teleporte
            shouldTeleport = true;
            teleportStage = 0;
        }

        // Método chamado pelo OnTick do script principal
        public void Update()
        {
            if (!shouldTeleport) return;

            switch (teleportStage)
            {
                case 0: // Início - Obtém coordenadas e teleporta para cima
                    int blip = Function.Call<int>(Hash.GET_FIRST_BLIP_INFO_ID, 8);
                    waypointPos = Function.Call<Vector3>(Hash.GET_BLIP_COORDS, blip);

                    // Teleporta para cima primeiro
                    Vector3 tempPos = new Vector3(waypointPos.X, waypointPos.Y, 1000f);

                    if (Game.Player.Character.IsInVehicle())
                    {
                        Game.Player.Character.CurrentVehicle.Position = tempPos;
                    }
                    else
                    {
                        Game.Player.Character.Position = tempPos;
                    }

                    // Requisita colisão
                    Function.Call(Hash.REQUEST_COLLISION_AT_COORD, waypointPos.X, waypointPos.Y, waypointPos.Z);

                    // Define timer para esperar 1 segundo
                    waitTimer = Game.GameTime + 1000;
                    teleportStage = 1;
                    break;

                case 1: // Aguardando área carregar
                    if (Game.GameTime >= waitTimer)
                    {
                        teleportStage = 2;
                    }
                    break;

                case 2: // Teleporte final para o chão
                    float groundZ = 1000f;
                    OutputArgument outZ = new OutputArgument();

                    if (Function.Call<bool>(Hash.GET_GROUND_Z_FOR_3D_COORD, waypointPos.X, waypointPos.Y, 1000f, outZ, false))
                    {
                        groundZ = outZ.GetResult<float>();
                    }

                    Vector3 finalPos = new Vector3(waypointPos.X, waypointPos.Y, groundZ + 1.5f);

                    if (Game.Player.Character.IsInVehicle())
                    {
                        Game.Player.Character.CurrentVehicle.Position = finalPos;
                    }
                    else
                    {
                        Game.Player.Character.Position = finalPos;
                    }

                    Notification.PostTicker("~g~Teleportado para o Waypoint!", true);

                    // Reseta o processo
                    shouldTeleport = false;
                    teleportStage = 0;
                    break;
            }
        }

        public void TeleportVehicleToPlayer()
        {
            Vehicle lastVehicle = Game.Player.LastVehicle;

            if (lastVehicle == null || !lastVehicle.Exists())
            {
                Notification.PostTicker("~r~Nenhum Veículo Encontrado!", true);
                return;
            }

            Vector3 playerPos = Game.Player.Character.Position;
            Vector3 spawnPos = playerPos + Game.Player.Character.ForwardVector * 5f;

            lastVehicle.Position = spawnPos;
            lastVehicle.PlaceOnGround();

            Notification.PostTicker("~g~Veículo Teleportado!", true);
        }
    }
}