namespace Dangeon
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DangeonGenerator
    {
        private DangeonGeneratorData data;
        private List<CellMap> cells = new List<CellMap>();
        public IReadOnlyList<CellMap> Cells { get => cells; }

        public DangeonGenerator(DangeonGeneratorData data) {
            this.data = data;

            foreach (var d in data.Tiles) {
                var waypoints = new List<WaypointArea>();
                FindWaypoints(d.tilePrefab, waypoints);
                var map = ToCellMap(d, waypoints);
                cells.Add(map);
                for (var i = 0; i < 3; i++) {
                    map = TurnCellMap(map);
                    cells.Add(map);
                }
            }
        }

        public void Generate(int width, int height, int levels) {
            
        }

        public enum CellType { None, Way, Exit }

        public struct CellMap {
            public DangeonGeneratorData.TileData Tile;
            public int Rotation;
            public CellType[,,] map;

        }

        static private void FindWaypoints(GameObject go, List<WaypointArea> waypoints) {
            var wp = go.GetComponent<WaypointArea>();
            if (null != wp) {
                waypoints.Add(wp);
            }
            for (var i = 0; i < go.transform.childCount; i++) {
                FindWaypoints(go.transform.GetChild(i).gameObject, waypoints);
            }
        }

        static CellMap ToCellMap(
            DangeonGeneratorData.TileData tile,
            List<WaypointArea> areas
        ) {
            var map = new CellType[5, 6, 5];

            foreach (var a in areas) {
                foreach(var p in a.Points) {
                    map[p.position.x, p.position.y, p.position.z] = p.IsExitPoint ? CellType.Exit : CellType.Way;
                }
            }

            return new CellMap()
            {
                Tile = tile,
                Rotation = 0,
                map = map
            };
        }

        static CellMap TurnCellMap(CellMap input) {
            var map = new CellType[5, 6, 5];

            for (var x = 0; x < 5; x ++) {
                for (var y = 0; y < 6; y++)
                {
                    for (var z = 0; z < 5; z++)
                    {
                        map[x, y, z] = input.map[z, y, 4 -  x];
                    }
                }
            }

            return new CellMap()
            {
                Tile = input.Tile,
                Rotation = input.Rotation + 1,
                map = map
            };
        }
    }

}