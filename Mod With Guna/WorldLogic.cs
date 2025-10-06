using GTA;
using GTA.Native;
using GTA.UI;

namespace Mod_With_Guna
{
    public class WorldLogic
    {
        private bool freezeTimeActive = false;
        private int currentHour = 12;

        private readonly string[] weatherTypes = new string[]
        {
            "CLEAR",        // Ensolarado
            "EXTRASUNNY",   // Claro
            "CLOUDS",       // Nublado
            "RAIN",         // Chuvoso
            "THUNDER",      // Trovões
            "FOGGY",        // Neblina
            "XMAS"          // Neve
        };

        public void ToggleFreezeTime(bool enabled)
        {
            freezeTimeActive = enabled;

            if (enabled)
            {
                currentHour = World.CurrentTimeOfDay.Hours;
                Notification.Show("~g~Tempo Congelado");
            }
            else
            {
                Notification.Show("~r~Tempo Descongelado");
            }
        }

        public void SetTime(int hour)
        {
            currentHour = hour;
            World.CurrentTimeOfDay = new System.TimeSpan(hour, 0, 0);
        }

        public void SetWeather(int weatherIndex)
        {
            if (weatherIndex >= 0 && weatherIndex < weatherTypes.Length)
            {
                Function.Call(Hash.SET_WEATHER_TYPE_NOW_PERSIST, weatherTypes[weatherIndex]);
                Notification.Show($"~g~Clima Alterado");
            }
        }

        public void Update()
        {
            if (freezeTimeActive)
            {
                World.CurrentTimeOfDay = new System.TimeSpan(currentHour, 0, 0);
                Function.Call(Hash.PAUSE_CLOCK, true);
            }
            else
            {
                Function.Call(Hash.PAUSE_CLOCK, false);
            }
        }
    }
}