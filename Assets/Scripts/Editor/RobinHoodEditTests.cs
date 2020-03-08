using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using robinhood;

public class RobinHoodEditTests
{

    [Test]
    public void RobinHoodEditTestsSimplePasses()
    {

        Debug.Log("Robin Hood Tests");

        int size = 10;

        IDictionary<string, bool> RH = new RobinHoodDictionary<string, bool>(100000);


        string str = "a";
        for (int i = 0; i < size; i++)
        {


            RH.Add(str, false);
            str = str + "b";

        }
        Assert.True(RH.ContainsKey("a"));
        Debug.Log(RH.Count);
        Assert.True(size == RH.Count);

        DeciderNode node = new DeciderNode(SmartSquare.StandardBoardSetUp());
        DeciderNode node2 = new DeciderNode(SmartSquare.StandardBoardSetUp());
        DeciderNode badNode = new DeciderNode(SmartSquare.NotStandardBoardSetUp());

      //  IDictionary<DeciderNode, Empty> game = new RobinHoodDictionary<DeciderNode, Empty>(1000);



    //    game.Add(node, new Empty());
        //test that the hash override works
        //Assert.True(game.ContainsKey(node2));


        //that doesn't contain bad node
       // Assert.False(game.ContainsKey(badNode));

       // game.Add(badNode, new Empty());

        //that it does
        //Assert.True(game.ContainsKey(badNode));
        //count is 2
        //Assert.True(game.Count == 2);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator RobinHoodEditTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
