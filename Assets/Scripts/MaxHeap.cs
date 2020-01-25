using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This struct is the implmentation 
/// </summary>
public struct MaxHeap {

    public const int size = 1000;

    /// <summary>
    /// beaware that the first index[0] is not used
    /// </summary>
    private readonly ITraversable[] heap;

    private int insertionIndex;

    public MaxHeap(ITraversable root)
    {
        insertionIndex = 1;

        heap = new ITraversable[size];

        TryAddToHeap(root);
    }
    /// <summary>
    /// Tries to add the node to the heap, it usually does, and also for all of it's children.
    /// </summary>
    /// <param name="node"></param>
    public void AddToHeap(ITraversable node)
    {
        TryAddToHeap(node);
    }

    //this isn't really recursive. IDK why just don't worry about it
    private void TryAddToHeap(ITraversable node)
    {

        if (node.IsLeaf) { AddNodeToHeap(node); }
        else
        {

            foreach (ITraversable traversable in node.ToNodes())
            {
                TryAddToHeap(traversable);

            }
        }
    }
    /// <summary>
    /// Pop the top node and float nodes up to fill
    /// </summary>
    /// <returns>HeapMax</returns>
    public ITraversable Pop()
    {
        //it will never happen that the root is the only one and gets popped. so i will code as if that never happens
        var returnNode = heap[1];

        //Float() idk why people right a func when a comment will do.
        var leftIndex = 2;
        var rightIndex = 3;

        bool leftIsNull = heap[leftIndex] == null;
        bool rightIsNull = heap[rightIndex] == null;
        //stop doing the float when there's nothing left to float
        while (!leftIsNull && !rightIsNull)
        {
            if (leftIsNull)//if only one on right,
            {
                //float right
                heap[rightIndex / 2] = heap[rightIndex];
                heap[rightIndex] = null;
                //assign null
                rightIsNull = true;

            }
            else if (rightIsNull)//for left
            {
                //float left
                heap[leftIndex / 2] = heap[leftIndex];
                heap[leftIndex] = null;
                //assign null bool
                leftIsNull = true;
            }
            else
            {//if left is bigger
                if (heap[leftIndex].HeapScore > heap[rightIndex].HeapScore)
                {
                    //float up left
                    heap[leftIndex / 2] = heap[leftIndex];
                    heap[leftIndex] = null;
                    //reasign indicies from left
                    rightIndex = (leftIndex * 2) + 1;
                    leftIndex = leftIndex * 2;
                    //reasign null status
                    leftIsNull = heap[leftIndex] == null;
                    rightIsNull = heap[rightIndex] == null;
                }
                else//if right is bigger or equal to left, their score. that is
                {
                    //float up right
                    heap[rightIndex / 2] = heap[rightIndex];
                    heap[rightIndex] = null;
                    //reasign indicies from right
                    rightIndex = (rightIndex * 2) + 1;
                    leftIndex = rightIndex * 2;
                    //reasign null status
                    leftIsNull = heap[leftIndex] == null;
                    rightIsNull = heap[rightIndex] == null;
                }
            }
        }
        return returnNode;
    }
   
 
    private void AddNodeToHeap(ITraversable node) {

        int childIndex = insertionIndex;

        int parentIndex = insertionIndex / 2;

        heap[insertionIndex] = node;

        while(parentIndex > 0 )
        {
            if (heap[parentIndex] == null)//if there is an open space above, just move to it
            {
                heap[parentIndex] = node;
                heap[childIndex] = null;
                childIndex = parentIndex;
                parentIndex = insertionIndex / 2;
            }
            else if(heap[childIndex].HeapScore > heap[parentIndex].HeapScore)//if you are larger than parent, swap
            {
                var temp = heap[parentIndex];

                heap[parentIndex] = heap[childIndex];

                heap[childIndex] = temp;

                childIndex = parentIndex;

                parentIndex = childIndex / 2;
            }
            else//else you are smaller, so 
            {
                parentIndex = 0;//break out of the loop; 
            }
          
        }

        insertionIndex++;
    }




}
