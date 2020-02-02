
/// <summary>
/// This struct is the implmentation
/// TODO generic it
/// </summary>
public struct MaxHeap {

    public const int size = 50000;

    /// <summary>
    /// beaware that the first index[0] is not used
    /// </summary>
    private readonly ITraversable[] heap;
    public int Count { get; private set; }
    public int InsertionIndex { get; private set; }

    public MaxHeap(ITraversable root)
    {
        Count = 0;
        InsertionIndex = 1;

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
    public bool HasTop()
    {
        return heap[1] != null;
    }
    /// <summary>
    /// Pop the top node and float nodes up to fill
    /// </summary>
    /// <returns>HeapMax</returns>
    public ITraversable Pop()
    {
        //it will never happen that the root is the only one and gets popped. so i will code as if that never happens
        int parentIndex = 1;
        var returnNode = heap[parentIndex];

        //slink the lowest one
        heap[parentIndex] = heap[InsertionIndex-1];
        

            
        var leftIndex = 2;
        var rightIndex = 3;
        int largest = parentIndex;
        bool continu = true;//continue is a reserved word. naming things is the hardest part of coding.
       
        while (continu)
        {
            if (rightIndex < InsertionIndex && heap[rightIndex].HeapScore > heap[largest].HeapScore)
            {
                largest = rightIndex;
            }
            if (leftIndex < InsertionIndex && heap[leftIndex].HeapScore > heap[largest].HeapScore)
            {
                largest = leftIndex;
            }

            if(largest != parentIndex)
            {
                Swap(largest, parentIndex);
                parentIndex = largest;
                leftIndex = parentIndex * 2;
                rightIndex = (parentIndex * 2) + 1;
            }
            else
            {
                continu = false;
            }

        }
        
        InsertionIndex--;
        Count--;
        return returnNode;
    }
    private void Swap(int i, int j)
    {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    private void AddNodeToHeap(ITraversable node)
    {

        int childIndex = InsertionIndex;

        int parentIndex = InsertionIndex / 2;

        heap[InsertionIndex] = node;

        while (parentIndex > 0)
        {
            if(heap[childIndex].HeapScore > heap[parentIndex].HeapScore)//if you are larger than parent, swap
            {
                Swap(childIndex, parentIndex);

                childIndex = parentIndex;

                parentIndex = childIndex / 2;
            }
            else//else you are smaller, so 
            {
                parentIndex = 0;//break out of the loop; 
            }
          
        }
        Count++;
        InsertionIndex++;
    }




}
