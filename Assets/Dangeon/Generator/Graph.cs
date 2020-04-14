namespace Dangeon.Generator
{

    using System.Collections.Generic;

    internal class Graph<N> where N : class
    {
        private List<N> nodes;
        private Dictionary<N, int> nodeIndex = new Dictionary<N, int>();
        private bool[,] links;

        public Graph(IEnumerable<N> nodes)
        {
            this.nodes = new List<N>(nodes);

            var size = this.nodes.Count;
            for (var i = 0; i < size; i++)
            {
                this.nodeIndex[this.nodes[i]] = i;
            }

            this.links = new bool[size, size];
        }

        public void Update(bool linked, params N[] links)
        {
            var size = links.Length;
            int[] indexes = new int[size];
            for (var i = 0; i < size; i++) {
                indexes[i] = nodeIndex[links[i]];
            }
            for (var a = 0; a < size - 1;a++) {
                for (var b = a + 1; b < size; b ++) {
                    var ia = indexes[a];
                    var ib = indexes[b];
                    this.links[ia, ib] = linked;
                    this.links[ib, ia] = linked;
                }
            }
        }

        public bool IsLinked(N a, N b)
        {
            var an = nodeIndex[a];
            var bn = nodeIndex[b];
            return links[an, bn];
        }

        public List<N> Neighbors(N a) {
            var result = new List<N>();

            var an = nodeIndex[a];
            var size = nodes.Count;
            for (var i = 0; i < size; i++) {
                if (links[an, i] && an != i) {
                    result.Add(nodes[i]);
                }
            }

            return result;
        }

        public List<(N,N)> Links() {
            var result = new List<(N,N)>();

            var size = nodes.Count;
            for (var a = 0; a <size; a++) {
                for (var b = 0; b < a; b++)
                {
                    if (links[a, b])
                    {
                        result.Add((nodes[a], nodes[b]));
                    }
                }
            }
            return result;
        }
    }

}