using System.ComponentModel;

namespace Mafia2Tool.Forms;

partial class EffectsEditor
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EffectsEditor));
        Tool_Strip = new System.Windows.Forms.ToolStrip();
        Button_File = new System.Windows.Forms.ToolStripDropDownButton();
        Button_Reload = new System.Windows.Forms.ToolStripMenuItem();
        Button_Exit = new System.Windows.Forms.ToolStripMenuItem();
        PropertyGrid_Effects = new System.Windows.Forms.PropertyGrid();
        TreeView_Effects = new Controls.MTreeView();
        Tool_Strip.SuspendLayout();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "EffectsEditor";
        SuspendLayout();
        // 
        // Tool_Strip
        // 
        Tool_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { Button_File });
        Tool_Strip.Location = new System.Drawing.Point(0, 0);
        Tool_Strip.Name = "Tool_Strip";
        Tool_Strip.Size = new System.Drawing.Size(933, 25);
        Tool_Strip.TabIndex = 18;
        Tool_Strip.Text = "ToolStrip_Main";
        // 
        // Button_File
        // 
        Button_File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        Button_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { Button_Reload, Button_Exit });
        //Button_File.Image = (System.Drawing.Image)resources.GetObject("Button_File.Image");
        Button_File.ImageTransparentColor = System.Drawing.Color.Magenta;
        Button_File.Name = "Button_File";
        Button_File.Size = new System.Drawing.Size(47, 22);
        Button_File.Text = "$FILE";
        // 
        // Button_Reload
        // 
        Button_Reload.Name = "Button_Reload";
        Button_Reload.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R;
        Button_Reload.Size = new System.Drawing.Size(165, 22);
        Button_Reload.Text = "$RELOAD";
        Button_Reload.Click += Button_Reload_OnClick;
        // 
        // Button_Exit
        // 
        Button_Exit.Name = "Button_Exit";
        Button_Exit.Size = new System.Drawing.Size(165, 22);
        Button_Exit.Text = "$EXIT";
        Button_Exit.Click += Button_Exit_OnClick;
        // 
        // PropertyGrid_Effects
        // 
        PropertyGrid_Effects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        PropertyGrid_Effects.Location = new System.Drawing.Point(469, 39);
        PropertyGrid_Effects.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        PropertyGrid_Effects.Name = "PropertyGrid_Cutscene";
        PropertyGrid_Effects.PropertySort = System.Windows.Forms.PropertySort.Categorized;
        PropertyGrid_Effects.Size = new System.Drawing.Size(450, 473);
        PropertyGrid_Effects.TabIndex = 16;
        //PropertyGrid_Effects.PropertyValueChanged += PropertyGrid_Effects_PropertyChanged;
        // 
        // TreeView_Cutscene
        // 
        TreeView_Effects.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        TreeView_Effects.ContextMenuStrip = TreeViewContextMenu;
        TreeView_Effects.Location = new System.Drawing.Point(14, 39);
        TreeView_Effects.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        TreeView_Effects.Name = "TreeView_Cutscene";
        TreeView_Effects.Size = new System.Drawing.Size(429, 472);
        TreeView_Effects.TabIndex = 17;
        TreeView_Effects.AfterSelect += TreeView_Effects_AfterSelect;
        // 
        // CutsceneEditor
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(933, 519);
        Controls.Add(Tool_Strip);
        Controls.Add(PropertyGrid_Effects);
        Controls.Add(TreeView_Effects);
        KeyPreview = true;
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "CutsceneEditor";
        Text = "$CUTSCENE_EDITOR";
        FormClosing += EffectsEditor_Closing;
        KeyUp += EffectsEditor_OnKeyUp;
        Tool_Strip.ResumeLayout(false);
        Tool_Strip.PerformLayout();
        //TreeViewContextMenu.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    
    private System.Windows.Forms.ToolStrip Tool_Strip;
    private Controls.MTreeView TreeView_Effects;
    private System.Windows.Forms.ContextMenuStrip TreeViewContextMenu;
    private System.Windows.Forms.ToolStripDropDownButton Button_File;
    private System.Windows.Forms.ToolStripMenuItem Button_Reload;
    private System.Windows.Forms.ToolStripMenuItem Button_Exit;
    private System.Windows.Forms.PropertyGrid PropertyGrid_Effects;
}