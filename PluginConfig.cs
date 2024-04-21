using Rocket.API;


namespace DiscordAnnouncerInfo
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        public bool On_WebHook_Player_Connected;
        public string WebHook_Player_Connected;
        public bool On_WebHook_Player_Disconnected;
        public string WebHook_Player_Disconnected;
        public bool On_WebHook_ServerStart;
        public string WebHook_ServerStart;
        public bool On_WebHook_Shutdown;
        public string WebHook_Shutdown;
        public bool On_WebHook_PlayerDead;
        public string WebHook_PlayerDead;
        public bool On_WebHook_PlayerGesture;
        public string WebHook_PlayerGesture;
        public bool On_WebHook_PlayerRevive;
        public string WebHook_PlayerRevive;
        public bool On_WebHook_PlayerBrokeAndCuredLeg;
        public string WebHook_PlayerBrokeAndCuredLeg;
        public bool On_WebHook_OnPlayerUpdateBleeding;
        public string WebHook_OnPlayerUpdateBleeding;
        public bool On_WebHook_OnPlayerUpdateExperience;
        public string WebHook_OnPlayerUpdateExperience;
        //public bool On_WebHook_OnPlayerInventoryUpdated;
        //public string WebHook_OnPlayerInventoryUpdated;
        public bool On_WebHook_OnPlayerChatted;
        public string WebHook_OnPlayerChatted;

        public void LoadDefaults()
        {
            On_WebHook_Player_Connected = false;
            WebHook_Player_Connected = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_Player_Disconnected = false;
            WebHook_Player_Disconnected = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_ServerStart = false;
            WebHook_ServerStart = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_Shutdown = false;
            WebHook_Shutdown = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_PlayerDead = false;
            WebHook_PlayerDead = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_PlayerGesture = false;
            WebHook_PlayerGesture = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_PlayerRevive = false;
            WebHook_PlayerRevive = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_PlayerBrokeAndCuredLeg = false;
            WebHook_PlayerBrokeAndCuredLeg = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_OnPlayerUpdateBleeding = false;
            WebHook_OnPlayerUpdateBleeding = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_OnPlayerUpdateExperience = false;
            WebHook_OnPlayerUpdateExperience = "https://youtu.be/mQH_Q3SObfo - инструкция";
            //On_WebHook_OnPlayerInventoryUpdated = false;
            //WebHook_OnPlayerInventoryUpdated = "https://youtu.be/mQH_Q3SObfo - инструкция";
            On_WebHook_OnPlayerChatted = false;
            WebHook_OnPlayerChatted = "https://youtu.be/mQH_Q3SObfo - инструкция";

        }
    }
}