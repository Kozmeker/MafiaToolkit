using System.IO;

namespace ResourceTypes.Effects.DataBlocks;

public class Emitter
{
    public int EmitterSize { get; set; }

    public void ReadFromFile(BinaryReader br)
    {
        EmitterSize = br.ReadInt32();
    }
}