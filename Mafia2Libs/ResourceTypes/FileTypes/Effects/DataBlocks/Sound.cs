using System.IO;

namespace ResourceTypes.Effects.DataBlocks;

public class Sound
{
    public int SoundSize { get; set; }
    public void ReadFromFile(BinaryReader br)
    {
        SoundSize = br.ReadInt32();
    }
}