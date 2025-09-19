using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;
using InControl;
using MenuChanger;
using MenuChanger.Extensions;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using System.Reflection;
using System.Linq;
using System.Security.Policy;

namespace FactionElimination
{
    internal class FactionModeMenu : ModeMenuConstructor
    {
        MenuPage modeConfigPage;
        MenuLabel mainLabel;
        SmallButton commandersButton;
        SmallButton shopButton;
        SmallButton mapButton;
        SmallButton startButton;

        MenuPage modeCommandersBluePage;
        MenuPage modeCommandersGreenPage;
        MenuPage modeCommandersRedPage;
        MenuPage modeCommandersOrangePage;
        MenuLabel commandersTitle1;
        MenuLabel commandersTitle2;
        MenuLabel commandersTitle3;
        MenuLabel commandersTitle4;
        MenuLabel commandersColor1;
        MenuLabel commandersColor2;
        MenuLabel commandersColor3;
        MenuLabel commandersColor4;
        SmallButton commandersBlueLeft;
        SmallButton commandersBlueRight;
        SmallButton commandersGreenLeft;
        SmallButton commandersGreenRight;
        SmallButton commandersRedLeft;
        SmallButton commandersRedRight;
        SmallButton commandersOrangeLeft;
        SmallButton commandersOrangeRight;
        ToggleButton commandersBlue1;
        ToggleButton commandersBlue2;
        ToggleButton commandersBlue3;
        ToggleButton commandersGreen1;
        ToggleButton commandersGreen2;
        ToggleButton commandersGreen3;
        ToggleButton commandersRed1;
        ToggleButton commandersRed2;
        ToggleButton commandersRed3;
        ToggleButton commandersOrange1;
        ToggleButton commandersOrange2;
        ToggleButton commandersOrange3;

        MenuPage modeShopPage;
        MenuLabel shopTitle;
        GridItemPanel shopMainGrid;
        IMenuElement[] shopMain;
        SmallButton charmsButton;
        SmallButton sabotageButton;

        ToggleButton addMask1;
        ToggleButton addMask2;
        ToggleButton addVessel1;
        ToggleButton addVessel2;
        ToggleButton pureNail;
        ToggleButton giveIsmas;
        ToggleButton giveDGate;
        ToggleButton giveGSlash;
        ToggleButton giveDSlash;
        ToggleButton giveCSlash;
        ToggleButton addNotches1;
        ToggleButton addNotches2;
        ToggleButton addNotches3;
        ToggleButton giveSpirit1;
        ToggleButton giveDive1;
        ToggleButton giveWraiths1;
        ToggleButton giveSpirit2;
        ToggleButton giveDive2;
        ToggleButton giveWraiths2;
        ToggleButton giveCharm1;
        ToggleButton giveCharm2;
        ToggleButton giveCharm3;
        ToggleButton giveCharm4;
        ToggleButton giveCharm5;
        ToggleButton giveCharm6;
        ToggleButton giveCharm7;
        ToggleButton giveCharm8;
        ToggleButton giveCharm9;
        ToggleButton giveCharm10;
        ToggleButton giveCharm12;
        ToggleButton giveCharm13;
        ToggleButton giveCharm14;
        ToggleButton giveCharm15;
        ToggleButton giveCharm16;
        ToggleButton giveCharm17;
        ToggleButton giveCharm18;
        ToggleButton giveCharm19;
        ToggleButton giveCharm20;
        ToggleButton giveCharm21;
        ToggleButton giveCharm22;
        ToggleButton giveCharm23;
        ToggleButton giveCharm24;
        ToggleButton giveCharm25;
        ToggleButton giveCharm26;
        ToggleButton giveCharm27;
        ToggleButton giveCharm28;
        ToggleButton giveCharm29;
        ToggleButton giveCharm30;
        ToggleButton giveCharm31;
        ToggleButton giveCharm32;
        ToggleButton giveCharm33;
        ToggleButton giveCharm34;
        ToggleButton giveCharm35;
        ToggleButton giveCharm37;
        ToggleButton giveCharm38;
        ToggleButton giveCharm39;
        ToggleButton giveCharm40;
        ToggleButton giveCharm41;
        ToggleButton huntFocus1;
        ToggleButton huntFocus2;
        ToggleButton[] charms;

        MenuPage modeShopCharmsPage;
        MenuLabel shopCharmsTitle;
        GridItemPanel shopCharmsGrid;
        IMenuElement[] shopCharms;

        MenuPage modeMap;
        MenuLabel mapTitle;
        GridItemPanel mapGrid;
        IMenuElement[] mapComplete;

        ToggleButton dirtmouthCrossroads;
        ToggleButton dirtmouthPeak;
        ToggleButton edgeHive;
        ToggleButton spiritsDescent;

        MenuPage modeShopSabotagePage;
        MenuLabel shopSabotageTitle;

        ToggleButton infectCrossroads;
        ToggleButton forcedHarm;
        ToggleButton forcedDecay;
        ToggleButton forcedStagnation;

        MenuPage modeSalubraFree;
        MenuLabel salubraFreeTitle;
        GridItemPanel salubraFreeGrid;
        IMenuElement[] salubraFree;
        SmallButton salubraStart;

        ToggleButton salubraCharm1;
        ToggleButton salubraCharm2;
        ToggleButton salubraCharm3;
        ToggleButton salubraCharm4;
        ToggleButton salubraCharm5;
        ToggleButton salubraCharm6;
        ToggleButton salubraCharm7;
        ToggleButton salubraCharm8;
        ToggleButton salubraCharm9;
        ToggleButton salubraCharm10;
        ToggleButton salubraCharm12;
        ToggleButton salubraCharm13;
        ToggleButton salubraCharm14;
        ToggleButton salubraCharm15;
        ToggleButton salubraCharm16;
        ToggleButton salubraCharm17;
        ToggleButton salubraCharm18;
        ToggleButton salubraCharm19;
        ToggleButton salubraCharm20;
        ToggleButton salubraCharm21;
        ToggleButton salubraCharm22;
        ToggleButton salubraCharm23;
        ToggleButton salubraCharm24;
        ToggleButton salubraCharm25;
        ToggleButton salubraCharm26;
        ToggleButton salubraCharm27;
        ToggleButton salubraCharm28;
        ToggleButton salubraCharm29;
        ToggleButton salubraCharm30;
        ToggleButton salubraCharm31;
        ToggleButton salubraCharm32;
        ToggleButton salubraCharm33;
        ToggleButton salubraCharm34;
        ToggleButton salubraCharm35;
        ToggleButton salubraCharm37;
        ToggleButton salubraCharm38;
        ToggleButton salubraCharm39;
        ToggleButton salubraCharm40;
        ToggleButton salubraCharm41;
        ToggleButton[] salubraCharms;

        // Storing player starting equipment [mothwingcloak, mantisclaw, crystalheart, monarchwings, ismastear, shadecloak, lumaflylantern, kingsbrand, trampass, dreamnail, dreamgate]
        public static bool[] playerEquipment;
        // Storing player starting stats [masks, soulvessels, naildamage, essence, charmnotches, geo]
        public static int[] playerStats;
        // Storing player starting charms [0-38 (normal); 39 (grimmchild), 40 (carefreemelody)]
        public static bool[] playerCharms;
        // Storing player starting nail arts [cycloneslash, greatslash, dashslash]
        public static bool[] playerNArts;
        // Storing player starting spells [none/vengefulspirit/shadesoul (0/1/2), none/desolatedive/descendingdark (0/1/2), none/howlingwraiths/abyssshriek (0/1/2)]
        public static int[] playerSpells;
        // Storing charmchanger integer values [furythreshold, focushealing, deepfocushealing, deepfocustimescale, glowingwombcost, spelltwistercost]
        public static int[] charmChanger1;
        // Storing charmchanger float values [stalwartshellinvulntime, glowingwombspawntime, shapeofunnspeed, shapeofunnquickfocusspeed, sprintmasterspeed, sprintmasterdashmasterspeed, nmgchargetime]
        public static float[] charmChanger2;
        // Storing thehuntison focus speed
        public static float focusSpeed;
        // Storing commander abilities [lifebloodoverheal, scholarability, gorgeousgeodiscount, delightabilities]
        public static bool[] commanderAbilities;
        // Storing the chosen playable area
        public static string playableArea;

        public bool flukeDiscount;
        public bool salubraCharm;

        public static int perkPoints;
        

        public static void Register()
        {
            ModeMenu.AddMode(new FactionModeMenu());
        }

        public override void OnEnterMainMenu(MenuPage modeMenu)
        {
            // Sets all player settings to their default values when creating a new save
            playerEquipment = [true, true, true, true, false, false, true, true, true, true, false];
            playerStats = [6, 0, 13, 3, 3, 0];
            playerCharms = new bool[41];
            playerNArts = new bool[3];
            playerSpells = new int[3];
            charmChanger1 = [5, 3, 5, 65, 0, 24];
            charmChanger2 = [2f, 2f, 12f, 18f, 15f, 16.5f, 0.75f];
            focusSpeed = 2.5f;
            commanderAbilities = new bool[4];
            flukeDiscount = false;
            salubraCharm = false;
            perkPoints = 3;

            modeConfigPage = new MenuPage("Mode Select", modeMenu);
            mainLabel = new MenuLabel(modeConfigPage, "Faction Elimination Settings");
            commandersButton = new SmallButton(modeConfigPage, "Commander Selection");
            shopButton = new SmallButton(modeConfigPage, "Perk Shop");
            shopButton.Lock();
            mapButton = new SmallButton(modeConfigPage, "Map Selection");
            mapButton.Lock();
            startButton = new SmallButton(modeConfigPage, "Start Game");
            startButton.Lock();
            

            modeCommandersBluePage = new MenuPage("Blue Commanders", modeConfigPage);
            commandersTitle1 = new MenuLabel(modeCommandersBluePage, "Commanders");
            commandersColor1 = new MenuLabel(modeCommandersBluePage, "BLUE SET");
            commandersBlueLeft = new SmallButton(modeCommandersBluePage, "<<");
            commandersBlueRight = new SmallButton(modeCommandersBluePage, ">>");
            commandersBlue1 = new ToggleButton(modeCommandersBluePage, "Baldur Sage");
            commandersBlue2 = new ToggleButton(modeCommandersBluePage, "Senior Senile Stone");
            commandersBlue3 = new ToggleButton(modeCommandersBluePage, "Mr. Fatlife");
            GridItemPanel commanderBlueArrows = new GridItemPanel(modeCommandersBluePage, new Vector2(0, 400f), 3, 0f, 750f, true, [commandersBlueLeft, commandersColor1, commandersBlueRight]);
            modeCommandersGreenPage = new MenuPage("Green Commanders", modeConfigPage);
            commandersTitle2 = new MenuLabel(modeCommandersGreenPage, "Commanders");
            commandersColor2 = new MenuLabel(modeCommandersGreenPage, "GREEN SET");
            commandersGreenLeft = new SmallButton(modeCommandersGreenPage, "<<");
            commandersGreenRight = new SmallButton(modeCommandersGreenPage, ">>");
            commandersGreen1 = new ToggleButton(modeCommandersGreenPage, "Lost Vessel");
            commandersGreen2 = new ToggleButton(modeCommandersGreenPage, "Cpt. I.A.M. Blooming");
            commandersGreen3 = new ToggleButton(modeCommandersGreenPage, "Esteemed Scholar");
            GridItemPanel commanderGreenArrows = new GridItemPanel(modeCommandersGreenPage, new Vector2(0, 400f), 3, 0f, 750f, true, [commandersGreenLeft, commandersColor2, commandersGreenRight]);
            modeCommandersRedPage = new MenuPage("Red Commanders", modeConfigPage);
            commandersTitle3 = new MenuLabel(modeCommandersRedPage, "Commanders");
            commandersColor3 = new MenuLabel(modeCommandersRedPage, "RED SET");
            commandersRedLeft = new SmallButton(modeCommandersRedPage, "<<");
            commandersRedRight = new SmallButton(modeCommandersRedPage, ">>");
            commandersRed1 = new ToggleButton(modeCommandersRedPage, "Cpt. Flukemarm");
            commandersRed2 = new ToggleButton(modeCommandersRedPage, "Sanguine Salubra");
            commandersRed3 = new ToggleButton(modeCommandersRedPage, "Crystal Chieftain");
            GridItemPanel commanderRedArrows = new GridItemPanel(modeCommandersRedPage, new Vector2(0, 400f), 3, 0f, 750f, true, [commandersRedLeft, commandersColor3, commandersRedRight]);
            modeCommandersOrangePage = new MenuPage("Orange Commanders", modeConfigPage);
            commandersTitle4 = new MenuLabel(modeCommandersOrangePage, "Commanders");
            commandersColor4 = new MenuLabel(modeCommandersOrangePage, "ORANGE SET");
            commandersOrangeLeft = new SmallButton(modeCommandersOrangePage, "<<");
            commandersOrangeRight = new SmallButton(modeCommandersOrangePage, ">>");
            commandersOrange1 = new ToggleButton(modeCommandersOrangePage, "The Gorgeous");
            commandersOrange2 = new ToggleButton(modeCommandersOrangePage, "Ms. De Light");
            commandersOrange3 = new ToggleButton(modeCommandersOrangePage, "Turbo Tuk");
            GridItemPanel commanderOrangeArrows = new GridItemPanel(modeCommandersOrangePage, new Vector2(0, 400f), 3, 0f, 750f, true, [commandersOrangeLeft, commandersColor4, commandersOrangeRight]);

            modeShopPage = new MenuPage("Perk Shop", modeConfigPage);
            modeShopCharmsPage = new MenuPage("Charms Shop", modeShopPage);
            modeShopSabotagePage = new MenuPage("Sabotage Shop (unfinished)", modeShopPage);
            modeSalubraFree = new MenuPage("Salubra Charm", modeConfigPage);

            AssignShopButtons();
            shopTitle = new MenuLabel(modeShopPage, "Perk Shop - ["+perkPoints+" Perks Remaining]");
            shopTitle.MoveTo(new Vector2(0, 1000f));
            charmsButton = new SmallButton(modeShopPage, "Charms");
            sabotageButton = new SmallButton(modeShopPage, "Sabotage (unfinished)");
            sabotageButton.Lock();
            shopMain = [new MenuLabel(modeShopPage, "Upgrades (1 Perk Each)"), new MenuLabel(modeShopPage, "Nail Arts (1 Perk Each)"), addMask1, giveGSlash, addMask2, giveDSlash, addVessel1, giveCSlash, addVessel2, new MenuLabel(modeShopPage, "Spells (2 Perks Each)"), pureNail, giveSpirit1, giveIsmas, giveDive1, giveDGate, giveWraiths1, new MenuLabel(modeShopPage, "Charm Notches (1 Perk Each)"), new MenuLabel(modeShopPage, "Exclusive Spells"), addNotches1, giveSpirit2, addNotches2, giveDive2, addNotches3, giveWraiths2, charmsButton, sabotageButton, new MenuLabel(modeShopPage, "TheHuntIsOn Upgrades (1 Perk Each)"), new MenuLabel(modeShopPage, ""), huntFocus1, new MenuLabel(modeShopPage, ""), huntFocus2];
            shopMainGrid = new GridItemPanel(modeShopPage, new Vector2(0, 0), 2, 50f, 1000f, true, shopMain);


            shopCharmsTitle = new MenuLabel(modeShopCharmsPage, "Charms - ["+perkPoints+" Perks Remaining]");
            shopCharms = [giveCharm2, giveCharm1, giveCharm4, giveCharm20, giveCharm19, giveCharm21, giveCharm31, giveCharm37, giveCharm3, giveCharm35, giveCharm23, giveCharm24, giveCharm25, giveCharm33, giveCharm14, giveCharm15, giveCharm32, giveCharm18, giveCharm13, giveCharm6, giveCharm12, giveCharm5, giveCharm10, giveCharm22, giveCharm7, giveCharm34, giveCharm8, giveCharm9, giveCharm27, giveCharm29, giveCharm17, giveCharm16, giveCharm28, giveCharm26, giveCharm39, giveCharm30, giveCharm38, giveCharm40, giveCharm41];
            shopCharmsGrid = new GridItemPanel(modeShopCharmsPage, new Vector2(0, 0), 5, 50f, 350f, true, shopCharms);


            shopSabotageTitle = new MenuLabel(modeShopSabotagePage, "Sabotages - ["+perkPoints+" Perks Remaining]");

            modeMap = new MenuPage("Map Setup", modeConfigPage);
            mapTitle = new MenuLabel(modeMap, "Select A Playable Area");
            dirtmouthCrossroads = new ToggleButton(modeMap, "Dirtmouth + Forgotten Crossroads");
            dirtmouthPeak = new ToggleButton(modeMap, "Dirtmouth + Crystal Peak");
            edgeHive = new ToggleButton(modeMap, "Kingdom's Edge + The Hive");
            spiritsDescent = new ToggleButton(modeMap, "Spirits' Descent");
            mapComplete = [dirtmouthCrossroads, dirtmouthPeak, edgeHive, spiritsDescent];
            mapGrid = new GridItemPanel(modeMap, new Vector2(0, 0), 2, 50f, 1000f, true, mapComplete);
            
            salubraFreeTitle = new MenuLabel(modeSalubraFree, "Sanguine Salubra's Charm");
            salubraFree = [salubraCharm2, salubraCharm1, salubraCharm4, salubraCharm20, salubraCharm19, salubraCharm21, salubraCharm31, salubraCharm37, salubraCharm3, salubraCharm35, salubraCharm23, salubraCharm24, salubraCharm25, salubraCharm33, salubraCharm14, salubraCharm15, salubraCharm32, salubraCharm18, salubraCharm13, salubraCharm6, salubraCharm12, salubraCharm5, salubraCharm10, salubraCharm22, salubraCharm7, salubraCharm34, salubraCharm8, salubraCharm9, salubraCharm27, salubraCharm29, salubraCharm17, salubraCharm16, salubraCharm28, salubraCharm26, salubraCharm39, salubraCharm30, salubraCharm38, salubraCharm40, salubraCharm41];
            salubraFreeGrid = new GridItemPanel(modeSalubraFree, new Vector2(0, 0), 5, 50f, 350f, true, salubraFree);
            salubraStart = new SmallButton(modeSalubraFree, "Start Game");
            salubraStart.Lock();

            commandersButton.AddHideAndShowEvent(modeConfigPage, modeCommandersBluePage);
            shopButton.AddHideAndShowEvent(modeConfigPage, modeShopPage);
            mapButton.AddHideAndShowEvent(modeConfigPage, modeMap);
            charmsButton.AddHideAndShowEvent(modeShopPage, modeShopCharmsPage);
            sabotageButton.AddHideAndShowEvent(modeShopPage, modeShopSabotagePage);
            commandersBlueLeft.AddHideAndShowEvent(modeCommandersBluePage, modeCommandersOrangePage);
            commandersBlueRight.AddHideAndShowEvent(modeCommandersBluePage, modeCommandersGreenPage);
            commandersGreenLeft.AddHideAndShowEvent(modeCommandersGreenPage, modeCommandersBluePage);
            commandersGreenRight.AddHideAndShowEvent(modeCommandersGreenPage, modeCommandersRedPage);
            commandersRedLeft.AddHideAndShowEvent(modeCommandersRedPage, modeCommandersGreenPage);
            commandersRedRight.AddHideAndShowEvent(modeCommandersRedPage, modeCommandersOrangePage);
            commandersOrangeLeft.AddHideAndShowEvent(modeCommandersOrangePage, modeCommandersRedPage);
            commandersOrangeRight.AddHideAndShowEvent(modeCommandersOrangePage, modeCommandersBluePage);
            startButton.OnClick += () => SalubraCheck();
            salubraStart.OnClick += StartGame;

            commandersBlue1.OnClick += () => SetCommander(commandersBlue1);
            commandersBlue2.OnClick += () => SetCommander(commandersBlue2);
            commandersBlue3.OnClick += () => SetCommander(commandersBlue3);
            commandersGreen1.OnClick += () => SetCommander(commandersGreen1);
            commandersGreen2.OnClick += () => SetCommander(commandersGreen2);
            commandersGreen3.OnClick += () => SetCommander(commandersGreen3);
            commandersRed1.OnClick += () => SetCommander(commandersRed1);
            commandersRed2.OnClick += () => SetCommander(commandersRed2);
            commandersRed3.OnClick += () => SetCommander(commandersRed3);
            commandersOrange1.OnClick += () => SetCommander(commandersOrange1);
            commandersOrange2.OnClick += () => SetCommander(commandersOrange2);
            commandersOrange3.OnClick += () => SetCommander(commandersOrange3);

            addMask1.OnClick += () => ShopPurchase(addMask1);
            addMask2.OnClick += () => ShopPurchase(addMask2);
            addVessel1.OnClick += () => ShopPurchase(addVessel1);
            addVessel2.OnClick += () => ShopPurchase(addVessel2);
            pureNail.OnClick += () => ShopPurchase(pureNail);
            giveIsmas.OnClick += () => ShopPurchase(giveIsmas);
            giveDGate.OnClick += () => ShopPurchase(giveDGate);
            addNotches1.OnClick += () => ShopPurchase(addNotches1);
            addNotches2.OnClick += () => ShopPurchase(addNotches2);
            addNotches3.OnClick += () => ShopPurchase(addNotches3);
            giveCharm1.OnClick += () => ShopPurchase(giveCharm1);
            giveCharm2.OnClick += () => ShopPurchase(giveCharm2);
            giveCharm3.OnClick += () => ShopPurchase(giveCharm3);
            giveCharm4.OnClick += () => ShopPurchase(giveCharm4);
            giveCharm5.OnClick += () => ShopPurchase(giveCharm5);
            giveCharm6.OnClick += () => ShopPurchase(giveCharm6);
            giveCharm7.OnClick += () => ShopPurchase(giveCharm7);
            giveCharm8.OnClick += () => ShopPurchase(giveCharm8);
            giveCharm9.OnClick += () => ShopPurchase(giveCharm9);
            giveCharm10.OnClick += () => ShopPurchase(giveCharm10);
            giveCharm12.OnClick += () => ShopPurchase(giveCharm12);
            giveCharm13.OnClick += () => ShopPurchase(giveCharm13);
            giveCharm14.OnClick += () => ShopPurchase(giveCharm14);
            giveCharm15.OnClick += () => ShopPurchase(giveCharm15);
            giveCharm16.OnClick += () => ShopPurchase(giveCharm16);
            giveCharm17.OnClick += () => ShopPurchase(giveCharm17);
            giveCharm18.OnClick += () => ShopPurchase(giveCharm18);
            giveCharm19.OnClick += () => ShopPurchase(giveCharm19);
            giveCharm20.OnClick += () => ShopPurchase(giveCharm20);
            giveCharm21.OnClick += () => ShopPurchase(giveCharm21);
            giveCharm22.OnClick += () => ShopPurchase(giveCharm22);
            giveCharm23.OnClick += () => ShopPurchase(giveCharm23);
            giveCharm24.OnClick += () => ShopPurchase(giveCharm24);
            giveCharm25.OnClick += () => ShopPurchase(giveCharm25);
            giveCharm26.OnClick += () => ShopPurchase(giveCharm26);
            giveCharm27.OnClick += () => ShopPurchase(giveCharm27);
            giveCharm28.OnClick += () => ShopPurchase(giveCharm28);
            giveCharm29.OnClick += () => ShopPurchase(giveCharm29);
            giveCharm30.OnClick += () => ShopPurchase(giveCharm30);
            giveCharm31.OnClick += () => ShopPurchase(giveCharm31);
            giveCharm32.OnClick += () => ShopPurchase(giveCharm32);
            giveCharm33.OnClick += () => ShopPurchase(giveCharm33);
            giveCharm34.OnClick += () => ShopPurchase(giveCharm34);
            giveCharm35.OnClick += () => ShopPurchase(giveCharm35);
            giveCharm37.OnClick += () => ShopPurchase(giveCharm37);
            giveCharm38.OnClick += () => ShopPurchase(giveCharm38);
            giveCharm39.OnClick += () => ShopPurchase(giveCharm39);
            giveCharm40.OnClick += () => ShopPurchase(giveCharm40);
            giveCharm41.OnClick += () => ShopPurchase(giveCharm41);
            giveGSlash.OnClick += () => ShopPurchase(giveGSlash);
            giveDSlash.OnClick += () => ShopPurchase(giveDSlash);
            giveCSlash.OnClick += () => ShopPurchase(giveCSlash);
            giveSpirit1.OnClick += () => ShopPurchase(giveSpirit1);
            giveDive1.OnClick += () => ShopPurchase(giveDive1);
            giveWraiths1.OnClick += () => ShopPurchase(giveWraiths1);
            giveSpirit2.OnClick += () => ShopPurchase(giveSpirit2);
            giveDive2.OnClick += () => ShopPurchase(giveDive2);
            giveWraiths2.OnClick += () => ShopPurchase(giveWraiths2);
            huntFocus1.OnClick += () => ShopPurchase(huntFocus1);
            huntFocus2.OnClick += () => ShopPurchase(huntFocus2);
            infectCrossroads.OnClick += () => ShopPurchase(infectCrossroads);
            forcedHarm.OnClick += () => ShopPurchase(forcedHarm);
            forcedDecay.OnClick += () => ShopPurchase(forcedDecay);
            forcedStagnation.OnClick += () => ShopPurchase(forcedStagnation);

            // Room for Salubra charm here
            salubraCharm1.OnClick += () => ShopPurchase(salubraCharm1);
            salubraCharm2.OnClick += () => ShopPurchase(salubraCharm2);
            salubraCharm3.OnClick += () => ShopPurchase(salubraCharm3);
            salubraCharm4.OnClick += () => ShopPurchase(salubraCharm4);
            salubraCharm5.OnClick += () => ShopPurchase(salubraCharm5);
            salubraCharm6.OnClick += () => ShopPurchase(salubraCharm6);
            salubraCharm7.OnClick += () => ShopPurchase(salubraCharm7);
            salubraCharm8.OnClick += () => ShopPurchase(salubraCharm8);
            salubraCharm9.OnClick += () => ShopPurchase(salubraCharm9);
            salubraCharm10.OnClick += () => ShopPurchase(salubraCharm10);
            salubraCharm12.OnClick += () => ShopPurchase(salubraCharm12);
            salubraCharm13.OnClick += () => ShopPurchase(salubraCharm13);
            salubraCharm14.OnClick += () => ShopPurchase(salubraCharm14);
            salubraCharm15.OnClick += () => ShopPurchase(salubraCharm15);
            salubraCharm16.OnClick += () => ShopPurchase(salubraCharm16);
            salubraCharm17.OnClick += () => ShopPurchase(salubraCharm17);
            salubraCharm18.OnClick += () => ShopPurchase(salubraCharm18);
            salubraCharm19.OnClick += () => ShopPurchase(salubraCharm19);
            salubraCharm20.OnClick += () => ShopPurchase(salubraCharm20);
            salubraCharm21.OnClick += () => ShopPurchase(salubraCharm21);
            salubraCharm22.OnClick += () => ShopPurchase(salubraCharm22);
            salubraCharm23.OnClick += () => ShopPurchase(salubraCharm23);
            salubraCharm24.OnClick += () => ShopPurchase(salubraCharm24);
            salubraCharm25.OnClick += () => ShopPurchase(salubraCharm25);
            salubraCharm26.OnClick += () => ShopPurchase(salubraCharm26);
            salubraCharm27.OnClick += () => ShopPurchase(salubraCharm27);
            salubraCharm28.OnClick += () => ShopPurchase(salubraCharm28);
            salubraCharm29.OnClick += () => ShopPurchase(salubraCharm29);
            salubraCharm30.OnClick += () => ShopPurchase(salubraCharm30);
            salubraCharm31.OnClick += () => ShopPurchase(salubraCharm31);
            salubraCharm32.OnClick += () => ShopPurchase(salubraCharm32);
            salubraCharm33.OnClick += () => ShopPurchase(salubraCharm33);
            salubraCharm34.OnClick += () => ShopPurchase(salubraCharm34);
            salubraCharm35.OnClick += () => ShopPurchase(salubraCharm35);
            salubraCharm37.OnClick += () => ShopPurchase(salubraCharm37);
            salubraCharm38.OnClick += () => ShopPurchase(salubraCharm38);
            salubraCharm39.OnClick += () => ShopPurchase(salubraCharm39);
            salubraCharm40.OnClick += () => ShopPurchase(salubraCharm40);
            salubraCharm41.OnClick += () => ShopPurchase(salubraCharm41);

            dirtmouthCrossroads.OnClick += () => SetMap(dirtmouthCrossroads);
            dirtmouthPeak.OnClick += () => SetMap(dirtmouthPeak);
            edgeHive.OnClick += () => SetMap(edgeHive);
            spiritsDescent.OnClick += () => SetMap(spiritsDescent);

            // Builds elements in each page
            IMenuElement[] elementsMain =
            [
                mainLabel,
                commandersButton,
                shopButton,
                mapButton,
                startButton
            ];
            IMenuElement[] elementsCommanderBlue =
            [
                commandersTitle1,
                commanderBlueArrows,
                commandersBlue1,
                commandersBlue2,
                commandersBlue3
            ];
            IMenuElement[] elementsCommanderGreen =
            [
                commandersTitle2,
                commanderGreenArrows,
                commandersGreen1,
                commandersGreen2,
                commandersGreen3
            ];
            IMenuElement[] elementsCommanderRed =
            [
                commandersTitle3,
                commanderRedArrows,
                commandersRed1,
                commandersRed2,
                commandersRed3
            ];
            IMenuElement[] elementsCommanderOrange =
            [
                commandersTitle4,
                commanderOrangeArrows,
                commandersOrange1,
                commandersOrange2,
                commandersOrange3
            ];
            IMenuElement[] elementsShopMain =
            [
                shopTitle,
                shopMainGrid
            ];
            IMenuElement[] elementsShopCharms =
            [
                shopCharmsTitle,
                shopCharmsGrid
            ];
            IMenuElement[] elementsShopSabotage =
            [
                shopSabotageTitle,
                infectCrossroads,
                forcedHarm,
                forcedDecay,
                forcedStagnation
            ];
            IMenuElement[] elementsMap =
            [
                mapTitle,
                mapGrid
            ];
            IMenuElement[] elementsSalubraFree =
            [
                salubraFreeTitle,
                salubraFreeGrid,
                salubraStart
            ];

            VerticalItemPanel vipMain = new(modeConfigPage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsMain);
            modeConfigPage.AddToNavigationControl(vipMain);
            VerticalItemPanel vipCommanderBlue = new(modeCommandersBluePage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsCommanderBlue);
            modeConfigPage.AddToNavigationControl(vipCommanderBlue);
            VerticalItemPanel vipCommanderGreen = new(modeCommandersGreenPage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsCommanderGreen);
            modeConfigPage.AddToNavigationControl(vipCommanderGreen);
            VerticalItemPanel vipCommanderRed = new(modeCommandersRedPage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsCommanderRed);
            modeConfigPage.AddToNavigationControl(vipCommanderRed);
            VerticalItemPanel vipCommanderOrange = new(modeCommandersOrangePage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsCommanderOrange);
            modeConfigPage.AddToNavigationControl(vipCommanderOrange);
            VerticalItemPanel vipShopMain = new(modeShopPage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsShopMain);
            modeConfigPage.AddToNavigationControl(vipShopMain);
            vipShopMain.Translate(new Vector2(0, 140));
            VerticalItemPanel vipShopCharms = new(modeShopCharmsPage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsShopCharms);
            modeConfigPage.AddToNavigationControl(vipShopCharms);
            vipShopCharms.Translate(new Vector2(0, -100));
            VerticalItemPanel vipShopSabotage = new(modeShopSabotagePage, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsShopSabotage);
            modeConfigPage.AddToNavigationControl(vipShopSabotage);
            VerticalItemPanel vipMap = new(modeMap, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsMap);
            modeConfigPage.AddToNavigationControl(vipMap);
            vipMap.Translate(new Vector2(0, 140));
            VerticalItemPanel vipSalubraFree = new(modeSalubraFree, SpaceParameters.TOP_CENTER_UNDER_TITLE, 100, false, elementsSalubraFree);
            modeConfigPage.AddToNavigationControl(vipSalubraFree);
            salubraStart.Translate(new Vector2(0, -350));
        }


        private void StartGame()
        {
            UIManager.instance.StartNewGame(permaDeath: false);
            
            // Applying shop purchases
            if (addMask1.Value)
                playerStats[0]++;
            if (addMask2.Value)
                playerStats[0]++;
            if (addVessel1.Value)
                playerStats[1]++;
            if (addVessel2.Value)
                playerStats[1]++;
            if (pureNail.Value)
                playerStats[2] = 21;
            if (giveIsmas.Value)
                playerEquipment[4] = true;
            if (giveDGate.Value)
                playerEquipment[10] = true;
            if (addNotches1.Value)
                playerStats[4] += 2;
            if (addNotches2.Value)
                playerStats[4] += 2;
            if (addNotches3.Value)
                playerStats[4] += 2;
            // Applying charm purchases
            if (giveCharm1.Value || salubraCharm1.Value)
                playerCharms[0] = true;
            if (giveCharm2.Value || salubraCharm2.Value)
                playerCharms[1] = true;
            if (giveCharm3.Value || salubraCharm3.Value)
                playerCharms[2] = true;
            if (giveCharm4.Value || salubraCharm4.Value)
                playerCharms[3] = true;
            if (giveCharm5.Value || salubraCharm5.Value)
                playerCharms[4] = true;
            if (giveCharm6.Value || salubraCharm6.Value)
                playerCharms[5] = true;
            if (giveCharm7.Value || salubraCharm7.Value)
                playerCharms[6] = true;
            if (giveCharm8.Value || salubraCharm8.Value)
                playerCharms[7] = true;
            if (giveCharm9.Value || salubraCharm9.Value)
                playerCharms[8] = true;
            if (giveCharm10.Value || salubraCharm10.Value)
                playerCharms[9] = true;
            if (giveCharm12.Value || salubraCharm12.Value)
                playerCharms[11] = true;
            if (giveCharm13.Value || salubraCharm13.Value)
                playerCharms[12] = true;
            if (giveCharm14.Value || salubraCharm14.Value)
                playerCharms[13] = true;
            if (giveCharm15.Value || salubraCharm15.Value)
                playerCharms[14] = true;
            if (giveCharm16.Value || salubraCharm16.Value)
                playerCharms[15] = true;
            if (giveCharm17.Value || salubraCharm17.Value)
                playerCharms[16] = true;
            if (giveCharm18.Value || salubraCharm18.Value)
                playerCharms[17] = true;
            if (giveCharm19.Value || salubraCharm19.Value)
                playerCharms[18] = true;
            if (giveCharm20.Value || salubraCharm20.Value)
                playerCharms[19] = true;
            if (giveCharm21.Value || salubraCharm21.Value)
                playerCharms[20] = true;
            if (giveCharm22.Value || salubraCharm22.Value)
                playerCharms[21] = true;
            if (giveCharm23.Value || salubraCharm23.Value)
                playerCharms[22] = true;
            if (giveCharm24.Value || salubraCharm24.Value)
                playerCharms[23] = true;
            if (giveCharm25.Value || salubraCharm25.Value)
                playerCharms[24] = true;
            if (giveCharm26.Value || salubraCharm26.Value)
                playerCharms[25] = true;
            if (giveCharm27.Value || salubraCharm27.Value)
                playerCharms[26] = true;
            if (giveCharm28.Value || salubraCharm28.Value)
                playerCharms[27] = true;
            if (giveCharm29.Value || salubraCharm29.Value)
                playerCharms[28] = true;
            if (giveCharm30.Value || salubraCharm30.Value)
                playerCharms[29] = true;
            if (giveCharm31.Value || salubraCharm31.Value)
                playerCharms[30] = true;
            if (giveCharm32.Value || salubraCharm32.Value)
                playerCharms[31] = true;
            if (giveCharm33.Value || salubraCharm33.Value)
                playerCharms[32] = true;
            if (giveCharm34.Value || salubraCharm34.Value)
                playerCharms[33] = true;
            if (giveCharm35.Value || salubraCharm35.Value)
                playerCharms[34] = true;
            if (giveCharm37.Value || salubraCharm37.Value)
                playerCharms[36] = true;
            if (giveCharm38.Value || salubraCharm38.Value)
                playerCharms[37] = true;
            if (giveCharm39.Value || salubraCharm39.Value)
                playerCharms[38] = true;
            if (giveCharm40.Value || salubraCharm40.Value)
                playerCharms[39] = true;
            if (giveCharm41.Value || salubraCharm41.Value)
                playerCharms[40] = true;
            if (giveCSlash.Value)
                playerNArts[0] = true;
            if (giveGSlash.Value)
                playerNArts[1] = true;
            if (giveDSlash.Value)
                playerNArts[2] = true;
            if (giveSpirit1.Value)
                playerSpells[0] = 1;
            if (giveDive1.Value)
                playerSpells[1] = 1;
            if (giveWraiths1.Value)
                playerSpells[2] = 1;
            if (giveSpirit2.Value)
                playerSpells[0] = 2;
            if (giveDive2.Value)
                playerSpells[1] = 2;
            if (giveWraiths2.Value)
                playerSpells[2] = 2;
            if (huntFocus1.Value)
                focusSpeed-=0.3f;
            if (huntFocus2.Value)
                focusSpeed-=0.3f;

            // Applying commander
            if (commandersBlue2.Value)
            {
                playerStats[0]++;
            }
            if (commandersBlue3.Value)
            {
                commanderAbilities[0] = true;
            }
            if (commandersGreen1.Value)
            {
                playerEquipment[5] = true;
            }
            if (commandersGreen3.Value)
            {
                commanderAbilities[1] = true;
            }
            if (commandersRed2.Value)
            {
                playerStats[4] = 11;
            }
            if (commandersRed3.Value)
            {
                charmChanger1[2] = 8;
                charmChanger1[3] = 90;
            }
            if (commandersOrange1.Value)
            {
                commanderAbilities[2] = true;
            }
            if (commandersOrange2.Value)
            {
                commanderAbilities[3] = true;
            }
        }

        public override void OnExitMainMenu()
        {
            modeConfigPage = null;
            modeCommandersBluePage = null;
            modeCommandersGreenPage = null;
            modeCommandersRedPage = null;
            modeCommandersOrangePage = null;
            modeShopPage = null;
            modeShopCharmsPage = null;
            modeShopSabotagePage = null;
            modeMap = null;
            modeSalubraFree = null;
        }

        public override bool TryGetModeButton(MenuPage modeMenu, out BigButton button)
        {
            button = new BigButton(modeMenu, "Faction Elimination");
            button.AddHideAndShowEvent(modeMenu, modeConfigPage);
            return true;
        }

        public void AssignShopButtons()
        {
            addMask1 = new ToggleButton(modeShopPage, "Additional Mask");
            addMask2 = new ToggleButton(modeShopPage, "Additional Mask");
            addVessel1 = new ToggleButton(modeShopPage, "Additional Soul Vessel");
            addVessel2 = new ToggleButton(modeShopPage, "Additional Soul Vessel");
            pureNail = new ToggleButton(modeShopPage, "Pure Nail");
            giveIsmas = new ToggleButton(modeShopPage, "Isma's Tear");
            giveDGate = new ToggleButton(modeShopPage, "Dream Gate");
            addNotches1 = new ToggleButton(modeShopPage, "2 Charm Notches");
            addNotches2 = new ToggleButton(modeShopPage, "2 Charm Notches");
            addNotches3 = new ToggleButton(modeShopPage, "2 Charm Notches");
            giveGSlash = new ToggleButton(modeShopPage, "Great Slash");
            giveDSlash = new ToggleButton(modeShopPage, "Dash Slash");
            giveCSlash = new ToggleButton(modeShopPage, "Cyclone Slash");
            giveSpirit1 = new ToggleButton(modeShopPage, "Vengeful Spirit");
            giveDive1 = new ToggleButton(modeShopPage, "Desolate Dive");
            giveWraiths1 = new ToggleButton(modeShopPage, "Howling Wraiths");
            giveSpirit2 = new ToggleButton(modeShopPage, "Shade Soul (3 Perks)");
            giveDive2 = new ToggleButton(modeShopPage, "Descending Dark (4 Perks)");
            giveWraiths2 = new ToggleButton(modeShopPage, "Abyss Shriek (3 Perks)");
            huntFocus1 = new ToggleButton(modeShopPage, "Reduce Focus Speed by 0.3");
            huntFocus2 = new ToggleButton(modeShopPage, "Reduce Focus Speed by 0.3");

            giveCharm1 = new ToggleButton(modeShopCharmsPage, "Gathering Swarm");
            giveCharm2 = new ToggleButton(modeShopCharmsPage, "Wayward Compass");
            giveCharm3 = new ToggleButton(modeShopCharmsPage, "Grubsong");
            giveCharm4 = new ToggleButton(modeShopCharmsPage, "Stalwart Shell");
            giveCharm5 = new ToggleButton(modeShopCharmsPage, "Baldur Shell");
            giveCharm6 = new ToggleButton(modeShopCharmsPage, "Fury of the Fallen");
            giveCharm7 = new ToggleButton(modeShopCharmsPage, "Quick Focus");
            giveCharm8 = new ToggleButton(modeShopCharmsPage, "Lifeblood Heart");
            giveCharm9 = new ToggleButton(modeShopCharmsPage, "Lifeblood Core");
            giveCharm10 = new ToggleButton(modeShopCharmsPage, "Defender's Crest");
            giveCharm12 = new ToggleButton(modeShopCharmsPage, "Thorns of Agony");
            giveCharm13 = new ToggleButton(modeShopCharmsPage, "Mark of Pride");
            giveCharm14 = new ToggleButton(modeShopCharmsPage, "Steady Body");
            giveCharm15 = new ToggleButton(modeShopCharmsPage, "Heavy Blow");
            giveCharm16 = new ToggleButton(modeShopCharmsPage, "Sharp Shadow");
            giveCharm17 = new ToggleButton(modeShopCharmsPage, "Spore Shroom");
            giveCharm18 = new ToggleButton(modeShopCharmsPage, "Longnail");
            giveCharm19 = new ToggleButton(modeShopCharmsPage, "Shaman Stone");
            giveCharm20 = new ToggleButton(modeShopCharmsPage, "Soul Catcher");
            giveCharm21 = new ToggleButton(modeShopCharmsPage, "Soul Eater");
            giveCharm22 = new ToggleButton(modeShopCharmsPage, "Glowing Womb");
            giveCharm23 = new ToggleButton(modeShopCharmsPage, "Unbreakable Heart");
            giveCharm24 = new ToggleButton(modeShopCharmsPage, "Unbreakable Greed");
            giveCharm25 = new ToggleButton(modeShopCharmsPage, "Unbreakable Strength");
            giveCharm26 = new ToggleButton(modeShopCharmsPage, "Nailmaster's Glory");
            giveCharm27 = new ToggleButton(modeShopCharmsPage, "Joni's Blessing");
            giveCharm28 = new ToggleButton(modeShopCharmsPage, "Shape of Unn");
            giveCharm29 = new ToggleButton(modeShopCharmsPage, "Hiveblood");
            giveCharm30 = new ToggleButton(modeShopCharmsPage, "Dream Wielder");
            giveCharm31 = new ToggleButton(modeShopCharmsPage, "Dashmaster");
            giveCharm32 = new ToggleButton(modeShopCharmsPage, "Quick Slash");
            giveCharm33 = new ToggleButton(modeShopCharmsPage, "Spell Twister");
            giveCharm34 = new ToggleButton(modeShopCharmsPage, "Deep Focus");
            giveCharm35 = new ToggleButton(modeShopCharmsPage, "Grubberfly's Elegy");
            giveCharm37 = new ToggleButton(modeShopCharmsPage, "Sprintmaster");
            giveCharm38 = new ToggleButton(modeShopCharmsPage, "Dreamshield");
            giveCharm39 = new ToggleButton(modeShopCharmsPage, "Weaversong");
            giveCharm40 = new ToggleButton(modeShopCharmsPage, "Grimmchild");
            giveCharm41 = new ToggleButton(modeShopCharmsPage, "Carefree Melody");
            charms = [giveCharm1, giveCharm2, giveCharm3, giveCharm4, giveCharm5, giveCharm6, giveCharm7, giveCharm8, giveCharm9, giveCharm10, giveCharm12, giveCharm13, giveCharm14, giveCharm15, giveCharm16, giveCharm17, giveCharm18, giveCharm19, giveCharm20, giveCharm21, giveCharm22, giveCharm23, giveCharm24, giveCharm25, giveCharm26, giveCharm27, giveCharm28, giveCharm29, giveCharm30, giveCharm31, giveCharm32, giveCharm33, giveCharm34, giveCharm35, giveCharm37, giveCharm38, giveCharm39, giveCharm40, giveCharm41];
            
            infectCrossroads = new ToggleButton(modeShopSabotagePage, "Infect Crossroads (2 Perks)");
            forcedHarm = new ToggleButton(modeShopSabotagePage, "Forced Harm (2 Perks)");
            forcedDecay = new ToggleButton(modeShopSabotagePage, "Forced Decay (1 Perk)");
            forcedStagnation = new ToggleButton(modeShopSabotagePage, "Forced Stagnation (1 Perk)");
            
            salubraCharm1 = new ToggleButton(modeSalubraFree, "Gathering Swarm");
            salubraCharm2 = new ToggleButton(modeSalubraFree, "Wayward Compass");
            salubraCharm3 = new ToggleButton(modeSalubraFree, "Grubsong");
            salubraCharm4 = new ToggleButton(modeSalubraFree, "Stalwart Shell");
            salubraCharm5 = new ToggleButton(modeSalubraFree, "Baldur Shell");
            salubraCharm6 = new ToggleButton(modeSalubraFree, "Fury of the Fallen");
            salubraCharm7 = new ToggleButton(modeSalubraFree, "Quick Focus");
            salubraCharm8 = new ToggleButton(modeSalubraFree, "Lifeblood Heart");
            salubraCharm9 = new ToggleButton(modeSalubraFree, "Lifeblood Core");
            salubraCharm10 = new ToggleButton(modeSalubraFree, "Defender's Crest");
            salubraCharm12 = new ToggleButton(modeSalubraFree, "Thorns of Agony");
            salubraCharm13 = new ToggleButton(modeSalubraFree, "Mark of Pride");
            salubraCharm14 = new ToggleButton(modeSalubraFree, "Steady Body");
            salubraCharm15 = new ToggleButton(modeSalubraFree, "Heavy Blow");
            salubraCharm16 = new ToggleButton(modeSalubraFree, "Sharp Shadow");
            salubraCharm17 = new ToggleButton(modeSalubraFree, "Spore Shroom");
            salubraCharm18 = new ToggleButton(modeSalubraFree, "Longnail");
            salubraCharm19 = new ToggleButton(modeSalubraFree, "Shaman Stone");
            salubraCharm20 = new ToggleButton(modeSalubraFree, "Soul Catcher");
            salubraCharm21 = new ToggleButton(modeSalubraFree, "Soul Eater");
            salubraCharm22 = new ToggleButton(modeSalubraFree, "Glowing Womb");
            salubraCharm23 = new ToggleButton(modeSalubraFree, "Unbreakable Heart");
            salubraCharm24 = new ToggleButton(modeSalubraFree, "Unbreakable Greed");
            salubraCharm25 = new ToggleButton(modeSalubraFree, "Unbreakable Strength");
            salubraCharm26 = new ToggleButton(modeSalubraFree, "Nailmaster's Glory");
            salubraCharm27 = new ToggleButton(modeSalubraFree, "Joni's Blessing");
            salubraCharm28 = new ToggleButton(modeSalubraFree, "Shape of Unn");
            salubraCharm29 = new ToggleButton(modeSalubraFree, "Hiveblood");
            salubraCharm30 = new ToggleButton(modeSalubraFree, "Dream Wielder");
            salubraCharm31 = new ToggleButton(modeSalubraFree, "Dashmaster");
            salubraCharm32 = new ToggleButton(modeSalubraFree, "Quick Slash");
            salubraCharm33 = new ToggleButton(modeSalubraFree, "Spell Twister");
            salubraCharm34 = new ToggleButton(modeSalubraFree, "Deep Focus");
            salubraCharm35 = new ToggleButton(modeSalubraFree, "Grubberfly's Elegy");
            salubraCharm37 = new ToggleButton(modeSalubraFree, "Sprintmaster");
            salubraCharm38 = new ToggleButton(modeSalubraFree, "Dreamshield");
            salubraCharm39 = new ToggleButton(modeSalubraFree, "Weaversong");
            salubraCharm40 = new ToggleButton(modeSalubraFree, "Grimmchild");
            salubraCharm41 = new ToggleButton(modeSalubraFree, "Carefree Melody");
            salubraCharms = [salubraCharm1, salubraCharm2, salubraCharm3, salubraCharm4, salubraCharm5, salubraCharm6, salubraCharm7, salubraCharm8, salubraCharm9, salubraCharm10, salubraCharm12, salubraCharm13, salubraCharm14, salubraCharm15, salubraCharm16, salubraCharm17, salubraCharm18, salubraCharm19, salubraCharm20, salubraCharm21, salubraCharm22, salubraCharm23, salubraCharm24, salubraCharm25, salubraCharm26, salubraCharm27, salubraCharm28, salubraCharm29, salubraCharm30, salubraCharm31, salubraCharm32, salubraCharm33, salubraCharm34, salubraCharm35, salubraCharm37, salubraCharm38, salubraCharm39, salubraCharm40, salubraCharm41];
        }

        public void SetCommander(ToggleButton button)
        {
            commandersBlue1.SetValue(false);
            commandersBlue2.SetValue(false);
            commandersBlue3.SetValue(false);
            commandersGreen1.SetValue(false);
            commandersGreen2.SetValue(false);
            commandersGreen3.SetValue(false);
            commandersRed1.SetValue(false);
            commandersRed2.SetValue(false);
            commandersRed3.SetValue(false);
            commandersOrange1.SetValue(false);
            commandersOrange2.SetValue(false);
            commandersOrange3.SetValue(false);

            button.SetValue(true);
            shopButton.Unlock();
            mapButton.Lock();
            startButton.Lock();
            salubraStart.Lock();

            // Set appropriate charms
            if (button == commandersBlue1)
                focusSpeed = 2f;
            else
            {
                focusSpeed = 2.5f;
            }
            if (button == commandersBlue2)
            {
                giveCharm23.SetValue(true);
                giveCharm23.Lock();
            }
            else
            {
                giveCharm23.Unlock();
                giveCharm23.SetValue(false);
            }
            if (button == commandersBlue3)
            {
                giveCharm9.SetValue(true);
                giveCharm9.Lock();
            }
            else
            {
                giveCharm9.Unlock();
                giveCharm9.SetValue(false);
            }
            if (button == commandersGreen1)
            {
                giveIsmas.SetValue(true);
                giveIsmas.Lock();
            }
            else
            {
                giveIsmas.Unlock();
                giveIsmas.SetValue(false);
            }
            if (button == commandersRed1)
            {
                flukeDiscount = true;
                playerCharms[10] = true;
                giveCharm19.SetValue(true);
                giveCharm19.Lock();
            }
            else
            {
                flukeDiscount = false;
                playerCharms[10] = false;
                giveCharm19.Unlock();
                giveCharm19.SetValue(false);
            }
            if (button == commandersRed2)
            {
                salubraCharm = true;
            }
            else
            {
                salubraCharm = false;
            }
            if (button == commandersRed3)
            {
                giveCharm34.SetValue(true);
                giveCharm34.Lock();
            }
            else
            {
                giveCharm34.Unlock();
                giveCharm34.SetValue(false);
            }
            if (button == commandersOrange1)
            {
                giveCharm24.SetValue(true);
                giveCharm24.Lock();
            }
            else
            {
                giveCharm24.Unlock();
                giveCharm24.SetValue(false);
            }
            if (button == commandersOrange1 || button == commandersOrange2)
            {

                sabotageButton.Unlock();
            }
            else
            {
                sabotageButton.Lock();
            }


            // Reset all Perk Shop purchases
            addMask1.SetValue(false);
            addMask2.SetValue(false);
            addVessel1.SetValue(false);
            addVessel2.SetValue(false);
            pureNail.SetValue(false);
            giveIsmas.SetValue(false);
            giveDGate.SetValue(false);
            addNotches1.SetValue(false);
            addNotches2.SetValue(false);
            addNotches3.SetValue(false);
            foreach (ToggleButton c in charms)
                c.SetValue(false);
            foreach (ToggleButton c in salubraCharms)
            {
                c.Unlock();
                c.SetValue(false);
            }
            giveGSlash.SetValue(false);
            giveDSlash.SetValue(false);
            giveCSlash.SetValue(false);
            giveSpirit1.SetValue(false);
            giveDive1.SetValue(false);
            giveWraiths1.SetValue(false);
            giveSpirit2.Unlock();
            giveSpirit2.SetValue(false);
            giveDive2.Unlock();
            giveDive2.SetValue(false);
            giveWraiths2.Unlock();
            giveWraiths2.SetValue(false);
            huntFocus1.SetValue(false);
            huntFocus2.SetValue(false);
            if (button == commandersGreen1)
                perkPoints = 1;
            else if (button == commandersGreen2)
                perkPoints = 5;
            else
                perkPoints = 3;
            shopTitle.Destroy();
            shopTitle = new MenuLabel(modeShopPage, "Perk Shop - [" + perkPoints + " Perks Remaining]");
            shopTitle.Translate(new Vector2(0, 420));
            shopCharmsTitle.Destroy();
            shopCharmsTitle = new MenuLabel(modeShopCharmsPage, "Charms - [" + perkPoints + " Perks Remaining]");
            shopCharmsTitle.Translate(new Vector2(0, 300));
        }

        public void ShopPurchase(ToggleButton button)
        {
            button.SetValue(!button.Value);
            // Free Sanguine Salubra charm
            if (salubraCharms.Contains(button))
            {
                if (!button.Value)
                {
                    button.SetValue(true);
                    foreach (ToggleButton c in salubraCharms)
                    {
                        c.Lock();
                        //Log("Locked " + c);
                    }
                    button.Unlock();
                    salubraStart.Unlock();
                }
                else
                {
                    button.SetValue(false);
                    foreach (ToggleButton c in salubraCharms)
                        c.Unlock();
                    int lockIndex;
                    foreach (ToggleButton c in charms)
                    {
                        if (c.Value)
                        {
                            lockIndex = Array.IndexOf(charms, c);
                            salubraCharms[lockIndex].SetValue(true);
                            salubraCharms[lockIndex].Lock();
                            if (c == giveCharm40)
                                salubraCharm41.Lock();
                            if (c == giveCharm41)
                                salubraCharm40.Lock();
                        }
                    }
                    salubraStart.Lock();
                }
            }
            
            // Purchasing system
            if (button == addMask1 || button == addMask2 || button == addVessel1 || button == addVessel2 || button == pureNail || button == giveIsmas || button == giveDGate || button == addNotches1 || button == addNotches2 || button == addNotches3 || charms.Contains(button) || button == giveGSlash || button == giveDSlash || button == giveCSlash || button == huntFocus1 || button == huntFocus2 || button == forcedDecay || button == forcedStagnation)
            {
                if (!button.Value && perkPoints >= 1)
                {
                    button.SetValue(true);
                    perkPoints--;
                }
                else if (button.Value)
                {
                    button.SetValue(false);
                    perkPoints++;
                }
                if (button == giveCharm40 && button.Value)
                    giveCharm41.Lock();
                else if (button == giveCharm40 && !button.Value)
                    giveCharm41.Unlock();
                if (button == giveCharm41 && button.Value)
                    giveCharm40.Lock();
                else if (button == giveCharm41 && !button.Value)
                    giveCharm40.Unlock();
            }
            if (button == giveSpirit1 || button == giveDive1 || button == giveWraiths1 || button == infectCrossroads || button == forcedHarm)
            {
                if (!button.Value && (perkPoints >= 2 || (flukeDiscount && perkPoints >= 1)))
                {
                    button.SetValue(true);
                    if (flukeDiscount)
                        perkPoints--;
                    else
                        perkPoints-=2;
                }
                else if (button.Value)
                {
                    button.SetValue(false);
                    if (flukeDiscount)
                        perkPoints++;
                    else
                        perkPoints+=2;
                }
            }
            if (button == giveSpirit2)
            {
                if (!button.Value && (perkPoints >= 3 || (flukeDiscount && perkPoints >= 1)))
                {
                    button.SetValue(true);
                    if (flukeDiscount)
                        perkPoints--;
                    else
                        perkPoints -= 3;
                    giveDive2.Lock();
                    giveWraiths2.Lock();
                }
                else if (button.Value)
                {
                    button.SetValue(false);
                    if (flukeDiscount)
                        perkPoints++;
                    else
                        perkPoints += 3;
                    giveDive2.Unlock();
                    giveWraiths2.Unlock();
                }
            }
            if (button == giveDive2)
            {
                if (!button.Value && (perkPoints >= 4 || (flukeDiscount && perkPoints >= 1)))
                {
                    button.SetValue(true);
                    if (flukeDiscount)
                        perkPoints--;
                    else
                        perkPoints -= 4;
                    giveSpirit2.Lock();
                    giveWraiths2.Lock();
                }
                else if (button.Value)
                {
                    button.SetValue(false);
                    if (flukeDiscount)
                        perkPoints++;
                    else
                        perkPoints += 4;
                    giveSpirit2.Unlock();
                    giveWraiths2.Unlock();
                }
            }
            if (button == giveWraiths2)
            {
                if (!button.Value && (perkPoints >= 3 || (flukeDiscount && perkPoints >= 1)))
                {
                    button.SetValue(true);
                    if (flukeDiscount)
                        perkPoints--;
                    else
                        perkPoints -= 3;
                    giveSpirit2.Lock();
                    giveDive2.Lock();
                }
                else if (button.Value)
                {
                    button.SetValue(false);
                    if (flukeDiscount)
                        perkPoints++;
                    else
                        perkPoints += 3;
                    giveSpirit2.Unlock();
                    giveDive2.Unlock();
                }
            }

            shopTitle.Destroy();
            shopTitle = new MenuLabel(modeShopPage, "Perk Shop - [" + perkPoints + " Perks Remaining]");
            shopTitle.Translate(new Vector2(0, 420));
            shopCharmsTitle.Destroy();
            shopCharmsTitle = new MenuLabel(modeShopCharmsPage, "Charms - [" + perkPoints + " Perks Remaining]");
            shopCharmsTitle.Translate(new Vector2(0, 300));
            shopSabotageTitle.Destroy();
            shopSabotageTitle = new MenuLabel(modeShopSabotagePage, "Sabotages - ["+perkPoints+" Perks Remaining]");
            shopSabotageTitle.Translate(new Vector2(0, 300));

            if (perkPoints == 0)
                mapButton.Unlock();
            else
            {
                playableArea = null;
                mapButton.Lock();
                startButton.Lock();
            }
        }

        public void SetMap(ToggleButton button)
        {
            dirtmouthCrossroads.SetValue(false);
            dirtmouthPeak.SetValue(false);
            edgeHive.SetValue(false);
            spiritsDescent.SetValue(false);

            button.SetValue(true);
            startButton.Unlock();

            if (button == dirtmouthCrossroads)
                playableArea = "dirtmouthcrossroads";
            if (button == dirtmouthPeak)
                playableArea = "dirtmouthpeak";
            if (button == edgeHive)
                playableArea = "edgehive";
            if (button == spiritsDescent)
                playableArea = "spiritsdescent";
        }

        public void SalubraCheck()
        {
            if (salubraCharm)
            {
                startButton.AddHideAndShowEvent(modeConfigPage, modeSalubraFree);
                int lockIndex;
                foreach (ToggleButton c in salubraCharms)
                {
                    c.Unlock();
                    c.SetValue(false);
                }
                foreach (ToggleButton c in charms)
                {
                    if (c.Value)
                    {
                        lockIndex = Array.IndexOf(charms, c);
                        salubraCharms[lockIndex].SetValue(true);
                        salubraCharms[lockIndex].Lock();
                        if (c == giveCharm40)
                            salubraCharm41.Lock();
                        if (c == giveCharm41)
                            salubraCharm40.Lock();
                    }
                }
            }
            else
                StartGame();
            
        }
    }
}