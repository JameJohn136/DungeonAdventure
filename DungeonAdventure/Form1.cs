using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Media;

namespace DungeonAdventure
{
    public partial class Form1 : Form
    {
        // Variables
        int page;
        int health;
        int coins;
        int choiceNum;
        bool goldenKey;
        bool dart;
        bool stick;
        bool purchaseHeal;

        int prevPage;
        int prevHealth;
        int prevCoin;
        bool prevGoldenKey;
        bool prevDart;
        bool prevStick;
        bool prevPurchaseHeal;
        SoundPlayer heartSound;
        SoundPlayer coinSound;
        SoundPlayer hurtSound;

        public Form1()
        {
            InitializeComponent();
            health = 5;
            coins = 10;
            page = 1; //  Set the variables at the start to make sure they are correct
            choiceNum = 0;
            goldenKey = false;
            DisplayPage(); // ****REMOVE IF A MAIN MENU IS ADDED****
            heartSound = new SoundPlayer(Properties.Resources.heartSound); // Assign the sounds
            coinSound = new SoundPlayer(Properties.Resources.coinSound);
            hurtSound = new SoundPlayer(Properties.Resources.heartSound);

            Random randGen = new Random();
        }

        private void Button1Enabled(bool active) // These are used to more easily activate and deactivate the buttons
        {
            if (active) // If true is sent through the function
            {
                choice1Button.Enabled = true; // Allow the button to be pressed again
                choice1Button.BackColor = Color.Red; // Change the colours
            }
            else if (!active)
            {
                choice1Button.Enabled = false; // Disable the buttons function
                choice1Label.Text = ""; // Remove the text beside it if there was any
                choice1Button.BackColor = Color.DarkGray; // Make the colours grey
            }
        }

        private void Button2Enabled(bool active) // These are used to more easily activate and deactivate the buttons
        {
            if (active) // If true is sent through the function
            {
                choice2Button.Enabled = true; // Allow the button to be pressed again
                choice2Button.BackColor = Color.Green; // Change the colours
            } else if (!active)
            {
                choice2Button.Enabled = false; // Disable the buttons function
                choice2Label.Text = ""; // Remove the text beside it if there was any
                choice2Button.BackColor = Color.DarkGray; // Make the colours grey
            }
        }

        private void Button3Enabled(bool active) // These are used to more easily activate and deactivate the buttons
        {
            if (active) // If true is sent through the function
            {
                choice3Button.Enabled = true; // Allow the button to be pressed again
                choice3Button.BackColor = Color.Aquamarine; // Change the colours
            }
            else if (!active)
            {
                choice3Button.Enabled = false; // Disable the buttons function
                choice3Label.Text = ""; // Remove the text beside it if there was any
                choice3Button.BackColor = Color.DarkGray; // Make the colours grey
            }
        }

        private void Button4Enabled(bool active) // Same as above
        {
            if (active)
            {
                choice4Button.Enabled = true;
                choice4Button.BackColor = Color.BlueViolet;
            }
            else if (!active)
            {
                choice4Button.Enabled = false;
                choice4Label.Text = "";
                choice4Button.BackColor = Color.DarkGray;
            }
        }

        private void ContinueScreen() // Short cut to set everything to a continue screen layout
        {
            Button1Enabled(true);
            Button2Enabled(false);
            Button3Enabled(false);
            Button4Enabled(false);
            choice1Label.Text = "Continue";
        }

        private void UpdateStats()
        {
            healthLabel.Text = $"x {health.ToString()}";
            coinsLabel.Text = $"x {coins.ToString()}";
            pageLabel.Text = $"Page {page.ToString()}";

            if (goldenKey)
            {
                goldenKeyLabel.Text = "Golden Key";
            } else
            {
                goldenKeyLabel.Text = "";
            }

            if (dart)
            {
                dartLabel.Text = "Poison Dart";
            } else
            {
                dartLabel.Text = "";
            }

            if (stick)
            {
                stickLabel.Text = "Stick";
            } else
            {
                stickLabel.Text = "";
            }

            if (page == 12)
            {
                if (coins < 25 || stick)
                {
                    Button3Enabled(false);
                }
                if (coins < 10 || dart)
                {
                    Button2Enabled(false);
                }
                if (coins < 5 || purchaseHeal)
                {
                    Button1Enabled(false);
                }
            }

            prevPageButton.Enabled = true;
        }

        private void DisplayPage()
        {
            switch (page)
            {
                case 1:
                    displayLabel.Text = "You arrive in the dungeon and upon entering you see a skeleton!" +
                        "\n\n Do you want to fight it?";
                    choice1Label.Text = "Fight";
                    choice2Label.Text = "Sneak Past";
                    Button2Enabled(true);
                    Button3Enabled(false);
                    Button4Enabled(false);
                    break;
                case 2:
                    displayLabel.Text = "You managed to successfully sneak past the skeleton, because of that you managed " +
                        "to find a health potion!\n\n+1 HP";
                    ContinueScreen();
                    health++; // Add one Health
                    break;
                case 3:
                    displayLabel.Text = "You managed to successfully fight off the skeleton, and it even dropped some gold!" +
                        " \n\n+5 Gold";
                    coins += 5;
                    ContinueScreen();
                    break;
                case 4:
                    displayLabel.Text = $"You find an oddly placed chest? This doesn't feel like it should be here.\n\nOpen it?";
                    Button2Enabled(true);
                    choice1Label.Text = "Open it";
                    choice2Label.Text = "Leave it";
                    break;
                case 5:
                    displayLabel.Text = $"That was quite risky but it wasn't a trap, or a Mimic. " +
                        $"You found a strange golden key..\n\n+1 Golden Key";
                    ContinueScreen();
                    goldenKey = true;
                    break;
                case 6:
                    displayLabel.Text = "You did not open the chest, perhaps a smart move";
                    ContinueScreen();
                    break;
                case 7:
                    displayLabel.Text = "You see a strong guard blocking the door, what do you do?";
                    Button2Enabled(true);
                    Button3Enabled(true);
                    Button4Enabled(false);
                    choice1Label.Text = "Fight them (-?? HP)";
                    choice2Label.Text = "Bribe Them (-?? Coins)";
                    choice3Label.Text = "Find another Pathway";
                    break;
                case 8:
                    displayLabel.Text = "You mention it to the guard but they want 10 Coins in order to let you pass, What do you do?";
                    Button2Enabled(false);
                    Button3Enabled(false);
                    Button4Enabled(false);
                    if (coins >= 10) // Only allow you to bribe them if you have enough coins
                    {
                    Button2Enabled(true);
                    choice2Label.Text = "Pay them 10 Coins (-10 Coins)";
                    } else
                    {
                        choice2Label.Text = "Bribe Guard (-10 Coins)";
                    }
                    choice1Label.Text = "Fight them (-3 HP)";
                    break;
                case 9:
                    displayLabel.Text = "You have a tough fight with the guard, he knew you were there already so you didn't have the element of surprise\n\n-3 HP\n+10 Coins";
                    ContinueScreen();
                    health -= 3;
                    coins += 10;
                    break;
                case 10:
                    displayLabel.Text = "You give the guard the coins, he lets you pass\n\n-10 Coins";
                    ContinueScreen();
                    coins -= 10;
                    break;
                case 11:
                    displayLabel.Text = "You have a tough fight with the guard, but you had the element of surprise\n\n-1 HP\n+5 Coins";
                    health--;
                    coins += 5;
                    ContinueScreen();
                    break;
                case 12:
                    displayLabel.Text = "You find a traveling Merchant, would you like to buy anything? " +
                        "\n\n Health Potion (+1 HP) - 5 Coins" +
                        "\n Poison Dart (One Time Use) - 10 Coins" +
                        "\n Stick (One Time Use) - 25 Coins";
                    ContinueScreen();
                    Button1Enabled(false);
                    if (coins >= 5) // Check you have enough to buy
                    {
                        Button1Enabled(true);
                    }
                    if (coins >= 10)
                    {
                    Button2Enabled(true);
                    }
                    if (coins >= 25)
                    {
                    Button3Enabled(true);
                    }
                    Button4Enabled(true);
                    choice1Label.Text = "Buy Health Potion (5 Coins)";
                    choice2Label.Text = "Buy Poison Dart (10 Coins)";
                    choice3Label.Text = "Buy Stick (25 Coins)";
                    choice4Label.Text = "Leave";
                    break;
                case 18:
                    displayLabel.Text = "You leave the shop and find a locked door, what do you do?";
                    ContinueScreen();
                    Button1Enabled(false);
                    Button2Enabled(true);
                    if (goldenKey)
                    {
                        Button1Enabled(true);
                    }
                    choice1Label.Text = "Use Key (-1 Golden Key)";
                    choice2Label.Text = "Find another Path";
                    break;
                case 19:
                    displayLabel.Text = "The Key breaks after the door unlocks, you enter through the door";
                    ContinueScreen();
                    break;
                case 20:
                    displayLabel.Text = "You find a different path";
                    ContinueScreen();
                    break;
            }
            UpdateStats(); // Is always called no matter what
        }


        #region 
        private void choice1Button_Click(object sender, EventArgs e)
        {

            /// Check what page we are currently on, and then flip
            /// to the page you need to go to if you selected option 1

            prevPage = page;
            prevHealth = health;
            prevCoin = coins;
            prevGoldenKey = goldenKey;
            prevDart = dart;
            prevStick = stick;
            prevPurchaseHeal = purchaseHeal;
            if (page == 1) // See skeleton
            {
                page = 3; // Fight it
            }
            else if (page == 2) {
                page = 4;
            }
            else if (page == 3) {
                page = 4;
            }
            else if (page == 4) {
                page = 5;
            }
            else if (page == 5) {
                page = 7;
            }
            else if (page == 6) {
                page = 7;
            }
            else if (page == 7) {
                page = 11;
            } else if (page == 8)
            {
                page = 9;
            } else if (page == 9)
            {
                page = 12;
            } else if (page == 11)
            {
                page = 12;
            }
            else if (page == 12)
            {
                coins -= 5;
                purchaseHeal = true;
                Button1Enabled(false);
            } else if (page == 18)
            {
                goldenKey = false;
                page = 19;
            }    


            /// Display text and game options to screen based on the 
            /// current page
            DisplayPage();
            
        }

        private void choice2Button_Click(object sender, EventArgs e)
        {

            /// Check what page we are currently on, and then flip
            /// to the page you need to go to if you selected option 1
            prevPage = page;
            prevHealth = health;
            prevCoin = coins;
            prevGoldenKey = goldenKey;
            prevDart = dart;
            prevStick = stick;
            prevPurchaseHeal = purchaseHeal;
            if (page == 1) // See Skeleton
            {
                page = 2; // Sneak Past
            }
            else if (page == 4) {
                page = 6;
            }
            else if (page == 7) {
                page = 8;
            } else if (page == 8)
            {
                page = 10;
            } else if (page == 12)
            {
                coins -= 10;
                health++;
                dart = true;
                Button2Enabled(false);
            } else if (page == 18)
            {
                page = 20;
            }


            /// Display text and game options to screen based on the 
            /// current page

            DisplayPage();
            
        }

        private void choice3Button_Click(object sender, EventArgs e)
        {
            prevPage = page;
            prevHealth = health;
            prevCoin = coins;
            prevGoldenKey = goldenKey;
            prevDart = dart;
            prevStick = stick;
            prevPurchaseHeal = purchaseHeal;
            /// Check what page we are currently on, and then flip
            /// to the page you need to go to if you selected option 1

            if (page == 7)
            {
                page = 24;
            } else if (page == 12)
            {
                coins -= 25;
                stick = true;
                Button3Enabled(false);
            }


        /// Display text and game options to screen based on the 
        /// current page

        DisplayPage();
        }

        private void choice4Button_Click(object sender, EventArgs e)
        {

            /// Check what page we are currently on, and then flip
            /// to the page you need to go to if you selected option 1

            if (page == 12)
            {
                page = 18;
            }


            /// Display text and game options to screen based on the 
            /// current page

            DisplayPage();
            }

        private void prevPageButton_Click(object sender, EventArgs e)
        {
            if (prevPage != 0)
            {
            page = prevPage;
            health = prevHealth;
            coins = prevCoin;
            goldenKey = prevGoldenKey;
            dart = prevDart;
            stick = prevStick;
            prevPage = 0;
            DisplayPage();
                prevPageButton.Enabled = false;
            } 
            
        }
    }
        #endregion
    }

