using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.Library
{
    public class Builder
    {

        public IEnumerable<int> BuildIntergerSequence()
        {
            //var integers = Enumerable.Range(0, 10)
            //                        .Select(i => 5+10*i);

            var integers = Enumerable.Repeat(-1, 10);

            return integers;
        }

        public IEnumerable<string> BuildStringSequence()
        {
            //Random rand = new Random();
            //var strings = Enumerable.Range(0, 10)
            //                        .Select(i => ((char)('A' + rand.Next(0,26))).ToString());

            var strings = Enumerable.Repeat("my string", 10);
            return strings;
        }

        public IEnumerable<int> CompareSequences()
        {
            var seq1 = Enumerable.Range(0, 10);
            var seq2 = Enumerable.Range(0, 10)
                                .Select(i => i * i);

            return seq1.Union(seq2);
        }
    }
}
