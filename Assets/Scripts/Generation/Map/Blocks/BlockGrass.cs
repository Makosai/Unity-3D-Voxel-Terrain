public class BlockGrass : Block
{

    public BlockGrass() : base()
    {
        density = 1;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        switch (direction)
        {
            case Direction.top:
                tile.x = 2;
                tile.y = 0;
                return tile;
            case Direction.bot:
                tile.x = 1;
                tile.y = 0;
                return tile;
        }
        tile.x = 3;
        tile.y = 0;
        return tile;
    }
}
