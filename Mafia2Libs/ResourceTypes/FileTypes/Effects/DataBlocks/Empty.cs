using System.IO;

namespace ResourceTypes.Effects.DataBlocks;

public class Empty
{
    public int EmptySize { get; set; }//must be 8
    public int Unk { get; set; }//likely always 2
    
    public void ReadFromFile(BinaryReader br)
    {
        EmptySize = br.ReadInt32();
        if (br.BaseStream.Position != br.BaseStream.Length)//otherwise this is the end of the file
        {
            Unk = br.ReadInt32();
        }
    }
}