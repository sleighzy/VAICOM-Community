using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.IO;
using System.Diagnostics;
using VAICOM.Static; // Ensure Log, Colors, and State are accessible
using VAICOM.PushToTalk; // Ensure PTT_Manage_Listen_VA is accessible
using VAICOM.UI; // Ensure Playsound is accessible

namespace VAICOM
{
    namespace Database
    {
        public partial class AliasEditor
        {
            // offline keywords training
            public static void TrainingStartStop()
            {
                if (!State.trainerrunning)
                {
                    SpeechTrainer.Initialize();
                    SpeechTrainer.Start();
                }
                else
                {
                    SpeechTrainer.Stop();
                }
            }

            // trainer functions
            public static class SpeechTrainer
            {
                public static SpeechRecognizer trainer;

                public static void Start()
                {
                    try
                    {
                        if (IsVoiceAccessAvailable())
                        {
                            Log.Write("Voice Access is already running.", Colors.System);
                            return; // No need to start anything if Voice Access is being used.
                        }

                        Playsound.Commandcomplete();
                        Log.Write("Offline keyword training initialized", Colors.System);
                        Log.Write("Ready for training.", Colors.Message);
                        State.trainerrunning = true;
                        trainer.SpeechRecognized += rec_SpeechRecognized;
                        trainer.SpeechRecognitionRejected += rec_SpeechRejected;
                        trainer.StateChanged += rec_StateChanged;
                    }
                    catch (Exception ex)
                    {
                        Log.Write($"Error during Start: {ex.Message}", Colors.Warning);
                    }
                }

                public static void Stop()
                {
                    try
                    {
                        if (IsVoiceAccessAvailable())
                        {
                            Log.Write("Voice Access does not require stopping.", Colors.System);
                            return; // No need to stop anything if Voice Access is being used.
                        }

                        trainer.StateChanged -= rec_StateChanged;
                        trainer.SpeechRecognitionRejected -= rec_SpeechRejected;
                        trainer.SpeechRecognized -= rec_SpeechRecognized;

                        State.trainerrunning = false;

                        Log.Write("Keyword training finished.", Colors.Message);
                        Log.Write("----------------------------------", Colors.Message);
                        PTT.PTT_Manage_Listen_VA(State.activeconfig.ReleaseHot);

                        Playsound.Commandcomplete();
                    }
                    catch (Exception ex)
                    {
                        Log.Write($"Error during Stop: {ex.Message}", Colors.Warning);
                    }
                }

                // state changes
                public static void rec_StateChanged(object sender, StateChangedEventArgs e)
                {
                    string result = string.Format("State changed");
                }

                // recognized handler
                public static void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
                {
                    string result = string.Format("Keyword recognized: " + e.Result.Text + " with confidence {0} %", (e.Result.Confidence * 100).ToString().Substring(0, 2));
                    Log.Write(result, Colors.Message);
                    Playsound.Commandcomplete();
                }

                // unrecognized handler
                public static void rec_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
                {
                    Playsound.Error();
                }

                public static void Exit()
                {
                }

                public static void Terminate()
                {
                    trainer.Dispose();
                }

                // startup
                public static void Initialize()
                {
                    try
                    {
                        Log.Write("----------------------------------", Colors.Message);
                        Log.Write("Initializing Training Mode", Colors.Message);

                        if (IsVoiceAccessAvailable())
                        {
                            Log.Write("Voice Access detected. Launching Voice Access...", Colors.System);
                            LaunchVoiceAccess();
                            State.trainerrunning = true;
                            return; // Exit early since Voice Access is being used.
                        }

                        // Fallback to Windows Speech Recognition
                        Log.Write("Voice Access not found. Falling back to Windows Speech Recognition.", Colors.System);
                        State.trainerrunning = true;
                        Log.Reset();
                        trainer = new SpeechRecognizer();
                        trainer.UnloadAllGrammars();
                        trainer.MaxAlternates = 0;

                        Log.Write(trainer.RecognizerInfo.Description, Colors.System); //type of recognizer

                        // add all aliases to new grammar
                        State.trainerchoices = new Choices();
                        foreach (KeyValuePair<string, Dictionary<string, string>> set in Aliases.categories)
                        {
                            foreach (KeyValuePair<string, string> listing in set.Value)
                            {
                                State.trainerchoices.Add(listing.Key.Replace("*", ""));
                            }
                        }

                        // load grammar and start
                        var grammarbuilder = new GrammarBuilder(State.trainerchoices);
                        var grammar = new Grammar(grammarbuilder);
                        trainer.LoadGrammar(grammar);
                        PTT.PTT_Manage_Listen_VA(false);

                        Log.Write("VAICOM PRO command phrases loaded", Colors.System);
                        trainer.Enabled = true;

                        trainer.EmulateRecognize("Start listening");
                    }
                    catch (Exception ex)
                    {
                        Log.Write($"Error during initialization: {ex.Message}", Colors.Warning);
                    }
                }
            }

            private static void LaunchVoiceAccess()
            {
                string voiceAccessPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "VoiceAccess.exe");
                try
                {
                    Process.Start(voiceAccessPath);
                    Log.Write("Voice Access launched successfully.", Colors.System);
                }
                catch (Exception ex)
                {
                    Log.Write($"Failed to launch Voice Access: {ex.Message}", Colors.Warning);
                }
            }

            private static bool IsVoiceAccessAvailable()
            {
                string voiceAccessPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "VoiceAccess.exe");
                return File.Exists(voiceAccessPath);
            }
        }
    }
}
