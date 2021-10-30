// Dungeon Adventure by James Johnson
// Choose your own Adventure Game with stats


// The Debug System (Code To use it is: 5555) still has some issues but its mostly for debugging so it wont be
// in the main user experience anyways


// GUIDE ON HOW TO BEAT THIS BECAUSE ITS REALLY SPECIIFC ON WHAT HAS TO BE DONE

// **SPOILER WARNING**

// Fight Skeleton, Open Chest (Optional, but reccomended), start to bribe guard, but fight instead after. Buy the stick,
// Answer stick on the riddle behind the locked door (optional). Use stick on guards
// **SPOILER WARNING**

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
        bool goldenKey;
        bool dart;
        bool stick;
        bool purchaseHeal;

        int prevPage; // Saving Previous stats so the back button works
        int prevHealth;
        int prevCoin;
        bool prevGoldenKey;
        bool prevDart;
        bool prevStick;
        bool prevPurchaseHeal;
        SoundPlayer heartSound;
        SoundPlayer coinSound;
        SoundPlayer hurtSound;
        SoundPlayer proceedSound;
        Random randGen;

        public Form1()
        {
            InitializeComponent();
            ResetVariables(); // Make sure all the variables are set
            DisplayPage(); // Make sure the page is displayed
            page = 1;
            heartSound = new SoundPlayer(Properties.Resources.heartSound); // Assign the sounds
            coinSound = new SoundPlayer(Properties.Resources.coinSound);
            hurtSound = new SoundPlayer(Properties.Resources.damageSound);
            proceedSound = new SoundPlayer(Properties.Resources.proceedSound);

            prevPageButton.Visible = false;
            debugImput.Visible = true;
            pageLabel.Visible = false;

            Random randGen = new Random();
        }

        private void ResetVariables() // Reset all the variabes and go back to the start
        {
            health = 5;
            coins = 10;
            page = 1; //  Set the variables at the start to make sure they are correct
            goldenKey = false;
            dart = false;
            stick = false;
            purchaseHeal = false;

            prevPage = page;
            prevHealth = health;
            prevCoin = coins;
            prevGoldenKey = goldenKey;
            prevDart = dart;
            prevStick = stick;
            prevPurchaseHeal = purchaseHeal;
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
            }
            else if (!active)
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
            choice1Label.Text = "Continue"; // Continue screens are used alot, this is made to make it easier
        }

        private void UpdateStats()
        {
            if (health >= 0)  // Update health on stats bar
            {
                healthLabel.Text = $"x {health.ToString()}";
            }
            
            coinsLabel.Text = $"x {coins.ToString()}"; // Show the coins
            pageLabel.Text = $"Page {page.ToString()}";

            if (goldenKey) // Shows the items on the stats bar
            {
                goldenKeyLabel.Text = "Golden Key";
            }
            else
            {
                goldenKeyLabel.Text = "";
            }

            if (dart)
            {
                dartLabel.Text = "Poison Dart";
            }
            else
            {
                dartLabel.Text = "";
            }

            if (stick)
            {
                stickLabel.Text = "Stick";
            }
            else
            {
                stickLabel.Text = "";
            }

            if (page == 12) // Used to update the shop
            {
                if (coins < 25 || stick)
                {
                    Button3Enabled(false);
                }
                if (coins < 10 || dart)
                {
                    Button2Enabled(false);
                }
                if (coins < 5 || purchaseHeal) // purchase heal variable is used to tell when the potion is bought, wouldn't use this if there
                {                              // were multiple shops since they would conflict but since theres only one and a time limit it should be ok
                    Button1Enabled(false);
                }
            }

            prevPageButton.Enabled = true; // Update stats is only ever called after a choice is made

            if (health < 0) // There are times when health may get reduced past 0, anything past there doesn't matter as 0 would cause a game over
            {               // So It just gets set back to 0
                health = 0;
            }

        }

        private void DisplayPage()
        {
            Random randGen = new Random();
            switch (page)
            {
                case 1:
                    displayLabel.Text = "You arrive in the dungeon and upon entering you see a skeleton!" +
                        "\n\n Do you want to fight it?";
                    choice1Label.Text = "Fight";
                    choice2Label.Text = "Sneak Past";
                    Button2Enabled(true); // Calling the stuff to enable / disable, using a method to make this 100x easier
                    Button3Enabled(false);
                    Button4Enabled(false);
                    break;
                case 2:
                    heartSound.Play(); // Playing sounds
                    displayLabel.Text = "You managed to successfully sneak past the skeleton, because of that you managed " +
                        "to find a health potion!\n\n+1 HP";
                    ContinueScreen();
                    break;
                case 3:
                    coinSound.Play();
                    displayLabel.Text = "You managed to successfully fight off the skeleton, and it even dropped some coins!" +
                        " \n\n+5 Coins";
                    ContinueScreen();
                    break;
                case 4:
                    proceedSound.Play();
                    displayLabel.Text = $"You find an oddly placed chest? This doesn't feel like it should be here.\n\nOpen it?";
                    Button2Enabled(true);
                    choice1Label.Text = "Open it";
                    choice2Label.Text = "Leave it";
                    break;
                case 5:
                    coinSound.Play();
                    displayLabel.Text = $"That was quite risky but it wasn't a trap, or a Mimic. " +
                        $"You found a strange golden key..\n\n+1 Golden Key";
                    ContinueScreen();
                    goldenKey = true; // using a bool to store whether or not you have a key since you can't get multiple and has less options to mess up
                    break;
                case 6:
                    proceedSound.Play();
                    displayLabel.Text = "You did not open the chest, perhaps a smart move"; // even though its safe, I wanted to add the "perhaps a smart move" part
                    ContinueScreen();
                    break;
                case 7:
                    proceedSound.Play();
                    displayLabel.Text = "You see a strong guard blocking the door, what do you do?";
                    Button2Enabled(true);
                    Button3Enabled(true);
                    Button4Enabled(false);
                    choice1Label.Text = "Fight them (-?? HP)"; // The last bit showing the HP loss is to show what each option could lead to, making some think that a different path
                    choice2Label.Text = "Bribe Them (-?? Coins)"; // would be better even though it almost instantly ends the playthrough
                    choice3Label.Text = "Find another Pathway";
                    break;
                case 8:
                    proceedSound.Play();
                    displayLabel.Text = "You mention it to the guard but they want 10 Coins in order to let you pass, What do you do?";
                    Button2Enabled(false);
                    Button3Enabled(false);
                    Button4Enabled(false);
                    if (coins >= 10) // Only allow you to bribe them if you have enough coins
                    {
                        Button2Enabled(true);
                        choice2Label.Text = "Pay them 10 Coins (-10 Coins)";
                    }
                    else
                    {
                        choice2Label.Text = "Bribe Guard (-10 Coins)";
                    }
                    choice1Label.Text = "Fight them (-3 HP)"; // They deal more damage since the guard was ready to fight if needed,
                    break;                                      // More coins are also dropped as a reward for a tougher fight, however the lack of coins makes this essential in order to win
                case 9:
                    hurtSound.Play();
                    displayLabel.Text = "You have a tough fight with the guard, he knew you were there already so you didn't have the element of surprise\n\n-3 HP\n+10 Coins";
                    ContinueScreen();
                    break;
                case 10:
                    proceedSound.Play();
                    displayLabel.Text = "You give the guard the coins, he lets you pass\n\n-10 Coins"; // Was originally gonna have the offer be 5 but then he wants 10
                    ContinueScreen();
                    break;
                case 11:
                    hurtSound.Play();
                    displayLabel.Text = "You have a tough fight with the guard, but you had the element of surprise\n\n-1 HP\n+5 Coins"; // have you take damage but get some coins
                    health--;
                    ContinueScreen();
                    break;
                case 12:
                    proceedSound.Play();
                    displayLabel.Text = "You find a traveling Merchant, would you like to buy anything? " + // Game design-wise, bad move to place the shop right after where
                        "\n\n Health Potion (+1 HP) - 5 Coins" +                                        // people might bribe the guard with 10 coins
                        "\n Poison Dart (One Time Use) - 10 Coins" +
                        "\n Stick (One Time Use) - 25 Coins"; // A stick being powerful is an on-going joke I always have with whatever im making
                    ContinueScreen();
                    Button1Enabled(false);
                    if (coins >= 5) // Check you have enough to buy, and only enable the buttons at that point
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
                    proceedSound.Play();
                    displayLabel.Text = "You leave the shop and find a locked door, what do you do?";
                    ContinueScreen();
                    Button1Enabled(false);
                    Button2Enabled(true);
                    if (goldenKey) // only allow the door to be opened if you have the key
                    {
                        Button1Enabled(true);
                    }
                    choice1Label.Text = "Use Key (-1 Golden Key)";
                    choice2Label.Text = "Find another Path";
                    break;
                case 19:
                    proceedSound.Play();
                    displayLabel.Text = "The Key breaks after the door unlocks, you enter through the door"; // reason for the key to be gone
                    ContinueScreen();
                    break;
                case 20:
                    proceedSound.Play();
                    displayLabel.Text = "You find a different path";
                    ContinueScreen();
                    break;
                case 21:
                    proceedSound.Play();
                    displayLabel.Text = "You find a riddle on the wall\n\n" +
                        "I am the weapon that you should use, nothing compares to how reliable and consistant I am. What am I?\nWrong Choice will result in DEATH!";
                    Button2Enabled(true); //  Not really happy with the riddle itself, but it allows you to eliminate both the health potion and dart with process of elimination
                    Button3Enabled(true);
                    Button4Enabled(true);
                    choice1Label.Text = "Sword (????)";
                    choice2Label.Text = "Poison Dart (????)";
                    choice3Label.Text = "Stick (????)";
                    choice4Label.Text = "Health Potion (????)";
                    break;
                case 23:
                    heartSound.Play();
                    ContinueScreen();
                    displayLabel.Text = "You chose right, the stick is the ultimate weapon, I fear you may not be able to succeed if you lack " +
                        "that item\n\n+5 HP"; // This isnt really essential in order to win, is just a nice bonus thing and can hint towards the stick being important
                    break;
                case 24:
                    displayLabel.Text = "A Very strong guard spots you, what do you do?";
                    ContinueScreen();
                    Button1Enabled(true);
                    Button2Enabled(true);
                    if (stick)
                    {
                    Button3Enabled(true);
                    }

                    if (dart)
                    {
                    Button4Enabled(true);
                    }
                    choice1Label.Text = "Fight (-?? HP)";
                    choice2Label.Text = "Run";
                    choice3Label.Text = "Stick";
                    choice4Label.Text = "Poison Dart";
                    break;
                case 25:
                    int chance;
                    chance = randGen.Next(1, 101);
                    ContinueScreen();
                    hurtSound.Play();
                    displayLabel.Text = "The Guard catches you and a tough fight breaks out";
                    if (chance <= 25)
                    {
                        health -= 3;
                        displayLabel.Text += "\n\n-3 HP (25% Chance)"; // 25% chance to take 3 damage, 75% chance to take 10 damage. I think you might be able to 
                    } else                                              // get 10 hearts if you do things near perfectly but you cant get the stick so it doesnt really matter
                    {
                        health -= 10;
                        displayLabel.Text += "\n\n-10 HP (75% Chance)";
                    }
                    break;
                case 26:
                    proceedSound.Play();
                    displayLabel.Text = "The Guard gets scared and runs away...?"; // Foreshadows that somethings up with the stick
                    ContinueScreen();
                    break;
                case 28:
                    displayLabel.Text = "You go to use the poison dart but it bounces off the guards armour and hits you... good job?\n\nGAME OVER\nTry Again?";
                    hurtSound.Play(); // I feel bad for having the poison dart end so badly, but this game punishes you for using common sense and choosing
                    ContinueScreen(); // this instead of a literal stick
                    Button2Enabled(true);
                    choice1Label.Text = "Try Again";
                    choice2Label.Text = "Quit";
                    break;
                case 30:
                    proceedSound.Play();
                        displayLabel.Text = "The Guard brought backup, you see a even more powerful guard, \n\nWhat do you do?";
                    Button1Enabled(true);
                    Button2Enabled(true);
                    Button3Enabled(false);
                    Button4Enabled(false);
                    if (stick)
                    {
                    Button3Enabled(true);
                    }
                    if (dart)
                    {
                    Button4Enabled(true);
                    }
                    choice1Label.Text = "Use Sword";
                    choice2Label.Text = "Run";
                    choice3Label.Text = "Use Stick"; // Stick is the only thing that matters
                    choice4Label.Text = "Poison Dart";
                    break;
                    case 31:
                    ContinueScreen();
                    displayLabel.Text = "You used the stick on the guard and they are instantly defeated."; // makes sense
                    stick = false;
                    break;
                case 32:
                    ContinueScreen();
                    displayLabel.Text = "You tried to that but it wasn't enough, the guard got you.\n\nGAME OVER\n\nTry Again?"; 
                    healthLabel.Text = "x 0";
                    hurtSound.Play();
                    break;
                case 34: // Win Screen
                    heartSound.Play();
                    ContinueScreen();
                    displayLabel.Text = "You Win! You managed to get the treasure from the dungeon! Thank you for playing!\n\nPlay Again?"; // only get here if the stick is used
                    choice1Label.Text = "Play Again";
                    choice2Label.Text = "Quit";
                    Button2Enabled(true);


                    break;

                case 98:
                    hurtSound.Play();
                    ContinueScreen();
                    displayLabel.Text = "You chose wrong, what could the right answer be?\n\nGAME OVER\nTry Again?"; // Used to have a hint towards the stick but later changed it
                    choice1Label.Text = "Try Again";
                    choice2Label.Text = "Quit";
                    healthLabel.Text = "x 0";
                    Button2Enabled(true);
                    //health -= 10000;
                    break;

                case 99:
                    hurtSound.Play();
                    ContinueScreen();
                    Button2Enabled(true);
                    displayLabel.Text = "You ran out of Health.\n\nGAME OVER\nTry Again?"; // Get here when you run out of health normally
                    choice1Label.Text = "Try Again";
                    choice2Label.Text = "Quit";
                    break;
            }

            UpdateStats(); // Is always called no matter what to update the stats
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
            prevPurchaseHeal = purchaseHeal; // Saves your current stats into the previous ones before they are updated

            if (health == 0) // Confirm that a game over happens
            {
                healthLabel.Text = $"x 0";
                page = 99;
                DisplayPage();
            }

            DisplayPage();
            UpdateStats();
            if (page == 1) // See skeleton
            {
                page = 3; // Fight it
                coins += 5;
            }
            else if (page == 2)
            {
                page = 4;
            }
            else if (page == 3)
            {
                page = 4;
            }
            else if (page == 4)
            {
                page = 5;
            }
            else if (page == 5)
            {
                page = 7;
            }
            else if (page == 6)
            {
                page = 7;
            }
            else if (page == 7)
            {
                page = 11;
                coins += 5;
            }
            else if (page == 8)
            {
                page = 9;
                health -= 3;
                coins += 10;
            }
            else if (page == 9)
            {
                page = 12;
            }
            else if (page == 10)
            {
                page = 12;
            }
            else if (page == 11)
            {
                page = 12;
            }
            else if (page == 12)
            {
                coins -= 5; // Updates happen here because the switch statement would call the stat updates twice
                purchaseHeal = true;
                health++;
                Button1Enabled(false);
            }
            else if (page == 18)
            {
                goldenKey = false;
                page = 19;
            }
            else if (page == 19)
            {
                page = 21;
            }
            else if (page == 20)
            {
                page = 24;
            }
            else if (page == 21)
            {
                page = 98;
            }
            else if (page == 23)
            {
                page = 24;
            }
            else if (page == 24)
            {
                page = 25;
            }
            else if (page == 25)
            {
                page = 30;
            } else if (page == 26)
            {
                page = 30;
            }
            else if (page == 30)
            {
                page = 32;
            } else if (page == 31)
            {
                page = 34;
            }
            else if (page == 28 || page == 32 || page == 34 || page == 98 || page == 99 )
            {
                ResetVariables(); //  All game over scenarios
            }


            /// Display text and game options to screen based on the 
            /// current page
            DisplayPage();
            UpdateStats();
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

            DisplayPage();
            UpdateStats();
            if (page == 1) // See Skeleton
            {
                page = 2; // Sneak Past
                health++; // Add one health
            }
            else if (page == 4)
            {
                page = 6;
            }
            else if (page == 7)
            {
                page = 8;
            }
            else if (page == 8)
            {
                page = 10;
                coins -= 10;
            }
            else if (page == 12)
            {
                coins -= 10;
                dart = true;
                Button2Enabled(false);
            }
            else if (page == 18)
            {
                page = 20;
            }
            else if (page == 21)
            {
                page = 98;
            }
            else if (page == 24)
            {
                page = 25;
            }
            else if (page == 30)
            {
                page = 32;
            }
            else if (page == 28 || page == 32 || page == 98 || page == 99)
            {
                Application.Exit();
            }


            /// Display text and game options to screen based on the 
            /// current page

            DisplayPage();
            UpdateStats();
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
            }
            else if (page == 12)
            {
                coins -= 25;
                stick = true;
                Button3Enabled(false);
            }
            else if (page == 21)
            {
                page = 23;
                health += 5;
            }
            else if (page == 24)
            {
                page = 26;
            }
            else if (page == 30)
            {
                page = 31;
            }


            /// Display text and game options to screen based on the 
            /// current page

            DisplayPage();
            UpdateStats();
        }

        private void choice4Button_Click(object sender, EventArgs e)
        {

            /// Check what page we are currently on, and then flip
            /// to the page you need to go to if you selected option 1

            if (page == 12)
            {
                page = 18;
            }
            else if (page == 21)
            {
                page = 98;
            }
            else if (page == 24)
            {
                page = 28;
            }
            else if (page == 30)
            {
                page = 32;
            }


            /// Display text and game options to screen based on the 
            /// current page

            DisplayPage();
            UpdateStats();
        }

        private void prevPageButton_Click(object sender, EventArgs e)
        {
            if (prevPage != 0) // allows you to go back a page
            {
                page = prevPage;
                health = prevHealth;
                coins = prevCoin;
                goldenKey = prevGoldenKey;
                dart = prevDart;
                stick = prevStick;
                prevPurchaseHeal = purchaseHeal;
                prevPage = 0;
                DisplayPage();
                prevPageButton.Enabled = false;
            }

        }

        public void EnableDebug() // code for enabling the debug function, I have it password protected so it can only be used by those who check the code
        {                         // after they already played
            prevPageButton.Visible = true;
            debugImput.Visible = false;
            pageLabel.Visible = true;
            submitButton.Visible = false;
            debugLabel.Text = "Debug:";
        }

        private void submitButton_Click(object sender, EventArgs e)
        {

            try
            {
            if (debugImput.Text == "5555") // CODE TO USE DEBUG IS 5555
            {
                EnableDebug();
            } else
                {
                    debugLabel.Text = "Wrong Code Input";
                }

            }
            catch // Don't know when this would ever get called, but just to help prevent a crash
            {
                debugLabel.Text = "Error";
            }
        }
    }
    #endregion
}

