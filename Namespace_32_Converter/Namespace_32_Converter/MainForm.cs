// <copyright file="MainForm.cs" company="www.PublicDomain.tech">All rights waived.</copyright>

// Programmed by Victor L. Senior (VLS) <support@publicdomain.tech>, 2016
//
// Web: http://publicdomain.tech
//
// Sources: http://github.com/publicdomaintech/
//
// This software and associated documentation files (the "Software") is
// released under the CC0 Public Domain Dedication, version 1.0, as
// published by Creative Commons. To the extent possible under law, the
// author(s) have dedicated all copyright and related and neighboring
// rights to the Software to the public domain worldwide. The Software is
// distributed WITHOUT ANY WARRANTY.
//
// If you did not receive a copy of the CC0 Public Domain Dedication
// along with the Software, see
// <http://creativecommons.org/publicdomain/zero/1.0/>

/// <summary>
/// Main form.
/// </summary>
namespace Namespace_32_Converter
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Windows.Forms;
    using PdBets;

    /// <summary>
    /// Main form.
    /// </summary>
    [Export(typeof(IPdBets))]
    public partial class MainForm : Form, IPdBets
    {
        /// <summary>
        /// The converter.
        /// </summary>
        private Converter converter = new Converter();

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace_32_Converter.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the module dictionary.
        /// </summary>
        /// <value>The module dictionary.</value>
        public Dictionary<string, object> ModuleDictionary { get; set; } = new Dictionary<string, object>()
        {
            ["menuPath"] = "Converters"
        };

        /// <summary>
        /// Processes incoming input and bet strings.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <param name="betString">Bet string.</param>
        /// <returns>>The processed input string.</returns>
        public string Input(string inputString, string betString)
        {
            // The return string
            string returnString = "-K";

            try
            {
                // Check if must show
                switch (inputString)
                {
                // Show
                    case "-S":

                        // Display form
                        this.Show();

                        // Exit flow
                        break;

                // Quit 
                    case "-Q":
                        
                        // Close form
                        this.Close();

                        // Exit flow
                        break;

                // Halt
                    case "-H":

                        // Exit application
                        Application.Exit();

                        // Exit flow
                        break;
                }    
            }
            catch (Exception)
            {
                // Set return string to error
                returnString = "-E";
            }

            // Send return string
            return returnString;
        }

        /// <summary>
        /// Raises the namespace button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnNamespaceButtonClick(object sender, EventArgs e)
        {
            // Check there's something to work with
            if (this.textToProcessTextBox.Text.Length == 0)
            {
                // Halt flow
                return;
            }

            try
            {
                // Hold resulting namespace string
                string namespaceString;

                // Convert current text to namespace
                namespaceString = this.converter.DisplayNameToFileName(this.textToProcessTextBox.Text);

                // Check if must copy to clipboard
                if (this.copyToClipboardCheckBox.Checked)
                {
                    // Copy namespace to clipboard
                    Clipboard.SetText(namespaceString);
                }

                // Assign namespace to text box
                this.textToProcessTextBox.Text = namespaceString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't convert to namespace:" + Environment.NewLine + Environment.NewLine + ex.Message, "Namespace conversion error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Raises the display name button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnDisplayNameButtonClick(object sender, EventArgs e)
        {
            // Check there's something to work with
            if (this.textToProcessTextBox.Text.Length == 0)
            {
                // Halt flow
                return;
            }

            try
            {
                // Hold resulting display name string
                string displayNameString;

                // Convert current text to display name
                displayNameString = this.converter.FileNameToDisplayName(this.textToProcessTextBox.Text);

                // Check if must copy to clipboard
                if (this.copyToClipboardCheckBox.Checked)
                {
                    // Copy display name to clipboard
                    Clipboard.SetText(displayNameString);
                }

                // Assign display name to text box
                this.textToProcessTextBox.Text = displayNameString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't convert to display name:" + Environment.NewLine + Environment.NewLine + ex.Message, "Display name conversion error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Raises the main form form closing event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if it's closing by user
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Hide form
                this.Hide();

                // Prevent closing
                e.Cancel = true;
            }
        }
    }
}