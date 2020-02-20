using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ChessBoardEditTest {

	[Test]
	public void ChessBoardEditTestSimplePasses() {

		// Use the Assert class to test conditions.
		PerformHashTests();
	}
    private void PerformHashTests()
    {
		
		ChessBoard startingBoard1 = new ChessBoard(SmartSquare.StandardBoardSetUp());
		ChessBoard startingBoard2 = new ChessBoard(SmartSquare.StandardBoardSetUp());

        //changed pieces
		ChessBoard notStartingBoard = new ChessBoard(SmartSquare.NotStandardBoardSetUp());
		ChessBoard notStartingBoard2 = new ChessBoard(SmartSquare.NotStandardBoardSetUp2());
		ChessBoard notStartingBoard3 = new ChessBoard(SmartSquare.NotStandardBoardSetUp3());
		ChessBoard notStartingBoard4 = new ChessBoard(SmartSquare.NotStandardBoardSetUp4());

        //pieces not there
		ChessBoard notStartingBoard5 = new ChessBoard(SmartSquare.NotStandardBoardSetUp5());
		ChessBoard notStartingBoard6 = new ChessBoard(SmartSquare.NotStandardBoardSetUp6());

        //visualize the tests
		Debug.Log(startingBoard1.Hash);
		Debug.Log(notStartingBoard.Hash);
		Debug.Log(notStartingBoard2.Hash);
		Debug.Log(notStartingBoard3.Hash);
		Debug.Log(notStartingBoard4.Hash);
		Debug.Log(notStartingBoard5.Hash);
		Debug.Log(notStartingBoard6.Hash);

		//the board is the board
		Assert.IsTrue(startingBoard1.Hash == startingBoard2.Hash);

        //not starting board is not the board
		Assert.False(startingBoard1.Hash == notStartingBoard.Hash);

		Assert.False(startingBoard1.Hash == notStartingBoard2.Hash);

		Assert.False(startingBoard1.Hash == notStartingBoard3.Hash);

        Assert.False(startingBoard1.Hash == notStartingBoard4.Hash);


	    //not stating board is also not other not the starting board
		Assert.False(notStartingBoard.Hash == notStartingBoard2.Hash);
		//missing pieces testing
		Assert.False(notStartingBoard5.Hash == notStartingBoard6.Hash);

	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator ChessBoardEditTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
