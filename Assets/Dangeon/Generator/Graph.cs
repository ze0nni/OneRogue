namespace Dangeon.Generator
{
    using System;
    using System.Collections.Generic;

    internal class Graph<N,V> where N : class
    {
        private List<N> nodes;
        private Predicate<V> predicate;

        private Dictionary<N, int> nodeIndex = new Dictionary<N, int>();
        private V[,] links;

        public Graph(List<N> nodes, Predicate<V> predicate)
        {
            this.nodes = new List<N>(nodes);
            this.predicate = predicate;

            var size = this.nodes.Count;
            for (var i = 0; i < size; i++)
            {
                this.nodeIndex[this.nodes[i]] = i;
            }

            this.links = new V[size, size];
        }

        public void Update(V value, params N[] links)
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
                    this.links[ia, ib] = value;
                    this.links[ib, ia] = value;
                }
            }
        }

        public delegate V ValueOF(N a, N b, V current);

        public void Update(ValueOF valueOf) {
            var size = nodes.Count;
            for (var a = 0; a < size; a++)
            {
                for (var b = 0; b < a; b++)
                {
                    var value = valueOf(nodes[a], nodes[b], links[a, b]);
                    links[a, b] = value;
                    links[a, b] = value;
                }
            }
        }

        public V Value(N a, N b) {
            var an = nodeIndex[a];
            var bn = nodeIndex[b];
            return links[an, bn];
        }

        public bool IsLinked(N a, N b)
        {
            return predicate.Invoke(Value(a, b));
        }

        public List<N> Neighbors(N a) {
            var result = new List<N>();

            var an = nodeIndex[a];
            var size = nodes.Count;
            for (var i = 0; i < size; i++) {
                if (predicate.Invoke(links[an, i]) && an != i) {
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
                    if (predicate.Invoke(links[a, b]))
                    {
                        result.Add((nodes[a], nodes[b]));
                    }
                }
            }
            return result;
        }
    }

}