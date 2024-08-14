using System.IO;
using Mafia2Tool.Forms;
using ResourceTypes.Effects;


namespace Core.IO;

public class FileEff : FileBase
{
    private EffectsLoader Loader;
    
    public FileEff(FileInfo info) : base(info)
    {
    }

    public override string GetExtensionUpper()
    {
        return "EFF";
    }
    public override bool Open()
    {
        Loader = new EffectsLoader(file);
        EffectsEditor editor = new EffectsEditor(this);
        editor.Show();
        return true;
    }
    
    public EffectsLoader GetEffectsLoader()
    {
        return Loader;
    }
}