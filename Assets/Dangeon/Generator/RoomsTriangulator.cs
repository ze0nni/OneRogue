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
            var nodes = rooms.Select(r => new Node()
            {
                room = r,
                rect = roomToRect(r)
            }).OrderBy(n => n.rect.center.x);

            var graph = new Graph<Node>(nodes);

            return graph.Links().Select(x => (x.Item1.room, x.Item2.room)).ToList();
        }
    }
}