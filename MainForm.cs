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
            // Populate the language dropdown menu
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

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.btnSpeak = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();

            // TabControl
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(360, 150);
            this.tabControl.TabIndex = 0;

            // TabPage1
            this.tabPage1.Controls.Add(this.textBoxInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 122);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TAB 1";
            this.tabPage1.UseVisualStyleBackColor = true;

            // TextBox
            this.textBoxInput.Location = new System.Drawing.Point(6, 6);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(340, 110);
            this.textBoxInput.TabIndex = 0;

            // TabPage2
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 122);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TAB 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            

            // Add content buttons to TabPage2
            for (int i = 0; i < 2; i++)
            {
                Button btnContent = new Button();
                btnContent.Location = new System.Drawing.Point(6, 6 + i * 30);
                btnContent.Name = "btnContent" + i;
                btnContent.Size = new System.Drawing.Size(340, 23);
                btnContent.TabIndex = i;
                btnContent.Text = "What";
                btnContent.UseVisualStyleBackColor = true;
                btnContent.Click += new System.EventHandler(this.btnContent_Click);
                this.tabPage2.Controls.Add(btnContent);
            }

            // Speak Button
            this.btnSpeak.Location = new System.Drawing.Point(297, 219);
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
            this.comboBoxLanguage.Location = new System.Drawing.Point(12, 193);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(360, 21);
            this.comboBoxLanguage.TabIndex = 2;
            this.comboBoxLanguage.Text = "Language";

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 254);
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
    }
}