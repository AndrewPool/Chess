using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class HeapTest {

	[Test]
	public void HeapTestSimplePasses() {

        MaxHeap maxHeap = new MaxHeap(StarterTree());

	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator HeapTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}



    private static ITraversable StarterTree()
    {
        return new DemoTraversable( heapScore: 5 );


    }

    ///generic ITraversable for this test
    private struct DemoTraversable : ITraversable
    {
        public bool IsLeaf { get; private set; }
        public int HeapScore { get; private set; }
        private ITraversable from;
        private ITraversable[] to;


        public DemoTraversable( int heapScore)
        {
            IsLeaf = true;
            HeapScore = heapScore;
            from = null;
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
