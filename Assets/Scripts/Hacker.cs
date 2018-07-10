using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    public bool isDebugMode = false;

    // Game configuration data.
    public float backDelay = 3;

    // Play stuff
    int patience = 0, guesses = 0;
    string aiName = "#$%^@";
    string greeting = "Hello pal.";

    // Game State
    int level;
    bool firstChallenge = true, autoWin = true, level3Unlock = false;
    enum Screen { MainMenu, Password, Win}
    Screen currentScreen;

    public String[] easyPW, mediumPW, hardPW;
    string passWord;
    string difficulty;

    // Use this for initialization
    void Start ()
    {
        ShowMainMenu();
    }

    private void Update()
    {
        if (isDebugMode)
        {
            level = 1;
            AskForPassword();
            print(passWord);
        }
    }

    private void ShowMainMenu()
    {
        guesses = 0;
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        //Terminal.WriteLine("      8UFUuvvv7ir7i::,      .....,,,::ir7UPMMP");
        //Terminal.WriteLine("      G11UJvvLrir7:,.7   .   . ..,..,ii;7i OMP");
        //Terminal.WriteLine("      G1kUjvvv7,:7i. Uu..         .,i5Eir7:MMX");
        //Terminal.WriteLine("      BuYNuL7L7,,vi,iLLr.        :7rv1X7;uSMOP");
        //Terminal.WriteLine("      8. j77vv7r77i.,..:rri:::ivPO;M@BLrrY2MMk");
        //Terminal.WriteLine("      X  v77vLvi,.    ,77r;i:::iSkS@B7iirJUMOX");
        //Terminal.WriteLine("      @Yv02vLUi   .   rY:   :i:,..8BL7;:7Y2MMX");
        //Terminal.WriteLine("      85S2JLjJ   5B@.:ii  .,.:i::.  :ZP.ruUMBN");
        //Terminal.WriteLine("      GU5ujLP.   L@r   .LBMB@P:::::..vM,iu5BkZ");
        //Terminal.WriteLine("      G11Uj57          :Y@O@B:  .,...:i:i22M.0");
        //Terminal.WriteLine("      GUFUU1   i@B@O   ..             ,.:vrr :");
        //Terminal.WriteLine("      vv5UFv   @B@B@i              ...:.,SX@iM");
        //Terminal.WriteLine("      M15Uk7  ,B@B@2 . .  .   ..,::,:i7:rUFB@G");
        //Terminal.WriteLine("      G212Uu   ZB@B@r:iLui.:::,:,....iirvuS;:8");
        //Terminal.WriteLine("      GU5uuS:   UB@B@B@BU,,:,.,.....:rir7JSL7E");
        //Terminal.WriteLine("      Z21Ujuu       .....,::,,...::ri:i;rJ2@BN");
        //Terminal.WriteLine("      GU5ujiLJ:     ..,,:::. .,,::::::ir7J2MMX");
        //Terminal.WriteLine("      Z21UJ7vuLi,....::i:i:::::,.,.:::ir7juMM0");
        Terminal.WriteLine(@"
        . ,::iiii7;rLFqiE@BZG8GGG00kXqUL77:7q   
        . ,,::i:i2uiLPM.,kEOMOMOMEOZZUv..r;i7   
          :.:iii:SUi757::r5@BO8OMMML:r:,.ir:.   
        .7Z:iii:iii:7S5XNu7ivjUY7:. r   :ir::   
        .YMiii:::7UEM@B@5i:rrLvjLL ..  ;77i:,   
          i.:::.7B@Bq8@Zr,LEOEYru1Nu  :irvi::   
          ,:::,,B@@.  PurvO@X2SurLuZBO7 .Fi:,   
        . ::::.1@B@. :O8OF:     YLuYukS: Uv,, , 
        . ,::.:@B@B@B@@BEu,    LOM0SSP127U7,, u 
        . :::.BB@i    EBMXEBGGMZBB@0MMME5S2:ri@:
        i.,:.:B@@     ;@BOE@@@BMOMMMZEPqLku,. r 
        . :,.:@@U    .qk8qGMZEO8EPFJuU2rijr,,   
        . ,:..B@B     iL;:.752JU2U1ZEGF777::.r7 
        . :::.uB@M.       ,2FJ5SkSE0EFL;vii:.:; 
        . ,:::.BB@BZqZk1SEP5JuuSNGF2vrrJ77i:,   
        . :::7:.uB@MM8N5FuuLuP80XuULuj2Y7ri::       "
        );
        //Terminal.WriteLine("      . ,::i:::72PXq5uJLv7vJuuuFPSk5jJ7ri::   ");
        //Terminal.WriteLine("      . ::::i:rri:7Jjj5uUUFFqEMGZ25Uuv7ir::  .");
        if (level3Unlock)
        {
            Terminal.WriteLine(greeting + " Wanna hack?[1]easy[2]medium[3]hard");
        }
        else
        {
            Terminal.WriteLine(greeting + " Wanna hack?[1]easy[2]medium");
        }
        
        patience = 0;
    }

    void OnUserInput(string input)
    {
        //print(input);
        if (input== null)
        {
            input = "";
        }
        else if (input.ToLower() == "menu")  // We can always go back to main menu.
        {
            greeting = aiName + ":You again.";
            CancelInvoke();
            ShowMainMenu();
        }
        else if (input.ToLower() == "dodge")
        {
            Debug.Log("You know my name!");
            aiName = "Dodge";
            Terminal.WriteLine(aiName + " : You know my name!");
            patience = 0;
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunPassword(input);
        }
        else if (currentScreen == Screen.Win)
        {
            Terminal.WriteLine(aiName + "Ok! Ok! You win, just be patient.");
        }
    }


    // Guessing
    void RunPassword(string input)
    {
        if (input.ToLower() == "hint")
        {
            AskForHint();
            return;
        }
        int wLength = passWord.Length;
        guesses++ ;
        // Win
        if (input.ToLower() == passWord)
        {
            DisplayWinScreen();
        }
        // Lose
        else 
        {
            if (level == 1) {
                if (guesses <= wLength)
                {
                    Terminal.WriteLine("Length is " + wLength);
                    for (int i = 0; i < guesses; i++)
                    {
                        Terminal.WriteLine(" letter " + (i + 1) + " is " + passWord[i] + ", Try again!");
                    }
                }
                else if (guesses == wLength + 1)
                {
                    Terminal.WriteLine("Seriously... I TOLD you the answer.");
                }
                else if (guesses < wLength + 4)
                {
                    Terminal.WriteLine("Please type : " + passWord);
                }
                else
                {
                    Terminal.WriteLine("You win !! Well done!");
                    if (autoWin)
                    {
                        autoWin = false;
                        Invoke("DisplayWinScreen", 2);
                    }
                    
                }
            }
            else if (level == 2)
            {
                if (guesses < 6)
                {                    
                    Terminal.WriteLine("Length is " + wLength + ", Try again!");
                }
                else
                {
                    Terminal.WriteLine("Wrong again.");
                    int r = UnityEngine.Random.Range(0, 10);
                    if (r > 6)
                    {
                        Terminal.WriteLine("You know you can type 'menu' and back to menu, right?");
                    }
                    else if (r < 3)
                    {
                        Terminal.WriteLine("You know...you can type 'hint' for hint.");
                    }                    
                }
                
            }
            else if (level == 3)
            {
                if (guesses < 6)
                {
                    Terminal.WriteLine("Length is " + wLength + ", Try again!");
                }
                else {
                    AskForPassword();
                }
                
            }
        }
       
    }

    void DisplayWinScreen()
    {
        guesses = 0;        
        autoWin = true;     // For level 1 reset
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();

        // Auto back to menu
        Terminal.WriteLine("Back to Main menu in "+ backDelay + " seconds.");
        Invoke("ShowMainMenu", backDelay);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"       
                    :BM:12uuJvEB2           
                    Y@B@       1@@.         
                   Yq q.,,    @   M:        
                  .@    0@  ,rZi   11.      
                  LJ      .B@B@B  .rXkP     
                 :O        @B@B   B:B 7@    
                 B    :BO    .@:vr  @  ,@   
                @i  v . i@PGB@B@@  @    B,  
           :@u 1M   u2   7@B@B@B  MNuuJ5L   
          GUN  @     MB@,      rB@   .,     
         B,::  Bi   iB@B@B@B@B@B@B          
        .@ Li   1XJuL    :7OB@BBB.          
        .B  BL7:LBi             G           
         72rB2..E.  Yr    0     B;               

                    Nice puppy~             "               
                );
                break;
            case 2:
                Terminal.WriteLine(@"
                  @B         FS             
                  @LB       15B             
                 i@r0B@Luj2@B7@,            
                 r@r:,7YvYv:rJB2            
                 MX.:  :;:  ,,S@            
                 @i: FG   Bi ,:@            
                 B7  :i   7.  i@            
                 :B    @@@    ML            
                  :B   u@u,  Or             
                  L@.       .@v             
                  @r:ii:::ii:i@             
                  B.:::i::::,.@             
                 ,@ :,,:::,:: @,            
       
                    Good boy!!              "
                );
                level3Unlock = true;
                break;
            case 3:
                Terminal.WriteLine(@"                           
                         u@P         .Bv       
                     ZSLS@r:F8OOEG0E2@L:qEPr   
          .         2B  vM:  .         P   B:  
   :SMB@@BB@@M2,    MO  B:             @N.P@   
 M@@q;.      .vSPOBN@5JB@              :@8@    
@X              Z@@5 .BL     u7         0:     
.             XBj     5     kBO,:,,,,  BMB     
             MB      iS    M. 72:Y@@B@u:B@     
            82       i@    .NZ7  M@B@Bk  qO    
   :       B8         BO  .Jk      YB.   S@    
  ,M       Y           L@M:.     LU  7v  ,@    
   B                    BM      P@    1  i@    
   L@k                  .BS::vEB@B0u2B@X@BY    
 kB@@@:                   i1PO0v   .P@@q:       

                You're a badass!!         "
                );
                break;
            default:
                break;
        }
        
    }

    // Game selection
    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || (input == "3" && level3Unlock));
        if (isValidLevelNumber)
        {
            level = int.Parse(input) ;
            firstChallenge = true;
            AskForPassword();
        }
        else
        {
            patience++;
            if (patience < 10)
            {
                //Debug.Log(aiName + " : Chose your hack mode");
                Terminal.WriteLine(aiName + " : Chose your hack mode");
            }
            else if (patience < 15)
            {
                //Debug.Log("I HATE YOU");
                Terminal.WriteLine(aiName + " : I HATE YOU");
            }
            else
            {
                //Debug.Log("...");
                int r = UnityEngine.Random.Range(0,10);
                if (r < 3)
                {
                    Terminal.WriteLine(aiName + " : Go away.");
                }
                else if (r > 8)
                {
                    Terminal.WriteLine(aiName + " : just close the game.");
                }
                else
                {
                    Terminal.WriteLine(aiName + " : ...");
                }
            }
        }
    }

    // Set the game
    void AskForPassword()
    {
        guesses = 0;
        SetRandomPassWord();
        Terminal.WriteLine("Enter the password. Hint : " + passWord.Anagram());
        Terminal.WriteLine("Type 'hint' to show again, 'menu' to go back.");
        currentScreen = Screen.Password;
    }

    private void AskForHint()
    {
        Terminal.WriteLine("Hint : " + passWord.Anagram());
    }

    void SetRandomPassWord()
    {
        
        int i;
        switch (level)
        {
            case 1:
                difficulty = "bed room";
                i = UnityEngine.Random.Range(0, easyPW.Length);
                passWord = easyPW[i];
                break;
            case 2:
                difficulty = "garden";
                i = UnityEngine.Random.Range(0, mediumPW.Length);
                passWord = mediumPW[i];
                break;
            case 3:
                difficulty = "wilderness";
                i = UnityEngine.Random.Range(0, hardPW.Length);
                passWord = hardPW[i];
                break;
            default:
                difficulty = "";
                Debug.LogError("Invalid Level number.");
                break;
        }
        Terminal.ClearScreen();
        if (firstChallenge)
        {
            Terminal.WriteLine(aiName + " : You have chosen the " + difficulty);
            firstChallenge = false;
        }
        else {
            Terminal.WriteLine("Seemed's too hard for a bunny. Try another one.");
        }        
    }
}
