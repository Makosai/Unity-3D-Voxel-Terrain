public class BlockAir : Block {

    public BlockAir() : base()
    {

    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        return meshData;
    }

    public override bool IsSolid(Direction direction)
    {
        return false;
    }
}
