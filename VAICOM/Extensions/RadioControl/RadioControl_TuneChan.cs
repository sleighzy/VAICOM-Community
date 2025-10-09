using System;
using VAICOM.Static;

namespace VAICOM
{

    namespace Client
    {

        public partial class DcsClient
        {

            public static partial class Message
            {

                public static void RadioControl_TuneChan()
                {
                    try
                    {


                        RadioTuneMessage SendMessage = new RadioTuneMessage();
                        SendMessage.tgtdevid = Message.GetSendDeviceId();

                        int chan = 0;
                        string header = State.Proxy.Utility.ParseTokens("{CMDSEGMENT:0}");
                        string rawChan = State.Proxy.Utility.ParseTokens("{CMDSEGMENT:1}");
                        if (!Int32.TryParse(rawChan, out chan)) // fails if not an integer and log it
                        {
                            Log.Write($"Invalid channel input: {rawChan}", Colors.Warning);
                            return;
                        }

                        int chnoffset = State.currentmodule.chnoffset; // 1 for A10C, varies per module

                        SendMessage.tunechn = (chan - chnoffset).ToString();
                        //Log to try and find out why this is failing.
                        Log.Write($"Parsed Channel: {chan}, Offset: {chnoffset}, Final Channel: {SendMessage.tunechn}", Colors.Text);

                        SendRadioControlMessage(SendMessage);

                        Log.Write("Select Channel " + chan.ToString(), Colors.Message);

                        UI.Playsound.Commandcomplete();

                        State.MessageReset();

                    }
                    catch (Exception e)
                    {
                        Log.Write("Error setting Radio: " + e.StackTrace, Colors.Inline);
                    }
                }

            }
        }
    }
}





