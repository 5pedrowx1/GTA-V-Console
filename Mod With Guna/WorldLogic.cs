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
                Notification.PostTicker("~g~Tempo Congelado", true);
            }
            else
            {
                Notification.PostTicker("~r~Tempo Descongelado", true);
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
                Notification.PostTicker($"~g~Clima Alterado", true);
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