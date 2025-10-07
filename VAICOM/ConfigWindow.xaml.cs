using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using VAICOM.Client;
using VAICOM.Static;

namespace VAICOM
{

    namespace UI
    {

        public partial class ConfigWindow : Window
        {

            // ------------ WINDOW HANDLERS -------------------------------

            public ConfigWindow()
            {

                InitializeComponent();

                this.ResizeMode = ResizeMode.CanMinimize;
                this.WindowStyle = WindowStyle.SingleBorderWindow;

                if (State.activeconfig.Windowrestorelocation != null)
                {
                    this.Left = State.activeconfig.Windowrestorelocation.X;
                    this.Top = State.activeconfig.Windowrestorelocation.Y;
                }

                this.Width = 500;
                this.Height = 340;

                //this.WindowStyle = WindowStyle.None;
                this.Topmost = false;

                //this.Opacity = 1.0;

                UpdateAllbugs();

                // Hook up the Voice Access Priority checkbox
                UseVoiceAccessPriority.Checked += EnableVoiceAccessPriority;
                UseVoiceAccessPriority.Unchecked += DisableVoiceAccessPriority;
                UseVoiceAccessPriority.Loaded += SetCurrentValueVoiceAccessPriority;

            }

            private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            {

                //if (State.configwindowthread.IsAlive)
                //{
                try
                {
                    State.activeconfig.Windowrestorelocation = this.RestoreBounds.Location;
                    Settings.ConfigFile.WriteConfigToFile(true);

                    if (State.trainerrunning)
                    {
                        Log.Write("Training mode closed.", Static.Colors.Message);
                        Log.Write("----------------------------------", Static.Colors.Message);
                        /* try
                        {
                            AliasEditor.SpeechTrainer.Terminate();
                        }
                        catch
                        {
                        } */
                        UI.Playsound.Commandcomplete();
                        State.trainerrunning = false;
                    }
                    Message.Text = "";
                    //}

                    State.configwindowopen = false;
                }
                catch
                {

                }
            }
            private void Window_Closed(object sender, EventArgs e)
            {
                State.configwindowopen = false;
            }

            // ------------------------------------------------------------

            // ------------  CONFIG UPDATE HELPERS ------------------------

            // write config file only
            private void ConfigUpdate(object sender, RoutedEventArgs e)
            {
                Settings.ConfigFile.WriteConfigToFile(true);
            }
            // write config and send
            private void ConfigAndServerUpdate(object sender, RoutedEventArgs e)
            {
                DcsClient.SendSettingsChange();
                Settings.ConfigFile.WriteConfigToFile(true);
            }

            // ------------------------------------------------------------


            public string setversionnumber()
            {
                return " Version " + State.versionstring;
            }

            private void setversionnumber1(object sender, EventArgs e)
            {
                labelsub1.Content = setversionnumber();
            }
            private void setversionnumber2(object sender, EventArgs e)
            {
                labelsub2.Content = setversionnumber();
            }
            private void setversionnumber3(object sender, EventArgs e)
            {
                labelsub3.Content = setversionnumber();
            }
            private void setversionnumber4(object sender, EventArgs e)
            {
                labelsub4.Content = setversionnumber();
            }
            private void setversionnumber5(object sender, EventArgs e)
            {
                labelsub5.Content = setversionnumber();
            }
            private void setversionnumber6(object sender, EventArgs e)
            {
                labelsub6.Content = setversionnumber();
            }
            private void setversionnumber7(object sender, EventArgs e)
            {
                labelsub7.Content = setversionnumber();
            }
            private void setversionnumber8(object sender, EventArgs e)
            {
                labelsub8.Content = setversionnumber();
            }
            private void setversionnumber9(object sender, EventArgs e)
            {
                labelsub9.Content = setversionnumber();
            }

            private void setversionnumber10(object sender, EventArgs e)
            {
                labelsub10.Content = setversionnumber();
            }


            private void Alternateupdatebug(object sender, EventArgs e)
            {
                Alternateupdatebug();
            }

            private void SwitchPage_Release(object sender, MouseEventArgs e)
            {

            }

            private void SendDebugCode(object sender, RoutedEventArgs e)
            {
                SendDebugMsg();
            }

            public void SendDebugMsg()
            {
                Client.DcsClient.Message.DebugMsg newdebug = new DcsClient.Message.DebugMsg();
                newdebug.exec = CodeBlock.Text;
                DcsClient.SendDebugCode(newdebug);
            }

            private void InitCodeBlock(object sender, EventArgs e)
            {
                if (State.clientmode.Equals(ClientModes.Debug))
                {
                    CodeBlock.IsEnabled = true;
                    CodeBlock.Visibility = Visibility.Visible;
                    CodeBlock.Text = Properties.Resources.Debug_code;
                }
                else
                {
                    CodeBlock.IsEnabled = false;
                    CodeBlock.Visibility = Visibility.Hidden;
                    CodeBlock.Text = "";
                }
            }

            private void InitDebugBtn(object sender, EventArgs e)
            {
                if (State.clientmode.Equals(ClientModes.Debug))
                {
                    SendButton.IsEnabled = true;
                    SendButton.Visibility = Visibility.Visible;
                }
                else
                {
                    SendButton.IsEnabled = false;
                    SendButton.Visibility = Visibility.Hidden;
                }
            }

            private void InitDebugTab(object sender, EventArgs e)
            {
                if (State.clientmode.Equals(ClientModes.Debug))
                {
                    debugpage.IsEnabled = true;
                    debugpage.Visibility = Visibility.Visible;
                }
                else
                {
                    debugpage.IsEnabled = false;
                    debugpage.Visibility = Visibility.Hidden;
                }
            }

            public void DebugViewState()
            {
                if (State.clientmode.Equals(ClientModes.Debug))
                {
                    debugpage.IsEnabled = true;
                    debugpage.Visibility = Visibility.Visible;
                    SendButton.IsEnabled = true;
                    SendButton.Visibility = Visibility.Visible;
                    CodeBlock.IsEnabled = true;
                    CodeBlock.Visibility = Visibility.Visible;
                    CodeBlock.Text = Properties.Resources.Debug_code;
                }
                else
                {
                    debugpage.IsEnabled = false;
                    debugpage.Visibility = Visibility.Hidden;
                    SendButton.IsEnabled = false;
                    SendButton.Visibility = Visibility.Hidden;

                    CodeBlock.IsEnabled = false;
                    CodeBlock.Visibility = Visibility.Hidden;
                    CodeBlock.Text = "";
                }
            }

            private void DumpState(object sender, MouseButtonEventArgs e)
            {
                Servers.Server.DumpStateToLog();
            }

            private void OpenVaicomLogFile(object sender, MouseButtonEventArgs e)
            {
                try
                {
                    // Construct the path to the VaicomPro.log file
                    string logFilePath = Path.Combine(State.VA_APPS, "VAICOMPRO", "logs", "VAICOMPRO.log");

                    // Ensure the directory exists
                    if (File.Exists(logFilePath))
                    {
                        // Open the file location in File Explorer and select the log file
                        Process.Start("explorer.exe", $"/select,\"{logFilePath}\"");
                    }
                    else
                    {
                        MessageBox.Show("The VaicomPro.log file does not exist.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred while trying to open the log file location.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void OpenVaicomManual(object sender, MouseButtonEventArgs e)
            {
                try
                {
                    // Construct the path to the VaicomPro manual
                    string manualPath = Path.Combine(State.VA_APPS, "VAICOMPRO", "Documentation", "VAICOM PRO Community User Manual.pdf");

                    // Ensure the file exists
                    if (File.Exists(manualPath))
                    {
                        // Use ProcessStartInfo to open the PDF file
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = manualPath,
                            UseShellExecute = true // Ensures the default PDF viewer is used
                        };

                        Process.Start(startInfo);
                    }
                    else
                    {
                        MessageBox.Show("The VaicomPro manual does not exist.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while trying to open the manual: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void OpenYouTubeTutorials(object sender, MouseButtonEventArgs e)
            {
                try
                {
                    // Open the YouTube tutorials link in the default browser
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://www.youtube.com/playlist?list=PLLkY2GByvtC2JxgURCFnobqz8iz3Vqooe",
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("An error occurred while trying to open the YouTube tutorials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
