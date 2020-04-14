namespace Dangeon.Generator
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class RoomsTriangulator<ROOM>
    {

        private Func<ROOM, RectInt> roomToRect;

        public RoomsTriangulator(
            Func<ROOM,RectInt> roomToRect
        ) {
            this.roomToRect = roomToRect;
        }

        private class Node {
            public ROOM room;
            public RectInt rect;
        }

        public List<(ROOM, ROOM)> Generate (List<ROOM> rooms) {

            var center = new Vector2(
                rooms.Average(r => roomToRect(r).center.x),
                rooms.Average(r => roomToRect(r).center.y)
            );

            var nodes = rooms.Select(r => new Node()
            {
                room = r,
                rect = roomToRect(r)
            }).OrderBy(n => Vector2.Distance(center, n.rect.center)).ToList();

            var graph = new Graph<Node, bool>(nodes, v => v);

            var weightGraph = new Graph<Node, float>(nodes, v => true);
            weightGraph.Update((a, b, _) => Vector2.Distance(a.rect.center, b.rect.center));

            var selected = new HashSet<Node>();
            foreach (var n in nodes)
            {
                if (0 == selected.Count)
                {
                    selected.Add(n);
                }
                else
                {
                    var nearest = selected.OrderBy(x => weightGraph.Value(n, x));
                    var first = nearest.First();

                    selected.Add(n);

                    graph.Update(true, n, first);
                    selected.Add(first);
                }
            }

            return graph.Links().Select(x => (x.Item1.room, x.Item2.room)).ToList();
        }
    }
}