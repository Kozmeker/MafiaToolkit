using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ResourceTypes.Effects.DataBlocks;
using ResourceTypes.Effects.DataBlocks.Emitter;
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
        effects = effectList.ToArray();
    }

    public class Effect
    {
        public int EffectID { get; set; }
        public string EmitterNames { get; set; }
        public int EffectSize { get; set; }
        public int version { get; set; }
        public int unk0 { get; set; }
        public Block20 block20 { get; set; }
        public Block12f block12F1 { get; set; }
        public Block12f block12F2 { get; set; }
        public Block12f block12F3 { get; set; }
        public Block12i block12i1 { get; set; }
        public Block32 block32 { get; set; }
        public Block12f block12F4 { get; set; }
        public Block12i block12i2 { get; set; }

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
                version = reader.ReadInt32();//version?
                EffectID = reader.ReadInt32();
                unk0 = reader.ReadInt32();
                block20 = new Block20(reader);
                block12F1 = new Block12f(reader);
                block12F2 = new Block12f(reader);
                block12F3 = new Block12f(reader);
                block12i1 = new Block12i(reader);
                block32 = new Block32(reader);
                block12F4 = new Block12f(reader);
                block12i2 = new Block12i(reader);
                
                //reader.BaseStream.Seek(124, SeekOrigin.Current);//dont care now
                
                int emitterSize = reader.ReadInt32();
                reader.BaseStream.Seek(-4, SeekOrigin.Current);//make function getblocksize as this will be common
                using (BinaryReader emitterReader = new(new MemoryStream(reader.ReadBytes(emitterSize))))
                {
                    EmitterData = new Emitter();
                    EmitterData.ReadFromFile(emitterReader);
                }

                EmitterNames = string.Join(", ", EmitterData.EmitterNames);
                
                
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
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class Block32
        {
            public float float1 { get; set; }
            public float float2 { get; set; }
            public float float3 { get; set; } 
            public float float4 { get; set; }
            public float float5 { get; set; }
            public float float6 { get; set; }
            public int unk { get; set; }

            public Block32(BinaryReader br)
            {
                ReadFromFile(br);
            }
            public void ReadFromFile(BinaryReader br)
            {
                int blocksize = br.ReadInt32();//must be 32
                float1 = br.ReadSingle();
                float2 = br.ReadSingle();
                float3 = br.ReadSingle();
                float4 = br.ReadSingle();
                float5 = br.ReadSingle();
                float6 = br.ReadSingle();
                unk = br.ReadInt32();
            }
        }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class Block20
        {
            public float float1 { get; set; }
            public float float2 { get; set; }
            public float float3 { get; set; }
            public int unk { get; set; }

            public Block20(BinaryReader br)
            {
                ReadFromFile(br);
            }
            public void ReadFromFile(BinaryReader br)
            {
                int blocksize = br.ReadInt32();//must be 20
                float1 = br.ReadSingle();
                float2 = br.ReadSingle();
                float3 = br.ReadSingle();
                unk = br.ReadInt32();
            }
        }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class Block12f
        {
            public float float1 { get; set; }
            public int unk { get; set; }

            public Block12f(BinaryReader br)
            {
                ReadFromFile(br);
            }
            public void ReadFromFile(BinaryReader br)
            {
                int blocksize = br.ReadInt32();//must be 12
                float1 = br.ReadSingle();
                unk = br.ReadInt32();
            }
        }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class Block12i
        {
            public int unk0 { get; set; }//it rarely has values, but it doesn't appear to be float
            public int unk1 { get; set; }

            public Block12i(BinaryReader br)
            {
                ReadFromFile(br);
            }
            public void ReadFromFile(BinaryReader br)
            {
                int blocksize = br.ReadInt32();//must be 12
                unk0 = br.ReadInt32();
                unk1 = br.ReadInt32();
            }
        }
    }
    
}