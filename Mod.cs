//using ColossalFramework.Steamworks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace WaterPipeUpgraderv2
{    
    public class Mod : LoadingExtensionBase, IUserMod, ILoadingExtension
    {
        private GameObject _gameObject; // Basisklasse für alles was mit dem MOD zu tun hat
        private LoadMode _loadMode; // Variable zum speichern des Loadmodes (New, Loaded, UnLoaded etc)
        public const string _version = "2.0.0"; // Variable zum speichern der Version des Mods
        
        // Die Beschreibung für den MOD
        public string Description
        {
            get
            {
                return "Upgrades all water pipes to heating pipes.";
            }
        }

        // Der Name des MODs, wird zum identifzieren benötigt
        public string Name
        {
            get
            {
                return ("Water Pipe Upgrader " + _version);
            }
        }

        public void OnEnabled()
        {
            Util.DebugPrint(this.Name + " enabled");

            if (!SteamHelper.IsDLCOwned(SteamHelper.DLC.SnowFallDLC)) //ist das benötigte DLC vorhanden?
            {
                // Falls NEIN eine Meldung im Debugfenster / Debuglog ausgeben und...
                Util.DebugPrint("Snowfall is not installed, aborting.");

                //... ein PopUp Fenster (Modal) über die Loadingmanager IntroLoaded Klasse aufrufen
                Singleton<LoadingManager>.instance.m_introLoaded += DisplayError;
            }
        }

        public override void OnCreated(ILoading loading)
        {
            Util.DebugPrint(this.Name + " Created", loading.loadingComplete.ToString());
        }

        public void OnDisabled()
        {
            Util.DebugPrint(this.Name + " OnDisabled");
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {                            
            this._loadMode = mode;
            
                if ((mode == LoadMode.LoadGame) || (mode == LoadMode.NewGame))
            {
                 UIView view = UnityEngine.Object.FindObjectOfType<UIView>();
                    if (view != null)
                    {
                        this._gameObject = new GameObject("WaterPipeUpgraderGui");
                            this._gameObject.transform.parent = view.transform;
                            this._gameObject.AddComponent<Gui>();                            
                            Util.DebugPrint(this.Name + " onLevelLoaded");
                    }
                }
            }
            catch (Exception ex)
            {                
                UnityEngine.Debug.LogException(ex);
            }
        }

        public override void OnLevelUnloading()
        {
            if (((this._loadMode == LoadMode.LoadGame) || (this._loadMode == LoadMode.NewGame)) && (this._gameObject != null))
            {
                try
                {
                        if (_gameObject != null)
                        GameObject.Destroy(_gameObject);
                        Util.DebugPrint(this.Name + " Undloaded");
                }
                catch (Exception e)
                {
                    Util.LogException(e);
                }
            }
        }

        private void DisplayError()
        {
            // Warnung anzeigen wenn das benötigte DLC (Snowfall) nicht installiert ist.
            UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                "Missing dependency",
                Name + " requires the SnowfallDLC. Please enable / buy this DLC if you wan´t to use the features of the mod!",
                false);
            return;
        }
    }
}

