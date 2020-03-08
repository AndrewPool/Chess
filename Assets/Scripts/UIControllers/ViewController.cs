using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour {

    [SerializeField] Text AndyScoreDisplay;
    [SerializeField] ButtonController[] GameView;
    [SerializeField] Button computerIcon;
    [SerializeField] Button computerPlayer;
    private Decider game;

    private Location from = new Location(9, 9);
    private Location to = new Location(9, 9);
    
    private bool selectingOrMoving = Selecting;
    private const bool Selecting = true;
    private const bool Moving = false;


    public bool CurrentPlayer { get; private set; }//this starts as true
    private const bool player1 = true;
    private const bool player2 = false;

    private bool computerPlaying = false;
    private bool computerPlayerBool = false;
    public void Select(Location location)
    {
        if (selectingOrMoving == Selecting)
        {
            from = location;
          
            ActivateToSpaces();
        }
        else
        {
            to = location;

            CurrentPlayer = !CurrentPlayer;

            MakeMove();
            SyncWithGame();
            Debug.Log(computerPlayerBool);
            //over is borken it;s not even firing
            if (game.Over() || game.Choices().Count == 0)
            {
                Debug.Log("winner!");
                Debug.Log("hooman won");
                SyncWithGame();
                return;
            }
            else if (computerPlaying && CurrentPlayer == computerPlayerBool)
            {
                Move computerMove = game.PickOneForMe();

                to = computerMove.to;
                from = computerMove.from;
                CurrentPlayer = !CurrentPlayer;
              
                MakeMove();
                if (game.Choices().Count == 0)
                {
                    Debug.Log("winner!");
                    Debug.Log("computer won");
                    SyncWithGame();
                    return;
                }
               SyncWithGame();

            }
    
        }
        selectingOrMoving = !selectingOrMoving;
      
    }

    //make a move, when oyu have from and to
    private void MakeMove()
    {
        Move move = null;
        foreach (Move qmove in game.Choices())
        {
            if (qmove.from.row == from.row && qmove.from.column == from.column && qmove.to.row == to.row && qmove.to.column == to.column)
            {
                
                move = qmove;
                break;
            }
        }
       // Debug.Log(move.from.String());
        game = game.Pick(move);
    }
    
    //---------------------all the spaces clearing you can do--------------------------------
    //activates only the active spaces you can move
    private void ActivateFromSpaces()
    {
       // Debug.Log("activingt from spaces");
        DisableAllSpaces();
       // Debug.Log(game.Choices().Count);
        foreach (Move move in game.Choices())
        {
           
            int j = move.from.Mapped2D();
          // Debug.Log("activeing from space: " + j + " to: " + move.to.Mapped2D());
            //Debug.Log(move.)
            GameView[j].ToggleActive(true);
        }
    }


    private void ActivateToSpaces()
    {
       // Debug.Log("activating to spaces");
        DisableAllSpaces();
        foreach (Move move in game.Choices())
        {
            if (move.from.row == from.row && move.from.column == from.column)
            {

                Location toLoc = move.to;
                int j = toLoc.Mapped2D();
               // Debug.Log("activeing to space" + j);
                GameView[j].ToggleActive(true);
            }
        }
    }
  
    private void DisableAllSpaces()
    {
       // Debug.Log("Disabling all spaces");
        for (int i = 0; i < GameView.Length; i++)
        {
            GameView[i].ToggleActive(false);
        };
    }
    //-------------above acivating and disavleing spaces--------------------------

        //---------------togglecomputer-----------------
    /// <summary>
    /// toggles the computer playing
    /// </summary>
    public void ToggleComputerPlaying()
    {
        Debug.Log("toggling computer player on/off");
        computerPlaying = !computerPlaying;

        SetComputerIcon();
    }
    private void SetComputerIcon()
    {
        if (computerPlaying)
        {
            computerIcon.image.color = Color.green;
        }
        else
        {
            computerIcon.image.color = Color.black;
        }
    }
    /// <summary>
    /// toggles the computer player
    /// </summary>
    public void ToggleComputerPlayer()
    {
        Debug.Log("toggling computer player color");
        computerPlayerBool = !computerPlayerBool;

        SetComputerPlayerIcon();
    }
    private void SetComputerPlayerIcon()
    {
        if (computerPlayerBool)
        {
            computerPlayer.image.color = Color.white;
        }
        else
        {
            computerPlayer.image.color = Color.black;
        }
    }
    //---------------toggle computer above-----------------

    //-----------------on start up stuff below, also SyncWithGame----------------------------
    /// <summary>
    /// refresh button calls this
    /// </summary>
    public void SetUpNewGame()
    {
        Debug.Log("Setting up new Game");
        CurrentPlayer = player1;
        game = new Decider(SmartSquare.StandardBoardSetUp());
        SetComputerPlayerIcon();
        SetComputerIcon();
        SyncWithGame();
    }

    private void SyncWithGame()
    {
        Debug.Log("sync with game");
        SmartSquare[,] board = game.GetCurrentState();
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {

                int i = row * 8 + col;
                GameView[i].ToggleActive(true);
               
                //set the token
                GameView[i].PlaySpace().SetToken(board[row, col].unit.token, board[row, col].unit.player);

            }
        }
        string scoreText = game.ScoreForCurrentState().ToString();
        AndyScoreDisplay.text = scoreText;
        ActivateFromSpaces();
        Debug.Log("finnished sync");
    }




    // idk why this is greyed out, i don't remember them being in other unity versions
    // Use this for initialization
    void Start()
    {
       
        //sets identity for each location
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                int i = row * 8 + col;
                GameView[i].SetIdentity(this, new Location(row, col));


            }
        }
        

    }


    //this is a bandaid
    bool firstframe = true;
    // Update is called once per frame
    void Update()
    {
        if (firstframe)
        {
            SetUpNewGame();
            firstframe = false;
        }
    }
}
