using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ChessGameTests {

	[Test]
	public void ChessGameTestsSimplePasses() {

		SmartSquare[,] dumbSquares = SmartSquare.StandardBoardSetUp();

		Decider game = new Decider(dumbSquares);

		int choices = game.Choices().Count;

		Debug.Log(choices);


		DeciderNode node = new DeciderNode(SmartSquare.StandardBoardSetUp());

		//node.SetMovesTo();

		//Debug.Log(node.To.Count);
		//TestPickOneForMe();

		PlayGame(10);

        
        
	}
	private static void TestPickOneForMe()
    {
		SmartSquare[,] dumbSquares = SmartSquare.StandardBoardSetUp();

		Decider game = new Decider(dumbSquares);

		game = game.Pick(game.PickOneForMe());
	}


	private static void PlayGame(int turns)
    {
		SmartSquare[,] dumbSquares = SmartSquare.StandardBoardSetUp();

		Decider game = new Decider(dumbSquares);

		int turnCount = turns;

        while(turnCount > 0)
        {
            //there should be a test for this
			game = game.Pick(game.PickOneForMe());
			turnCount--;
		}

	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator ChessGameTestsWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
