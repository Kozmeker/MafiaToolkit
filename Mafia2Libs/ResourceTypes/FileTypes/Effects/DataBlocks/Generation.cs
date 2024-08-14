using System.IO;

namespace ResourceTypes.Effects.DataBlocks;

public class Generation
{
    public int GenerationSize { get; set; }
    public void ReadFromFile(BinaryReader br)
    {
        GenerationSize = br.ReadInt32();
    }
}