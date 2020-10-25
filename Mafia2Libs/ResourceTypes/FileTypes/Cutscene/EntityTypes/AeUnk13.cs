﻿using System.Diagnostics;
using System.IO;
using Utils.Extensions;

namespace ResourceTypes.Cutscene.AnimEntities
{
    public class AeUnk13 : AeBase
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
            return AnimEntityTypes.AeUnk13;
        }
    }

    public class AeUnk13Data : AeBaseData
    {
        public int Unk02 { get; set; }
        public override void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            base.ReadFromFile(stream, isBigEndian);
            Debug.Assert(stream.Position != stream.Length, "I've read the parent class data, although i've hit the eof!");

            Unk02 = stream.ReadInt32(isBigEndian);
        }

        public override void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            base.WriteToFile(stream, isBigEndian);
            stream.Write(Unk02, isBigEndian);
        }
    }
}
