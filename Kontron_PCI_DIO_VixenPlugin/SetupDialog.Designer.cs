namespace Kontron_PCI_DIO_VixenPlugin
{
    partial class SetupDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.comboBoxCards = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPortGroups = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCardBaseAddress = new System.Windows.Forms.Label();
            this.labelPortGroupBaseAddress = new System.Windows.Forms.Label();
            this.labelChannels = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxUseEntireCard = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxInvert = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxCards
            // 
            this.comboBoxCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCards.Enabled = false;
            this.comboBoxCards.FormattingEnabled = true;
            this.comboBoxCards.Items.AddRange(new object[] {
            "[No Cards Detected]"});
            this.comboBoxCards.Location = new System.Drawing.Point(117, 38);
            this.comboBoxCards.Name = "comboBoxCards";
            this.comboBoxCards.Size = new System.Drawing.Size(253, 21);
            this.comboBoxCards.TabIndex = 0;
            this.comboBoxCards.SelectedIndexChanged += new System.EventHandler(this.comboBoxCards_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PCI-DIOxx Card:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port Group:";
            this.label2.Visible = false;
            // 
            // comboBoxPortGroups
            // 
            this.comboBoxPortGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPortGroups.Enabled = false;
            this.comboBoxPortGroups.FormattingEnabled = true;
            this.comboBoxPortGroups.Items.AddRange(new object[] {
            "[No Cards Detected]"});
            this.comboBoxPortGroups.Location = new System.Drawing.Point(117, 114);
            this.comboBoxPortGroups.Name = "comboBoxPortGroups";
            this.comboBoxPortGroups.Size = new System.Drawing.Size(253, 21);
            this.comboBoxPortGroups.TabIndex = 2;
            this.comboBoxPortGroups.Visible = false;
            this.comboBoxPortGroups.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortGroups_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Base Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Base Address:";
            this.label4.Visible = false;
            // 
            // labelCardBaseAddress
            // 
            this.labelCardBaseAddress.AutoSize = true;
            this.labelCardBaseAddress.Location = new System.Drawing.Point(471, 45);
            this.labelCardBaseAddress.Name = "labelCardBaseAddress";
            this.labelCardBaseAddress.Size = new System.Drawing.Size(42, 13);
            this.labelCardBaseAddress.TabIndex = 6;
            this.labelCardBaseAddress.Text = "0x0000";
            // 
            // labelPortGroupBaseAddress
            // 
            this.labelPortGroupBaseAddress.AutoSize = true;
            this.labelPortGroupBaseAddress.Location = new System.Drawing.Point(471, 117);
            this.labelPortGroupBaseAddress.Name = "labelPortGroupBaseAddress";
            this.labelPortGroupBaseAddress.Size = new System.Drawing.Size(42, 13);
            this.labelPortGroupBaseAddress.TabIndex = 7;
            this.labelPortGroupBaseAddress.Text = "0x0000";
            this.labelPortGroupBaseAddress.Visible = false;
            // 
            // labelChannels
            // 
            this.labelChannels.AutoSize = true;
            this.labelChannels.Location = new System.Drawing.Point(117, 64);
            this.labelChannels.Name = "labelChannels";
            this.labelChannels.Size = new System.Drawing.Size(13, 13);
            this.labelChannels.TabIndex = 9;
            this.labelChannels.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Channels:";
            // 
            // checkBoxUseEntireCard
            // 
            this.checkBoxUseEntireCard.AutoSize = true;
            this.checkBoxUseEntireCard.Checked = true;
            this.checkBoxUseEntireCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseEntireCard.Location = new System.Drawing.Point(117, 80);
            this.checkBoxUseEntireCard.Name = "checkBoxUseEntireCard";
            this.checkBoxUseEntireCard.Size = new System.Drawing.Size(146, 17);
            this.checkBoxUseEntireCard.TabIndex = 11;
            this.checkBoxUseEntireCard.Text = "Use entire card for output";
            this.checkBoxUseEntireCard.UseVisualStyleBackColor = true;
            this.checkBoxUseEntireCard.CheckedChanged += new System.EventHandler(this.checkBoxUseEntireCard_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(354, 195);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(438, 195);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxInvert
            // 
            this.checkBoxInvert.AutoSize = true;
            this.checkBoxInvert.Location = new System.Drawing.Point(53, 165);
            this.checkBoxInvert.Name = "checkBoxInvert";
            this.checkBoxInvert.Size = new System.Drawing.Size(93, 17);
            this.checkBoxInvert.TabIndex = 14;
            this.checkBoxInvert.Text = "Invert Outputs";
            this.checkBoxInvert.UseVisualStyleBackColor = true;
            // 
            // SetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 230);
            this.Controls.Add(this.checkBoxInvert);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxUseEntireCard);
            this.Controls.Add(this.labelChannels);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelPortGroupBaseAddress);
            this.Controls.Add(this.labelCardBaseAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxPortGroups);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxCards);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kontron PCI-DIOxx";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SetupDialog_HelpButtonClicked);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCards;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPortGroups;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCardBaseAddress;
        private System.Windows.Forms.Label labelPortGroupBaseAddress;
        private System.Windows.Forms.Label labelChannels;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxUseEntireCard;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxInvert;
    }
}