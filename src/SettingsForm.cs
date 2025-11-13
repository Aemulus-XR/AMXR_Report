using System;
using System.Drawing;
using System.Windows.Forms;
using AemulusConnect.Strings;
using AemulusConnect.Helpers;

namespace AemulusConnect
{
    public class SettingsForm : Form
    {
        private TextBox txtOutputPath;
        private ComboBox cmbLanguage;
        private Button btnSave;
        private Button btnCancel;
        private Action<string, string, string>? _onSave;
        private string _initialLanguage;
        // Keep the initial remote paths for backend persistence, but don't expose in UI
        private string _reportsPath;
        private string _archivePath;

        public SettingsForm(string initialReportsPath, string initialArchivePath, string initialOutputPath, Action<string, string, string>? onSave)
        {
            _onSave = onSave;
            _initialLanguage = SettingsManager.Language;
            _reportsPath = initialReportsPath;
            _archivePath = initialArchivePath;
            Text = Properties.Resources.Settings_WindowTitle;
            Size = new Size(520, 180);
            StartPosition = FormStartPosition.CenterParent;

            // Output path (PC location)
            var lblOutput = new Label() { Text = Properties.Resources.Settings_OutputPathLabel, Location = new Point(10, 15), AutoSize = true };
            txtOutputPath = new TextBox() { Location = new Point(10, 35), Width = 360, Text = initialOutputPath };

            // Browse button for output folder
            var btnBrowse = new Button() { Text = Properties.Resources.Settings_BrowseButton, Location = new Point(380, 33), Size = new Size(100, 24) };
            btnBrowse.Click += (s, e) =>
            {
                using var dlg = new FolderBrowserDialog();
                dlg.Description = Properties.Resources.Settings_FolderBrowserDescription;
                try { dlg.SelectedPath = txtOutputPath.Text; } catch { }
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtOutputPath.Text = dlg.SelectedPath;
            };

            // Language selector
            var lblLanguage = new Label() { Text = Properties.Resources.Settings_LanguageLabel, Location = new Point(10, 70), AutoSize = true };
            cmbLanguage = new ComboBox()
            {
                Location = new Point(10, 90),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Populate language dropdown
            var availableCultures = LocalizationHelper.GetAvailableCultures();
            foreach (var culture in availableCultures)
            {
                cmbLanguage.Items.Add(culture);
            }

            // Select current language
            var currentCulture = availableCultures.Find(c => c.Code == _initialLanguage);
            if (currentCulture != null)
            {
                cmbLanguage.SelectedItem = currentCulture;
            }
            else
            {
                // Default to first item (English) if current language not found
                if (cmbLanguage.Items.Count > 0)
                    cmbLanguage.SelectedIndex = 0;
            }

            btnSave = new Button() { Text = Properties.Resources.Settings_SaveButton, Location = new Point(320, 130), DialogResult = DialogResult.OK };
            btnCancel = new Button() { Text = Properties.Resources.Settings_CancelButton, Location = new Point(410, 130), DialogResult = DialogResult.Cancel };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => Close();

            Controls.Add(lblOutput);
            Controls.Add(txtOutputPath);
            Controls.Add(btnBrowse);
            Controls.Add(lblLanguage);
            Controls.Add(cmbLanguage);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            // Apply RTL layout if Arabic is selected
            LocalizationHelper.ApplyRTLToForm(this);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            // Use the stored remote paths (unchanged by user)
            var reports = _reportsPath;
            var archive = _archivePath;
            var output = txtOutputPath.Text?.Trim() ?? string.Empty;

            // Get selected language
            var selectedCulture = cmbLanguage.SelectedItem as CultureOption;
            var selectedLanguage = selectedCulture?.Code ?? LocalizationHelper.DefaultCulture;

            // Update static strings (remote paths remain as they were)
            FSStrings.ReportsLocation = reports;
            FSStrings.ArchiveLocation = archive;
            FSStrings.OutputLocation = output;
            SettingsManager.Language = selectedLanguage;

            // Persist settings to disk
            try { SettingsManager.SaveSettings(); } catch { }

            // Show restart notification if language changed
            if (selectedLanguage != _initialLanguage)
            {
                MessageBox.Show(
                    Properties.Resources.Settings_RestartMessage,
                    Properties.Resources.Settings_RestartTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            _onSave?.Invoke(reports, archive, output);
            Close();
        }
    }
}
