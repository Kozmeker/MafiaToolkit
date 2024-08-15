using System.IO;

namespace ResourceTypes.Effects.DataBlocks;

public class EmitterSubBlock
{
    public string EmitterString { get; set; }
    public int SubBlockSize { get; set; }
    int const5;//?
    int Unk0;
    int const65;//?
    int const101;//?
    public FirstESBData FirstEsbData;
    
    
    public void ReadFromFile(BinaryReader br)
    {
        SubBlockSize = br.ReadInt32();
        const5 = br.ReadInt32();
        Unk0 = br.ReadInt32();
        const65 = br.ReadInt32();
        const101 = br.ReadInt32();
        
        int FirstBlockSize = br.ReadInt32();
        br.BaseStream.Seek(-4, SeekOrigin.Current);
        using (BinaryReader generationReader = new(new MemoryStream(br.ReadBytes(FirstBlockSize))))
        {
            FirstEsbData = new FirstESBData();
            FirstEsbData.ReadFromFile(generationReader);
        }

        EmitterString = FirstEsbData.EmitterString;
    }
}