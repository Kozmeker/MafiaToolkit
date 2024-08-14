using System;
using System.Collections.Generic;
using System.IO;
using ResourceTypes.Effects.DataBlocks;
using Utils.Logging;

namespace ResourceTypes.Effects;

public class EffectsLoader
{
    int chunk666;
    int fileSize;
    int chunk669;
    int const12a;
    int const2a;
    int chunk667;
    int const12b;
    int const2b;
    int chunk668;
    int fileSizeMinus;//must be fileSize-32
    int unk;//end of header
    
    
    private List<Effect> effectList;//wip
    private Effect[] effects;
    
    public Effect[] Effects {
        get { return effects; }
        set { effects = value; }
    }
    
    public EffectsLoader(FileInfo EffFile)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(EffFile.FullName, FileMode.Open)))
        {
            ReadFromFile(reader);
        }
    }
    
    public void ReadFromFile(BinaryReader reader)
    {
        chunk666 = reader.ReadInt32();
        fileSize = reader.ReadInt32();
        chunk669 = reader.ReadInt32();
        const12a = reader.ReadInt32();
        const2a = reader.ReadInt32();
        chunk667 = reader.ReadInt32();
        const12b = reader.ReadInt32();
        const2b = reader.ReadInt32();
        chunk668 = reader.ReadInt32();
        fileSizeMinus = reader.ReadInt32();
        if (fileSizeMinus!=fileSize-32)
        {
            throw new FormatException("Header file size check doesn't match!");
        }
        unk = reader.ReadInt32();

        effectList = new List<Effect>();
        while (reader.BaseStream.Position < fileSize)
        {
            effectList.Add(new Effect(reader));
        }
        ToolkitAssert.Ensure(reader.BaseStream.Position == reader.BaseStream.Length, "This is not the end of the file.");
        effects = new Effect[effectList.Count];
        for (int i = 0; i < effectList.Count; i++)//will rework
        {
            effects[i] = effectList[i];
        }
    }

    public class Effect
    {
        public int EffectID { get; set; }
        public int EffectSize { get; set; }

        public Emitter EmitterData;
        public Generation GenerationData;
        public DataBlocks.Sound SoundData;
        public Empty Empty;//private?
        
        public Effect(BinaryReader reader)
        {
            ReadFromFile(reader);
        }
        
        public void ReadFromFile(BinaryReader reader)
        {
                EffectSize = reader.ReadInt32();
                int version = reader.ReadInt32();//version?
                EffectID = reader.ReadInt32();
                int unk1 = reader.ReadInt32();
                reader.BaseStream.Seek(124, SeekOrigin.Current);//dont care now
                
                int emitterSize = reader.ReadInt32();
                reader.BaseStream.Seek(-4, SeekOrigin.Current);//make function getblocksize as this will be common
                using (BinaryReader emitterReader = new(new MemoryStream(reader.ReadBytes(emitterSize))))
                {
                    EmitterData = new Emitter();
                    EmitterData.ReadFromFile(emitterReader);
                }
                int generationSize = reader.ReadInt32();
                reader.BaseStream.Seek(-4, SeekOrigin.Current);
                using (BinaryReader generationReader = new(new MemoryStream(reader.ReadBytes(generationSize))))
                {
                    GenerationData = new Generation();
                    GenerationData.ReadFromFile(generationReader);
                }
                //check for either sound or empty block, because last block is either sound or empty
                int lastBlockSize = reader.ReadInt32();
                reader.BaseStream.Seek(-4, SeekOrigin.Current);
                if (lastBlockSize==8)//empty block is 8
                {
                    using (BinaryReader emptyReader = new(new MemoryStream(reader.ReadBytes(lastBlockSize))))//redundant?
                    {
                        Empty = new Empty();
                        Empty.ReadFromFile(emptyReader);
                    }
                }
                else
                {
                    using (BinaryReader soundReader = new(new MemoryStream(reader.ReadBytes(lastBlockSize))))//redundant?
                    {
                        SoundData = new DataBlocks.Sound();
                        SoundData.ReadFromFile(soundReader);
                    }
                }
                
        }
    }
    
}