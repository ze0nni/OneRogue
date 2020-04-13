using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BSPGenerator
{
    private int minRoomSize;
    private int maxRoomSize;
    private float maxRatio;

    public BSPGenerator(
        int minRoomSize,
        int maxRoomSize,
        float maxRatio
    ) {
        this.minRoomSize = minRoomSize;
        this.maxRoomSize = maxRoomSize;
        this.maxRatio = maxRatio;
    }

    public List<RectInt> Generate(int width, int height, System.Random random) {
        return Split(
            new RectInt(0, 0, width, height),
            random
        );
    }

    private List<RectInt> Split(RectInt rect, System.Random random) {
        if (rect.width < minRoomSize || rect.height < minRoomSize) {
            return new List<RectInt>();
        }
        if (rect.width <= maxRoomSize 
            && rect.height <= maxRoomSize
            && GetRatio(rect.width, rect.height) >= maxRatio
        ) {
            return new List<RectInt>() { rect };
        }
        if (rect.width > rect.height) {
            var ratio = random.Next(0, rect.width);
            var left = new RectInt(rect.x, rect.y, ratio, rect.height);
            var right = new RectInt(rect.x + ratio, rect.y, rect.width - ratio, rect.height );
            return Enumerable.Concat(
                Split(left, random),
                Split(right, random)
            ).ToList();
        } else {
            var ratio = random.Next(0, rect.height);
            var top = new RectInt(rect.x, rect.y, rect.width, ratio);
            var bottom = new RectInt(rect.x, rect.y + ratio, rect.width, rect.height - ratio);
            return Enumerable.Concat(
                Split(top, random),
                Split(bottom, random)
            ).ToList();
        }
    }

    private float GetRatio(int a, int b) {
        return a > b ? b / (float)a : a / (float)b;
    }
}
