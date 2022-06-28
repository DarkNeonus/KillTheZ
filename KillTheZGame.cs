﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

using KTZEngine;
using Vector2 = Vector2Namespace.Vector2;


namespace KillTheZGame
{
    public class GameShell
    {
        public static KillTheZGame aplic = new ();
        public static void Main() { aplic.MainProtector(); }
    }

    public class KillTheZGame
    {
        

        public void MyAplicationInit() 
        {

            Console.CursorVisible = false;
            Console.CursorSize = 1;

            GameData.myGameWindowWidth = Console.LargestWindowWidth;
            GameData.myGameWindowHeight = Console.LargestWindowHeight;

            KTZEngineAplication.windowWidth = GameData.myGameWindowWidth;
            KTZEngineAplication.windowHeight = GameData.myGameWindowHeight;

            GameData.myGameWidth = GameData.myGameWindowWidth - 20;
            GameData.myGameHeight = GameData.myGameWindowHeight - 10;

            GameData.aplication.Init();

            Console.OutputEncoding = Encoding.Unicode;


            ConsoleFullScreen.SetMode(3);


            GameData.english = new Language("en");
            GameData.ukraine = new Language("ua");
            GameData.polish = new Language("pl");
            
            LanguagesInit();
            

            GameData.originalMyGame = new MyGame(ref GameData.aplication, new Vector2(GameData.myGameWindowWidth, GameData.myGameWindowHeight), "Game", 40);
            GameData.myGameShell = new MyGameShell(ref GameData.originalMyGame, MyGameTabBehavior, new Vector2((int)((GameData.myGameWindowWidth - GameData.myGameWidth) / 2), (int)((GameData.myGameWindowHeight - GameData.myGameHeight) / 2)), new Vector2(GameData.myGameWidth, GameData.myGameHeight) );
            

            GameData.mainMenu = new MainMenu();
            GameData.tutorialMenu = new TutorialMenu();
            GameData.settingsMenu = new SettingsMenu();

            GameData.aplication.tabsBehavior.Add(GameData.mainMenu.menuTab.name, MainMenuBehavior);
            GameData.aplication.tabsBehavior.Add(GameData.settingsMenu.name, SettingsMenuBehavior);

            GameData.lostCityTab = new("LostCityTab", new List<SLTItem>() { new SLTItem("Message1", GameData.aplication.gameText.GetText("YouLost"), GameData.EmptyMethod, 0) });
            GameData.captureCityTab = new("CaptureCityTab", new List<SLTItem>() { new SLTItem("Message1", GameData.aplication.gameText.GetText("Congratulations"), GameData.EmptyMethod, 0), new SLTItem("Message2", GameData.aplication.gameText.GetText("YouWin"), GameData.EmptyMethod, 0) });

            GameData.finalWinTab = new FinalWinTab();
            GameData.aplication.tabsBehavior.Add(GameData.finalWinTab.name, FinalWinTabBehavior);
            GameData.moscowDecideFateTab = new DecideFateTab();
            GameData.aplication.tabsBehavior.Add(GameData.moscowDecideFateTab.name, DecidemoscowFateTabBehavior);
            GameData.warStartTab = new WarStartTab();
            GameData.aplication.tabsBehavior.Add(GameData.warStartTab.name, WarStartTabTabBehavior);

            GameData.aplication.tabsBehavior.Add(GameData.lostCityTab.name, LoseCityTabBehavior);
            GameData.aplication.tabsBehavior.Add(GameData.captureCityTab.name, CaptureCityTabBehavior);

            TextUpdate();
        }

        public void MyAplicationStart()
        {
            GameData.aplication.currentGameTabName = GameData.mainMenu.menuTab.name;
            Console.CursorVisible = false;
            Console.CursorSize = 1;
        }

        public void MainProtector()
        {
            MyAplicationInit();
            MyAplicationStart();

            while (GameData.aplication.isInAplication)
            {
                GameData.aplication.Update();
            }
        }

        public void MyGameTabBehavior()
        {
            // KeyManager.ClearKeyPressBuffer();

            // GameData.myGameShell.PreUpdate();
            // GameData.myGameShell.Update();
        }

        public void MainMenuBehavior()
        {
            GameData.mainMenu.menuTab.Update();
            GameData.mainMenu.menuTab.Draw();
            
        }
        public void SettingsMenuBehavior()
        {
            GameData.settingsMenu.Update();
            GameData.settingsMenu.Draw();
        }

        public void LoseCityTabBehavior() { GameData.lostCityTab.Update(); GameData.lostCityTab.Draw(); }
        public void CaptureCityTabBehavior() { GameData.captureCityTab.Update(); GameData.captureCityTab.Draw(); }


        public void FinalWinTabBehavior()
        {
            GameData.finalWinTab.Update();
            GameData.finalWinTab.Draw();
        }
        public void DecidemoscowFateTabBehavior()
        {
            GameData.moscowDecideFateTab.Update();
            GameData.moscowDecideFateTab.Draw();
        }
        public void WarStartTabTabBehavior()
        {
            GameData.warStartTab.Update();
            GameData.warStartTab.Draw();
        }

        public void LanguagesInit()
        {
            GameData.aplication.gameText = new GameText(GameData.ukraine.name, new List<Language>() { GameData.english, GameData.ukraine, GameData.polish });
            GameData.aplication.gameText.AddSomeText(new Dictionary<string, Dictionary<string, string>>() 
            {
                {"MainMenu", new Dictionary<string, string>() { { GameData.english.name, "Main Menu" }, { GameData.ukraine.name, "Головне Меню" }, { GameData.polish.name, "Menu Główne" } } },
                {"StartGame", new Dictionary<string, string>() { { GameData.english.name, "New Game" }, { GameData.ukraine.name, "Нова гра" }, { GameData.polish.name, "Nowa gra" } } },
                {"ContinueGame", new Dictionary<string, string>() { { GameData.english.name, "Continue Game" }, { GameData.ukraine.name, "Продовжити Гру" }, { GameData.polish.name, "Kontynuuj grę" } } },
                {"Tutorial", new Dictionary<string, string>() { { GameData.english.name, "Tutorial" }, { GameData.ukraine.name, "Підручник" }, { GameData.polish.name, "Podręcznik" } } },
                {"Settings", new Dictionary<string, string>() { { GameData.english.name, "Settings" }, { GameData.ukraine.name, "Налаштування" }, { GameData.polish.name, "Ustawienia" } } },
                {"Credits", new Dictionary<string, string>() { { GameData.english.name, "Credits" }, { GameData.ukraine.name, "Автори" }, { GameData.polish.name, "Autorski" } } },
                {"Exit", new Dictionary<string, string>() { { GameData.english.name, "Exit" }, { GameData.ukraine.name, "Вихід" }, { GameData.polish.name, "Wyjście" } } },

                {"NowDecideFatemoscow", new Dictionary<string, string>() { { GameData.english.name, "Now you must decide fate of moscow" }, { GameData.ukraine.name, "Тепер вам належить вирішити долю москви" }, { GameData.polish.name, "Now you must decide fate of moscow " } } },


                {"FateDestroymoscow", new Dictionary<string, string>() { { GameData.english.name, "Destroy [ plus 100000 to score ]" }, { GameData.ukraine.name, "Стрерти з лиця землі [ плюс 100000 до очок ]" }, { GameData.polish.name, "Destroy [ plus 100000 to score ]" } } },
                {"FateUkrainianizemoscow", new Dictionary<string, string>() { { GameData.english.name, "Ukrainianize [ colony will multiply score by 1.3 times ]" }, { GameData.ukraine.name, "Українізувати [ колонія примножить очки у 1.3 рази ]" }, { GameData.polish.name, "Ukrainianize [ colony will multiply score by 1.3 times ]" } } },


                {"GameTheme", new Dictionary<string, string>() { { GameData.english.name, "Game Theme:" }, { GameData.ukraine.name, "Тема гри:" }, { GameData.polish.name, "Ustawienia" } } },
                
                {"ThemeDark", new Dictionary<string, string>() { { GameData.english.name, "Dark" }, { GameData.ukraine.name, "Темна" }, { GameData.polish.name, "Dark" } } },
                {"ThemeLight", new Dictionary<string, string>() { { GameData.english.name, "Light" }, { GameData.ukraine.name, "Світла" }, { GameData.polish.name, "Light" } } },
                {"ThemeDoom", new Dictionary<string, string>() { { GameData.english.name, "Doom" }, { GameData.ukraine.name, "Doom" }, { GameData.polish.name, "Doom" } } },
                {"ThemeHell", new Dictionary<string, string>() { { GameData.english.name, "Hell" }, { GameData.ukraine.name, "Пекло" }, { GameData.polish.name, "Hell" } } },
                {"ThemeHecker", new Dictionary<string, string>() { { GameData.english.name, "Hecker" }, { GameData.ukraine.name, "Хакер" }, { GameData.polish.name, "Hecker" } } },
                {"ThemePatriotic", new Dictionary<string, string>() { { GameData.english.name, "Patriotic" }, { GameData.ukraine.name, "Патріотична" }, { GameData.polish.name, "Patriotic" } } },
                {"MyLittlePony", new Dictionary<string, string>() { { GameData.english.name, "MyLittlePony" }, { GameData.ukraine.name, "MyLittlePony" }, { GameData.polish.name, "MyLittlePony" } } },

                {"GameLanguage", new Dictionary<string, string>() { { GameData.english.name, "Game Language:" }, { GameData.ukraine.name, "Мова гри:" }, { GameData.polish.name, "Autorski" } } },
                {"ClearGameProgress", new Dictionary<string, string>() { { GameData.english.name, "Clear Game Progress" }, { GameData.ukraine.name, "Видалити Ігровий Прогрес" }, { GameData.polish.name, "Wyjście" } } },
                
                {"PressEnterSpaceDeleteData", new Dictionary<string, string>() { { GameData.english.name, "Press Enter or Space to clear game progress" }, { GameData.ukraine.name, "Натисніть Enter або Space(Укр.Пробіл) щоб очистити прогрес гри" }, { GameData.polish.name, "Press Enter or Space to clear game progress" } } },


                {"YouLost", new Dictionary<string, string>() { { GameData.english.name, "You lost the city" }, { GameData.ukraine.name, "Ви втратили місто" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"Congratulations", new Dictionary<string, string>() { { GameData.english.name, "Congratulations" }, { GameData.ukraine.name, "Вітаємо" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"YouWin", new Dictionary<string, string>() { { GameData.english.name, "You have captured the city" }, { GameData.ukraine.name, "Ви захопили місто" }, { GameData.polish.name, "KillTheZ tutorial" } } },

                {"Today", new Dictionary<string, string>() { { GameData.english.name, "Today " }, { GameData.ukraine.name, "Сьогодні " }, { GameData.polish.name, "Today " } } },
                {"IsHistoricDay", new Dictionary<string, string>() { { GameData.english.name, " is a historic day" }, { GameData.ukraine.name, " - історичний день" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"YouCapturemoscow", new Dictionary<string, string>() { { GameData.english.name, "Today you capture moscow" }, { GameData.ukraine.name, "Сьогодні ви захопили москву" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"YouFinWinWar", new Dictionary<string, string>() { { GameData.english.name, "and you finally won the war that started in " }, { GameData.ukraine.name, "і ви нарешті виграли війну, яка почалася " }, { GameData.polish.name, "KillTheZ tutorial" } } },

                {"StartWar1", new Dictionary<string, string>() { { GameData.english.name, "All the destroyed cities were rebuilt and the people returned home" }, { GameData.ukraine.name, "Всі зруйновані міста були відбудовані, а люди повернуті додому" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar2", new Dictionary<string, string>() { { GameData.english.name, "And everyone lived well, until " }, { GameData.ukraine.name, "І усі жили добре, до " }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar3", new Dictionary<string, string>() { { GameData.english.name, "On this day, a group of religious and political fanatics led by the leader," }, { GameData.ukraine.name, "Цього дня група релігійно-політичних фанатиків на чолі з вождем," }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar4", new Dictionary<string, string>() { { GameData.english.name, "which have recently begun to gather in flocks in northeastern Asia," }, { GameData.ukraine.name, "які недавно почали збиратися зграями в північно-східній частині Азії," }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar5", new Dictionary<string, string>() { { GameData.english.name, "decided that they did not like neighbors better than them" }, { GameData.ukraine.name, "вирішили, що їм не подобаються сусіди, кращі за них" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar6", new Dictionary<string, string>() { { GameData.english.name, "and began to partially seize the territory of our state." }, { GameData.ukraine.name, "і почали почастинно загарбувати території нашої держави." }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar7", new Dictionary<string, string>() { { GameData.english.name, "This time, the Ukrainians knew whose descendants they were and did not wait long to fight back." }, { GameData.ukraine.name, "Цього разу, українці знали чиї це нащадки і довго не чекали щоб дати відсіч." }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar8", new Dictionary<string, string>() { { GameData.english.name, "Because there were so many fanatics and they didn't care about their lives," }, { GameData.ukraine.name, "Оскільки фанатиків було дуже багато і їм було байдуже на свої життя," }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar9", new Dictionary<string, string>() { { GameData.english.name, "people who remember the history that a similar situation had already occurred" }, { GameData.ukraine.name, "люди памятаючі історію згадали, що схожа ситуація вже відбувалася" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar10", new Dictionary<string, string>() { { GameData.english.name, "and in a few days they found you in warehouses - artificial intelligence," }, { GameData.ukraine.name, "і за кілька днів знайшли на складах вас - штучний інтелект," }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar11", new Dictionary<string, string>() { { GameData.english.name, "which was developed earlier by professional Ukrainian programmers" }, { GameData.ukraine.name, "який був розроблений давніше професійними українськими програмістами" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar12", new Dictionary<string, string>() { { GameData.english.name, "and launched into the latest combat vehicle" }, { GameData.ukraine.name, "і запущений у новітню бойову машину" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar13", new Dictionary<string, string>() { { GameData.english.name, "specifically for the extermination of large groups of orcs." }, { GameData.ukraine.name, "спеціально для винищення великих груп орків." }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar14", new Dictionary<string, string>() { { GameData.english.name, "Now your new task is to capture the most important place of their accumulation," }, { GameData.ukraine.name, "Тепер ваша нова задача - це захопити найважливіше місце їхнього скупчення," }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar15", new Dictionary<string, string>() { { GameData.english.name, "namely, a multi-kilometer open temple with a fanatically chosen name - moscow" }, { GameData.ukraine.name, "а саме багатокілометровий відкритий храм із фанатично підібраною назвою - москва" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"StartWar16", new Dictionary<string, string>() { { GameData.english.name, "Good luck!" }, { GameData.ukraine.name, "Удачі!" }, { GameData.polish.name, "KillTheZ tutorial" } } },



                {"PressEscape", new Dictionary<string, string>() { { GameData.english.name, "Press Escape to go back to menu" }, { GameData.ukraine.name, "Нажміть Escape щоб вийти в меню" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"PressEnter", new Dictionary<string, string>() { { GameData.english.name, "Press Enter or Space to continue game" }, { GameData.ukraine.name, "Нажміть Enter або Space(Укр.Пробіл) щоб продовжити гру" }, { GameData.polish.name, "KillTheZ tutorial" } } },

                {"PressEnterToContinue", new Dictionary<string, string>() { { GameData.english.name, "Press Enter or Space to continue" }, { GameData.ukraine.name, "Нажміть Enter або Space(Укр.Пробіл) щоб продовжити" }, { GameData.polish.name, "KillTheZ settings" } } },


                {"TutTitle", new Dictionary<string, string>() { { GameData.english.name, "KillTheZ tutorial" }, { GameData.ukraine.name, "KillTheZ підручник" }, { GameData.polish.name, "KillTheZ tutorial" } } },
                {"SettTitle", new Dictionary<string, string>() { { GameData.english.name, "KillTheZ settings" }, { GameData.ukraine.name, "KillTheZ настройки" }, { GameData.polish.name, "KillTheZ settings" } } },

                {"Tut1_2", new Dictionary<string, string>() { { GameData.english.name, "<Previor Page            1                Next Page>" }, { GameData.ukraine.name, "<Минула Сторінка                 1                Наступна Сторінка>" }, { GameData.polish.name, "<Previor Page            1                Next Page>" } } },
                {"Tut1_3", new Dictionary<string, string>() { { GameData.english.name, "────────────────── Mission ──────────────────" }, { GameData.ukraine.name, "────────────────── Місія ──────────────────" }, { GameData.polish.name, "────────────────── Mission ──────────────────" } } },
                {"Tut1_4", new Dictionary<string, string>() { { GameData.english.name, "Your have only 2 main missions:" }, { GameData.ukraine.name, "У вас є тільки 2 основні місії:" }, { GameData.polish.name, "Your have only 2 main missions:" } } },
                {"Tut1_5", new Dictionary<string, string>() { { GameData.english.name, "1) Capture moscow" }, { GameData.ukraine.name, "1) Захопити москву" }, { GameData.polish.name, "1) Capture moscow" } } },
                {"Tut1_6", new Dictionary<string, string>() { { GameData.english.name, "2) Collect more scores" }, { GameData.ukraine.name, "2) Назбирати більше очок" }, { GameData.polish.name, "2) Collect more scores" } } },
                {"Tut1_7", new Dictionary<string, string>() { { GameData.english.name, "Good luck!" }, { GameData.ukraine.name, "Удачі!" }, { GameData.polish.name, "Good luck" } } },

                {"Tut2_2", new Dictionary<string, string>() { { GameData.english.name, "<Previor Page            2                Next Page>" }, { GameData.ukraine.name, "<Минула Сторінка                 2                Наступна Сторінка>" }, { GameData.polish.name, "<Previor Page            2                Next Page>" } } },
                {"Tut2_3", new Dictionary<string, string>() { { GameData.english.name, "────────────────── Player Control ──────────────────" }, { GameData.ukraine.name, "────────────────── Керування гравцем ──────────────────" }, { GameData.polish.name, "────────────────── Player Control ──────────────────" } } },
                {"Tut2_4", new Dictionary<string, string>() { { GameData.english.name, "Player" }, { GameData.ukraine.name, "Гравець" }, { GameData.polish.name, "Player" } } },
                {"Tut2_5", new Dictionary<string, string>() { { GameData.english.name, "Press D or Right Arrow to move right" }, { GameData.ukraine.name, "Нажміть D(Укр.В) або Стрілку Вправо щоб рухатися праворуч" }, { GameData.polish.name, "Press D or Right Arrow to move right" } } },
                {"Tut2_7", new Dictionary<string, string>() { { GameData.english.name, "Press A or Left Arrow to move left" }, { GameData.ukraine.name, "Нажміть A(Укр.Ф) або Стрілку Вліво щоб рухатися ліворуч" }, { GameData.polish.name, "Press A or Left Arrow to move left" } } },
                {"Tut2_9", new Dictionary<string, string>() { { GameData.english.name, "Press W or Up Arrow to move up" }, { GameData.ukraine.name, "Нажміть W(Укр.Ц) або Стрілку Вгору щоб рухатися вгору" }, { GameData.polish.name, "Press W or Up Arrow to move up" } } },
                {"Tut2_11", new Dictionary<string, string>() { { GameData.english.name, "Press S or Down Arrow to move down" }, { GameData.ukraine.name, "Нажміть S(Укр.І) або Стрілку Вниз щоб рухатися вниз" }, { GameData.polish.name, "Press S or Down Arrow to move down" } } },
                {"Tut2_13", new Dictionary<string, string>() { { GameData.english.name, "Press Space to shoot" }, { GameData.ukraine.name, "Нажміть Space(Укр.Пробіл) щоб стріляти" }, { GameData.polish.name, "Press Space to shoot" } } },
                {"Tut2_15", new Dictionary<string, string>() { { GameData.english.name, "Press E to plant mine" }, { GameData.ukraine.name, "Нажміть E(Укр.У) щоб встановити міну" }, { GameData.polish.name, "Press E to place mine" } } },
                {"Tut2_17", new Dictionary<string, string>() { { GameData.english.name, "Press Q to start Bayraktar attack" }, { GameData.ukraine.name, "Нажміть Q(Укр.Й) щоб почати атаку Байрактарів" }, { GameData.polish.name, "Press Q to start Bayraktar attack" } } },

                {"Tut3_2", new Dictionary<string, string>() { { GameData.english.name, "<Previor Page            3                Next Page>" }, { GameData.ukraine.name, "<Минула Сторінка                 3                Наступна Сторінка>" }, { GameData.polish.name, "<Previor Page            3                Next Page>" } } },
                {"Tut3_3", new Dictionary<string, string>() { { GameData.english.name, "────────────────── Companions ──────────────────" }, { GameData.ukraine.name, "────────────────── Напарники ──────────────────" }, { GameData.polish.name, "────────────────── Friends ──────────────────" } } },
                {"Tut3_4", new Dictionary<string, string>() { { GameData.english.name, "Tractor driver Mykola" }, { GameData.ukraine.name, "Тракторист Микола" }, { GameData.polish.name, "Tractor driver Mykola" } } },
                {"Tut3_6", new Dictionary<string, string>() { { GameData.english.name, "Tractor driver Mykola can easily eat enemy tanks with his tractor" }, { GameData.ukraine.name, "Тракторист Микола своїм трактором може з легкістю їсти ворожі танки" }, { GameData.polish.name, "Press D or Right Arrow to move left" } } },
                {"Tut3_7", new Dictionary<string, string>() { { GameData.english.name, "He is usually slow and calm," }, { GameData.ukraine.name, "Зазвичай він повільний і спокійний," }, { GameData.polish.name, "Press A or Left Arrow to move left" } } },
                {"Tut3_8", new Dictionary<string, string>() { { GameData.english.name, "but when he notices an enemy tank, that will change" }, { GameData.ukraine.name, "але коли він помітить ворожий танк, це зміниться" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut3_9", new Dictionary<string, string>() { { GameData.english.name, "Ukrainian soldiers" }, { GameData.ukraine.name, "Українські солдати" }, { GameData.polish.name, "Ukrainian soldiers" } } },
                {"Tut3_11", new Dictionary<string, string>() { { GameData.english.name, "Valiant heroes who protect civilians from orcs" }, { GameData.ukraine.name, "Доблесні герої які захищають цивільних людей від орків" }, { GameData.polish.name, "Valiant heroes who protect civilians from orcs" } } },
                {"Tut3_12", new Dictionary<string, string>() { { GameData.english.name, "These daredevils can:" }, { GameData.ukraine.name, "Ці сміливці можуть:" }, { GameData.polish.name, "These daredevils can:" } } },
                {"Tut3_13", new Dictionary<string, string>() { { GameData.english.name, "1) Shoot the enemies at a distance of up to " }, { GameData.ukraine.name, "1) Стріляти у ворогів на відстані до " }, { GameData.polish.name, "1) Shoot the enemies at a distance of up to " } } },
                {"Tut3_14", new Dictionary<string, string>() { { GameData.english.name, "2) Plant on the map flashing surprises for enemies" }, { GameData.ukraine.name, "2) Ставити на карті миготливі сюрпризи для ворогів" }, { GameData.polish.name, "2) Plant on the map flashing surprises for enemies" } } },

                {"Tut4_2", new Dictionary<string, string>() { { GameData.english.name, "<Previor Page            4                Next Page>" }, { GameData.ukraine.name, "<Минула Сторінка                 4                Наступна Сторінка>" }, { GameData.polish.name, "<Previor Page            4                Next Page>" } } },
                {"Tut4_3", new Dictionary<string, string>() { { GameData.english.name, "────────────────── Enemy ──────────────────" }, { GameData.ukraine.name, "────────────────── Вороги ──────────────────" }, { GameData.polish.name, "────────────────── Friends ──────────────────" } } },
                {"Tut4_4", new Dictionary<string, string>() { { GameData.english.name, "Shitty russian tank" }, { GameData.ukraine.name, "Лайняні російські танки" }, { GameData.polish.name, "Tractor driver Mykola" } } },
                {"Tut4_6", new Dictionary<string, string>() { { GameData.english.name, "These tanks are cans that can barely drive" }, { GameData.ukraine.name, "Ці танки - це бляшанки, які ледве можуть їхати" }, { GameData.polish.name, "Press D or Right Arrow to move left" } } },
                {"Tut4_7", new Dictionary<string, string>() { { GameData.english.name, "They can't do anything advanced for Ukrainian vehicles," }, { GameData.ukraine.name, "Вони не можуть зашкодити просунутій українській техніці," }, { GameData.polish.name, "Press A or Left Arrow to move left" } } },
                {"Tut4_8", new Dictionary<string, string>() { { GameData.english.name, "and the only thing they realy can do is destroy a car" }, { GameData.ukraine.name, "і єдине, що вони реально можуть - це переїхати легкове авто," }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut4_9", new Dictionary<string, string>() { { GameData.english.name, "or drive to civilians in the yard" }, { GameData.ukraine.name, "або заїхати у двір мирним людям" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },

                {"Tut5_2", new Dictionary<string, string>() { { GameData.english.name, "<Previor Page            5                Next Page>" }, { GameData.ukraine.name, "<Минула Сторінка                 5                Наступна Сторінка>" }, { GameData.polish.name, "<Previor Page            5                Next Page>" } } },
                {"Tut5_3", new Dictionary<string, string>() { { GameData.english.name, "────────────────── Objects on map ──────────────────" }, { GameData.ukraine.name, "────────────────── Об'єкти на карті ──────────────────" }, { GameData.polish.name, "────────────────── Friends ──────────────────" } } },
                {"Tut5_4", new Dictionary<string, string>() { { GameData.english.name, "House" }, { GameData.ukraine.name, "Будинок" }, { GameData.polish.name, "Tractor driver Mykola" } } },
                {"Tut5_6", new Dictionary<string, string>() { { GameData.english.name, "A simple house used as a wall" }, { GameData.ukraine.name, "Простий будинок, який використовується як стіна" }, { GameData.polish.name, "Press D or Right Arrow to move left" } } },
                {"Tut5_7", new Dictionary<string, string>() { { GameData.english.name, "City important zone" }, { GameData.ukraine.name, "Важлива міська зона" }, { GameData.polish.name, "Press A or Left Arrow to move left" } } },
                {"Tut5_9", new Dictionary<string, string>() { { GameData.english.name, "Enemy spawns on top zone" }, { GameData.ukraine.name, "Вороги зявляються у верхній зоні" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_10", new Dictionary<string, string>() { { GameData.english.name, "If enemy reach the bottom zone you lose the current city" }, { GameData.ukraine.name, "Якщо ворог досягне нижньої зони, ви втрачаєте поточне місто" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_11", new Dictionary<string, string>() { { GameData.english.name, "Crater" }, { GameData.ukraine.name, "Кратер" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_13", new Dictionary<string, string>() { { GameData.english.name, "Consequences of the explosion of ruscists missiles" }, { GameData.ukraine.name, "Наслідки вибуху рашистських ракет" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_14", new Dictionary<string, string>() { { GameData.english.name, "Slows down enemies and your soldiers" }, { GameData.ukraine.name, "Сповільнює ворогів і ваших солдатів" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_15", new Dictionary<string, string>() { { GameData.english.name, "Hedgehog" }, { GameData.ukraine.name, "Їжачок" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_17", new Dictionary<string, string>() { { GameData.english.name, "Very useful thing" }, { GameData.ukraine.name, "Дуже корисна штука" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_18", new Dictionary<string, string>() { { GameData.english.name, "Biolaboratory" }, { GameData.ukraine.name, "Біолабораторія" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },
                {"Tut5_20", new Dictionary<string, string>() { { GameData.english.name, "A very common thing in the Ukrainian lands" }, { GameData.ukraine.name, "Дуже поширена річ в Українських краях" }, { GameData.polish.name, "Press W or Up Arrow to move left" } } },

                {"MineCount", new Dictionary<string, string>() { { GameData.english.name, "    Mine count: " }, { GameData.ukraine.name, "Кількість мін: " }, { GameData.polish.name, "    Mine count: " } } },
                {"BayraktarAttackCount", new Dictionary<string, string>() { { GameData.english.name, "    Bayraktar attack count: " }, { GameData.ukraine.name, "    Доступно атак Байрактарів: " }, { GameData.polish.name, "    Bayraktar attack count: " } } },
                {"CurrentTime", new Dictionary<string, string>() { { GameData.english.name, "Current Time: " }, { GameData.ukraine.name, "Поточний час: " }, { GameData.polish.name, "Current Time: " } } },
                {"ms", new Dictionary<string, string>() { { GameData.english.name, "ms" }, { GameData.ukraine.name, "мс" }, { GameData.polish.name, "ms" } } },
                {"AttackStartTime", new Dictionary<string, string>() { { GameData.english.name, "    Attack start time: " }, { GameData.ukraine.name, "    Час початку наступу: " }, { GameData.polish.name, "    Attack start time: " } } },
                {"EnemyLeftSpawn", new Dictionary<string, string>() { { GameData.english.name, "Enemy left to spawn: " }, { GameData.ukraine.name, "Ворогів прибуде: " }, { GameData.polish.name, "Enemy left to spawn: " } } },
                {"EnemyLeftKill", new Dictionary<string, string>() { { GameData.english.name, "    Enemy left to kill: " }, { GameData.ukraine.name, "    Ворогів залишилося вбити: " }, { GameData.polish.name, "    Enemy left to kill: " } } },
                {"Score", new Dictionary<string, string>() { { GameData.english.name, "Score: " }, { GameData.ukraine.name, "Очки: " }, { GameData.polish.name, "Score: " } } },

            });

            
        }
        public void TextUpdate()
        {
            GameData.tutorailTabPages = new List<List<SLTItem>>()
            {
                new List<SLTItem>()
                {

                    new SLTItem("1_0", GameData.aplication.gameText.GetText("TutTitle"), GameData.EmptyMethod, 1),
                    new SLTItem("1_1", "────────────────────────────────────────────────────────────",GameData.EmptyMethod, 1),
                    new SLTItem("1_2", GameData.aplication.gameText.GetText("Tut1_2"), GameData.EmptyMethod, 1),
                    new SLTItem("1_3", GameData.aplication.gameText.GetText("Tut1_3"), GameData.EmptyMethod, 1),
                    new SLTItem("1_4", GameData.aplication.gameText.GetText("Tut1_4"), GameData.EmptyMethod, 2),
                    new SLTItem("1_5", GameData.aplication.gameText.GetText("Tut1_5"), GameData.EmptyMethod, 1),
                    new SLTItem("1_6", GameData.aplication.gameText.GetText("Tut1_6"), GameData.EmptyMethod, 1),
                    new SLTItem("1_7", GameData.aplication.gameText.GetText("Tut1_7"), GameData.EmptyMethod, 2),
                },
                new List<SLTItem>()
                {

                    new SLTItem("2_0", GameData.aplication.gameText.GetText("TutTitle"), GameData.EmptyMethod, 1),
                    new SLTItem("2_1", "────────────────────────────────────────────────────────────",GameData.EmptyMethod, 1),
                    new SLTItem("2_2", GameData.aplication.gameText.GetText("Tut2_2"), GameData.EmptyMethod, 1),
                    new SLTItem("2_3", GameData.aplication.gameText.GetText("Tut2_3"), GameData.EmptyMethod, 1),
                    new SLTItem("2_4", GameData.aplication.gameText.GetText("Tut2_4"), GameData.EmptyMethod, 1),
                    new SLTItem("2_5", GameData.aplication.gameText.GetText("Tut2_5"), GameData.EmptyMethod, 1),
                    new SLTItem("2_6", MyPlayer.basicRightChar.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("2_7", GameData.aplication.gameText.GetText("Tut2_7"), GameData.EmptyMethod, 1),
                    new SLTItem("2_8", MyPlayer.basicLeftChar.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("2_9", GameData.aplication.gameText.GetText("Tut2_9"), GameData.EmptyMethod, 1),
                    new SLTItem("2_10",MyPlayer.basicUpChar.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("2_11",GameData.aplication.gameText.GetText("Tut2_11"), GameData.EmptyMethod, 1),
                    new SLTItem("2_12",MyPlayer.basicDownChar.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("2_13",GameData.aplication.gameText.GetText("Tut2_13"), GameData.EmptyMethod, 2),
                    new SLTItem("2_14",MyPlayer.basicRightChar.ToString() + "    " + GoodBullet.bulletIcon + "    " + GoodBullet.bulletIcon, GameData.EmptyMethod, 1),
                    new SLTItem("2_15",GameData.aplication.gameText.GetText("Tut2_15"), GameData.EmptyMethod, 2),
                    new SLTItem("2_16",GoodMine.mine1Ico + " " + MyPlayer.basicRightChar.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("2_17",GameData.aplication.gameText.GetText("Tut2_17"), GameData.EmptyMethod, 2),
                    new SLTItem("2_18"," " + SimpleBayraktarRocket.basicUpChar + "       " + SimpleBayraktarRocket.basicUpChar, GameData.EmptyMethod, 1),
                    new SLTItem("2_18","  " + SimpleBayraktarRocket.basicUpChar + "          " + MyPlayer.basicUpChar.ToString() + "       " + SimpleBayraktarRocket.basicUpChar + "      ", GameData.EmptyMethod, 1),
                    new SLTItem("2_18",SimpleBayraktarRocket.basicUpChar + "  ", GameData.EmptyMethod, 1),


                },
                new List<SLTItem>()
                {

                    new SLTItem("3_0", GameData.aplication.gameText.GetText("TutTitle"), GameData.EmptyMethod, 1),
                    new SLTItem("3_1", "────────────────────────────────────────────────────────────",GameData.EmptyMethod, 1),
                    new SLTItem("3_2", GameData.aplication.gameText.GetText("Tut3_2"), GameData.EmptyMethod, 1),
                    new SLTItem("3_3", GameData.aplication.gameText.GetText("Tut3_3"), GameData.EmptyMethod, 1),
                    new SLTItem("3_4", GameData.aplication.gameText.GetText("Tut3_4"), GameData.EmptyMethod, 1),
                    new SLTItem("3_5", UkraineTraktorMykola.icons[0] + " " + UkraineTraktorMykola.icons[4], GameData.EmptyMethod, 1),
                    new SLTItem("3_6", GameData.aplication.gameText.GetText("Tut3_6"), GameData.EmptyMethod, 1),
                    new SLTItem("3_7", GameData.aplication.gameText.GetText("Tut3_7"), GameData.EmptyMethod, 1),
                    new SLTItem("3_8", GameData.aplication.gameText.GetText("Tut3_8"), GameData.EmptyMethod, 1),
                    new SLTItem("3_9", GameData.aplication.gameText.GetText("Tut3_9"), GameData.EmptyMethod, 4),
                    new SLTItem("3_10", UkraineSoldier.soldierPoses[Vector2.up] + " " + UkraineSoldier.soldierPoses[Vector2.down] + " " + UkraineSoldier.soldierPoses[Vector2.left] + " " + UkraineSoldier.soldierPoses[Vector2.right], GameData.EmptyMethod, 1),
                    new SLTItem("3_11", GameData.aplication.gameText.GetText("Tut3_11"), GameData.EmptyMethod, 1),
                    new SLTItem("3_12", GameData.aplication.gameText.GetText("Tut3_12"), GameData.EmptyMethod, 1),
                    new SLTItem("3_13", GameData.aplication.gameText.GetText("Tut3_13") + UkraineSoldier.attackMaxDistance, GameData.EmptyMethod, 1),
                    new SLTItem("3_14", GameData.aplication.gameText.GetText("Tut3_14"), GameData.EmptyMethod, 1),
                },
                new List<SLTItem>()
                {

                    new SLTItem("4_0", GameData.aplication.gameText.GetText("TutTitle"), GameData.EmptyMethod, 1),
                    new SLTItem("4_1", "────────────────────────────────────────────────────────────",GameData.EmptyMethod, 1),
                    new SLTItem("4_2", GameData.aplication.gameText.GetText("Tut4_2"), GameData.EmptyMethod, 1),
                    new SLTItem("4_3", GameData.aplication.gameText.GetText("Tut4_3"), GameData.EmptyMethod, 1),
                    new SLTItem("4_4", GameData.aplication.gameText.GetText("Tut4_4"), GameData.EmptyMethod, 1),
                    new SLTItem("4_5", ShitrussiaTankZ.basicIcon.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("4_6", GameData.aplication.gameText.GetText("Tut4_6"), GameData.EmptyMethod, 1),
                    new SLTItem("4_7", GameData.aplication.gameText.GetText("Tut4_7"), GameData.EmptyMethod, 1),
                    new SLTItem("4_8", GameData.aplication.gameText.GetText("Tut4_8"), GameData.EmptyMethod, 1),
                    new SLTItem("4_9", GameData.aplication.gameText.GetText("Tut4_9"), GameData.EmptyMethod, 1),
                },
                new List<SLTItem>()
                {

                    new SLTItem("5_0", GameData.aplication.gameText.GetText("TutTitle"), GameData.EmptyMethod, 1),
                    new SLTItem("5_1", "────────────────────────────────────────────────────────────",GameData.EmptyMethod, 1),
                    new SLTItem("5_2", GameData.aplication.gameText.GetText("Tut5_2"), GameData.EmptyMethod, 1),
                    new SLTItem("5_3", GameData.aplication.gameText.GetText("Tut5_3"), GameData.EmptyMethod, 1),
                    new SLTItem("5_4", GameData.aplication.gameText.GetText("Tut5_4"), GameData.EmptyMethod, 1),
                    new SLTItem("5_5", MyGameShell.wall.ico.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("5_6", GameData.aplication.gameText.GetText("Tut5_6"), GameData.EmptyMethod, 1),
                    new SLTItem("5_7", GameData.aplication.gameText.GetText("Tut5_7"), GameData.EmptyMethod, 2),
                    new SLTItem("5_8", MyGameShell.zoneGround.ico.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("5_9", GameData.aplication.gameText.GetText("Tut5_9"), GameData.EmptyMethod, 1),
                    new SLTItem("5_10", GameData.aplication.gameText.GetText("Tut5_10"), GameData.EmptyMethod, 1),
                    new SLTItem("5_11", GameData.aplication.gameText.GetText("Tut5_11"), GameData.EmptyMethod, 2),
                    new SLTItem("5_12", MyGameShell.slowDownGround.ico.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("5_13", GameData.aplication.gameText.GetText("Tut5_13"), GameData.EmptyMethod, 1),
                    new SLTItem("5_14", GameData.aplication.gameText.GetText("Tut5_14"), GameData.EmptyMethod, 1),
                    new SLTItem("5_15", GameData.aplication.gameText.GetText("Tut5_15"), GameData.EmptyMethod, 2),
                    new SLTItem("5_16", PricklyHedgehogs.basicIcon.ToString(), GameData.EmptyMethod, 1),
                    new SLTItem("5_17", GameData.aplication.gameText.GetText("Tut5_17"), GameData.EmptyMethod, 1),
                    new SLTItem("5_18", GameData.aplication.gameText.GetText("Tut5_18"), GameData.EmptyMethod, 2),
                    new SLTItem("5_19", "⌂", GameData.EmptyMethod, 1),
                    new SLTItem("5_20", GameData.aplication.gameText.GetText("Tut5_20"), GameData.EmptyMethod, 1),
                },
            };

            GameData.mainMenu.TextUpdate();
            GameData.settingsMenu.TextUpdate();

            GameData.finalWinTab.TextUpdate();
            GameData.moscowDecideFateTab.TextUpdate();

            GameData.captureCityTab.itemList = new List<SLTItem>()
            {
                new SLTItem("Message1", GameData.aplication.gameText.GetText("Congratulations"), GameData.EmptyMethod, 0),
                new SLTItem("Message2", GameData.aplication.gameText.GetText("YouWin"), GameData.EmptyMethod, 0),
                new SLTItem("GoToMenu", GameData.aplication.gameText.GetText("PressEscape"), GameData.EmptyMethod, 3),
                new SLTItem("ContinueGame", GameData.aplication.gameText.GetText("PressEnter"), GameData.EmptyMethod, 1)
            };
            GameData.captureCityTab.AlignToCenter();
            GameData.captureCityTab.HeightAlignToCenter();
            GameData.lostCityTab.itemList = new List<SLTItem>()
            {
                new SLTItem("Message1", GameData.aplication.gameText.GetText("YouLost"), GameData.EmptyMethod, 0),
                new SLTItem("GoToMenu", GameData.aplication.gameText.GetText("PressEscape"), GameData.EmptyMethod, 3),
                new SLTItem("ContinueGame", GameData.aplication.gameText.GetText("PressEnter"), GameData.EmptyMethod, 1)
            };
            GameData.lostCityTab.AlignToCenter();
            GameData.lostCityTab.HeightAlignToCenter();

        }
    }

    public class MainMenu
    {
        public SelectListTab menuTab;

        public MainMenu()
        {
            
            menuTab = new SelectListTab(GameData.aplication, new List<SLTItem>(), "MainMenu", 20);
            TextUpdate();
            menuTab.keyManager.keyPressActions.Add(ConsoleKey.Escape, Exit);
            menuTab.keyManager.keyPressActions.Add(ConsoleKey.Spacebar, menuTab.SelectItem);
        }

        public void TextUpdate()
        {
            menuTab.itemList = new List<SLTItem>()
            {
                new SLTItem("StartGame", GameData.aplication.gameText.GetText("StartGame"), StartGame, 0),
                new SLTItem("ContinueGame", GameData.aplication.gameText.GetText("ContinueGame"), ContinueGame, 1),
                new SLTItem("Tutorial", GameData.aplication.gameText.GetText("Tutorial"), Tutorial, 1),
                new SLTItem("Settings", GameData.aplication.gameText.GetText("Settings"), Settings, 1),
                new SLTItem("Credits", GameData.aplication.gameText.GetText("Credits"), Credits, 1),
                new SLTItem("Exit", GameData.aplication.gameText.GetText("Exit"), Exit, 1)
            };
            menuTab.AlignToCenter();
            menuTab.HeightAlignToCenter();
        }

        public void StartGame() { InitGame(); RunGame(); }
        public void ContinueGame() { RunGame(); }
        public void Tutorial() { OpenTutorial(); }
        public void Settings() { OpenSettings(); }
        public void Credits() { }
        public void Exit() { GameData.aplication.isInAplication = false; }

        public void InitGame() { GameData.myGameShell.Init(); }

        public void RunGame() { GameData.aplication.currentGameTabName = GameData.myGameShell.game.name; }
        public void OpenTutorial() { GameData.aplication.currentGameTabName = GameData.tutorialMenu.name; GameData.tutorialMenu.Open(); }
        public void OpenSettings() { GameData.aplication.currentGameTabName = GameData.settingsMenu.name; GameData.settingsMenu.Open(); }
    }

    public class TutorialMenu : SelectListTab
    {
        public int currentTutorialPage = 0;

        List<List<SLTItem>> pages = new () { };

        public TutorialMenu() : base(GameData.aplication, new List<SLTItem>(), "TutorialMenu", 10, "", "", 1)
        {
            keyManager.keyPressActions = new Dictionary<ConsoleKey, Action>()
            {
                {ConsoleKey.Escape, PressKeyEscape},
                {ConsoleKey.RightArrow, PressKeyRightOrD},
                {ConsoleKey.D, PressKeyRightOrD},
                {ConsoleKey.LeftArrow, PressKeyLeftOrA},
                {ConsoleKey.A, PressKeyLeftOrA}
            };
        }

        public void Open()
        {
            pages = GameData.tutorailTabPages;
            currentTutorialPage = 0;
            UpdateTutMenu();
        }

        public void UpdateTutMenu()
        {
            itemList = pages[currentTutorialPage];
            AlignToCenter();
            Console.Clear();
            Draw();
        }

        public void PressKeyEscape() { GoToMainMenu(); }
        public void PressKeyRightOrD() { GoToRightPage(); }
        public void PressKeyLeftOrA() { GoToLeftPage(); }

        public void GoToMainMenu() { Console.Clear(); aplication.currentGameTabName = GameData.mainMenu.menuTab.name; }
        public void GoToRightPage() { currentTutorialPage++; currentTutorialPage = KTZMath.LoopNumberInInterval(currentTutorialPage, 0, pages.Count - 1); Console.Clear(); UpdateTutMenu(); }
        public void GoToLeftPage() { currentTutorialPage--; currentTutorialPage = KTZMath.LoopNumberInInterval(currentTutorialPage, 0, pages.Count - 1); Console.Clear(); UpdateTutMenu(); }
    }

    public class SettingsMenu : SelectSelectListTab
    {
        public int windowWidthCenter = (int)(GameData.myGameWindowWidth / 2);
        public SettingsMenu() : base(GameData.aplication, new List<SSLTItem>(), "SettingsMenu", 20, distanceBetweenElements_:1)
        {
            keyManager.keyPressActions.Add(ConsoleKey.Escape, PressKeyEscape);
            itemList = new List<SSLTItem>()
            {
                new SSLTItem("GameTheme", GameData.aplication.gameText.GetText("GameTheme"),
                    new List<SSLTItemItem>()
                    {
                        new SSLTItemItem(GameData.aplication.gameText.GetText("ThemeDark"), SetThemeDark),
                        new SSLTItemItem(GameData.aplication.gameText.GetText("ThemeLight"), SetThemeLight),
                        new SSLTItemItem(GameData.aplication.gameText.GetText("ThemeDoom"), SetThemeDoom),
                        new SSLTItemItem(GameData.aplication.gameText.GetText("ThemeHecker"), SetThemeHecker),
                        new SSLTItemItem(GameData.aplication.gameText.GetText("ThemeHell"), SetThemeHell),
                        new SSLTItemItem(GameData.aplication.gameText.GetText("ThemePatriotic"), SetThemePatriotic),
                        new SSLTItemItem(GameData.aplication.gameText.GetText("MyLittlePony"), SetThemePony)
                    }, selectorOffset_:1
                ),
                new SSLTItem("Language", GameData.aplication.gameText.GetText("GameLanguage"),
                    new List<SSLTItemItem>()
                    {
                        new SSLTItemItem("Українська", SetLangUkraine),
                        new SSLTItemItem("English", SetLangEnglish),
                        new SSLTItemItem("Polski", SetLangPolish)
                    }, 3, selectorOffset_:1
                ),
                new SSLTItem("ClearGameProgress", GameData.aplication.gameText.GetText("ClearGameProgress"),
                    new List<SSLTItemItem>()
                    {
                        new SSLTItemItem(GameData.aplication.gameText.GetText("PressEnterSpaceDeleteData"), ClearGameData),
                    }, 3, selectorOffset_:1
                )
            };

            AlignToCenter();

            itemList[0].topOffset = 6;
        }

        public override void Draw()
        {
            base.Draw();
            DrawAtCenterWithClear(GameData.aplication.gameText.GetText("SettTitle"), 2);
            DrawAtCenterWithClear(new string('─', 20) + " " + GameData.aplication.gameText.GetText("Settings") + " " + new string('─', 20), 4);

        }
        public static void DrawAtCenterWithClear(string text, int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', GameData.myGameWindowWidth - 1));
            Console.SetCursorPosition((int)((GameData.myGameWindowWidth - text.Length) / 2), y);
            Console.Write(text);
        }

        public void TextUpdate()
        {
            itemList[0].title = GameData.aplication.gameText.GetText("GameTheme");

            itemList[0].variants[0].text = GameData.aplication.gameText.GetText("ThemeDark");
            itemList[0].variants[1].text = GameData.aplication.gameText.GetText("ThemeLight");
            itemList[0].variants[2].text = GameData.aplication.gameText.GetText("ThemeDoom");
            itemList[0].variants[3].text = GameData.aplication.gameText.GetText("ThemeHell");
            itemList[0].variants[4].text = GameData.aplication.gameText.GetText("ThemeHecker");
            itemList[0].variants[5].text = GameData.aplication.gameText.GetText("ThemePatriotic");
            itemList[0].variants[6].text = GameData.aplication.gameText.GetText("MyLittlePony");


            itemList[1].title = GameData.aplication.gameText.GetText("GameLanguage");


            itemList[2].title = GameData.aplication.gameText.GetText("ClearGameProgress");

            itemList[2].variants[0].text = GameData.aplication.gameText.GetText("PressEnterSpaceDeleteData");

            AlignToCenter();
        }

        public void ClearGameData() { }

        public void SetLangUkraine() { GameData.aplication.gameText.currentLanguageName = GameData.ukraine.name; GameShell.aplic.TextUpdate(); }
        public void SetLangEnglish() { GameData.aplication.gameText.currentLanguageName = GameData.english.name; GameShell.aplic.TextUpdate(); }
        public void SetLangPolish() { GameData.aplication.gameText.currentLanguageName = GameData.polish.name; GameShell.aplic.TextUpdate(); }

        public void SetThemeDark() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.Black; GameData.aplication.ConsoleForegroundColor = ConsoleColor.Gray; GameData.dangerColor = ConsoleColor.Red; GameData.aplication.UpdateConsoleColors(); Console.Clear(); }
        public void SetThemeLight() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.White; GameData.aplication.ConsoleForegroundColor = ConsoleColor.Black; GameData.dangerColor = ConsoleColor.Red; GameData.aplication.UpdateConsoleColors(); Console.Clear();}
        public void SetThemeDoom() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.Black; GameData.aplication.ConsoleForegroundColor = ConsoleColor.DarkRed; GameData.dangerColor = ConsoleColor.White; GameData.aplication.UpdateConsoleColors(); Console.Clear();}
        public void SetThemeHecker() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.Black; GameData.aplication.ConsoleForegroundColor = ConsoleColor.Green; GameData.dangerColor = ConsoleColor.Red; GameData.aplication.UpdateConsoleColors(); Console.Clear();}
        public void SetThemeHell() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.DarkRed; GameData.aplication.ConsoleForegroundColor = ConsoleColor.Red; GameData.dangerColor = ConsoleColor.White; GameData.aplication.UpdateConsoleColors(); Console.Clear();}
        public void SetThemePatriotic() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.DarkBlue; GameData.aplication.ConsoleForegroundColor = ConsoleColor.DarkYellow; GameData.dangerColor = ConsoleColor.Red; GameData.aplication.UpdateConsoleColors(); Console.Clear();}
        public void SetThemePony() { GameData.aplication.ConsoleBackgroundColor = ConsoleColor.Magenta; GameData.aplication.ConsoleForegroundColor = ConsoleColor.White; GameData.dangerColor = ConsoleColor.Red; GameData.aplication.UpdateConsoleColors(); Console.Clear();}

        public void Open() { selectedElemIndex = 0; Console.Clear(); }

        public void PressKeyEscape() { GoToMainMenu(); }

        public void GoToMainMenu() { Console.Clear(); aplication.currentGameTabName = GameData.mainMenu.menuTab.name; }
    }

    public class DecideFateTab : SelectListTab
    {

        public DecideFateTab() : base(GameData.aplication, new List<SLTItem>(), "moscowDecideFateTab", 20)
        {
            itemList = new List<SLTItem>() { new SLTItem("DestroyFate", "Destroy moscow", Destroymoscow), new SLTItem("Ukrainianizemoscow", "UkrainianizeMoscow", Ukrainianizemoscow, 3) };
            keyManager.keyPressActions.Add(ConsoleKey.Spacebar, SelectItem);
            TextUpdate();
        }

        public void TextUpdate()
        {
            itemList[0].text = GameData.aplication.gameText.GetText("FateDestroymoscow");
            itemList[1].text = GameData.aplication.gameText.GetText("FateUkrainianizemoscow");

            AlignToCenter();
            HeightAlignToCenter();

            itemList[0].topOffset += 5;
        }

        public override void Draw()
        {
            base.Draw();
            SettingsMenu.DrawAtCenterWithClear(GameData.aplication.gameText.GetText("NowDecideFatemoscow"), itemList[0].topOffset - 5);
        }

        public void Destroymoscow() 
        { 
            GameData.score += 100000; 
            Console.Clear();
            RestartWar();
            aplication.currentGameTabName = GameData.warStartTab.name; 
            GameData.warStartTab.TextUpdate();
        }
        public void Ukrainianizemoscow() 
        { 
            GameData.score = (int)(GameData.score * 1.3);
            Console.Clear();
            RestartWar();
            aplication.currentGameTabName = GameData.warStartTab.name; 
            GameData.warStartTab.TextUpdate();
        }

        public void RestartWar()
        {
            GameData.yearsOfPeace = KTZEngineAplication.random.Next(3, 10);
            GameData.warStartVirtualTime = new DateTime(GameData.virtualTime.Year + GameData.yearsOfPeace, 4, 24);
            GameData.virtualTime = GameData.warStartVirtualTime.AddDays(KTZEngineAplication.random.Next(1, 10));
            GameData.currentCityIndex = 3;

        }
    }

    

    public class MyGameShell
    {
        public MyGame game;
        public KTZEngineAplication aplication;
        public MyGame origGame;
        public string origGameName;
        public Action TabBehavior;

        // Display Layer
        public Vector2 gameDisplaySize;

        // Game Layers 
        public Vector2 gameFieldSize = Vector2.zero;
        public Vector2 gameStartPos = Vector2.zero;

        public GameLayer floorLayer;
        public GameLayer staticObjectsLayer;
        public GameLayer gameObjectsLayer;
        public GameLayer gameDisplay;

        public int currentCityEnemy;

        public static StaticGameObject wall = new (Vector2.zero, GameData.gameLayerRef, '█', new List<string>() { GameData.gameIds["collision"] });
        public static StaticGameObject emptyWall = new (Vector2.zero, GameData.gameLayerRef, '█');
        public static StaticGameObject zoneGround = new (Vector2.zero, GameData.gameLayerRef, '▒');
        public static EnemySlowDownTile slowDownGround = new (Vector2.zero, GameData.gameLayerRef, '░');

        public PricklyHedgehogs pricklyHedgehogs;

        public int updateIndex = 0;
        public int updateIndexMaximum = 1000;

        public int levelEnemysSpawnTimeMiliseconds;
        public int levelPrepareTime;
        public int levelOneEnemySpawnTimeMiliseconds;

        public int enemyLeftToKill;

        public int levelEnemyCount;

        public int bayraktarAttackCount;

        public Dictionary<int, int> goSoldierVariants;
        public List<int> goSoldiersXPositions;

        public bool isSirenDraw = false;
        public int sirenChangeTickCount = 10;

        public bool isWin = false;
        public bool isLose = false;

        public MyGameShell(ref MyGame game_, Action TabBehavior_, Vector2 gameStartPos_, Vector2 gameSize, bool isInitable = true) 
        { 
            origGame = game_;
            origGameName = origGame.name;

            aplication = origGame.aplication;

            TabBehavior = TabBehavior_;

            gameFieldSize = gameSize;
            gameStartPos = gameStartPos_;

            if (isInitable)
            {
                Init();
                KeyInit();
            }
        }

        public void Init()
        {
            aplication.gameTabs.Remove(origGameName);
            game = new MyGame(ref origGame.aplication, Vector2.upRight, "lolBotlol", origGame.UPS);
            aplication.gameTabs.Remove(game.name);

            FieldInfo[] gameFieldInfo = origGame.GetType().GetFields();

            for (int i = 0; i < gameFieldInfo.Length; i++)
            {
                gameFieldInfo[i].SetValue(game, gameFieldInfo[i].GetValue(origGame));
            }

            aplication.tabsBehavior.Remove(game.name);
            aplication.gameTabs.Remove(game.name);

            aplication.gameTabs.Add(game.name, game);
            aplication.tabsBehavior.Add(game.name, TabBehavior);

            isWin = false;
            isLose = false;

            game.gameGrid.layers.Clear();
            game.gameObjects.Clear();

            levelEnemyCount = GameData.cityEnemyCount.ElementAt(GameData.currentCityIndex).Value;

            gameDisplay = game.gameGrid.CreateLayer("gameDisplay", game.gameGrid.grid.Width, game.gameGrid.grid.Height, Vector2.zero, ' ', -1);
            floorLayer = game.gameGrid.CreateLayer("floor", gameFieldSize.x, gameFieldSize.y, gameStartPos, ' ', 0);
            staticObjectsLayer = game.gameGrid.CreateLayer("staticObjects", gameFieldSize.x, gameFieldSize.y, gameStartPos, ' ', 1);
            gameObjectsLayer = game.gameGrid.CreateLayer("gameObjects", gameFieldSize.x, gameFieldSize.y, gameStartPos, ' ', 2);

            currentCityEnemy = GameData.cityEnemyCount[GameData.cityNames[GameData.currentCityIndex]];

            levelEnemysSpawnTimeMiliseconds = (GameData.currentCityIndex + 2) * 60000;
            levelPrepareTime = (int)(levelEnemysSpawnTimeMiliseconds * 0.1);
            levelOneEnemySpawnTimeMiliseconds = (int)(levelEnemysSpawnTimeMiliseconds / levelEnemyCount);

            ShitrussiaTankZ.basicEnemyStopTicks = 40 - GameData.currentCityIndex * 5;

            bayraktarAttackCount = (int)((GameData.currentCityIndex)/1.5) + 1;

            MapInit();

        }

        public void MapInit()
        {
            wall = new StaticGameObject(Vector2.zero, staticObjectsLayer, '█', new List<string>() { GameData.gameIds["collision"] });
            emptyWall = new StaticGameObject(Vector2.zero, floorLayer, '█');
            zoneGround = new StaticGameObject(Vector2.zero, floorLayer, '▒');
            slowDownGround = new EnemySlowDownTile(Vector2.zero, staticObjectsLayer, '░');

            int[,] virtualMap;

            if (GameData.cityNamesRU.Contains(GameData.cityNames[GameData.currentCityIndex])) { virtualMap = ShitrussiaCityInit(); }
            else { virtualMap = UkraineCityInit(); }



            // int textLen = GameData.cityNames[GameData.currentCityIndex].Length;
            Vector2 textStartPos = new Vector2((int)Math.Round((double)((floorLayer.layerWidth - GameData.cityNames[GameData.currentCityIndex + 1].Length) / 2)), floorLayer.layerHeight - KTZMapsGeneration.zoneHeight);
            for (int i = 0; i < GameData.cityNames[GameData.currentCityIndex + 1].Length; i++) { floorLayer.CreateStaticObject(textStartPos + new Vector2(i, 0), GameData.cityNames[GameData.currentCityIndex + 1][i], new List<string>() { GameData.gameIds["collision"] }); }

            textStartPos = new Vector2((int)Math.Round((double)((floorLayer.layerWidth - GameData.cityNames[GameData.currentCityIndex].Length) / 2)), KTZMapsGeneration.zoneHeight - 1);
            for (int i = 0; i < GameData.cityNames[GameData.currentCityIndex].Length; i++) { floorLayer.CreateStaticObject(textStartPos + new Vector2(i, 0), GameData.cityNames[GameData.currentCityIndex][i], new List<string>() { GameData.gameIds["collision"] }); }

            int mapCenterX = (int)Math.Round((float)(gameObjectsLayer.layerWidth / 2));

            GameData.player = new MyPlayer(this, "Player", gameObjectsLayer, new Vector2(mapCenterX, /*gameObjectsLayer.layerHeight -*/ 3), new List<GameLayer>() { staticObjectsLayer, gameObjectsLayer }, new List<string>() { GameData.gameIds["collision"], GameData.gameIds["mine"], GameData.gameIds["pricklyHedgehogs"] });

            GameData.traktorMykola = new UkraineTraktorMykola(this, gameObjectsLayer, new Vector2(KTZEngineAplication.random.Next(1, gameObjectsLayer.layerWidth - 2), /*gameObjectsLayer.layerHeight -*/ KTZEngineAplication.random.Next(1, 3)));
            GenerateObjectAtGamePlaceRandom(virtualMap, GameData.traktorMykola);

            GenerateUkraineSoldiers(virtualMap);

            /*
            for (int i = 0; i < 1000; i++)
            {
                GenerateEnemy();
            }
            */
            
            UkraineSoldier.stupidPositionToBlock = new() { };

            GameData.wallPositionList = new() { };
            GameData.pricklyHedgehogsPositionList = new() { };
            GameData.slowdownPositionList = new() { };

            foreach (StaticGameObject obj in staticObjectsLayer.staticObjects) 
            { 
                if (obj.id.Contains(GameData.gameIds["collision"]) && obj.ico == wall.ico) { GameData.wallPositionList.Add(obj.globalPosition); } 
                if (obj.ico == wall.ico) { GameData.allWallPositionList.Add(obj.globalPosition); }
                if (obj.id.Contains(GameData.gameIds["pricklyHedgehogs"])) { GameData.pricklyHedgehogsPositionList.Add(obj.globalPosition); }
                if (obj.id.Contains(GameData.gameIds["enemySlowDown"])) { GameData.slowdownPositionList.Add(obj.globalPosition); }
            }
            
            UkraineSoldier.cornersPos = new List<Vector2>() { Vector2.upRight * 1 + gameObjectsLayer.layerStartPosition, new Vector2(gameObjectsLayer.layerWidth - 2, 1) + gameObjectsLayer.layerStartPosition,new Vector2(1, gameObjectsLayer.layerHeight - 2) + gameObjectsLayer.layerStartPosition, new Vector2(gameObjectsLayer.layerWidth - 2, gameObjectsLayer.layerHeight - 2) + gameObjectsLayer.layerStartPosition };

            for (int i = 0; i < 10; i++) { GenerateObjectAtGamePlaceRandom(virtualMap, new MineRecoverBox(gameObjectsLayer, Vector2.zero)); }
        }
        public int[,] ShitrussiaCityInit()
        {
            int[,] virtualMap;

            virtualMap = KTZMapsGeneration.GenerateMapByReference(
                new Dictionary<int, StaticGameObject>() { { 1, wall }, { 2, zoneGround }, { 3, emptyWall } },
                KTZMapsGeneration.EdgeGeneration
                (
                    KTZMapsGeneration.ShitrussiaCityMapGeneration
                    (
                        KTZMapsGeneration.CreateVirtualMap(wall.layer.layerWidth, wall.layer.layerHeight, 0),
                        GameData.cityNames,
                        GameData.currentCityIndex
                    )
                )
            );

            return virtualMap;
        }
        public int[,] UkraineCityInit()
        {
            int[,] virtualMap;
            pricklyHedgehogs = new PricklyHedgehogs(Vector2.zero, staticObjectsLayer);



            virtualMap = KTZMapsGeneration.GenerateMapByReference(
                new Dictionary<int, StaticGameObject>() { { 1, wall }, { 2, zoneGround }, { 3, emptyWall }, { 4, slowDownGround } },
                KTZMapsGeneration.EdgeGeneration
                (
                    KTZMapsGeneration.UkrainCityMapGeneration
                    (
                        KTZMapsGeneration.CreateVirtualMap(wall.layer.layerWidth, wall.layer.layerHeight, 1),
                        GameData.cityNames,
                        GameData.currentCityIndex
                    )
                )
            );

            virtualMap = KTZMapsGeneration.GeneratePricklyHedgehogs(virtualMap);

            virtualMap = KTZMapsGeneration.GenerateMapByReference(new Dictionary<int, StaticGameObject>() { { 5, pricklyHedgehogs } }, virtualMap);

            return virtualMap;
        }

        public void GenerateUkraineSoldiers(int[,] virtualMap)
        {
            int groupsCount = 1 + (int)(GameData.currentCityIndex * 1.5);
            int sectorSize = (int)((gameObjectsLayer.layerWidth) / groupsCount);
            int goXPos;

            for (int i = 0; i < groupsCount; i++)
            {
                goSoldierVariants = new Dictionary<int, int>();
                for (int x = sectorSize * i; x < sectorSize * (i + 1); x++)
                {
                    goSoldierVariants.Add(x, 0);
                    for (int y = KTZMapsGeneration.zoneHeight; y < gameObjectsLayer.layerHeight - 1 - KTZMapsGeneration.zoneHeight; y++)
                    {
                        if (virtualMap[x, y] == 1 || virtualMap[x, y] == 3) { goSoldierVariants[x] += 1; }
                    }
                    // goSoldierVariants.Add(x, staticObjectsLayer.staticObjects.FindAll(o => o.id.Contains(GameData.gameIds["collision"]) &&
                    // o.globalPosition.x == x && o.localPosition.y < (int)(gameObjectsLayer.layerHeight / 1)).ToArray().Length +
                    // floorLayer.staticObjects.FindAll(o => o.ico == wall.ico && o.globalPosition.x == x && o.localPosition.y < (int)(gameObjectsLayer.layerHeight / 2)).ToArray().Length);
                }
                goXPos = goSoldierVariants.FirstOrDefault(o => o.Value == goSoldierVariants.Values.Min()).Key;
                for (int q = 0; q < 2; q++)
                {
                    game.AddExistGameObject(new UkraineSoldier(this, gameObjectsLayer, new Vector2(goXPos + q, 2)));
                }
            }

            UkraineSoldier.cornersPos = new List<Vector2>() { Vector2.upRight, new Vector2(gameObjectsLayer.layerWidth - 2, 1), new Vector2(1, gameObjectsLayer.layerHeight - 2), new Vector2(gameObjectsLayer.layerWidth - 2, gameObjectsLayer.layerHeight - 2) };
            
        }

        public void KeyInit()
        {
            game.keyManager.keyPressActions.Add(ConsoleKey.UpArrow, PressKeyUpOrW);
            game.keyManager.keyPressActions.Add(ConsoleKey.W, PressKeyUpOrW);
            game.keyManager.keyPressActions.Add(ConsoleKey.DownArrow, PressKeyDownOrS);
            game.keyManager.keyPressActions.Add(ConsoleKey.S, PressKeyDownOrS);
            game.keyManager.keyPressActions.Add(ConsoleKey.RightArrow, PressKeyRightOrD);
            game.keyManager.keyPressActions.Add(ConsoleKey.D, PressKeyRightOrD);
            game.keyManager.keyPressActions.Add(ConsoleKey.LeftArrow, PressKeyLeftOrA);
            game.keyManager.keyPressActions.Add(ConsoleKey.A, PressKeyLeftOrA);

            game.keyManager.keyPressActions.Add(ConsoleKey.Spacebar, PressSpacebarOrKeyV);
            game.keyManager.keyPressActions.Add(ConsoleKey.V, PressSpacebarOrKeyV);
            game.keyManager.keyPressActions.Add(ConsoleKey.E, PressKeyE);
            game.keyManager.keyPressActions.Add(ConsoleKey.Q, PressKeyQ);
            

            game.keyManager.keyPressActions.Add(ConsoleKey.Escape, PressKeyEscape);

            game.keyManager.keyPressActions.Add(ConsoleKey.X, PressKeyX);
            game.keyManager.keyPressActions.Add(ConsoleKey.Z, PressKeyZ);

            game.keyManager.keyPressActions.Add(ConsoleKey.D0, PressKey0);
            game.keyManager.keyPressActions.Add(ConsoleKey.D1, PressKey1);
            game.keyManager.keyPressActions.Add(ConsoleKey.D2, PressKey2);



            game.keyManager.keyPressActions.Add(ConsoleKey.P, PressKeyP);
            game.keyManager.keyPressActions.Add(ConsoleKey.K, PressKeyK);
            game.keyManager.keyPressActions.Add(ConsoleKey.L, PressKeyL);
            // game.keyManager.keyPressActions.Add(ConsoleKey.D3, PressKey3);
            // game.keyManager.keyPressActions.Add(ConsoleKey.D4, PressKey4);
        }

        public void PreUpdate()
        {
            GameData.isDanger = false;

            GameData.bulletPositionList = new() { };
            MineAnimationTick();
        }

        public void Update()
        {

            enemyLeftToKill = levelEnemyCount + GameData.enemyPositionList.Count;

            updateIndex = updateIndex > updateIndexMaximum ? 0 : updateIndex + 1;

            if (levelEnemyCount > 0 && game.time > (ulong)levelPrepareTime)
            {
                if  (game.time > (ulong)(levelPrepareTime + (GameData.cityEnemyCount.ElementAt(GameData.currentCityIndex).Value - levelEnemyCount) * levelOneEnemySpawnTimeMiliseconds)) { GenerateEnemy(); }
            }
            DataOutput();
        }

        public void AfterUpdate()
        {
            // foreach (UkraineSoldier soldier in GameData.myGameShell.game.gameObjectsList.FindAll(o => o.id.Contains(GameData.gameIds["UkraineSoldier"])).ToList())
            // {
            //     GameData.myGameShell.game.PostProcessing(soldier.globalPosition + Vector2.right, soldier.priorityDir.Position());
            // }
            // foreach (Vector2 pos in UkraineSoldier.stupidPositionToBlock)
            // {
            //     GameData.myGameShell.game.PostProcessing(pos, '+');
            // }

            GameResultCheck();
            ObjectsPositionGet();
            SirenCheck();
        }


        public void ObjectsPositionGet()
        {
            GameData.enemyPositionList = new() { };
            GameData.minePositionList = new() { };
            GameData.UkraineSoldierPositionList = new() { };
            GameData.collisionIdPositionList = new() { };


            foreach (GameObject obj in game.gameObjectsList)
            {
                if (obj.id.Contains(GameData.gameIds["enemy"])) { GameData.enemyPositionList.Add(obj.globalPosition); }
                if (obj.id.Contains(GameData.gameIds["mine"])) { GameData.minePositionList.Add(obj.globalPosition); }
                if (obj.id.Contains(GameData.gameIds["UkraineSoldier"])) { GameData.UkraineSoldierPositionList.Add(obj.globalPosition); }
                if (obj.id.Contains(GameData.gameIds["collision"])) { GameData.collisionIdPositionList.Add(obj.globalPosition); }
            }
        }
        public void MineAnimationTick()
        {
            if (GameData.myGameShell.updateIndex % GoodMine.animationMaxTick == 0)
            {
                GoodMine.iconIndex++;
                GoodMine.iconIndex = KTZMath.LoopNumberInInterval(GoodMine.iconIndex, 0, GoodMine.icons.Length - 1);

            }
        }
        
        public void DataOutput()
        {
            // game.gameGrid.AddDisposablePostProcessingText(new Vector2(0, GameData.myGameWindowHeight - 2), "\t\tIn this line is control Information for developer(can be ignore):" + game.gameObjects.Count + "   " + ShitrussiaTankZ.basicEnemyStopTicks + "   ");

            //game.gameGrid.AddDisposablePostProcessingText(new Vector2(0, GameData.myGameWindowHeight - 4), "\t\tFPS:" + Math.Min((int)GameData.aplication.currentAverageFPS, game.UPS) + "    Current Time: " + game.time + "ms    Attack start time: " + levelPrepareTime + "ms    Enemy left to spawn: " + levelEnemyCount + "    Enemy left to kill: " + enemyLeftToKill + "    Mine count: " + GameData.player.mineCount + "    Bayraktar attack count: " + bayraktarAttackCount);

            game.gameGrid.AddDisposablePostProcessingText(new Vector2((int)(GameData.myGameWindowWidth / 2) - 27, GameData.myGameWindowHeight - 2), GameData.aplication.gameText.GetText("MineCount") + GameData.player.mineCount + "[" + GoodMine.icons[GoodMine.iconIndex] + "]" + GameData.aplication.gameText.GetText("BayraktarAttackCount") + bayraktarAttackCount + "[" + SimpleBayraktarRocket.basicUpChar + "]");
            game.gameGrid.AddDisposablePostProcessingText(new Vector2(gameObjectsLayer.layerStartPosition.x + 3, GameData.myGameWindowHeight - 2), "FPS:" + Math.Min((int)GameData.aplication.currentAverageFPS, game.UPS));
            game.gameGrid.AddDisposablePostProcessingText(new Vector2((int)(GameData.myGameWindowWidth / 2) - 58, GameData.myGameWindowHeight - 4), GameData.aplication.gameText.GetText("CurrentTime") + game.time + GameData.aplication.gameText.GetText("ms") + GameData.aplication.gameText.GetText("AttackStartTime") + levelPrepareTime + GameData.aplication.gameText.GetText("ms"));
            game.gameGrid.AddDisposablePostProcessingText(new Vector2((int)(GameData.myGameWindowWidth / 2) + 6, GameData.myGameWindowHeight - 4), GameData.aplication.gameText.GetText("EnemyLeftSpawn") + levelEnemyCount + GameData.aplication.gameText.GetText("EnemyLeftKill") + enemyLeftToKill);
            game.gameGrid.AddDisposablePostProcessingText(new Vector2(gameObjectsLayer.layerStartPosition.x + 3, GameData.myGameWindowHeight - 4), GameData.aplication.gameText.GetText("Score") + GameData.score.ToString());
            
        }
        public void SirenCheck()
        {
            if (updateIndex % sirenChangeTickCount == 0) { isSirenDraw = !isSirenDraw; }
            if (GameData.isDanger && isSirenDraw)
            {
                Console.ForegroundColor = GameData.dangerColor;
                for (int y = 1; y < staticObjectsLayer.layerStartPosition.y; y++)
                {
                    game.PostProcessing(new Vector2(0, y), new string(zoneGround.ico, GameData.myGameWindowWidth));
                }
                Console.ForegroundColor = GameData.aplication.ConsoleForegroundColor;
            }
        }

        // Key Press Actions
        public void PressKeyUpOrW() { GameData.player.MoveUp(); }
        public void PressKeyDownOrS() { GameData.player.MoveDown(); }
        public void PressKeyRightOrD() { GameData.player.MoveRight(); }
        public void PressKeyLeftOrA() { GameData.player.MoveLeft(); }
        public void PressSpacebarOrKeyV() { GameData.player.Shot(); }
        public void PressKeyE() { GameData.player.PlantMine(); }
        public void PressKeyP() { for (int i = 0; i < 1000; i++) { GenerateEnemy(); } }
        public void PressKeyQ() { if (bayraktarAttackCount > 0) { GameData.player.bayraktarAttacksTimes += 3; bayraktarAttackCount--; } }

        public void PressKey1() { ShitrussiaTankZ.basicEnemyStopTicks = (ShitrussiaTankZ.basicEnemyStopTicks <= 0) ?  0 : ShitrussiaTankZ.basicEnemyStopTicks - 1; }
        public void PressKey2() { ShitrussiaTankZ.basicEnemyStopTicks++; }
        public void PressKey0() { GenerateEnemy(); }
        public void PressKeyEscape() { GoToMainMenu(); }

        public void PressKeyK() { GameData.aplication.gameText.currentLanguageName = GameData.ukraine.name; GameShell.aplic.TextUpdate(); }
        public void PressKeyL() { GameData.aplication.gameText.currentLanguageName = GameData.english.name; GameShell.aplic.TextUpdate(); }

        public void PressKeyX() { isWin = true; }
        public void PressKeyZ() { isLose = true; }

        public void GoToMainMenu() { Console.Clear(); aplication.currentGameTabName = GameData.mainMenu.menuTab.name; }
        public void GenerateEnemy() { game.AddExistGameObject(new ShitrussiaTankZ(this, gameObjectsLayer, new Vector2(KTZEngineAplication.random.Next(1, gameFieldSize.x - 1), gameFieldSize.y - 2))); levelEnemyCount = (levelEnemyCount <= 0)? 0 : levelEnemyCount - 1; }

        public void GenerateObjectAtGamePlaceRandom(int[,] virtualMap, GameObject obj)
        {
            Vector2 pos;
            for (int i = 0; i < 200; i++)
            {
                pos = new Vector2(KTZEngineAplication.random.Next(2, gameObjectsLayer.layerWidth - 2), KTZEngineAplication.random.Next(KTZMapsGeneration.zoneHeight * 2, gameObjectsLayer.layerHeight - KTZMapsGeneration.zoneHeight * 2));
                if (virtualMap[pos.x, pos.y] == 0) { obj.localPosition = pos; break; }
            }
            game.AddExistGameObject(obj);
        }
        public void GameResultCheck()
        {
            if ((GameData.enemyPositionList.FindAll(o => o.y < gameObjectsLayer.layerStartPosition + KTZMapsGeneration.zoneHeight - 2).Count > 0 && GameData.currentCityIndex != 0) || isLose) { LostCity(); GameData.isDanger = false; Console.Clear(); }
            else if (enemyLeftToKill <= 0 || isWin)
            {
                if (GameData.currentCityIndex == GameData.cityNames.Count - 2) { Capturemoscow(); }
                else { CaptureCity(); }
            }
        }

        public void LostCity()
        {
            GameData.currentCityIndex = (GameData.currentCityIndex > 0) ? GameData.currentCityIndex - 1 : GameData.currentCityIndex;

            aplication.currentGameTabName = GameData.lostCityTab.name;

            GameData.score = Math.Max(0, GameData.score - 500 * (GameData.cityNames.Count - GameData.currentCityIndex));
            EndGame();
        }

        public void CaptureCity()
        {
            GameData.currentCityIndex = (GameData.currentCityIndex < GameData.cityNames.Count - 2) ? GameData.currentCityIndex + 1 : GameData.currentCityIndex;

            aplication.currentGameTabName = GameData.captureCityTab.name;

            GameData.score += 1000 * (GameData.currentCityIndex + 1);
            EndGame();
        }

        public void Capturemoscow()
        {
            aplication.currentGameTabName = GameData.finalWinTab.name;

            EndGame();
            GameData.finalWinTab.TextUpdate();
        }

        public void EndGame()
        {
            Console.Clear();
            GameData.virtualTime = GameData.virtualTime.AddDays(KTZEngineAplication.random.Next(14, 50));
            game.gameObjects.Clear();
            isWin = false;
            isLose = false;
        }
    }

    public class MyGame : Game
    {
        public MyGame(ref KTZEngineAplication aplication_, Vector2 gameGridSize_, string name, int ups_=1) : base(ref aplication_, gameGridSize_, name, ups_)
        {

        }

        public override void PreUpdateGame()
        {
            GameData.myGameShell.PreUpdate();
        }

        public override void UpdateGame()
        {
            GameData.myGameShell.Update();
        }

        public override void AfterUpdateGame()
        {
            GameData.myGameShell.AfterUpdate();
        }

        // public override void SpecialUpdate()
        // {
        //     GameData.myGameShell.SpecialUpdate();
        // }
    }

    public class MyPlayer : GameObject
    {
        public static char basicUpChar = "\u001E".ToCharArray()[0];//'▲';
        public static char basicRightChar = "\u0010".ToCharArray()[0];//'►'; 
        public static char basicDownChar = "\u001F".ToCharArray()[0];//'▼';
        public static char basicLeftChar = "\u0011".ToCharArray()[0];//'◄';


        public Dictionary<Vector2, char> playerPoses;
        public Vector2 direction = Vector2.up;
        public List<GameLayer> collisonCheckLayers = new ();
        public List<string> collisionCheckIds = new ();
        public MyGameShell myGame;
        public int shootIndex = 0;
        public static int shootMaxIndex = 8;
        public int mineCount;
        public int bayraktarAttacksTimes = 0;

        public MyPlayer(MyGameShell game_, string playerName, GameLayer thisLayer, Vector2 _position, List<GameLayer> collisonCheckLayers_=null, List<string> collisonCheckIds_=null) : base(playerName, thisLayer, _position, ' ', new List<string>() { GameData.gameIds["player"], GameData.gameIds["collision"] }, 3)
        {
            playerPoses = new Dictionary<Vector2, char>() { { Vector2.up, basicUpChar }, { Vector2.right, basicRightChar }, { Vector2.down, basicDownChar }, { Vector2.left, basicLeftChar } };
            icon = basicUpChar;
            localPosition = _position;
            name = playerName;
            layer = thisLayer;
            myGame = game_;
            myGame.game.AddExistGameObject(this);
            mineCount = 50 * (GameData.currentCityIndex + 1);

            if (collisonCheckLayers_ != null) { collisonCheckLayers = collisonCheckLayers_; }
            if (collisonCheckIds_ != null) { collisionCheckIds = collisonCheckIds_; }

        }

        public void MoveUp() { disposableVelocity += Vector2.up; direction = Vector2.up; }
        public void MoveDown() { disposableVelocity += Vector2.down; direction = Vector2.down; }
        public void MoveRight() { disposableVelocity += Vector2.right; direction = Vector2.right; }
        public void MoveLeft() { disposableVelocity += Vector2.left; direction = Vector2.left; }

        public void Shot()
        {
            if (shootIndex >= shootMaxIndex) { myGame.game.AddExistGameObject(new GoodBullet(myGame.game, layer, localPosition, direction, GameData.gameIds["playerBullet"])); shootIndex = 0; }
        }

        public void PlantMine()
        {
            if (mineCount > 0 && !GameData.minePositionList.Contains(globalPosition)) { myGame.game.AddExistGameObject(new GoodMine(globalPosition, layer)); mineCount--; }
        }

        public override void Update()
        {
            icon = playerPoses[direction];
        }

        public override void PreUpdate()
        {
            blockedDirections = ObjectsCollisionCheck();
            shootIndex++;
            if (bayraktarAttacksTimes > 0 && shootIndex > shootMaxIndex - 1) { BayraktarRocketAttack(); bayraktarAttacksTimes--; shootIndex = 0; }
        }

        public List<Vector2> ObjectsCollisionCheck()
        {
            List<Vector2> blockedDir = new ();
            for (int dirI = 0; dirI < Vector2.eightDirections.Count; dirI++)
            {
                
                if (GameData.wallPositionList.Contains(globalPosition + Vector2.eightDirections[dirI]) || 
                    GameData.collisionIdPositionList.Contains(globalPosition + Vector2.eightDirections[dirI]) ||
                    GameData.pricklyHedgehogsPositionList.Contains(globalPosition + Vector2.eightDirections[dirI]) || 
                    GameData.minePositionList.Contains(globalPosition + Vector2.eightDirections[dirI]) || 
                    GameData.enemyPositionList.Contains(globalPosition + Vector2.eightDirections[dirI]))
                { blockedDir.Add(Vector2.eightDirections[dirI]); continue; } 
            }
            
            return blockedDir;
        }

        public void BayraktarRocketAttack()
        {
            int tX;
            int tY;
            int startY;
            int startX;
            int rocketCount;
            List<GameObject> enemys;
            List<int> xPositions;

            enemys = GameData.myGameShell.game.gameObjectsList.Where(o => o.id.Contains(GameData.gameIds["enemy"])).ToList();
            enemys.OrderBy(o => o.localPosition.y);
            rocketCount = 30;
            xPositions = Enumerable.Range(1, layer.layerWidth - 2).ToList();

            for (int i = 0; i < enemys.Count && rocketCount > 0; i++)
            {
                startY = -KTZEngineAplication.random.Next(0, 8);
                GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(enemys[i].localPosition.x, startY), enemys[i].localPosition));
                rocketCount--;
            }

            for (int i = 0; i < rocketCount; i++)
            {
                startY = -KTZEngineAplication.random.Next(0, 8);
                tX = xPositions[KTZEngineAplication.random.Next(xPositions.Count)];
                tY = KTZEngineAplication.random.Next(KTZMapsGeneration.zoneHeight + 3, layer.layerHeight - KTZMapsGeneration.zoneHeight);
                GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(tX, startY), new Vector2(tX, tY)));
                xPositions.Remove(tX);
            }
            
            for (int q = -1; q < 2; q += 2)
            {
                int plusOffsetY = (layer.layerHeight - 1) * Math.Abs(Math.Sign(q - 1));

                // Attack to right
                enemys = GameData.myGameShell.game.gameObjectsList.Where(o => o.id.Contains(GameData.gameIds["enemy"]) && KTZMath.IsInInterval(o.localPosition.y, plusOffsetY + KTZMapsGeneration.zoneHeight * q, plusOffsetY + (KTZMapsGeneration.zoneHeight + 3) * q)).ToList();
                enemys.OrderBy(o => o.localPosition.x);
                rocketCount = 3;
                foreach (GameObject enemy in enemys)
                {
                    tX = -KTZEngineAplication.random.Next(4);
                    GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(tX, enemy.localPosition.y), enemy.localPosition, Vector2.right));
                    rocketCount--;
                    if (rocketCount <= 0) { break; }
                }
                for (int i = 0; i < rocketCount; i++)
                {
                    startX = -KTZEngineAplication.random.Next(0, 8);
                    tX = KTZEngineAplication.random.Next(3, layer.layerWidth - 2);
                    // tY = KTZEngineAplication.random.Next(KTZMapsGeneration.zoneHeight, KTZMapsGeneration.zoneHeight + 3);
                    tY = plusOffsetY + (KTZMapsGeneration.zoneHeight + i) * q;
                    GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(startX, tY), new Vector2(tX, tY), Vector2.right));
                }
                // Attack to left
                enemys = GameData.myGameShell.game.gameObjectsList.Where(o => o.id.Contains(GameData.gameIds["enemy"]) && KTZMath.IsInInterval(o.localPosition.y, plusOffsetY + KTZMapsGeneration.zoneHeight * q, plusOffsetY + (KTZMapsGeneration.zoneHeight + 3) * q)).ToList();
                enemys.OrderBy(o => -o.localPosition.x);
                rocketCount = 3;
                foreach (GameObject enemy in enemys)
                {
                    tX = layer.layerWidth + KTZEngineAplication.random.Next(4);
                    GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(tX, enemy.localPosition.y), enemy.localPosition, Vector2.left));
                    rocketCount--;
                    if (rocketCount <= 0) { break; }
                }
                for (int i = 0; i < rocketCount; i++)
                {
                    startX = layer.layerWidth + KTZEngineAplication.random.Next(0, 8);
                    tX = KTZEngineAplication.random.Next(3, layer.layerWidth - 2);
                    // tY = KTZEngineAplication.random.Next(KTZMapsGeneration.zoneHeight, KTZMapsGeneration.zoneHeight + 3);
                    tY = plusOffsetY + (KTZMapsGeneration.zoneHeight + i) * q;
                    GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(startX, tY), new Vector2(tX, tY), Vector2.left));
                }
            }
        }
    }

    public class GoodBullet : GameObject
    {
        public Vector2 dir;
        public const char bulletIcon = '•';
        public Vector2 layerSize; 
        public Game game;
        public GoodBullet(Game game_, GameLayer layer_, Vector2 pos, Vector2 dir_) : base("bullet" + KTZEngineAplication.GenerateProtectName(), layer_, pos, bulletIcon, new List<string>() { GameData.gameIds["bullet"] })
        {
            localPosition = pos;
            globalPosition = pos + layer.layerStartPosition;
            dir = dir_;
            regularVelocity = dir;
            icon = bulletIcon;
            layerSize = new Vector2(layer.layerWidth, layer.layerHeight);
            game = game_;
        }
        public GoodBullet(Game game_, GameLayer layer_, Vector2 pos, Vector2 dir_, string plusId) : base("bullet" + KTZEngineAplication.GenerateProtectName(), layer_, pos, bulletIcon, new List<string>() { GameData.gameIds["bullet"], plusId })
        {
            localPosition = pos;
            globalPosition = pos + layer.layerStartPosition;
            dir = dir_;
            regularVelocity = dir;
            icon = bulletIcon;
            layerSize = new Vector2(layer.layerWidth, layer.layerHeight);
            game = game_;
        }

        public override void Update()
        {
            if (!KTZMath.IsInInterval(localPosition , Vector2.downLeft, layerSize) || game.gameGrid.layers["staticObjects"].staticObjects.Exists(o => o.id.Contains(GameData.gameIds["collision"]) && o.globalPosition == globalPosition)) { game.DeleteGameObject(name); }
            
        }

        public override void AfterMoveUpdate()
        {
            GameData.bulletPositionList.Add(globalPosition);
        }
    }

    public class SimpleBayraktarRocket : GoodBullet
    {
        public Vector2 target;

        public static char basicRightChar = '╠';
        public static char basicLeftChar = '╣';
        public static char basicUpChar = '╩';
        public static char basicDownChar = '╦';

        public static Dictionary<Vector2, char> icons = new Dictionary<Vector2, char>() { { Vector2.up, basicUpChar }, { Vector2.down, basicDownChar }, { Vector2.right, basicRightChar }, { Vector2.left, basicLeftChar } };

        public SimpleBayraktarRocket(Game game_, GameLayer layer_, Vector2 pos, Vector2 target_, Vector2? direction=null) : base(game_, layer_, pos, Vector2.up)
        {
            if (direction == null) { direction = Vector2.up; }
            dir = (Vector2)direction;
            regularVelocity = dir;

            target = target_;
            icon = icons[dir];
        }

        public override void Update() 
        {
            if (localPosition == target || GameData.enemyPositionList.Contains(globalPosition))
            {
                InTarget();
            }
            else if (!KTZMath.IsInInterval(localPosition, Vector2.downLeft * 10, layerSize + Vector2.upRight * 9) && game.gameObjects.Keys.Contains(name)) { game.DeleteGameObject(name); }
        }

        public override void AfterMoveUpdate() {}

        public void InTarget()
        {
            regularVelocity = Vector2.zero;

            bool canPlantMine = true;

            foreach (GameLayer checkLayer in GameData.myGameShell.game.gameGrid.layers.Values)
            {
                if (checkLayer.staticObjects.Find(o => o.globalPosition == globalPosition && (o.id.Contains(GameData.gameIds["collision"]) || o.id.Contains(GameData.gameIds["pricklyHedgehogs"]) || o.ico == '█')) != null) { canPlantMine = false; break; }
            }

            if (canPlantMine)
            {
                GoodMine explosionMine = new GoodMine(globalPosition, GameData.myGameShell.gameObjectsLayer);
                game.AddExistGameObject(explosionMine);
                explosionMine.Explosion();

                explosionMine = new GoodMine(globalPosition, GameData.myGameShell.gameObjectsLayer);
                game.AddExistGameObject(explosionMine);
            }

            game.DeleteGameObject(name);
        }
    }

    public class ShitrussiaTankZ : GameObject
    {
        public Vector2 layerSize;
        public MyGameShell myGameShell;
        public Vector2 dir;
        public static Vector2 priorityDir = Vector2.down;
        public List<string> actionList = new() { "stay", "stay", "stay", "changeDirection", "moveToDirection", "moveNice", "moveNice", "moveNice" };
        
        public string action;
        public List<GameLayer> collisonCheckLayers;
        public List<string> collisionCheckIds;
        public int moveIndex = 0;
        public int moveTick = 0;
        
        public static int sameMoveCount = 6;
        public static int basicEnemyStopTicks = 30;
        
        public int enemyStopTicks;

        public List<Vector2> statBlockDir = new List<Vector2>();

        public static List<Vector2> moveDirections = new List<Vector2>() { priorityDir, Vector2.GetVectorRotatedRight90(priorityDir), Vector2.GetVectorRotatedLeft90(priorityDir), -priorityDir };

        public Type currObjType;
        public string currStatObjId;

        public List<GameObject> currPosObjects;
        public StaticGameObject currPosStaticObject;

        public bool isCloseToTarget = false;

        public static char basicIcon = 'z';

        public ShitrussiaTankZ (MyGameShell myGame_, GameLayer layer_, Vector2 pos) : base ("russiaTankZ" + KTZEngineAplication.GenerateProtectName(), layer_, pos, basicIcon, new List<string>() { GameData.gameIds["enemy"] })
        {
            localPosition = pos;
            layerSize = new Vector2(layer.layerWidth, layer.layerHeight);
            myGameShell = myGame_;
            dir = Vector2.zero;
            enemyStopTicks = basicEnemyStopTicks;
        }

        public override void PreUpdate()
        {
            isCloseToTarget = false;

            if (moveTick > sameMoveCount)
            {
                action = actionList[KTZEngineAplication.random.Next(0, actionList.Count)];
                moveTick = 0;
            }
            if (moveIndex < 0)
            {
                switch (action)
                    {
                        case "stay":
                            dir = Vector2.zero;
                            break;
                        case "changeDirection":
                            dir = moveDirections.ElementAt(KTZEngineAplication.random.Next(moveDirections.Count));
                            break;
                        case "moveNice":
                            if (!blockedDirections.Contains(priorityDir)) { dir = priorityDir; }
                            else
                            {
                                dir = moveDirections[KTZEngineAplication.random.Next(2)];
                                if (blockedDirections.Contains(dir)) { dir.RotationToRight90(2); if (blockedDirections.Contains(dir)) { dir = -priorityDir; } }
                            }
                            break;
                    }
                disposableVelocity = dir;
                moveTick++;

            }
            
            if (disposableVelocity != Vector2.zero)
            {
                statBlockDir = SideCollisionDetect();
            }
            
            moveIndex = (disposableVelocity != Vector2.zero && moveIndex <= 0) ? enemyStopTicks : moveIndex - 1;
            

            blockedDirections = statBlockDir;
        }

        public override void Update() 
        { 
            AlmostAtFinishCheck();
        }

        public override void AfterUpdate()
        {
            CurrentPosCollision();
            if (isCloseToTarget && GameData.aplication.currentGameTabName == "Game")
            {
                enemyStopTicks = 30 + GameData.currentCityIndex * 2;
                Console.BackgroundColor = GameData.dangerColor;
                GameData.myGameShell.game.PostProcessing(globalPosition, 'Z');
                Console.BackgroundColor = GameData.aplication.ConsoleBackgroundColor;
            }
        }

        public List<Vector2> SideCollisionDetect()
        {
            List<Vector2> blockedDir = new();

            for (int dirI = 0; dirI < Vector2.fourDirections.Count; dirI++)
            {
                if (GameData.wallPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) || GameData.enemyPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) || globalPosition + Vector2.fourDirections[dirI] == GameData.player.globalPosition)
                {
                    blockedDir.Add(Vector2.fourDirections[dirI]);
                    continue;
                }

            }

            return blockedDir;
        }

        public void CurrentPosCollision ()
        {
            if (GameData.bulletPositionList.Contains(globalPosition)) {
                GameObject currBull = myGameShell.game.gameObjectsList.Find(o => o.id.Contains(GameData.gameIds["bullet"]) && o.globalPosition == globalPosition);
                if (currBull != null)
                {
                    if (currBull.id.Contains(GameData.gameIds["playerBullet"])) { GameData.score++; }
                    myGameShell.game.DeleteGameObject(currBull.name);
                    GameData.bulletPositionList.Remove(globalPosition);
                    Die();
                }
            }
            
            else if (globalPosition == GameData.traktorMykola.globalPosition || GameData.pricklyHedgehogsPositionList.Contains(globalPosition)) { Die(); }
            else if (GameData.minePositionList.Contains(globalPosition))
            { 
                GoodMine currMine = (GoodMine)(GameData.myGameShell.game.gameObjectsList.Find(o => o.id.Contains(GameData.gameIds["mine"]) && o.globalPosition == globalPosition));
                if (currMine != null) { currMine.Explosion(); }
            }

            if (GameData.slowdownPositionList.Contains(globalPosition)) { enemyStopTicks = basicEnemyStopTicks * 3; }
            
        }

        public void AlmostAtFinishCheck()
        {
            if (localPosition.y < KTZMapsGeneration.zoneHeight * 1.5 + GameData.currentCityIndex && GameData.aplication.currentGameTabName == "Game")
            {
                if (GameData.currentCityIndex == 0)
                {
                    if (GameData.myGameShell.updateIndex % 10 == 0)
                    {
                        GameData.myGameShell.game.AddExistGameObject(new SimpleBayraktarRocket(GameData.myGameShell.game, layer, new Vector2(localPosition.x, -2), localPosition));
                        GameData.myGameShell.GenerateEnemy();
                    }
                }
                else
                {
                    isCloseToTarget = true;
                    GameData.isDanger = true;
                }
            }
        }

        public void Die()
        {
            GameData.enemyPositionList.Remove(globalPosition);
            myGameShell.game.DeleteGameObject(name);
        }

        public void SetPriorDir(Vector2 dir)
        {
            priorityDir = dir;
            moveDirections = new List<Vector2>() { priorityDir, Vector2.GetVectorRotatedRight90(priorityDir), Vector2.GetVectorRotatedLeft90(priorityDir), -priorityDir };
        }
    }

    public class GoodMine : GameObject
    {
        public List<GameObject> objectsToKill = new List<GameObject>();
        public bool isExplosion = false;
        public GameObject collisionBullet = null;
        public GameObject nextExplosionMine = null;

        public const char mine1Ico = 'Θ';
        public const char mine2Ico = 'O';
        public static char[] icons = new char[] { mine1Ico, mine2Ico };
        public static int iconIndex = 0;

        public static int animationMaxTick = 12;

        public GoodMine(Vector2 pos, GameLayer layer_, bool currentPosDetect=true) : base("GoodMine" + KTZEngineAplication.GenerateProtectName(), layer_, pos, 'Θ', new List<string> { GameData.gameIds["mine"]}, -1) 
        {
            if (currentPosDetect)
            {
                GoodMine currPosMine = (GoodMine)GameData.myGameShell.game.gameObjectsList.ToList().Find(o => o.GetType() == GameData.goodMineType && o.globalPosition == globalPosition);
                if (currPosMine != null)
                {
                    // GameData.myGameShell.game.gameObjects.Remove(currPosMine.name);
                    GameData.myGameShell.game.DeleteGameObject(currPosMine.name);
                }
            }

        }

        public override void PreUpdate()
        {
            IconAnimation();
            
        }

        public override void AfterUpdate()
        {
            BulletCollison();
        }

        public void Explosion(GameObject detectObj)
        {
            isExplosion = true;

            AnotherMineExplosion();
            FindAllObjectsToKill();

            objectsToKill.Remove(detectObj);

            for (int q = 0; q < objectsToKill.Count; q++)
            {
                // GameData.myGame.game.DeleteGameObject(objectsToKill[q].name);
                ((ShitrussiaTankZ)objectsToKill[q]).Die();
            }

            if (GameData.myGameShell.game.gameObjects.ContainsKey(detectObj.name)) { GameData.myGameShell.game.DeleteGameObject(detectObj.name); }
            // GameData.myGame.game.DeleteGameObject(detectObj.name);


            GameData.myGameShell.game.DeleteGameObject(name);
        }
        public void Explosion()
        {
            isExplosion = true;

            AnotherMineExplosion();
            FindAllObjectsToKill();

            for (int q = 0; q < objectsToKill.Count; q++)
            {
                // GameData.myGame.game.DeleteGameObject(objectsToKill[q].name);
                ((ShitrussiaTankZ)objectsToKill[q]).Die();
            }

            
            GameData.myGameShell.game.DeleteGameObject(name);
        }

        public void FindAllObjectsToKill(GameObject detectedObj)
        {
            for (int q = 0; q < Vector2.eightDirections.Count; q++) { objectsToKill.AddRange(GameData.myGameShell.game.gameObjectsList.FindAll(o => o.globalPosition == globalPosition + Vector2.eightDirections[q] && o.GetType() == GameData.shitrussiaTankZType)); }
            objectsToKill.AddRange(GameData.myGameShell.game.gameObjectsList.FindAll(o => o.globalPosition == globalPosition && o.GetType() == GameData.shitrussiaTankZType));
            // mineKillObjects.Add(GameData.myGame.game.gameObjects.Values.ToList().Find(o => o.globalPosition == globalPosition && o.GetType() == GameData.goodMineType));
            objectsToKill.Remove(detectedObj);
            objectsToKill.Remove(this);
        }
        public void FindAllObjectsToKill()
        {
            for (int q = 0; q < Vector2.eightDirections.Count; q++) { objectsToKill.AddRange(GameData.myGameShell.game.gameObjectsList.FindAll(o => o.globalPosition == globalPosition + Vector2.eightDirections[q] && o.GetType() == GameData.shitrussiaTankZType)); }
            objectsToKill.AddRange(GameData.myGameShell.game.gameObjectsList.FindAll(o => o.globalPosition == globalPosition && o.GetType() == GameData.shitrussiaTankZType));
            // mineKillObjects.Add(GameData.myGame.game.gameObjects.Values.ToList().Find(o => o.globalPosition == globalPosition && o.GetType() == GameData.goodMineType));
            objectsToKill.Remove(this);
        }

        public void AnotherMineExplosion()
        {
            for (int q = 0; q < Vector2.eightDirections.Count; q++)
            {
                nextExplosionMine = GameData.myGameShell.game.gameObjectsList.Find(o => o.GetType() == GameData.goodMineType && !((GoodMine)o).isExplosion && o.globalPosition == globalPosition + Vector2.eightDirections[q]);
                if (nextExplosionMine != null) { ((GoodMine)nextExplosionMine).Explosion(); }
                // Debug.WriteLine(Vector2.namedEightDirections.ElementAt(q).Key);
            }
        }

        public void BulletCollison()
        {
            if (GameData.bulletPositionList.Contains(globalPosition))
            {
                collisionBullet = GameData.myGameShell.game.gameObjectsList.Find(o => o.GetType() == GameData.goodBulletType && o.globalPosition == globalPosition && o.GetType() != GameData.simpleBayraktarRocketType);
                if (collisionBullet != null) { Explosion(collisionBullet); }
            }
        }

        public void IconAnimation()
        {
            icon = icons[iconIndex];
        }

    }

    public class UkraineTraktorMykola : GameObject
    {
        public Vector2 layerSize;
        public MyGameShell myGame;
        public Vector2 dir;
        public List<string> actionList = new() { "stay", "stay", "changeDirection", "moveToDirection", "moveToDirection" };
        public string action;
        public int moveIndex = 0;
        public int moveTick = 0;
        public static int sameMoveCount = 6;
        public static int NickolaStopTicks = 20;
        public static int attackMaxLength = 6;

        public Vector2 targetPos;
        public Vector2 directionToTarget;
        public bool isAttack = false;

        
        public static char[] icons = new char[] { 'O', 'O', 'o', '—', '—', '—', '—', 'o', 'O', 'O' };
        public int iconIndex = 0;
        public int animationTick = 0;
        public static int animationMaxTickNormal = 3;
        public static int animationMaxTickRage = 1;
        public static int animationMaxTick = animationMaxTickNormal;
        public List<Vector2> statBlockDir = new List<Vector2>();

        public UkraineTraktorMykola(MyGameShell myGame_, GameLayer layer_, Vector2 pos) : base("TankMykola", layer_, pos, '\u00f6', new List<string>() { GameData.gameIds["Mykola"], GameData.gameIds["collision"] }, 2)
        {
            localPosition = pos;
            layerSize = new Vector2(layer.layerWidth, layer.layerHeight);
            myGame = myGame_;
            dir = Vector2.zero;
        }

        public override void PreUpdate()
        {
            if (moveTick > sameMoveCount)
            {
                if (!isAttack)
                {
                    action = actionList[KTZEngineAplication.random.Next(0, actionList.Count)];
                    moveTick = 0;
                }
            }
            if (moveIndex < 0 && !isAttack)
            {
                switch (action)
                {
                    case "stay":
                        dir = Vector2.zero;
                        break;
                    case "changeDirection":
                        dir = Vector2.fourDirections.ElementAt(KTZEngineAplication.random.Next(4));
                        break;
                }
                disposableVelocity = dir;
                moveTick++;
            }

            statBlockDir = StaticObjectsCollisionDetect(); 
            
            moveIndex = (disposableVelocity != Vector2.zero && moveIndex <= 0) ? NickolaStopTicks : moveIndex - 1;

            blockedDirections = statBlockDir;

            AttackTanks();
            IconAnimation();
        }

        public override void AfterUpdate()
        {
            BulletEat();
        }

        public List<Vector2> StaticObjectsCollisionDetect()
        {
            List<Vector2> blockedDir = new();

            for (int dirI = 0; dirI < Vector2.fourDirections.Count; dirI++)
            {

                if (GameData.wallPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) ||
                    GameData.collisionIdPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) ||
                    GameData.pricklyHedgehogsPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) ||
                    GameData.minePositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) ||
                    globalPosition + Vector2.fourDirections[dirI] == GameData.player.globalPosition)
                { blockedDir.Add(Vector2.fourDirections[dirI]); continue; }
            }

            if (GameData.allWallPositionList.Contains(globalPosition)) { globalPosition = Vector2.upRight; }
             

            return blockedDir;
        }
         
        public void AttackTanks()
        {
            Vector2 lookPos;
            for (int i = 0; i < Vector2.fourDirections.Count && !isAttack; i++)
            {
                for (int q = 0; q < attackMaxLength && !isAttack; q++)
                {
                    lookPos = globalPosition + Vector2.fourDirections[i] * q;
                    if (!GameData.wallPositionList.Contains(lookPos))
                    {
                        if (GameData.enemyPositionList.Contains(lookPos))
                        {
                            targetPos = lookPos;
                            isAttack = true;
                            directionToTarget = Vector2.fourDirections[i];
                            dir = directionToTarget;
                        }
                    } else { break; }
                }
            }
            if (globalPosition == targetPos) { isAttack = false; }
            if (isAttack) { disposableVelocity = directionToTarget; animationMaxTick = animationMaxTickRage; }
            else { animationMaxTick = animationMaxTickNormal; }
        }

        public void IconAnimation()
        {
            animationTick++;
            if (animationTick >= animationMaxTick)
            {
                iconIndex++;
                iconIndex = KTZMath.LoopNumberInInterval(iconIndex, 0, icons.Length - 1);
                icon = icons[iconIndex];
                animationTick = 0;
            }
        }

        public void BulletEat()
        {
            if (GameData.bulletPositionList.Contains(globalPosition))
            {
                GameObject bulletCollision = myGame.game.gameObjectsList.Find(o => (o.id.Contains(GameData.gameIds["bullet"])) && o.globalPosition == globalPosition);
                if (bulletCollision != null)
                {
                    disposableVelocity = (disposableVelocity.Direction.x != bulletCollision.finalVelocity.Direction.x ^ disposableVelocity.Direction.y != bulletCollision.finalVelocity.Direction.y) ? disposableVelocity + bulletCollision.finalVelocity : disposableVelocity;
                    myGame.game.DeleteGameObject(bulletCollision.name);
                }
            }
        }
    }

    public class EnemySlowDownTile : StaticGameObject
    {
        public EnemySlowDownTile(Vector2 pos_, GameLayer layer_, char ico_) : base(pos_, layer_, ico_, new List<string> { GameData.gameIds["enemySlowDown"] }) { }
    }

    public class PricklyHedgehogs : StaticGameObject
    {
        public static char basicIcon = 'x';
        public PricklyHedgehogs(Vector2 pos, GameLayer layer) : base(pos, layer, basicIcon, new List<string>() { GameData.gameIds["pricklyHedgehogs"] }) { }
    }

    public class EndGameTab : SelectListTab
    {

        public EndGameTab(string tabName, List<SLTItem> outputText) : base(GameData.aplication, outputText, tabName, 1) 
        {
            selectItemLeftSign = "";
            selectItemRightSign = "";
            keyManager.keyPressActions.Add(ConsoleKey.Escape, GoToMenu);
            keyManager.keyPressActions[ConsoleKey.Enter] = ContinueGame;
            keyManager.keyPressActions.Add(ConsoleKey.Spacebar, ContinueGame);

            itemList.Add(new SLTItem("GoToMenu", GameData.aplication.gameText.GetText("PressEscape"), GameData.EmptyMethod, 3));
            itemList.Add(new SLTItem("ContinueGame", GameData.aplication.gameText.GetText("PressEnter"), GameData.EmptyMethod, 1));

            AlignToCenter();
            HeightAlignToCenter();
        }

        public void GoToMenu() { Console.Clear(); aplication.currentGameTabName = GameData.mainMenu.menuTab.name; }
        public void ContinueGame() { GameData.mainMenu.StartGame(); }
    }

    public class FinalWinTab : EndGameTab
    {
        public FinalWinTab() : base("FinalWinTab", new List<SLTItem>() )
        {
            keyManager.keyPressActions = new Dictionary<ConsoleKey, Action>() { { ConsoleKey.Enter, PressEnterOrSpace }, { ConsoleKey.Spacebar, PressEnterOrSpace } };
            TextUpdate();
        }


        public void TextUpdate()
        {
            itemList = new List<SLTItem>() 
            { 
                new SLTItem("Message1", GameData.aplication.gameText.GetText("Congratulations"), GameData.EmptyMethod, 0), 
                new SLTItem("Message2", GameData.aplication.gameText.GetText("Today") + GameData.virtualTime.ToShortDateString() + GameData.aplication.gameText.GetText("IsHistoricDay"), GameData.EmptyMethod, 2),
                new SLTItem("Message3", GameData.aplication.gameText.GetText("YouCapturemoscow"), GameData.EmptyMethod, 2), 
                new SLTItem("Message4", GameData.aplication.gameText.GetText("YouFinWinWar") + GameData.warStartVirtualTime.ToShortDateString(), GameData.EmptyMethod, 2), 
                new SLTItem("Message5", GameData.aplication.gameText.GetText("PressEnterToContinue"), GameData.EmptyMethod, 7) 
            };

            AlignToCenter();
            HeightAlignToCenter();
        }

        public void PressEnterOrSpace()
        {
            Console.Clear();
            GameData.aplication.currentGameTabName = GameData.moscowDecideFateTab.name;
        }
    }

    public class WarStartTab : EndGameTab
    {
        public WarStartTab() : base("warStartTab", new List<SLTItem>())
        {
            keyManager.keyPressActions = new Dictionary<ConsoleKey, Action>() { { ConsoleKey.Enter, PressEnterOrSpace }, { ConsoleKey.Escape, PressEnterOrSpace }, { ConsoleKey.Spacebar, PressEnterOrSpace } };
            TextUpdate();
        }

        public void TextUpdate()
        {
            itemList = new List<SLTItem>()
            {
                new SLTItem("Message1", GameData.aplication.gameText.GetText("StartWar1"), GameData.EmptyMethod, 0),
                new SLTItem("Message2", GameData.aplication.gameText.GetText("StartWar2") + GameData.warStartVirtualTime.ToShortDateString(), GameData.EmptyMethod, 0),
                new SLTItem("Message3", GameData.aplication.gameText.GetText("StartWar3"), GameData.EmptyMethod, 3),
                new SLTItem("Message4", GameData.aplication.gameText.GetText("StartWar4"), GameData.EmptyMethod, 0),
                new SLTItem("Message5", GameData.aplication.gameText.GetText("StartWar5"), GameData.EmptyMethod, 0),
                new SLTItem("Message6", GameData.aplication.gameText.GetText("StartWar6"), GameData.EmptyMethod, 0),
                new SLTItem("Message7", GameData.aplication.gameText.GetText("StartWar7"), GameData.EmptyMethod, 2),
                new SLTItem("Message8", GameData.aplication.gameText.GetText("StartWar8"), GameData.EmptyMethod, 0),
                new SLTItem("Message9", GameData.aplication.gameText.GetText("StartWar9"), GameData.EmptyMethod, 0),
                new SLTItem("Message10", GameData.aplication.gameText.GetText("StartWar10"), GameData.EmptyMethod, 0),
                new SLTItem("Message11", GameData.aplication.gameText.GetText("StartWar11"), GameData.EmptyMethod, 0),
                new SLTItem("Message12", GameData.aplication.gameText.GetText("StartWar12"), GameData.EmptyMethod, 0),
                new SLTItem("Message13", GameData.aplication.gameText.GetText("StartWar13"), GameData.EmptyMethod, 0),
                new SLTItem("Message14", GameData.aplication.gameText.GetText("StartWar14"), GameData.EmptyMethod, 2),
                new SLTItem("Message15", GameData.aplication.gameText.GetText("StartWar15"), GameData.EmptyMethod, 0),
                new SLTItem("Message16", GameData.aplication.gameText.GetText("StartWar16"), GameData.EmptyMethod, 3),
            };

            AlignToCenter();
            HeightAlignToCenter();
        }

        public void PressEnterOrSpace()
        {
            Console.Clear();
            GameData.aplication.currentGameTabName = GameData.mainMenu.menuTab.name;
        }
    }



    public class UkraineSoldier : GameObject
    {
        public Vector2 layerSize;
        public MyGameShell myGame;
        public Vector2 dir;
        public Vector2 priorityDir = Vector2.up;
        public List<string> actionList = new() { "stay", "stay", "stay", "changeDirection", "moveToDirection", "moveNice", "moveNice", "moveNice" };

        public string action;
        public int moveIndex = 0;
        public int moveTick = 0;
        public int shootIndex = 0;
        public static int sameMoveCount = 3;
        public static int basicUASoldierStopTicks = 10;
        public static int maxShootIndex = 6;
        public int mineCount = 3;
        public int soldierStopTicks;

        public static int attackMaxDistance = 10;

        public List<Vector2> statBlockDir = new ();

        public static List<Vector2> moveDirections;
        public static List<Vector2> canMoveDirections;

        public string currStatObjId;

        public List<GameObject> currPosObjects;
        public StaticGameObject currPosStaticObject;

        public static Dictionary<Vector2, char> soldierPoses = new Dictionary<Vector2, char>() { { Vector2.up, '˄' }, { Vector2.right, '>' }, { Vector2.down, '˅' }, { Vector2.left, '<' } };

        public static List<Vector2> stupidPositionToBlock = new() { };
        public static List<Vector2> cornersPos = new() { };

        public UkraineSoldier(MyGameShell myGame_, GameLayer layer_, Vector2 pos) : base("UkraineSoldier" + KTZEngineAplication.GenerateProtectName(), layer_, pos, '>', new List<string>() { GameData.gameIds["UkraineSoldier"], GameData.gameIds["collision"] })
        {
            localPosition = pos;
            layerSize = new Vector2(layer.layerWidth, layer.layerHeight);
            myGame = myGame_;
            dir = Vector2.up;
            SetPriorDir(Vector2.up);
            canMoveDirections = new List<Vector2>() { Vector2.up, Vector2.right, Vector2.left, Vector2.down };
            soldierStopTicks = basicUASoldierStopTicks;
        }

        public override void PreUpdate()
        {
            icon = (dir == Vector2.zero) ? icon : soldierPoses[dir];

            soldierStopTicks = basicUASoldierStopTicks;
            CurrentPosCollision();
            
            if (shootIndex == 0) { AttackEnemy(); }
            

            if (localPosition.y > layer.layerHeight - (int)(KTZMapsGeneration.zoneHeight * 1.5))
            {
                sameMoveCount = 6;
                if (priorityDir == Vector2.up && !Convert.ToBoolean(KTZEngineAplication.random.Next(3)))
                {
                    SetPriorDir(Vector2.fourDirections[KTZEngineAplication.random.Next(2)]);
                }
                else if (priorityDir != Vector2.left && localPosition.x > layer.layerWidth - 1 - (int)(KTZMapsGeneration.zoneHeight * 1.5) && !Convert.ToBoolean(KTZEngineAplication.random.Next(3)))
                {
                    SetPriorDir(Vector2.left);
                }
                else if (priorityDir != Vector2.right && localPosition.x < (int)(KTZMapsGeneration.zoneHeight * 1.5) && !Convert.ToBoolean(KTZEngineAplication.random.Next(3)))
                {
                    SetPriorDir(Vector2.right);
                }
            } 
            else if (localPosition.y < (int)(layer.layerHeight / 2) && priorityDir != Vector2.up)
            {
                SetPriorDir(Vector2.up);
            }

            if (moveTick > sameMoveCount)
            {
                action = actionList[KTZEngineAplication.random.Next(0, actionList.Count)];
                moveTick = 0;
            }
            if (moveIndex < 0)
            {
                switch (action)
                {
                    case "stay":
                        dir = Vector2.zero;
                        if (KTZEngineAplication.random.Next(100) == 7 && blockedDirections.Count < 3)  { PlantMine(); }
                        break;
                    case "changeDirection":
                        dir = moveDirections.ElementAt(KTZEngineAplication.random.Next(moveDirections.Count));
                        break;
                    case "moveNice":
                        if (!blockedDirections.Contains(priorityDir)) { dir = priorityDir; }
                        else
                        {
                            dir = moveDirections[1 + KTZEngineAplication.random.Next(1)];
                            if (blockedDirections.Contains(dir)) { dir.RotationToRight90(2); if (blockedDirections.Contains(dir)) { dir = -priorityDir; } }
                        }
                        break;
                }
                disposableVelocity = dir;
                moveTick++;
            }
            

            if (disposableVelocity != Vector2.zero)
            {
                statBlockDir = StaticObjectsCollisionDetect();
            }

            moveIndex = (disposableVelocity != Vector2.zero && moveIndex <= 0) ? soldierStopTicks : moveIndex - 1;
            shootIndex = (shootIndex < 0) ? 0 : shootIndex - 1;


            blockedDirections = statBlockDir;

            
        }

        public override void AfterUpdate()
        {
            BulletEat();
        }

        public List<Vector2> StaticObjectsCollisionDetect()
        {
            int stupidBlockedDirCount = 0;
            List<Vector2> blockedDir = new();
            for (int dirI = 0; dirI < Vector2.fourDirections.Count; dirI++)
            {
                if (stupidPositionToBlock.Contains(globalPosition + Vector2.fourDirections[dirI]) || GameData.wallPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) || GameData.pricklyHedgehogsPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]))
                {
                    blockedDir.Add(Vector2.fourDirections[dirI]);
                    stupidBlockedDirCount++;
                    continue;
                }
                else if (GameData.collisionIdPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) || GameData.minePositionList.Contains(globalPosition + Vector2.fourDirections[dirI]))
                {
                    blockedDir.Add(Vector2.fourDirections[dirI]); continue;
                }
                
            }

            
            if (stupidBlockedDirCount >= 4)
            {
                blockedDir = new();
                for (int dirI = 0; dirI < Vector2.fourDirections.Count; dirI++)
                {
                    if (GameData.wallPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) || GameData.pricklyHedgehogsPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]))
                    {
                        blockedDir.Add(Vector2.fourDirections[dirI]);
                        stupidBlockedDirCount++;
                        continue;
                    }
                    else if (GameData.collisionIdPositionList.Contains(globalPosition + Vector2.fourDirections[dirI]) || GameData.minePositionList.Contains(globalPosition + Vector2.fourDirections[dirI]))
                    {
                        blockedDir.Add(Vector2.fourDirections[dirI]); continue; 
                    }
                
                }
            }
            

            if (stupidBlockedDirCount == 2 && blockedDir[0].x != -blockedDir[1].x && 
                !GameData.wallPositionList.Contains(globalPosition - blockedDir[0] - blockedDir[1]) && 
                !stupidPositionToBlock.Contains(globalPosition - blockedDir[0] - blockedDir[1]) &&
                localPosition > 1 && localPosition < layer.layerWidth - 2
                )
            { stupidPositionToBlock.Add(globalPosition); }
            else if (stupidBlockedDirCount > 2) { stupidPositionToBlock.Add(globalPosition); }   

            return blockedDir;
        }

        public void BulletEat()
        {
            if (GameData.bulletPositionList.Contains(globalPosition)) { 
                GameObject bulletCollision = myGame.game.gameObjectsList.Find(o => (o.id.Contains(GameData.gameIds["bullet"])) && o.globalPosition == globalPosition);
                if (bulletCollision != null) 
                { 
                    disposableVelocity = (disposableVelocity.Direction.x != bulletCollision.finalVelocity.Direction.x ^ disposableVelocity.Direction.y != bulletCollision.finalVelocity.Direction.y) ? disposableVelocity + bulletCollision.finalVelocity : disposableVelocity; 
                    myGame.game.DeleteGameObject(bulletCollision.name); 
                }
            }
        }

        public void CurrentPosCollision()
        {
            if (GameData.slowdownPositionList.Contains(globalPosition))
            {
                soldierStopTicks = basicUASoldierStopTicks * 3;
            }

        }

        public void PlantMine()
        {
            if (mineCount > 0 && !GameData.minePositionList.Contains(globalPosition)) { myGame.game.AddExistGameObject(new GoodMine(globalPosition, layer)); mineCount--; }
        }

        public bool AttackEnemy()
        {
            Vector2 lookPos;
            for (int i = 0; i < Vector2.fourDirections.Count; i++)
            {
                for (int q = 0; q < attackMaxDistance; q++)
                {
                    lookPos = globalPosition + Vector2.fourDirections[i] * q;
                    if (!GameData.wallPositionList.Contains(lookPos)/*myGame.staticObjectsLayer.staticObjects.Find(o => o.globalPosition == lookPos && o.id.Contains(GameData.gameIds["collision"])) == null*/)
                    {
                        if (GameData.enemyPositionList.Contains(lookPos)/*myGame.game.gameObjects.Values.ToList().Find(o => o.globalPosition == lookPos && o.id.Contains(GameData.gameIds["enemy"])) != null*/)
                        {
                            dir = Vector2.fourDirections[i];
                            // disposableVelocity = dir;
                            Shot();
                            shootIndex = maxShootIndex;
                            return true;
                        }
                    }
                    else { break; }
                }
            }

            return false;
        }

        public void Shot()
        {
            myGame.game.AddExistGameObject(new GoodBullet(myGame.game, layer, localPosition, dir)); moveIndex = soldierStopTicks;
        }

        public void SetPriorDir(Vector2 dir)
        {
            if (dir == Vector2.zero) 
            {
                priorityDir = Vector2.zero;
                moveDirections = new List<Vector2>() { Vector2.up, Vector2.right, Vector2.left, Vector2.down };
            }
            else
            {
                priorityDir = dir;
                moveDirections = new List<Vector2>() { priorityDir, Vector2.GetVectorRotatedRight90(priorityDir), Vector2.GetVectorRotatedLeft90(priorityDir), -priorityDir };
            }
        }
    }

    public class MineRecoverBox : GameObject
    {
        public static int countOfRecoverMine = 10;
        public const char standartIcon = '◙';
        public static char statStandartIcon = standartIcon;

        public MineRecoverBox(GameLayer layer_, Vector2 pos) : base("MineRecoverBox" + KTZEngineAplication.GenerateProtectName(), layer_, pos, standartIcon, new List<string>() { GameData.gameIds["mineRecoverBox"] })
        { }

        public override void AfterUpdate()
        {
            if (globalPosition == GameData.player.globalPosition)
            {
                GameData.player.mineCount += countOfRecoverMine;
                GameData.myGameShell.game.DeleteGameObject(name);
            }
            else if (GameData.UkraineSoldierPositionList.Contains(globalPosition))
            {
                ((UkraineSoldier)GameData.myGameShell.game.gameObjectsList.Find(o => o.globalPosition == globalPosition && o.id.Contains(GameData.gameIds["UkraineSoldier"]))).mineCount += countOfRecoverMine; ;
                GameData.myGameShell.game.DeleteGameObject(name);
            }
        }

    }

    public static class GameData
    {

        public static KTZEngineAplication aplication = new();

        // Aplication
        public static int myGameWindowWidth = 125;
        public static int myGameWindowHeight = 70;

        // Menu
        public static MainMenu mainMenu;
        public static SettingsMenu settingsMenu;

        public static TutorialMenu tutorialMenu;

        public static EndGameTab lostCityTab;
        public static EndGameTab captureCityTab;
        public static FinalWinTab finalWinTab;
        public static DecideFateTab moscowDecideFateTab;
        public static WarStartTab warStartTab;

        // Game
        public static MyGameShell myGameShell;
        public static MyGame originalMyGame;
        public static int myGameWidth = 125;
        public static int myGameHeight = 60;

        public static int score = 0;

        public static DateTime virtualTime = new DateTime(2022, 4, 24);
        public static DateTime warStartVirtualTime = new DateTime(2014, 2, 20);
        public static int yearsOfPeace = 1;

        // Languages
        public static Language english;
        public static Language ukraine;
        public static Language polish;

        public static Dictionary<string, string> gameIds = new() 
        { { "collision", "Collision" }, { "player", "Player" }, 
            { "bullet", "Bullet" }, { "playerBullet", "PlayerBullet" }, 
            { "enemy", "Enemy" }, { "Mykola", "Mykola" }, { "mine", "Mine" }, 
            { "enemySlowDown", "EnemySlowDown" }, { "pricklyHedgehogs", "PricklyHedgehogs" }, 
            { "UkraineSoldier", "UkraineSoldier" }, { "mineRecoverBox", "MineRecoverBox" } };


        public static MyGame myGameRef = new(ref aplication, Vector2.upRight, "gameRef", 1);
        public static MyGameShell myGameShellRef = new(ref myGameRef, EmptyMethod, Vector2.zero, Vector2.upRight, false);
        public static GameLayer gameLayerRef = new (1, 1, "gameLayerRef", ' ', 0, Vector2.zero);
        public static UkraineTraktorMykola traktorUkraineMykolaRef = new(myGameShellRef, gameLayerRef, Vector2.zero);
        public static GoodBullet goodBulletRef = new (myGameRef, gameLayerRef, Vector2.zero, Vector2.zero);
        public static GoodMine goodMineRef = new (Vector2.zero, gameLayerRef, false);
        public static ShitrussiaTankZ shitrussiaTankZRef = new (myGameShellRef, gameLayerRef, Vector2.zero);
        public static EnemySlowDownTile enemySlowDownTileRef = new (Vector2.zero, gameLayerRef, '5');
        public static SimpleBayraktarRocket simpleBayraktarRocketRef = new(myGameRef, gameLayerRef, Vector2.zero, Vector2.upRight);

        public static readonly Type goodBulletType = goodBulletRef.GetType();
        // public static readonly Type traktorkUkraineMykolaType = traktorUkraineMykolaRef.GetType();
        public static readonly Type goodMineType = goodMineRef.GetType();
        public static readonly Type shitrussiaTankZType = shitrussiaTankZRef.GetType();
        // public static readonly Type enemySlowDownTileType = enemySlowDownTileRef.GetType();
        public static readonly Type simpleBayraktarRocketType = simpleBayraktarRocketRef.GetType();

        public static List<string> cityNames = new() { "Kyiv", "Poltava", "Kharkiv", "Lugansk", "Voronezh", "Lipetsk", "Tula", "Podolsk", "Moscow" };
        public static List<string> cityNamesUA = new() { "Kyiv", "Poltava", "Kharkiv", "Lugansk" };
        public static List<string> cityNamesRU = new() { "Voronezh", "Lipetsk", "Tula", "Podolsk", "Moscow" };

        public static Dictionary<string, int> cityEnemyCount = new() { { cityNames[0], 50 }, { cityNames[1], 100 }, { cityNames[2], 150 }, { cityNames[3], 250 }, { cityNames[4], 350 }, { cityNames[5], 500 }, { cityNames[6], 700 }, { cityNames[7], 1000 } };
        // public static List<string> CurrentCitys { get { return new List<string>() { cityNames[currentCityIndex], cityNames[currentCityIndex + 1] }; } }
        public static int currentCityIndex = 3;

        public static List<Vector2> wallPositionList = new() { };
        public static List<Vector2> allWallPositionList = new() { };
        public static List<Vector2> collisionIdPositionList = new() { };
        public static List<Vector2> enemyPositionList = new() { };
        public static List<Vector2> minePositionList = new() { };
        public static List<Vector2> slowdownPositionList = new() { };
        public static List<Vector2> bulletPositionList = new() { };
        public static List<Vector2> pricklyHedgehogsPositionList = new() { };
        public static List<Vector2> mineRecoverBoxPositionList = new() { };
        public static List<Vector2> UkraineSoldierPositionList = new() { };

        public static List<List<SLTItem>> tutorailTabPages;

        public static MyPlayer player;
        public static UkraineTraktorMykola traktorMykola;

        public static bool isDanger;
        public static ConsoleColor dangerColor = ConsoleColor.Red;
        public static void EmptyMethod() { } 
    }

}