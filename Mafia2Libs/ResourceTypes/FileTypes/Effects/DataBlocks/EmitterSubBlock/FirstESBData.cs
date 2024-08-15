using System.IO;
using Gibbed.IO;

namespace ResourceTypes.Effects.DataBlocks;

public class FirstESBData
{
    public string EmitterString { get; set; }
    public short Size { get; set; }

    public void ReadFromFile(BinaryReader br)
    {
        Size = br.ReadInt16();
        switch (Size)
        {
            case 83:
                break;
            case 436:
                break;
            default:
                br.BaseStream.Seek(76, SeekOrigin.Current);
                long placeholderpos = br.BaseStream.Position;//todo emptybyte check func
                byte checkb = br.ReadByte();
                br.BaseStream.Seek(placeholderpos, SeekOrigin.Begin);
                int checki = br.ReadInt32();
                
                if (checkb == 0 && checki != 0)
                {
                    //there is extra empty byte
                    br.BaseStream.Seek(placeholderpos+1, SeekOrigin.Begin);
                    placeholderpos = br.BaseStream.Position;
                }
                else
                {
                    br.BaseStream.Seek(placeholderpos, SeekOrigin.Begin);
                    placeholderpos = br.BaseStream.Position;
                }
                
                //todo hashtype/nonhashtype check func
                br.BaseStream.Seek(placeholderpos+8, SeekOrigin.Begin);
                short stringSizeCheck = br.ReadInt16();
                if (stringSizeCheck > 0 && stringSizeCheck < 32) {//hash version, i assume the string wont be bigger than 32, will need proper rework
                    EmitterString = new MemoryStream(br.ReadBytes(stringSizeCheck)).ReadString((uint)stringSizeCheck);//wip

                } else {
                    br.BaseStream.Seek(placeholderpos+16, SeekOrigin.Begin);
                    uint stringSize = br.ReadUInt32();
                    if (stringSize!=0)
                    {
                        EmitterString = new MemoryStream(br.ReadBytes((int)stringSize)).ReadString(stringSize);
                    }


                }
                
                
                
            break;
        }
    }
}