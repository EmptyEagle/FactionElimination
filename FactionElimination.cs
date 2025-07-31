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

namespace FactionElimination
{
    public class FactionElimination : Mod
    {
        public FactionElimination() : base("Faction Elimination") { }
        public override string GetVersion() => "0.1.0.0";
        public override void Initialize()
        {
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            ModHooks.BeforeAddHealthHook += BeforeAddHealth;
            ModHooks.SetPlayerIntHook += SetPlayerIntHook;
            ModHooks.SetPlayerBoolHook += SetPlayerBoolHook;
            On.HeroController.AddGeo += GeoProgression;
            ModHooks.NewGameHook += NewGameHook;
            FactionModeMenu.Register();
        }

        // Mr. Fatlife's overheal ability
        bool lifebloodOverheal = false;
        // Esteemed Scholar's decrease in NArt charge time or Spell Twister cost when collecting 3 NArts or 3 Spells
        bool scholarAbility = false;
        // The Gorgeous' geo Perks never increase in price
        bool gorgeousGeoDiscount = false;
        // When Ms. De Light eliminates another Team before Sudden Death, that Team becomes Infected and cannot partipate in Sudden Death
        // When Ms. De Light enters Sudden Death, the Max Health of all players on the Team is lowered by 2
        bool delightAbilities = false;

        // Currently resets when reopening the game. Persists through save and quitting savefile though.
        public int perkPoints;


        int geoProgress;
        int curGeoGoal = 500;

        int grubsProgress;


        private void NewGameHook()
        {
            Log("New Game started.");
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

            // Setting up Bretta and elevators
            PlayerData.instance.brettaRescued = true;
            PlayerData.instance.mineLiftOpened = true;
            PlayerData.instance.cityLift1 = true;

            if (scholarAbility)
            {
                if (PlayerData.instance.hasAllNailArts)
                {
                    Log("Scholar NArt ability activated!");
                    CharmChangerMod.LS.nailmastersGloryChargeTime = 0.5f;
                }
                if (PlayerData.instance.fireballLevel + PlayerData.instance.quakeLevel + PlayerData.instance.screamLevel >= 3)
                {
                    Log("Scholar Spell ability activated!");
                    CharmChangerMod.LS.spellTwisterSpellCost = 12;
                }
            }
        }

        private void OnHeroUpdate()
        {
            while (PlayerData.instance.maxHealth < FactionModeMenu.playerStats[0])
            {
                HeroController.instance.MaxHealth();
                HeroController.instance.AddToMaxHealth(1);
                PlayMakerFSM.BroadcastEvent("MAX HP UP");
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
            Log(name);
            
            // Gives complete Mask or Soul Vessel when Mask Shard or Vessel Fragment is collected
            if (name == "heartPieces" && PlayerData.instance.maxHealth < 9)
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
                if (PlayerData.instance.fireballLevel + PlayerData.instance.quakeLevel + PlayerData.instance.screamLevel >= 3)
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

            return orig;
        }

    }
}
