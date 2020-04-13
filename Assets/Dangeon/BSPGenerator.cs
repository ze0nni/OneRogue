using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPGenerator
{
    private float minRoomSize;
    private float maxRoomSize;
    private float minRatio;
    private float maxRatio;

    public BSPGenerator(
        float minRoomSize,
        float maxRoomSize,
        float minRatio,
        float maxRatio
    ) {
        this.minRoomSize = minRoomSize;
        this.maxRoomSize = maxRoomSize;
        this.minRatio = minRatio;
        this.maxRatio = maxRatio;
    }

    public List<RectInt> Generate(int width, int height, System.Random random) {
        return new List<RectInt>() {
            new RectInt(0, 0, width, height)
        };
    }
}
