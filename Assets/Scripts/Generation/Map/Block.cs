using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {

    /// <summary>
    /// Whether or not the object can be walked through (0 = not solid, 1+ is solid but it isn't solid if the object walking through it is more dense).
    /// </summary>
    public int density;

    public enum Direction { north, south, east, west, top, bot };

    public struct Tile { public int x; public int y; }
    public virtual Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 0;
        tile.y = 0;
        return tile;
    }

    const float tileSize = 0.25f;

    public Block()
    {
        density = 0;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public virtual MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.bot))
        {
            meshData = FaceData(Direction.top, chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.top))
        {
            meshData = FaceData(Direction.bot, chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.south))
        {
            meshData = FaceData(Direction.north, chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.north))
        {
            meshData = FaceData(Direction.south, chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.west))
        {
            meshData = FaceData(Direction.east, chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.east))
        {
            meshData = FaceData(Direction.west, chunk, x, y, z, meshData);
        }

        return meshData;
    }

    public virtual bool IsSolid(Direction direction)
    {
        switch(direction)
        {
            case Direction.north:
                return true;

            case Direction.south:
                return true;

            case Direction.east:
                return true;

            case Direction.west:
                return true;

            case Direction.top:
                return true;

            case Direction.bot:
                return true;
        }

        return false;
    }

    protected virtual MeshData FaceData(Direction direction, Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        switch(direction)
        {
            case Direction.north:
                meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
                break;

            case Direction.south:
                meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
                break;

            case Direction.east:
                meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
                break;

            case Direction.west:
                meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
                break;

            case Direction.top:
                meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
                break;

            case Direction.bot:
                meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
                meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
                meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
                break;
        }

        meshData.AddQuadTriangles();
        meshData.uv.AddRange(FaceUVs(direction));
        return meshData;
    }

    public virtual Vector2[] FaceUVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);
        UVs[0] = new Vector2(tileSize * tilePos.x + tileSize,
            tileSize * tilePos.y);
        UVs[1] = new Vector2(tileSize * tilePos.x + tileSize,
            tileSize * tilePos.y + tileSize);
        UVs[2] = new Vector2(tileSize * tilePos.x,
            tileSize * tilePos.y + tileSize);
        UVs[3] = new Vector2(tileSize * tilePos.x,
            tileSize * tilePos.y);
        return UVs;
    }
}
