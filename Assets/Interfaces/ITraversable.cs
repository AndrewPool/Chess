
using System.Collections.Generic;


    public interface ITraversable
    {
        int HeapScore { get; }
        bool IsLeaf { get; }
        ITraversable From();

        /// <summary>
        /// this will bill null if IsLeaf, but will be empty if is mate or tie
        /// </summary>
        /// <returns>The nodes it can go to</returns>
        ICollection<ITraversable> ToNodes();




    }
