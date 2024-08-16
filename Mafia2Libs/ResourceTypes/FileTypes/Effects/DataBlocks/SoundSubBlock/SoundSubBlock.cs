using System.ComponentModel;
using System.IO;
using Gibbed.IO;

namespace ResourceTypes.Effects.DataBlocks;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class SoundSubBlock
{
    public string SoundString { get; set; }
    public int SubBlockSize { get; set; }
    public int version { get; set; }//version?
    public StringBlock stringBlock { get; set; }
    public Block44 block44a { get; set; }
    public Block12 block12a { get; set; }
    public Block12 block12b { get; set; }
    public Block9 block9 { get; set; }
    public Block44 block44b { get; set; }
    public Block44 block44c { get; set; }
    public Block52 block52 { get; set; }

    public void ReadFromFile(BinaryReader br)
    {
        SubBlockSize = br.ReadInt32();
        version = br.ReadInt32();
        stringBlock = new StringBlock(br);
        SoundString = stringBlock.SoundString;
        block44a = new Block44(br);
        block12a = new Block12(br);
        block12b = new Block12(br);
        block9 = new Block9(br);
        block44b = new Block44(br);
        block44c = new Block44(br);
        block52 = new Block52(br);
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class StringBlock
    {
        public string SoundString { get; set; }
        public int unk { get; set; }

        public StringBlock(BinaryReader br)
        {
            ReadFromFile(br);
        }
        public void ReadFromFile(BinaryReader br)
        {
            int StringBlockSize = br.ReadInt32();
            short StringSize = br.ReadInt16();
            SoundString = new MemoryStream(br.ReadBytes(StringSize)).ReadString((uint)StringSize);//"Sound" everytime?
            unk = br.ReadInt32();
        }
    }
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Block44
    {
        public int unk0 { get; set; }
        public int unk1 { get; set; }
        public int const123 { get; set; }
        public Block28 block28 { get; set; }

        public Block44(BinaryReader br)
        {
            ReadFromFile(br);
        }
        public void ReadFromFile(BinaryReader br)
        {
            int blocksize = br.ReadInt32();//44
            unk0 = br.ReadInt32();
            unk1 = br.ReadInt32();
            const123 = br.ReadInt32();
            block28 = new Block28(br);
        }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class Block28
        {
            public int unk { get; set; }
            public int const124 { get; set; }
            public Block16 block16 { get; set; }
            
            public Block28(BinaryReader br)
            {
                ReadFromFile(br);
            }
            public void ReadFromFile(BinaryReader br)
            {
                int blocksize = br.ReadInt32();//28
                unk = br.ReadInt32();
                const124 = br.ReadInt32();
                block16 = new Block16(br);
            }
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public class Block16
            {
                public int unk0 { get; set; }
                public float float0 { get; set; }//i am sure this is float, not sure about the other unks
                public int unk1 { get; set; }
            
                public Block16(BinaryReader br)
                {
                    ReadFromFile(br);
                }
                public void ReadFromFile(BinaryReader br)
                {
                    int blocksize = br.ReadInt32();//16
                    unk0 = br.ReadInt32();
                    float0 = br.ReadSingle();
                    unk1 = br.ReadInt32();
                }
            
            }
        }
    }
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Block12
    {
        public int unk0 { get; set; }
        public int unk1 { get; set; }
            
        public Block12(BinaryReader br)
        {
            ReadFromFile(br);
        }
        public void ReadFromFile(BinaryReader br)
        {
            int blocksize = br.ReadInt32();//12
            unk0 = br.ReadInt32();
            unk1 = br.ReadInt32();
        }
            
    }
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Block9
    {
        public byte byte0 { get; set; }//bool?
        public int unk0 { get; set; }
            
        public Block9(BinaryReader br)
        {
            ReadFromFile(br);
        }
        public void ReadFromFile(BinaryReader br)
        {
            int blocksize = br.ReadInt32();//9
            byte0 = br.ReadByte();
            unk0 = br.ReadInt32();
        }
            
    }
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Block52
    {
        public int unk0 { get; set; }
        public int unk1 { get; set; }
        public int const123 { get; set; }
        public Block36 block36 { get; set; }
            
        public Block52(BinaryReader br)
        {
            ReadFromFile(br);
        }
        public void ReadFromFile(BinaryReader br)
        {
            int blocksize = br.ReadInt32();//52
            unk0 = br.ReadInt32();
            unk1 = br.ReadInt32();
            const123 = br.ReadInt32();
            block36 = new Block36(br);
        }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class Block36
        {
            public int unk { get; set; }
            public int const124 { get; set; }
            public Block24 block24 { get; set; }
            
            public Block36(BinaryReader br)
            {
                ReadFromFile(br);
            }
            public void ReadFromFile(BinaryReader br)
            {
                int blocksize = br.ReadInt32();//36
                unk = br.ReadInt32();
                const124 = br.ReadInt32();
                block24 = new Block24(br);
            }
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public class Block24
            {
                public int unk0 { get; set; }//not sure about the data type
                public int unk1 { get; set; }
                public int unk2 { get; set; }
                public int unk3 { get; set; }
                public int unk4 { get; set; }//0 or 400?
            
                public Block24(BinaryReader br)
                {
                    ReadFromFile(br);
                }
                public void ReadFromFile(BinaryReader br)
                {
                    int blocksize = br.ReadInt32();//24
                    unk0 = br.ReadInt32();
                    unk1 = br.ReadInt32();
                    unk2 = br.ReadInt32();
                    unk3 = br.ReadInt32();
                    unk4 = br.ReadInt32();
                }
            
            }
        }
    }
}