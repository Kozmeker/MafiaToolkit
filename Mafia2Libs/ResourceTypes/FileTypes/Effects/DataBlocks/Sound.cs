using System.Collections.Generic;
using System.IO;
using Utils.Logging;

namespace ResourceTypes.Effects.DataBlocks;

public class Sound
{
    public int SoundSize { get; set; }
    public int Const400 { get; set; }
    
    private List<SoundSubBlock> soundSubBlocklist;//wip
    private SoundSubBlock[] soundSubBlocks;
    
    public SoundSubBlock[] SoundSubBlocks {
        get { return soundSubBlocks; }
        set { soundSubBlocks = value; }
    }
    public void ReadFromFile(BinaryReader br)
    {
        SoundSize = br.ReadInt32();
        Const400 = br.ReadInt32();
        
        soundSubBlocklist = new List<SoundSubBlock>();
        while (br.BaseStream.Position < SoundSize)
        {
            int subBlockSize = br.ReadInt32();//lidl wip
            br.BaseStream.Seek(-4, SeekOrigin.Current);
            using (BinaryReader soundSubBlockReader = new(new MemoryStream(br.ReadBytes(subBlockSize))))
            {
                SoundSubBlock ssbl = new SoundSubBlock();
                ssbl.ReadFromFile(soundSubBlockReader);
                soundSubBlocklist.Add(ssbl);
            }
        }
        ToolkitAssert.Ensure(br.BaseStream.Position == br.BaseStream.Length, "This is not the end of the block.");
        soundSubBlocks = soundSubBlocklist.ToArray();
    }
}