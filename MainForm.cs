using System;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace TextToSpeechApp
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;

        public Form1()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the language dropdown menu with a friendly label
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Items.Add(" Select Voice");
            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                comboBoxLanguage.Items.Add(voice.VoiceInfo.Name);
            }
            comboBoxLanguage.SelectedIndex = 0;
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            string text = textBoxInput.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                synthesizer.SelectVoice(comboBoxLanguage.SelectedItem.ToString());
                synthesizer.SpeakAsync(text);
            }
        }

        private void btnContent_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string content = clickedButton.Text;
            synthesizer.SelectVoice(comboBoxLanguage.SelectedItem.ToString());
            synthesizer.SpeakAsync(content);
        }

        private void btnAddContent_Click(object sender, EventArgs e)
        {
            FormAddContent formAddContent = new FormAddContent();
            if (formAddContent.ShowDialog() == DialogResult.OK)
            {
                string content = formAddContent.Content;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    AddContentButton(content);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            Button contentButton = (Button)editButton.Tag;

            string newContent = Microsoft.VisualBasic.Interaction.InputBox("Enter new content:", "Edit Content", contentButton.Text);
            if (!string.IsNullOrWhiteSpace(newContent))
            {
                contentButton.Text = newContent;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Button contentButton = (Button)deleteButton.Tag;
            Button editButton = (Button)contentButton.Tag;

            panelContent.Controls.Remove(contentButton);
            panelContent.Controls.Remove(editButton);
            panelContent.Controls.Remove(deleteButton);

            UpdateButtonPositions();
        }

        private void AddContentButton(string content)
        {
            Button btnContent = new Button();
            btnContent.Location = new System.Drawing.Point(6, 10 + panelContent.Controls.Count * 30);
            btnContent.Name = "btnContent" + panelContent.Controls.Count;
            btnContent.Size = new System.Drawing.Size(280, 23);
            btnContent.TabIndex = panelContent.Controls.Count;
            btnContent.Text = content;
            btnContent.TextAlign = ContentAlignment.MiddleLeft;
            btnContent.UseVisualStyleBackColor = true;
            btnContent.Click += new System.EventHandler(this.btnContent_Click);
            panelContent.Controls.Add(btnContent);

            Button btnEdit = new Button();
            btnEdit.Location = new System.Drawing.Point(292, 10 + panelContent.Controls.Count * 30);
            btnEdit.Name = "btnEdit" + panelContent.Controls.Count;
            btnEdit.Size = new System.Drawing.Size(23, 23);
            btnEdit.TabIndex = panelContent.Controls.Count;
            btnEdit.Image = Image.FromFile("Resources\\EditIcon.png"); // Replace with your edit icon image
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Tag = btnContent;
            btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            panelContent.Controls.Add(btnEdit);

            Button btnDelete = new Button();
            btnDelete.Location = new System.Drawing.Point(321, 10 + panelContent.Controls.Count * 30);
            btnDelete.Name = "btnDelete" + panelContent.Controls.Count;
            btnDelete.Size = new System.Drawing.Size(23, 23);
            btnDelete.TabIndex = panelContent.Controls.Count;
            btnDelete.Image = Image.FromFile("Resources\\DeleteIcon.png"); // Replace with your delete icon image
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Tag = btnContent;
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            panelContent.Controls.Add(btnDelete);

            btnContent.Tag = btnEdit;
            btnEdit.Tag = btnDelete;
        }

        private void UpdateButtonPositions()
        {
            int index = 0;
            foreach (Control control in panelContent.Controls)
            {
                if (control is Button)
                {
                    control.Location = new System.Drawing.Point(control.Location.X, 10 + index * 30);
                    index++;
                }
            }
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.btnSpeak = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.btnAddContent = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();

            // TabControl
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(360, 200);
            this.tabControl.TabIndex = 0;

            // TabPage1
            this.tabPage1.Controls.Add(this.textBoxInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 172);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TAB 1";
            this.tabPage1.UseVisualStyleBackColor = true;

            // TextBox
            this.textBoxInput.Location = new System.Drawing.Point(6, 6);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(340, 160);
            this.textBoxInput.TabIndex = 0;

            // TabPage2
            this.tabPage2.Controls.Add(this.panelContent);
            this.tabPage2.Controls.Add(this.btnAddContent);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 172);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TAB 2";
            this.tabPage2.UseVisualStyleBackColor = true;

            // Button for adding content
            this.btnAddContent.Location = new System.Drawing.Point(6, 6);
            this.btnAddContent.Name = "btnAddContent";
            this.btnAddContent.Size = new System.Drawing.Size(23, 23);
            this.btnAddContent.TabIndex = 1;
            this.btnAddContent.Text = "+";
            this.btnAddContent.UseVisualStyleBackColor = true;
            this.btnAddContent.Click += new System.EventHandler(this.btnAddContent_Click);

            // Panel for content buttons
            this.panelContent.AutoScroll = true;
            this.panelContent.Location = new System.Drawing.Point(6, 35);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(340, 131);
            this.panelContent.TabIndex = 2;

            // Speak Button
            this.btnSpeak.Location = new System.Drawing.Point(297, 249);
            this.btnSpeak.Name = "btnSpeak";
            this.btnSpeak.Size = new System.Drawing.Size(75, 23);
            this.btnSpeak.TabIndex = 3;
            this.btnSpeak.Text = "SAY";
            this.btnSpeak.UseVisualStyleBackColor = true;
            this.btnSpeak.Click += new System.EventHandler(this.btnSpeak_Click);

            // Title Label
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(178, 25);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "Text to speech Software";

            // Language ComboBox
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(12, 249);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(279, 21);
            this.comboBoxLanguage.TabIndex = 2;
            this.comboBoxLanguage.Text = "Language";

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 284);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.btnSpeak);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Text-to-Speech App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button btnSpeak;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Button btnAddContent;
        private System.Windows.Forms.Panel panelContent;
    }
}