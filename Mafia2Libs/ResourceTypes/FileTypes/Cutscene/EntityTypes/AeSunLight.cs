﻿using System.IO;
using SharpDX;
using Utils.Extensions;
using Utils.SharpDXExtensions;

namespace ResourceTypes.Cutscene.AnimEntities
{
    //AeSunLight
    public class AeSunLight : AeBase
    {
        public byte Unk05 { get; set; }
        public int Unk06 { get; set; }
        public int Unk07 { get; set; }
        public Matrix Transform { get; set; }
        public int Unk09 { get; set; }
        public int Unk10 { get; set; }
        public float[] Unk08 { get; set; }
        public int Unk11 { get; set; }
        public int Unk12 { get; set; }
        public string Name33 { get; set; }
        public float[] Unk14 { get; set; }
        public byte Unk15 { get; set; }
        public float[] Unk16 { get; set; }

        public override void ReadFromFile(MemoryStream stream, bool isBigEndian)
        {
            base.ReadFromFile(stream, isBigEndian);
            Unk05 = stream.ReadByte8();
            Unk06 = stream.ReadInt32(isBigEndian);
            Unk07 = stream.ReadInt32(isBigEndian);
            Transform = MatrixExtensions.ReadFromFile(stream, isBigEndian);
            Unk09 = stream.ReadInt32(isBigEndian);
            Unk10 = stream.ReadInt32(isBigEndian);
            Unk08 = new float[10];
            for (int i = 0; i < 10; i++)
            {
                Unk08[i] = stream.ReadSingle(isBigEndian);
            }
            Unk11 = stream.ReadInt32(isBigEndian);
            Unk12 = stream.ReadInt32(isBigEndian);
            Name33 = stream.ReadString16(isBigEndian);
            Unk14 = new float[9];
            for (int i = 0; i < 9; i++)
            {
                Unk14[i] = stream.ReadSingle(isBigEndian);
            }
            Unk15 = stream.ReadByte8();
            Unk16 = new float[12];
            for (int i = 0; i < 12; i++)
            {
                Unk16[i] = stream.ReadSingle(isBigEndian);
            }
        }

        public override void WriteToFile(MemoryStream stream, bool isBigEndian)
        {
            base.WriteToFile(stream, isBigEndian);
            stream.WriteByte(Unk05);
            stream.Write(Unk06, isBigEndian);
            stream.Write(Unk07, isBigEndian);
            Transform.WriteToFile(stream, isBigEndian);
            stream.Write(Unk09, isBigEndian);
            stream.Write(Unk10, isBigEndian);

            foreach(var Value in Unk08)
            {
                stream.Write(Value, isBigEndian);
            }

            stream.Write(Unk11, isBigEndian);
            stream.Write(Unk12, isBigEndian);
            stream.WriteString16(Name33, isBigEndian);

            foreach (var Value in Unk14)
            {
                stream.Write(Value, isBigEndian);
            }

            stream.WriteByte(Unk15);

            foreach (var Value in Unk16)
            {
                stream.Write(Value, isBigEndian);
            }
        }
        public override AnimEntityTypes GetEntityType()
        {
            return AnimEntityTypes.AeSunLight;
        }
    }
}