using GTA;
using GTA.Native;
using GTA.UI;

namespace Mod_With_Guna
{
    public class WeaponLogic
    {
        private bool infiniteAmmoActive = false;
        private bool noReloadActive = false;

        public void ToggleInfiniteAmmo(bool enabled)
        {
            infiniteAmmoActive = enabled;

            if (enabled)
            {
                Function.Call(Hash.SET_PED_INFINITE_AMMO_CLIP, Game.Player.Character, true);
                Notification.Show("~g~Munição Infinita Ativada");
            }
            else
            {
                Function.Call(Hash.SET_PED_INFINITE_AMMO_CLIP, Game.Player.Character, false);
                Notification.Show("~r~Munição Infinita Desativada");
            }
        }

        public void ToggleNoReload(bool enabled)
        {
            noReloadActive = enabled;

            if (enabled)
            {
                Notification.Show("~g~Sem Recarga Ativado");
            }
            else
            {
                Notification.Show("~r~Sem Recarga Desativado");
            }
        }

        public void GiveAllWeapons()
        {
            WeaponHash[] weapons = new WeaponHash[]
            {
                WeaponHash.Pistol,
                WeaponHash.CombatPistol,
                WeaponHash.APPistol,
                WeaponHash.SNSPistol,
                WeaponHash.HeavyPistol,
                WeaponHash.VintagePistol,
                WeaponHash.MarksmanPistol,
                WeaponHash.Revolver,
                WeaponHash.APPistol,
                WeaponHash.StunGun,
                WeaponHash.FlareGun,
                WeaponHash.MicroSMG,
                WeaponHash.MachinePistol,
                WeaponHash.SMG,
                WeaponHash.AssaultSMG,
                WeaponHash.CombatPDW,
                WeaponHash.MG,
                WeaponHash.CombatMG,
                WeaponHash.Gusenberg,
                WeaponHash.AssaultRifle,
                WeaponHash.CarbineRifle,
                WeaponHash.AdvancedRifle,
                WeaponHash.SpecialCarbine,
                WeaponHash.BullpupRifle,
                WeaponHash.CompactRifle,
                WeaponHash.SniperRifle,
                WeaponHash.HeavySniper,
                WeaponHash.MarksmanRifle,
                WeaponHash.PumpShotgun,
                WeaponHash.SawnOffShotgun,
                WeaponHash.BullpupShotgun,
                WeaponHash.AssaultShotgun,
                WeaponHash.Musket,
                WeaponHash.HeavyShotgun,
                WeaponHash.DoubleBarrelShotgun,
                WeaponHash.GrenadeLauncher,
                WeaponHash.RPG,
                WeaponHash.Minigun,
                WeaponHash.Firework,
                WeaponHash.Railgun,
                WeaponHash.HomingLauncher,
                WeaponHash.CompactGrenadeLauncher,
                WeaponHash.Grenade,
                WeaponHash.StickyBomb,
                WeaponHash.ProximityMine,
                WeaponHash.BZGas,
                WeaponHash.Molotov,
                WeaponHash.FireExtinguisher,
                WeaponHash.PetrolCan,
                WeaponHash.Knife,
                WeaponHash.Nightstick,
                WeaponHash.Hammer,
                WeaponHash.Bat,
                WeaponHash.Crowbar,
                WeaponHash.GolfClub,
                WeaponHash.Bottle,
                WeaponHash.Dagger,
                WeaponHash.Hatchet,
                WeaponHash.KnuckleDuster,
                WeaponHash.Machete,
                WeaponHash.Flashlight,
                WeaponHash.SwitchBlade,
                WeaponHash.BattleAxe
            };

            foreach (WeaponHash weapon in weapons)
            {
                Game.Player.Character.Weapons.Give(weapon, 9999, true, false);
            }

            Notification.Show("~g~Todas as Armas Adicionadas!");
        }

        public void Update()
        {
            if (infiniteAmmoActive)
            {
                Function.Call(Hash.SET_PED_INFINITE_AMMO, Game.Player.Character, true);
            }

            if (noReloadActive)
            {
                Weapon currentWeapon = Game.Player.Character.Weapons.Current;
                if (currentWeapon != null)
                {
                    currentWeapon.AmmoInClip = currentWeapon.MaxAmmoInClip;
                }
            }
        }
    }
}