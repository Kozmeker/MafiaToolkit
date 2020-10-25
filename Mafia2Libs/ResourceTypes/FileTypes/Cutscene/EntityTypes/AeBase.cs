﻿using ResourceTypes.Cutscene.KeyParams;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Utils.Extensions;
using Utils.StringHelpers;

namespace ResourceTypes.Cutscene.AnimEntities
{
    public class AeBase
    {
        public short Unk01 { get; set; }
        public string EntityName0 { get; set; }
        public string EntityName1 { get; set; }
        public byte Unk02 { get; set; }
        public ulong FrameHash { get; set; }
        public ulong Hash1 { get; set; }
        public string FrameName { get; set; }
        public int Unk03 { get; set; }
        public int Unk04 { get; set; }
        public int Unk044 { get; set; }

        public int Size { get; set; } // Not in file

        public virtual void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            Unk01 = stream.ReadInt16(isBigEndian);

            if(Unk01 == 0)
            {
                // Nothing here. return.
                return;
            }

            EntityName0 = stream.ReadString16(isBigEndian);
            EntityName1 = stream.ReadString16(isBigEndian);

            if (!string.IsNullOrEmpty(EntityName1))
            {
                Unk02 = stream.ReadByte8();
            }

            FrameHash = stream.ReadUInt64(isBigEndian);
            Hash1 = stream.ReadUInt64(isBigEndian);
            FrameName = stream.ReadString16(isBigEndian);
            Unk03 = stream.ReadInt32(isBigEndian);
            Unk04 = stream.ReadInt32(isBigEndian);
            Unk044 = stream.ReadInt32(isBigEndian);
        }

        public virtual void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            stream.Write(Unk01, isBigEndian);

            if(Unk01 == 0)
            {
                return;
            }

            stream.WriteString16(EntityName0, isBigEndian);
            stream.WriteString16(EntityName1, isBigEndian);

            if(!string.IsNullOrEmpty(EntityName1))
            {
                stream.WriteByte(Unk02);
            }

            stream.Write(FrameHash, isBigEndian);
            stream.Write(Hash1, isBigEndian);
            stream.WriteString16(FrameName, isBigEndian);
            stream.Write(Unk03, isBigEndian);
            stream.Write(Unk04, isBigEndian);
            stream.Write(Unk044, isBigEndian);
        }

        public virtual AnimEntityTypes GetEntityType()
        {
            return 0;
        }
    }

    public class AeBaseData
    {
        [Browsable(false)]
        public int DataType { get; set; } // We might need an enumator for this
        [Browsable(false)]
        public int Size { get; set; } // Total Size of the data. includes Size and DataType.
        [Browsable(false)]
        public int KeyDataSize { get; set; } // Size of all the keyframes? Also count and the Unk01?
        public int Unk00 { get; set; }
        public int Unk01 { get; set; }
        public int NumKeyFrames { get; set; } // Number of keyframes. Start with 0xE803 or 1000
        public IKeyType[] KeyFrames { get; set; }

        public virtual void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            DataType = stream.ReadInt32(isBigEndian);
            Size = stream.ReadInt32(isBigEndian);
            Unk00 = stream.ReadInt32(isBigEndian);
            KeyDataSize = stream.ReadInt32(isBigEndian);
            Unk01 = stream.ReadInt32(isBigEndian);
            NumKeyFrames = stream.ReadInt32(isBigEndian);

            KeyFrames = new IKeyType[NumKeyFrames];

            for (int i = 0; i < NumKeyFrames; i++)
            {
                Debug.Assert(stream.Position != stream.Length, "Reached the end to early?");

                int Header = stream.ReadInt32(isBigEndian);
                Debug.Assert(Header == 1000, "Keyframe magic did not equal 1000");
                int Size = stream.ReadInt32(isBigEndian);
                int KeyType = stream.ReadInt32(isBigEndian);
                AnimKeyParamTypes KeyParamType = (AnimKeyParamTypes)KeyType;

                IKeyType KeyParam = CutsceneKeyParamFactory.ReadAnimEntityFromFile(KeyParamType, Size, stream);
                KeyFrames[i] = KeyParam;
            }
        }

        public virtual void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            stream.Write(DataType, isBigEndian);
            stream.Write(Size, isBigEndian);
            stream.Write(Unk00, isBigEndian);
            stream.Write(KeyDataSize, isBigEndian);
            stream.Write(Unk01, isBigEndian);
            stream.Write(NumKeyFrames, isBigEndian);

            for(int i = 0; i < NumKeyFrames; i++)
            {
                // Get KeyParam
                IKeyType KeyParam = KeyFrames[i];
                stream.Write(1000, isBigEndian); // Write the header
                stream.Write(KeyParam.Size, isBigEndian);
                stream.Write(KeyParam.KeyType, isBigEndian);
                KeyParam.WriteToFile(stream, isBigEndian);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", DataType);
        }
    }
}
