using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;

namespace Mod_With_Guna
{
    public class SpawnerLogic
    {
        // Sistema de stages para spawn assíncrono
        private bool shouldSpawnVehicle = false;
        private bool shouldSpawnPed = false;
        private string modelToSpawn = "";
        private int spawnStage = 0;
        private Model currentModel;
        private int waitTimer = 0;
        private int loadAttempts = 0;

        /// <summary>
        /// Chamado pelo botão - apenas seta flag
        /// </summary>
        public void SpawnVehicle(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                Notification.PostTicker("~r~Digite um nome de veículo!", true);
                return;
            }

            shouldSpawnVehicle = true;
            modelToSpawn = modelName.ToLower().Trim();
            spawnStage = 0;
            loadAttempts = 0;
        }

        /// <summary>
        /// Chamado pelo botão - apenas seta flag
        /// </summary>
        public void SpawnPed(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                Notification.PostTicker("~r~Digite um nome de ped!", true);
                return;
            }

            shouldSpawnPed = true;
            modelToSpawn = modelName.ToLower().Trim();
            spawnStage = 0;
            loadAttempts = 0;
        }

        /// <summary>
        /// Chamado pelo OnTick - processa spawns
        /// </summary>
        public void Update()
        {
            if (shouldSpawnVehicle)
            {
                ProcessVehicleSpawn();
            }

            if (shouldSpawnPed)
            {
                ProcessPedSpawn();
            }
        }

        private void ProcessVehicleSpawn()
        {
            try
            {
                switch (spawnStage)
                {
                    case 0: // Criar e validar modelo
                        // Tenta converter para hash primeiro
                        int modelHash;
                        if (int.TryParse(modelToSpawn, out modelHash))
                        {
                            currentModel = new Model(modelHash);
                        }
                        else
                        {
                            currentModel = new Model(modelToSpawn);
                        }

                        if (!currentModel.IsValid)
                        {
                            Notification.PostTicker($"~r~Modelo '{modelToSpawn}' inválido!", true);
                            shouldSpawnVehicle = false;
                            return;
                        }

                        if (!currentModel.IsVehicle)
                        {
                            Notification.PostTicker($"~r~'{modelToSpawn}' não é um veículo!", true);
                            shouldSpawnVehicle = false;
                            return;
                        }

                        spawnStage = 1;
                        break;

                    case 1: // Requisitar modelo
                        currentModel.Request();
                        waitTimer = Game.GameTime + 5000; // Timeout de 5 segundos
                        spawnStage = 2;
                        break;

                    case 2: // Aguardar carregamento
                        if (currentModel.IsLoaded)
                        {
                            spawnStage = 3;
                            return;
                        }

                        // Verifica timeout
                        if (Game.GameTime >= waitTimer)
                        {
                            loadAttempts++;
                            if (loadAttempts >= 3)
                            {
                                Notification.PostTicker($"~r~Timeout ao carregar '{modelToSpawn}'!", true);
                                currentModel.MarkAsNoLongerNeeded();
                                shouldSpawnVehicle = false;
                                spawnStage = 0;
                                return;
                            }
                            // Tenta requisitar novamente
                            spawnStage = 1;
                        }
                        break;

                    case 3: // Criar veículo
                        Vector3 spawnPos = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5f;
                        spawnPos.Z += 1f; // Levanta um pouco do chão

                        Vehicle vehicle = World.CreateVehicle(currentModel, spawnPos);

                        if (vehicle != null && vehicle.Exists())
                        {
                            vehicle.PlaceOnGround();
                            vehicle.Heading = Game.Player.Character.Heading;
                            Notification.PostTicker($"~g~Veículo '{modelToSpawn}' criado!", true);
                        }
                        else
                        {
                            Notification.PostTicker($"~r~Falha ao criar '{modelToSpawn}'!", true);
                        }

                        currentModel.MarkAsNoLongerNeeded();
                        shouldSpawnVehicle = false;
                        spawnStage = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                Notification.PostTicker($"~r~Erro: {ex.Message}", true);
                if (currentModel.IsValid)
                {
                    currentModel.MarkAsNoLongerNeeded();
                }
                shouldSpawnVehicle = false;
                spawnStage = 0;
            }
        }

        private void ProcessPedSpawn()
        {
            try
            {
                switch (spawnStage)
                {
                    case 0: // Criar e validar modelo
                        int modelHash;
                        if (int.TryParse(modelToSpawn, out modelHash))
                        {
                            currentModel = new Model(modelHash);
                        }
                        else
                        {
                            currentModel = new Model(modelToSpawn);
                        }

                        if (!currentModel.IsValid)
                        {
                            Notification.PostTicker($"~r~Modelo '{modelToSpawn}' inválido!", true);
                            shouldSpawnPed = false;
                            return;
                        }

                        if (!currentModel.IsPed)
                        {
                            Notification.PostTicker($"~r~'{modelToSpawn}' não é um ped!", true);
                            shouldSpawnPed = false;
                            return;
                        }

                        spawnStage = 1;
                        break;

                    case 1: // Requisitar modelo
                        currentModel.Request();
                        waitTimer = Game.GameTime + 5000;
                        spawnStage = 2;
                        break;

                    case 2: // Aguardar carregamento
                        if (currentModel.IsLoaded)
                        {
                            spawnStage = 3;
                            return;
                        }

                        if (Game.GameTime >= waitTimer)
                        {
                            loadAttempts++;
                            if (loadAttempts >= 3)
                            {
                                Notification.PostTicker($"~r~Timeout ao carregar '{modelToSpawn}'!", true);
                                currentModel.MarkAsNoLongerNeeded();
                                shouldSpawnPed = false;
                                spawnStage = 0;
                                return;
                            }
                            spawnStage = 1;
                        }
                        break;

                    case 3: // Criar ped
                        Vector3 spawnPos = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 3f;
                        Ped ped = World.CreatePed(currentModel, spawnPos);

                        if (ped != null && ped.Exists())
                        {
                            Notification.PostTicker($"~g~Ped '{modelToSpawn}' criado!", true);
                        }
                        else
                        {
                            Notification.PostTicker($"~r~Falha ao criar '{modelToSpawn}'!", true);
                        }

                        currentModel.MarkAsNoLongerNeeded();
                        shouldSpawnPed = false;
                        spawnStage = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                Notification.PostTicker($"~r~Erro: {ex.Message}", true);
                if (currentModel.IsValid)
                {
                    currentModel.MarkAsNoLongerNeeded();
                }
                shouldSpawnPed = false;
                spawnStage = 0;
            }
        }
    }
}