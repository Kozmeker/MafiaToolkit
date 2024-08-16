using System;
using System.Windows.Forms;
using Core.IO;
using ResourceTypes.Effects;
using Utils.Language;

namespace Mafia2Tool.Forms;

public partial class EffectsEditor : Form
{
    private FileEff OriginalFile;

    private EffectsLoader.Effect[] Effects;

    private bool bIsFileEdited = false;
    
    
    public EffectsEditor(FileEff EffFile)
    {
        InitializeComponent();
        OriginalFile = EffFile;
        
        Localise();
        BuildData();
    }
    
    public void Localise()
    {
        Text = Language.GetString("$EFFECTS_EDITOR");
        Button_File.Text = Language.GetString("$FILE");
        //Button_Save.Text = Language.GetString("$SAVE");
        Button_Reload.Text = Language.GetString("$RELOAD");
        Button_Exit.Text = Language.GetString("$EXIT");
    }
    
    private void AddEffectToTreeView(EffectsLoader.Effect effect)
    {
        TreeNode EffectParent = new TreeNode("Effect ID: " + effect.EffectID +" ,Emitters: " + effect.EmitterNames);
        if (effect.EmitterNames.Equals(""))
        {
            EffectParent = new TreeNode("Effect ID: " + effect.EffectID);
        }
        
        EffectParent.Tag = effect;

        if (effect.EmitterData != null)
        {
            var Assets = effect.EmitterData;
            TreeNode AssetsParent = new TreeNode("Emitter DataBlock Content");

            AssetsParent.Tag = Assets;

            EffectParent.Nodes.Add(AssetsParent);
        }

        if (effect.GenerationData != null)
        {
            var Assets = effect.GenerationData;
            TreeNode AssetsParent = new TreeNode("Generation DataBlock Content");

            AssetsParent.Tag = Assets;

            EffectParent.Nodes.Add(AssetsParent);
        }
        
        if (effect.SoundData != null)
        {
            var Assets = effect.SoundData;
            TreeNode AssetsParent = new TreeNode("Sound DataBlock Content");

            AssetsParent.Tag = Assets;

            EffectParent.Nodes.Add(AssetsParent);
        }
        
        if (effect.Empty != null)
        {
            var Assets = effect.Empty;
            TreeNode AssetsParent = new TreeNode("Empty DataBlock Content");

            AssetsParent.Tag = Assets;

            EffectParent.Nodes.Add(AssetsParent);
        }

        TreeView_Effects.Nodes.Add(EffectParent);
    }
    
    public void BuildData()
    {
        Effects = OriginalFile.GetEffectsLoader().Effects;

        for (int i = 0; i < Effects.Length; i++)
        {
            AddEffectToTreeView(Effects[i]);
        }
    }
    
    private void Reload()
    {
        PropertyGrid_Effects.SelectedObject = null;
        TreeView_Effects.SelectedNode = null;
        TreeView_Effects.Nodes.Clear();
        BuildData();
        Text = Language.GetString("$EFFECTS_EDITOR");
        bIsFileEdited = false;
    }
    
    //private void Button_Save_OnClick(object sender, EventArgs e) => Save();
    
    private void EffectsEditor_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.D)
        {
            PropertyGrid_Effects.SelectedObject = null;
            TreeView_Effects.SelectedNode = null;
        }
    }
    
    private void EffectsEditor_Closing(object sender, FormClosingEventArgs e)
    {

    }

    private void TreeView_Effects_AfterSelect(object sender, TreeViewEventArgs e)
    {
        PropertyGrid_Effects.SelectedObject = e.Node.Tag;
    }
    private void Button_Exit_OnClick(object sender, EventArgs e) => Close();

    private void Button_Reload_OnClick(object sender, EventArgs e) => Reload();
}