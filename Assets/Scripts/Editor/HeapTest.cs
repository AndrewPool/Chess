using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class HeapTest {

	[Test]
	public void HeapTestSimplePasses() {

        ITraversable demoT = StarterTree(true);

        MaxHeap maxHeap = new MaxHeap(demoT);
       
        //Debug.Log(maxHeap.InsertionIndex);

        Assert.True(maxHeap.HasTop());
      
        int count = demoT.ToNodes().Count;
        Assert.True(maxHeap.Count == count);
        Assert.True(maxHeap.Count + 1 == maxHeap.InsertionIndex);
        //Debug.Log(count);

        int score = maxHeap.Pop().HeapScore;
        
        while (maxHeap.HasTop())
        {
            int nextScore = maxHeap.Pop().HeapScore;
            Debug.Log(nextScore);
            Assert.True(score >= nextScore);
            score = nextScore;
        }

        ThreeDeepTest(); 
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator HeapTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}


    private static void ThreeDeepTest()
    {
        ITraversable demoTree = ThreeDeepTree();

        MaxHeap maxHeap = new MaxHeap(demoTree);

        Assert.True(maxHeap.Count + 1 == maxHeap.InsertionIndex);
        Assert.True(maxHeap.Count == 30);
      //  Debug.Log(maxHeap.Count);
        int counter = 1;
        int score = maxHeap.Pop().HeapScore;
        while (maxHeap.HasTop())
        {
            int nextScore = maxHeap.Pop().HeapScore;
            Assert.True(score >= nextScore);
            Debug.Log(nextScore);
            score = nextScore;
            counter++;
        }
        Debug.Log("Insertion Index: " + maxHeap.InsertionIndex);
        Debug.Log("incrementor counter: "+counter);
        Debug.Log("maxHeap.Count: " + maxHeap.Count);
        Assert.True(maxHeap.Count + 1 == maxHeap.InsertionIndex);

    }





    private static ITraversable ThreeDeepTree()
    {
        DemoTraversable rootNode = (DemoTraversable)StarterTree(false);
        ITraversable[] nodes = rootNode.to;
        for (int i = 0; i < nodes.Length; i++)
        {
            DemoTraversable node = (DemoTraversable)nodes[i];
            node.to = new ITraversable[]
                  {
                new DemoTraversable((int)(Random.value * 100), rootNode,true),
                new DemoTraversable((int)(Random.value * 100), rootNode,true),
                new DemoTraversable((int)(Random.value * 100), rootNode,true),
                new DemoTraversable((int)(Random.value * 100), rootNode,true),
                new DemoTraversable((int)(Random.value * 100), rootNode,true)
                  };
        }



        return rootNode;
    }



    private static ITraversable StarterTree(bool isLeaf)
    {
        DemoTraversable rootNode = new DemoTraversable(heapScore: 5);

        rootNode.to = new ITraversable[]
        {
            new DemoTraversable(3, rootNode,isLeaf),
            new DemoTraversable(5, rootNode,isLeaf),
            new DemoTraversable(1, rootNode,isLeaf),
            new DemoTraversable(2, rootNode,isLeaf),
            new DemoTraversable(-1, rootNode,isLeaf),
            new DemoTraversable(7, rootNode,isLeaf)
        };

        return rootNode;


    }

    ///generic ITraversable for this test
    private class DemoTraversable : ITraversable
    {
        public bool IsLeaf { get; private set; }
        public int HeapScore { get; private set; }
        public ITraversable from;
        public ITraversable[] to;


        public DemoTraversable( int heapScore)
        {
            IsLeaf = false;
            HeapScore = heapScore;
            from = null;
            to = null;
        }

        public DemoTraversable(int heapScore, DemoTraversable from, bool isLeaf)
        {
            IsLeaf = isLeaf;
            HeapScore = heapScore;
            this.from = from;
            to = null;
        }

        public DemoTraversable(bool isLeaf, int heapScore, ITraversable from, ITraversable[] to)
        {
            IsLeaf = isLeaf;
            HeapScore = heapScore;
            this.from = from;
            this.to = to;

        }


        public ITraversable From()
        {
            return from;
        }

        public ICollection<ITraversable> ToNodes()
        {
            return to;
        }
    }
}
