using System.Collections.Generic;
using System.IO;
using Utils.Logging;

namespace ResourceTypes.Effects.DataBlocks.Emitter;

public class Emitter
{
    public int EmitterSize { get; set; }
    public int Const100 { get; set; }//probably always 100
    public List<string> EmitterNames { get; set; }

    private List<EmitterSubBlock> emitterSubBlocklist;//wip
    private EmitterSubBlock[] emitterSubBlocks;
    
    public EmitterSubBlock[] EmitterSubBlocks {
        get { return emitterSubBlocks; }
        set { emitterSubBlocks = value; }
    }
    
    public void ReadFromFile(BinaryReader br)
    {
        EmitterSize = br.ReadInt32();
        Const100 = br.ReadInt32();
        
        emitterSubBlocklist = new List<EmitterSubBlock>();
        while (br.BaseStream.Position < EmitterSize)
        {
            int subBlockSize = br.ReadInt32();//lidl wip
            br.BaseStream.Seek(-4, SeekOrigin.Current);
            using (BinaryReader emitterSubBlockReader = new(new MemoryStream(br.ReadBytes(subBlockSize))))
            {
                EmitterSubBlock esbl = new EmitterSubBlock();
                esbl.ReadFromFile(emitterSubBlockReader);
                emitterSubBlocklist.Add(esbl);
            }
        }
        ToolkitAssert.Ensure(br.BaseStream.Position == br.BaseStream.Length, "This is not the end of the block.");
        emitterSubBlocks = emitterSubBlocklist.ToArray();
        EmitterNames = new List<string>();
        for (int i = 0; i < emitterSubBlocks.Length; i++)
        {
            if (emitterSubBlocks[i].EmitterString != null)
            {
               EmitterNames.Add(emitterSubBlocks[i].EmitterString);  
            }
            
        }
    }
}