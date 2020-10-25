﻿using System.Diagnostics;
using System.IO;
using Utils.Extensions;

namespace ResourceTypes.Cutscene.AnimEntities
{
    public class AeUnk18 : AeBase
    {
        public override void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            base.ReadFromFile(stream, isBigEndian);
        }

        public override void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            base.WriteToFile(stream, isBigEndian);
        }
        public override AnimEntityTypes GetEntityType()
        {
            return AnimEntityTypes.AeUnk18;
        }
    }

    public class AeUnk18Data : AeBaseData
    {
        public int Unk02 { get; set; }
        public byte Unk03 { get; set; }

        private bool bHasDerivedData;
        public override void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            base.ReadFromFile(stream, isBigEndian);
            Debug.Assert(stream.Position != stream.Length, "I've read the parent class data, although i've hit the eof!");

            if (stream.Position != stream.Length)
            {
                Unk02 = stream.ReadInt32(isBigEndian);
                Unk03 = stream.ReadByte8();
                bHasDerivedData = true;
            }
        }

        public override void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            base.WriteToFile(stream, isBigEndian);

            if (bHasDerivedData)
            {
                stream.Write(Unk02, isBigEndian);
                stream.Write(Unk03, isBigEndian);
            }
        }
    }
}
