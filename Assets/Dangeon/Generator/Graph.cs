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
        }

        public void Link(N a, N b)
        {
            if (a == b) {
                return;
            }
            var an = nodeIndex[a];
            var bn = nodeIndex[b];
            links[an, bn] = true;
            links[bn, an] = true;
        }

        public void UnLink(N a, N b)
        {
            var an = nodeIndex[a];
            var bn = nodeIndex[b];
            links[an, bn] = false;
            links[bn, an] = false;
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
                    result.Add((nodes[a], nodes[b]));
                }
            }

            return result;
        }
    }

}