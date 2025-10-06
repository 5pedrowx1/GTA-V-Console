using GTA;
using GTA.Math;using GTA.UI;

namespace Mod_With_Guna
{
    public class SpawnerLogic
    {
        public void SpawnVehicle(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                Notification.Show("~r~Digite o nome do veículo!");
                return;
            }

            Model vehicleModel = new Model(modelName);

            if (!vehicleModel.IsValid)
            {
                Notification.Show($"~r~Veículo '{modelName}' não encontrado!");
                return;
            }

            if (!vehicleModel.IsVehicle)
            {
                Notification.Show($"~r~'{modelName}' não é um veículo!");
                return;
            }

            vehicleModel.Request(500);

            if (!vehicleModel.IsLoaded)
            {
                Notification.Show("~r~Erro ao carregar o modelo do veículo!");
                return;
            }

            Vector3 spawnPosition = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5f;
            float heading = Game.Player.Character.Heading;

            Vehicle vehicle = World.CreateVehicle(vehicleModel, spawnPosition, heading);

            if (vehicle != null && vehicle.Exists())
            {
                vehicle.PlaceOnGround();
                vehicle.IsPersistent = true;
                Notification.Show($"~g~Veículo '{modelName}' criado!");
            }
            else
            {
                Notification.Show("~r~Erro ao criar o veículo!");
            }

            vehicleModel.MarkAsNoLongerNeeded();
        }

        public void SpawnPed(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                Notification.Show("~r~Digite o nome do ped!");
                return;
            }

            Model pedModel = new Model(modelName);

            if (!pedModel.IsValid)
            {
                Notification.Show($"~r~Ped '{modelName}' não encontrado!");
                return;
            }

            if (!pedModel.IsPed)
            {
                Notification.Show($"~r~'{modelName}' não é um ped!");
                return;
            }

            pedModel.Request(500);

            if (!pedModel.IsLoaded)
            {
                Notification.Show("~r~Erro ao carregar o modelo do ped!");
                return;
            }

            Vector3 spawnPosition = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 3f;

            Ped ped = World.CreatePed(pedModel, spawnPosition);

            if (ped != null && ped.Exists())
            {
                ped.IsPersistent = true;
                ped.MarkAsNoLongerNeeded();
                Notification.Show($"~g~Ped '{modelName}' criado!");
            }
            else
            {
                Notification.Show("~r~Erro ao criar o ped!");
            }

            pedModel.MarkAsNoLongerNeeded();
        }
    }
}