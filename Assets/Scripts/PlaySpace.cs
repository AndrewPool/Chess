using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySpace : MonoBehaviour {


#pragma warning disable IDE0044 // Add readonly modifier doubtful
    [SerializeField] Sprite WhitePawn;
    [SerializeField] Sprite BlackPawn;

    [SerializeField] Sprite WhiteKnight;
    [SerializeField] Sprite BlackKnight;

    [SerializeField] Sprite WhiteBishop;
    [SerializeField] Sprite BlackBishop;

    [SerializeField] Sprite WhiteRook;
    [SerializeField] Sprite BlackRook;

    [SerializeField] Sprite WhiteQueen;
    [SerializeField] Sprite BlackQueen;

    [SerializeField] Sprite WhiteKing;
    [SerializeField] Sprite BlackKing;
#pragma warning restore IDE0044 // Add readonly modifier doubtful
    Image image;


    private Sprite[] pieces = null;
    private int index = 0;

    //Cycles either forward or backwards
        public void CycleUnit(bool forward)
    {

        if (forward)
        {
            index++;
            if (index >= pieces.Length) index = 0;
            image.sprite = pieces[index];
            return;//note this return
        }
        //else if backward
        index--;
        if (index < 0) index = pieces.Length -1;
        image.sprite = pieces[index];
        return;

    }

    // this is a big function, it had to go somewhere, maybe who knows.

    public void SetToken(Token token, bool player){
        switch (token)
        {
            case Token.None: image.sprite = null; return;
            case Token.Pawn: if (player)
                {
                    index = 0;
                    image.sprite = WhitePawn;
                    return;
                }
                else
                {
                    index = 1;
                    image.sprite = BlackPawn;
                    return;
                }
            case Token.Knight:
                if (player)
                {
                    index = 2;
                    image.sprite = WhiteKnight;
                    return;
                }
                else
                {
                    index = 3;
                    image.sprite = BlackKnight;
                    return;
                }
            case Token.Bishop:
                if (player)
                {
                    index = 4;
                    image.sprite = WhiteBishop;
                    return;
                }
                else
                {
                    index = 5;
                    image.sprite = BlackBishop;
                    return;
                }
            case Token.Rook:
                if (player)
                {
                    index = 6;
                    image.sprite = WhiteRook;
                    return;
                }
                else
                {

                    index = 7;
                    image.sprite = BlackRook;
                    return;
                }
            case Token.Queen:
                if (player)
                {
                    index = 8;
                    image.sprite = WhiteQueen;
                    return;
                }
                else
                {
                    index = 9;
                    image.sprite = BlackQueen;
                    return;
                }
            case Token.King:
                if (player)
                {
                    index = 10;
                    image.sprite = WhiteKing;
                    return;
                }
                else
                {
                    index = 11;
                    image.sprite = BlackKing;
                    return;
                }
            }
        }


    // Use this for initialization
     void Start () {
       // Debug.Log("playSpace start");
   image = gameObject.GetComponent<Image>();
        // Debug.Log(image.ToString());
        if (image == null)
        {
            Debug.Log("image null");
            //image = new Image();
        }
        pieces = new Sprite[]
         {
            WhitePawn,BlackPawn,WhiteKnight,BlackKnight,WhiteBishop,BlackBishop,WhiteRook,BlackRook,WhiteQueen,BlackQueen,WhiteKing,BlackKing
         };
    }
	
	
}
