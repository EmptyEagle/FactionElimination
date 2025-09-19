using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;
using InControl;
using CharmChanger;
using HuntMod = TheHuntIsOn.TheHuntIsOn;
using System.Linq;
using ItemChanger;
using ItemChanger.Extensions;
using TheHuntIsOn;
using System.Linq.Expressions;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace FactionElimination
{
    public class FactionElimination : Mod
    {
        public FactionElimination() : base("Faction Elimination") { }
        public override string GetVersion() => "0.1.2.0";
        public override void Initialize()
        {
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            ModHooks.BeforeAddHealthHook += BeforeAddHealth;
            ModHooks.SetPlayerIntHook += SetPlayerIntHook;
            ModHooks.SetPlayerBoolHook += SetPlayerBoolHook;
            On.HeroController.AddGeo += GeoProgression;
            ModHooks.CharmUpdateHook += CheckTwister;
            ModHooks.NewGameHook += NewGameHook;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += TransitionBlocker;
            FactionModeMenu.Register();
        }


        // Mr. Fatlife's overheal ability
        bool lifebloodOverheal;
        // Esteemed Scholar's decrease in NArt charge time or Spell Twister cost when collecting 3 NArts or 3 Spells
        bool scholarAbility;
        // The Gorgeous' geo Perks never increase in price
        bool gorgeousGeoDiscount;
        // When Ms. De Light eliminates another Team before Sudden Death, that Team becomes Infected and cannot participate in Sudden Death
        // When Ms. De Light enters Sudden Death, the Max Health of all players on the Team is lowered by 2
        bool delightAbilities;

        // Currently resets when reopening the game. Persists through save and quitting savefile though.
        public int perkPoints;

        // dirtmouthcrossroads | dirtmouthpeak | edgehive | spiritsdescent
        private string gameMap;

        int geoProgress;
        int curGeoGoal = 500;

        int grubsProgress;

        int normalSpell;

        bool healthSetup;


        private void NewGameHook()
        {
            Log("New Game started.");
            healthSetup = false;
            //ItemChangerMod.CreateSettingsProfile(overwrite: false);
            PlayerData.instance.hasDash = FactionModeMenu.playerEquipment[0];
            PlayerData.instance.canDash = FactionModeMenu.playerEquipment[0];
            PlayerData.instance.hasWalljump = FactionModeMenu.playerEquipment[1];
            PlayerData.instance.canWallJump = FactionModeMenu.playerEquipment[1];
            PlayerData.instance.hasSuperDash = FactionModeMenu.playerEquipment[2];
            PlayerData.instance.canSuperDash = FactionModeMenu.playerEquipment[2];
            PlayerData.instance.hasDoubleJump = FactionModeMenu.playerEquipment[3];
            PlayerData.instance.hasAcidArmour = FactionModeMenu.playerEquipment[4];
            PlayerData.instance.hasShadowDash = FactionModeMenu.playerEquipment[5];
            PlayerData.instance.canShadowDash = FactionModeMenu.playerEquipment[5];
            PlayerData.instance.hasLantern = FactionModeMenu.playerEquipment[6];
            PlayerData.instance.hasKingsBrand = FactionModeMenu.playerEquipment[7];
            PlayerData.instance.hasTramPass = FactionModeMenu.playerEquipment[8];
            PlayerData.instance.hasDreamNail = FactionModeMenu.playerEquipment[9];
            PlayerData.instance.hasDreamGate = FactionModeMenu.playerEquipment[10];
            PlayerData.instance.MPReserveMax = FactionModeMenu.playerStats[1] * 33;
            if (PlayerData.instance.MPReserveMax > 99)
                PlayerData.instance.MPReserveMax = 99;
            PlayerData.instance.nailDamage = FactionModeMenu.playerStats[2];
            if (FactionModeMenu.playerStats[2] == 13)
                PlayerData.instance.nailSmithUpgrades = 2;
            if (FactionModeMenu.playerStats[2] == 21)
                PlayerData.instance.nailSmithUpgrades = 4;
            PlayerData.instance.dreamOrbs = FactionModeMenu.playerStats[3];
            PlayerData.instance.charmSlots = FactionModeMenu.playerStats[4];
            PlayerData.instance.geo = FactionModeMenu.playerStats[5];
            PlayerData.instance.hasCharm = true;
            for (int i = 0; i < 40; i++)
            {
                PlayerData.instance.SetBoolInternal("gotCharm_" + (i + 1), FactionModeMenu.playerCharms[i]);
            }
            PlayerData.instance.fragileHealth_unbreakable = true;
            PlayerData.instance.fragileGreed_unbreakable = true;
            PlayerData.instance.fragileStrength_unbreakable = true;
            PlayerData.instance.grimmChildLevel = 4;
            if (FactionModeMenu.playerCharms[40])
            {
                PlayerData.instance.SetBoolInternal("gotCharm_40", true);
                PlayerData.instance.grimmChildLevel = 5;
            }
            PlayerData.instance.hasNailArt = true;
            PlayerData.instance.hasCyclone = FactionModeMenu.playerNArts[0];
            PlayerData.instance.hasDashSlash = FactionModeMenu.playerNArts[1];
            PlayerData.instance.hasUpwardSlash = FactionModeMenu.playerNArts[2];
            if (FactionModeMenu.playerNArts[0] && FactionModeMenu.playerNArts[1] && FactionModeMenu.playerNArts[2])
                PlayerData.instance.hasAllNailArts = true;
            PlayerData.instance.hasSpell = true;
            PlayerData.instance.fireballLevel = FactionModeMenu.playerSpells[0];
            PlayerData.instance.quakeLevel = FactionModeMenu.playerSpells[1];
            PlayerData.instance.screamLevel = FactionModeMenu.playerSpells[2];
            CharmChangerMod.LS.furyOfTheFallenHealth = FactionModeMenu.charmChanger1[0];
            CharmChangerMod.LS.regularFocusHealing = FactionModeMenu.charmChanger1[1];
            CharmChangerMod.LS.deepFocusHealing = FactionModeMenu.charmChanger1[2];
            CharmChangerMod.LS.deepFocusHealingTimeScale = FactionModeMenu.charmChanger1[3];
            CharmChangerMod.LS.glowingWombSpawnCost = FactionModeMenu.charmChanger1[4];
            CharmChangerMod.LS.spellTwisterSpellCost = FactionModeMenu.charmChanger1[5];
            CharmChangerMod.LS.stalwartShellInvulnerability = FactionModeMenu.charmChanger2[0];
            CharmChangerMod.LS.glowingWombSpawnRate = FactionModeMenu.charmChanger2[1];
            CharmChangerMod.LS.shapeOfUnnSpeed = FactionModeMenu.charmChanger2[2];
            CharmChangerMod.LS.shapeOfUnnQuickFocusSpeed = FactionModeMenu.charmChanger2[3];
            CharmChangerMod.LS.sprintmasterSpeed = FactionModeMenu.charmChanger2[4];
            CharmChangerMod.LS.sprintmasterSpeedCombo = FactionModeMenu.charmChanger2[5];
            CharmChangerMod.LS.nailmastersGloryChargeTime = FactionModeMenu.charmChanger2[6];
            HuntMod.SaveData.FocusSpeed = FactionModeMenu.focusSpeed;
            normalSpell = 33;
            HuntMod.SaveData.IsHunter = true;
            HuntMod.SaveData.AffectionTable["BenchModule"] = ModuleAffection.OnlyHunter;
            HuntMod.SaveData.AffectionTable["ElevatorModule"] = ModuleAffection.OnlyHunter;
            HuntMod.SaveData.AffectionTable["ShadeModule"] = ModuleAffection.OnlyHunter;
            HuntMod.SaveData.AffectionTable["SpaModule"] = ModuleAffection.OnlyHunter;
            lifebloodOverheal = FactionModeMenu.commanderAbilities[0];
            scholarAbility = FactionModeMenu.commanderAbilities[1];
            gorgeousGeoDiscount = FactionModeMenu.commanderAbilities[2];
            delightAbilities = FactionModeMenu.commanderAbilities[3];

            // Giving full map
            PlayerData.instance.hasMap = true;
            PlayerData.instance.hasQuill = true;
            PlayerData.instance.mapAbyss = true;
            PlayerData.instance.mapCity = true;
            PlayerData.instance.mapCliffs = true;
            PlayerData.instance.mapCrossroads = true;
            PlayerData.instance.mapDeepnest = true;
            PlayerData.instance.mapDirtmouth = true;
            PlayerData.instance.mapFogCanyon = true;
            PlayerData.instance.mapFungalWastes = true;
            PlayerData.instance.mapGreenpath = true;
            PlayerData.instance.mapMines = true;
            PlayerData.instance.mapOutskirts = true;
            PlayerData.instance.mapRestingGrounds = true;
            PlayerData.instance.mapRoyalGardens = true;
            PlayerData.instance.mapWaterways = true;
            PlayerData.instance.mapAllRooms = true;
            gameMap = FactionModeMenu.playableArea;

            // Setting up Bretta and elevators/shortcuts
            PlayerData.instance.brettaRescued = true;
            PlayerData.instance.mineLiftOpened = true;
            PlayerData.instance.outskirtsWall = true;

            // Setting up Map Specific changes
            if (gameMap != "spiritsdescent")
                PlayerData.instance.cityLift1 = true;
            if (gameMap == "dirtmouthcrossroads")
                PlayerData.instance.hasTramPass = false;
            if (gameMap == "edgehive")
            {
                PlayerData.instance.hasLoveKey = true;
                PlayerData.instance.hasTramPass = false;
            } 
            if (gameMap == "spiritsdescent")
                PlayerData.instance.hasWhiteKey = true;

            if (scholarAbility)
            {
                if (PlayerData.instance.hasAllNailArts)
                {
                    Log("Scholar NArt ability activated!");
                    CharmChangerMod.LS.nailmastersGloryChargeTime = 0.5f;
                }
                if (PlayerData.instance.fireballLevel > 0 && PlayerData.instance.quakeLevel > 0 && PlayerData.instance.screamLevel > 0)
                {
                    Log("Scholar Spell ability activated!");
                    CharmChangerMod.LS.spellTwisterSpellCost = 12;
                }
            }
        }

        public void OnHeroUpdate()
        {
            while (PlayerData.instance.maxHealth < FactionModeMenu.playerStats[0] && !healthSetup)
            {
                HeroController.instance.MaxHealth();
                HeroController.instance.AddToMaxHealth(1);
                PlayMakerFSM.BroadcastEvent("MAX HP UP");
                if (PlayerData.instance.maxHealth == FactionModeMenu.playerStats[0])
                    healthSetup = true;
                if (PlayerData.instance.maxHealth >= 9)
                    break;
            }
        }
        
        private int BeforeAddHealth(int amount)
        {
            if (lifebloodOverheal)
            {
                // First lifeblood mask if healing would send player over max health
                if (((PlayerData.instance.health + 1 > PlayerData.instance.maxHealth && !PlayerData.instance.GetBool("equippedCharm_34")) || (PlayerData.instance.health + amount > PlayerData.instance.maxHealth && PlayerData.instance.GetBool("equippedCharm_34"))) && PlayerData.instance.healthBlue + 1 <= 4)
                {
                    Log("Overhealing!");
                    EventRegister.SendEvent("ADD BLUE HEALTH");
                    // Second lifeblood mask for Deep Focus
                    if (PlayerData.instance.health == PlayerData.instance.maxHealth && PlayerData.instance.GetBool("equippedCharm_34") && PlayerData.instance.healthBlue + 1 <= 4)
                    {
                        EventRegister.SendEvent("ADD BLUE HEALTH");
                    }
                }
            }
            return amount;
        }

        private int SetPlayerIntHook(string name, int orig)
        {
            //Log(name);
            
            // Gives complete Mask or Soul Vessel when Mask Shard or Vessel Fragment is collected
            if (name == "heartPieces" && ((PlayerData.instance.maxHealth < 9 && !PlayerData.instance.GetBool("equippedCharm_23")) || (PlayerData.instance.maxHealth < 11 && PlayerData.instance.GetBool("equippedCharm_23"))))
            {
                Log("Mask Shard -> Mask");
                HeroController.instance.MaxHealth();
                HeroController.instance.AddToMaxHealth(1);
                PlayMakerFSM.BroadcastEvent("MAX HP UP");
            }
            if (name == "vesselFragments" && PlayerData.instance.MPReserveMax < 99)
            {
                Log("Vessel Fragment -> Soul Vessel");
                HeroController.instance.AddToMaxMPReserve(33);
                PlayMakerFSM.BroadcastEvent("NEW SOUL ORB");
            }

            // Mask and Soul Vessels are capped at 9 and 3 respectively
            if (name == "health")
            {
                if (PlayerData.instance.maxHealth > 9 && !PlayerData.instance.GetBool("equippedCharm_23"))
                {
                    PlayerData.instance.maxHealth = 9;
                    PlayerData.instance.maxHealthBase = 9;
                    PlayerData.instance.health = 9;
                    if (!GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
                        GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                    else
                    {
                        GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                        GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                    }
                }
                // Fragile/Unbreakable Heart Exception (9 -> 11 Mask maximum)
                else if (PlayerData.instance.maxHealth > 11 && PlayerData.instance.GetBool("equippedCharm_23"))
                {
                    PlayerData.instance.maxHealth = 11;
                    PlayerData.instance.maxHealthBase = 11;
                    PlayerData.instance.health = 11;
                    if (!GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
                        GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                    else
                    {
                        GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                        GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                    }
                }
            }
            if ((name == "MPReserve" || name == "MPCharge") && PlayerData.instance.MPReserveMax > 99)
            {
                PlayerData.instance.MPReserveMax = 99;
                if (!GameCameras.instance.hudCanvas.gameObject.activeInHierarchy)
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                else
                {
                    GameCameras.instance.hudCanvas.gameObject.SetActive(false);
                    GameCameras.instance.hudCanvas.gameObject.SetActive(true);
                }
            }

            // Esteemed Scholar effect when 3 NArts or 3 Spells have been obtained
            if (name == "profileID" && scholarAbility)
            {
                if (PlayerData.instance.hasAllNailArts)
                {
                    Log("Scholar NArt ability activated!");
                    CharmChangerMod.LS.nailmastersGloryChargeTime = 0.5f;
                }
                if (PlayerData.instance.fireballLevel > 0 && PlayerData.instance.quakeLevel > 0 && PlayerData.instance.screamLevel > 0)
                {
                    Log("Scholar Spell ability activated!");
                    CharmChangerMod.LS.spellTwisterSpellCost = 12;
                }
            }

            if (name == "grubsCollected")
            {
                grubsProgress++;
                Log("Grub Perk: "+grubsProgress+"/3");
                if (grubsProgress == 3)
                {
                    perkPoints++;
                    Log("3 grubs collected! 1 Perk awarded.");
                    Log("You now have " + perkPoints + " Perks.");
                    grubsProgress = 0;
                }
            }

            if (name == "ore")
            {
                perkPoints++;
                Log("1 Pale Ore collected! 1 Perk awarded.");
                Log("You now have " + perkPoints + " Perks.");
            }
            
            return orig;
        }
        private void GeoProgression(On.HeroController.orig_AddGeo orig, HeroController self, int amount)
        {
            geoProgress += amount;
            Log("Geo Perk: " + geoProgress + "/" + curGeoGoal);
            if (geoProgress >= curGeoGoal)
            {
                Log("Geo goal of " + curGeoGoal + " reached! 1 Perk awarded.");
                perkPoints++;
                if (!gorgeousGeoDiscount)
                {
                    curGeoGoal += 250;
                    Log("Geo goal increased to " + curGeoGoal + ".");
                }
                geoProgress = 0;
                Log("You now have " + perkPoints + " Perks.");
            }

            PlayerData.instance.AddGeo(amount);
            self.AddGeoToCounter(amount);
        }

        private void TransitionBlocker(Scene sc1, Scene sc2)
        {
            string roomEntered = sc2.name;
            string roomExited = sc1.name;
            Log("Room entered: "+roomEntered);
            TransitionPoint[] transitionList = UnityEngine.Object.FindObjectsOfType<TransitionPoint>();
            GameObject blocker1;
            GameObject blocker2;
            GameObject blocker3;
            foreach (TransitionPoint transitionPoint in transitionList)
                Log(transitionPoint);
            if (roomExited == "Ruins1_24")
                PlayerData.instance.hasDreamNail = true;
            switch (roomEntered)
            {
                case "Town":
                    Log(GameObject.Find("BlockTownLeft") == null);
                    Log(GameObject.Find("BlockTownWell") == null);
                    Log(GameObject.Find("BlockTownRight") == null);
                    blocker1 = new GameObject("BlockTownLeft");
                    blocker1.transform.Translate(2, 48.5f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 11, 0);
                    blocker2 = new GameObject("BlockTownWell");
                    blocker2.transform.Translate(185, 4, 0);
                    blocker2.transform.localScale = new Vector3(4, 0.5f, 0);
                    blocker3 = new GameObject("BlockTownRight");
                    blocker3.transform.Translate(263, 51.5f, 0);
                    blocker3.transform.localScale = new Vector3(0.5f, 9, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            blocker3.AddComponent<BoxCollider2D>();
                            break;
                        case "dirtmouthpeak":
                            blocker1.AddComponent<BoxCollider2D>();
                            blocker2.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins2_07":
                    Log(GameObject.Find("BlockBrokenKingsLeft") == null);
                    blocker1 = new GameObject("BlockBrokenKingsLeft");
                    blocker1.transform.Translate(0, 6.7f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "edgehive":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Deepnest_East_09":
                    Log(GameObject.Find("BlockColoPathLeft") == null);
                    blocker1 = new GameObject("BlockColoPathLeft");
                    blocker1.transform.Translate(0, 13.2f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 7, 0);

                    switch (gameMap)
                    {
                        case "edgehive":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Waterways_14":
                    Log(GameObject.Find("BlockTopIsmasLeft") == null);
                    blocker1 = new GameObject("BlockTopIsmasLeft");
                    blocker1.transform.Translate(8, 1.5f, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "edgehive":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Deepnest_East_02":
                    try
                    {
                        if (GameObject.Find("One Way Wall") != null)
                        {
                            Log("Breakable found!");
                            UnityEngine.Object.Destroy(GameObject.Find("One Way Wall"));
                        }
                    }
                    catch
                    {
                        Log("Failed to destroy breakable!");
                    }
                    break;
                case "Crossroads_11_alt":
                    Log(GameObject.Find("BlockCrossroadsGreenpathLeft") == null);
                    blocker1 = new GameObject("BlockCrossroadsGreenpathLeft");
                    blocker1.transform.Translate(0, 18.7f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);
                    
                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_35":
                    Log(GameObject.Find("BlockCrossroadsCanyonBottom") == null);
                    blocker1 = new GameObject("BlockCrossroadsCanyonBottom");
                    blocker1.transform.Translate(64, 1, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_18":
                    Log(GameObject.Find("BlockCrossroadsWastesBottom") == null);
                    blocker1 = new GameObject("BlockCrossroadsWastesBottom");
                    blocker1.transform.Translate(21, 1, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_45":
                    Log(GameObject.Find("BlockCrossroadsPeak1Right") == null);
                    blocker1 = new GameObject("BlockCrossroadsPeak1Right");
                    blocker1.transform.Translate(69.75f, 40.7f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Mines_33":
                    Log(GameObject.Find("BlockCrossroadsPeak2Right") == null);
                    blocker1 = new GameObject("BlockCrossroadsPeak2Right");
                    blocker1.transform.Translate(100, 7.7f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_04":
                    Log(GameObject.Find("BlockCrossroadsGroundsRight") == null);
                    blocker1 = new GameObject("BlockCrossroadsGroundsRight");
                    blocker1.transform.Translate(160, 24.2f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 3, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_49b":
                    Log(GameObject.Find("BlockCrossroadsCityRight") == null);
                    blocker1 = new GameObject("BlockCrossroadsCityRight");
                    blocker1.transform.Translate(30, 6, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthcrossroads":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Mines_01":
                    Log(GameObject.Find("BlockPeakCrossroads1Left") == null);
                    blocker1 = new GameObject("BlockPeakCrossroads1Left");
                    blocker1.transform.Translate(0, 49, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthpeak":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Mines_02":
                    Log(GameObject.Find("BlockPeakCrossroads2Left") == null);
                    blocker1 = new GameObject("BlockPeakCrossroads2Left");
                    blocker1.transform.Translate(0, 28, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthpeak":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Mines_28":
                    Log(GameObject.Find("BlockPeakGroundsBottom") == null);
                    blocker1 = new GameObject("BlockPeakGroundsBottom");
                    blocker1.transform.Translate(15, 45.5f, 0);
                    blocker1.transform.localScale = new Vector3(18, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "dirtmouthpeak":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_08":
                    Log(GameObject.Find("BlockCrossroadsAspidTopLeft") == null);
                    Log(GameObject.Find("BlockCrossroadsAspidBottomLeft") == null);
                    blocker1 = new GameObject("BlockCrossroadsAspidTopLeft");
                    blocker1.transform.Translate(0, 22, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);
                    blocker2 = new GameObject("BlockCrossroadsAspidBottomLeft");
                    blocker2.transform.Translate(0, 6, 0);
                    blocker2.transform.localScale = new Vector3(0.5f, 4, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            blocker2.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Crossroads_03":
                    Log(GameObject.Find("BlockCrossroadsLeverTop") == null);
                    Log(GameObject.Find("BlockCrossroadsLeverLeft") == null);
                    blocker1 = new GameObject("BlockCrossroadsLeverTop");
                    blocker1.transform.Translate(14, 72, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);
                    blocker2 = new GameObject("BlockCrossroadsLeverLeft");
                    blocker2.transform.Translate(1, 33, 0);
                    blocker2.transform.localScale = new Vector3(0.5f, 6, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            blocker2.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "RestingGrounds_05":
                    Log(GameObject.Find("BlockGroundsDoorRight") == null);
                    blocker1 = new GameObject("BlockGroundsDoorRight");
                    blocker1.transform.Translate(33, 75.5f, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 5, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins2_10b":
                    Log(GameObject.Find("BlockGroundsCityRight") == null);
                    Log(GameObject.Find("BlockGroundsEdgeRight") == null);
                    blocker1 = new GameObject("BlockGroundsCityRight");
                    blocker1.transform.Translate(30, 11, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 4, 0);
                    blocker2 = new GameObject("BlockGroundsEdgeRight");
                    blocker2.transform.Translate(30, 139.5f, 0);
                    blocker2.transform.localScale = new Vector3(0.5f, 7, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            blocker2.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins1_17":
                    Log(GameObject.Find("BlockStoreroomsBottom") == null);
                    blocker1 = new GameObject("BlockStoreroomsBottom");
                    blocker1.transform.Translate(7, 1.5f, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins1_05":
                    Log(GameObject.Find("BlockBelowSanctumBottom") == null);
                    blocker1 = new GameObject("BlockBelowSanctumBottom");
                    blocker1.transform.Translate(55, 104.5f, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins2_03b":
                    Log(GameObject.Find("BlockSpireMidBottom") == null);
                    blocker1 = new GameObject("BlockSpireMidBottom");
                    blocker1.transform.Translate(71, 1.5f, 0);
                    blocker1.transform.localScale = new Vector3(4, 0.5f, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins1_24":
                    if (PlayerData.instance.mageLordDefeated && gameMap == "spiritsdescent")
                    {
                        PlayerData.instance.hasDreamNail = !PlayerData.instance.mageLordDefeated;
                    }
                    break;
                case "Room_Town_Stag_Station":
                    Log(GameObject.Find("BlockDirtmouthStag") == null);
                    blocker1 = new GameObject("BlockDirtmouthStag");
                    blocker1.transform.Translate(50, 8, 0);
                    blocker1.transform.localScale = new Vector3(0.5f, 6, 0);

                    switch (gameMap)
                    {
                        case "spiritsdescent":
                            blocker1.AddComponent<BoxCollider2D>();
                            break;
                    }
                    break;
                case "Ruins1_18":
                    try
                    {
                        if (GameObject.Find("Ruins Gate") != null && gameMap == "spiritsdescent")
                        {
                            Log("Breakable found!");
                            UnityEngine.Object.Destroy(GameObject.Find("Ruins Gate"));
                        }
                    }
                    catch
                    {
                        Log("Failed to destroy breakable!");
                    }

                    break;
            }
        }

        private void CheckTwister(PlayerData data, HeroController controller)
        {
            if (PlayerData.instance.equippedCharm_33)
            {
                HuntMod.SaveData.SpellCost = CharmChangerMod.LS.spellTwisterSpellCost;
            }
            else
            {
                HuntMod.SaveData.SpellCost = normalSpell;
            }
        }

        private bool SetPlayerBoolHook(string name, bool orig)
        {
            Log(name);

            if (name == "killedBigFly" || name == "killedMawlek" || name == "killedBigBuzzer" || name == "killedMegaMossCharger" || name == "killedMegaJellyfish" || name == "killedMantisLord" || name == "killedMageKnight" || name == "killedJarCollector" || name == "killedMageLord" || name == "killedFlukeMother" || name == "killedDungDefender" || name == "killedMimicSpider" || name == "killedHiveKnight" || name == "killedTraitorLord" || name == "killedOblobble" || name == "killedLobsterLancer" || name == "killedGhostAladar" || name == "killedGhostXero" || name == "killedGhostHu" || name == "killedGhostMarmu" || name == "killedGhostNoEyes" || name == "killedGhostMarkoth" || name == "killedGhostGalien" || name == "killedWhiteDefender" || name == "killedGreyPrince" || name == "killedHollowKnight" || name == "killedFinalBoss" || name == "killedGrimm" || name == "killedNightmareGrimm" || name == "killedNailBros" || name == "killedPaintmaster" || name == "killedNailsage" || name == "killedHollowKnightPrime" || name == "killedInfectedKnight" || name == "hornet1Defeated" || name == "hornetOutskirtsDefeated" || name == "falseKnightDefeated" || name == "falseKnightDreamDefeated" || name == "mageLordDreamDefeated" || name == "infectedKnightDreamDefeated" || name == "defeatedMegaBeamMiner" || name == "defeatedMegaBeamMiner2" || name == "killedBlackKnight")
            {
                perkPoints++;
                Log("Boss beaten! 1 Perk awarded.");
                Log("You now have " + perkPoints + " Perks.");
            }

            if (name == "lurienDefeated" || name == "hegemolDefeated" || name == "monomonDefeated")
            {
                perkPoints++;
                Log("Dreamer beaten! 1 Perk awarded.");
                Log("You now have " + perkPoints + " Perks.");
            }
            
            if (name == "openedCrossroads" && gameMap == "spiritsdescent")
            {
                PlayerData.instance.SetBool("openedRuins1", true);
            }
            
            return orig;
        }

    }
}
