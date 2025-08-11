using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VAICOM.Servers;
using VAICOM.Static;

namespace VAICOM
{
    namespace Extensions
    {
        namespace Kneeboard
        {

            // core class for kneeboard messages

            public class KneeboardMessage
            {
                public int eventid;
                public bool? dictmode;
                public double opacity;
                public bool? autoswitch;
                public bool? switchpage;

                // payload:

                public KneeboardServerData serverdata;
                public KneeboardUnitsData unitsdata;
                public KneeboardUnitsDetails unitsdetails;
                public LogData logdata;
                public AliasData aliasdata;

                public KneeboardMessage()
                {
                    eventid = 4000; // default
                    opacity = State.activeconfig.KneeboardOpacity;
                    dictmode = State.Proxy.Dictation.IsOn();
                    autoswitch = State.activeconfig.KneeboardlinkPTT;
                    switchpage = false; // only when explicitly set true 
                }
            }

            public class AliasData
            {
                public string category;
                public SortedDictionary<string, List<string>> content;
                public int chunk;

                public AliasData(string cat, SortedDictionary<string, List<string>> cont)
                {
                    category = cat;
                    content = cont;
                    chunk = 0;
                }
            }

            public class LogData
            {
                public string category;
                public string content;
                public int timer;

                public LogData(string cat, string cont)
                {
                    category = cat;
                    content = cont;
                }
            }

            public class KneeboardServerData
            {
                public string theater;
                public string dcsversion;
                public string aircraft;
                public int flightsize;
                public string playerusername;
                public string playercallsign;
                public string coalition;
                public string sortie;
                public string task;
                public string country;
                public string missiontitle;
                public string missionbriefing;
                public string missiondetails;
                public bool multiplayer;

                public KneeboardServerData()
                {
                    theater = State.currentstate.theatre;
                    dcsversion = State.currentstate.dcsversion;
                    aircraft = State.currentstate.id;
                    flightsize = State.currentstate.availablerecipients["Flight"].Count;
                    playerusername = State.currentstate.playerusername;
                    playercallsign = State.currentstate.playercallsign;
                    coalition = State.currentstate.playercoalition.ToUpper();
                    Int32.TryParse(State.currentstate.sortie, out int daytimeinsecs);
                    int hr = (daytimeinsecs / 3600);
                    int min = (daytimeinsecs - hr * 3600) / 60;
                    sortie = "TO " + KneeboardHelper.theatercode(State.currentstate.theatre) + " MST " + hr.ToString().PadLeft(2, '0') + ":" + min.ToString().PadRight(2, '0') + " " + KneeboardHelper.theatertimezonestring(State.currentstate.theatre);
                    task = State.currentstate.task;
                    country = State.currentstate.country;
                    missiontitle = State.currentstate.missiontitle;
                    missionbriefing = State.currentstate.missionbriefing;
                    missiondetails = State.currentstate.missiondetails;
                    multiplayer = State.currentstate.multiplayer;
                }

            }

            public class KneeboardUnitSummary
            {
                public string cat;
                public string code;
                public string callsign;
                public string alias;
                public string range;
                public string bearing;
                public string alt;
                public string frq;
                public string istuned;
                public string humanname;
                public List<string> altfreq;

            }

            public class KneeboardUnitsDetails
            {
                public string category;
                public List<string> unitsummary;
                public int timer;

                public KneeboardUnitsDetails(string recipientcat, List<string> contents, bool AOCS)
                {
                    string cat;

                    if (recipientcat.Equals("AOCS"))
                    {
                        cat = "Aux";
                    }
                    else
                    {
                        cat = recipientcat;
                    }

                    category = cat.ToUpper();
                    unitsummary = contents;
                }
            }

            public class KneeboardUnitsData
            {
                public string category;
                public List<string> unitslist;
                public int timer;

                public KneeboardUnitsData(string recipientcat, bool AOCS)
                {
                    category = recipientcat.ToUpper();

                    string cat;

                    if (recipientcat.Equals("AOCS"))
                    {
                        cat = "Aux";
                    }
                    else
                    {
                        cat = recipientcat;
                    }

                    unitslist = new List<string>();
                    List<KneeboardUnitSummary> units = new List<KneeboardUnitSummary>();

                    Log.Write($"Processing {State.currentstate.availablerecipients[cat].Count} Flight units for kneeboard.", Colors.Text);

                    foreach (Server.DcsUnit unit in State.currentstate.availablerecipients[cat])
                    {
                        try
                        {
                            string altfreqs = "";

                            KneeboardUnitSummary descr = new KneeboardUnitSummary();

                            descr.cat = cat;
                            descr.code = "XXX";
                            descr.callsign = unit.callsign;

                            string searchcallsign = Regex.Replace(unit.callsign.Replace("-", ""), "[0-9]", "");

                            var FoundKey = Database.Aliases.airecipients.FirstOrDefault(x =>
                                (x.Value.Equals(searchcallsign) ||
                                 x.Value.Equals(searchcallsign.ToLower()) ||
                                 x.Value.Equals(unit.fullname) ||
                                 x.Value.Equals(unit.fullname.ToLower()))).Key;

                            if (!FoundKey.Equals(null))
                            {
                                descr.alias = FoundKey;
                            }
                            else
                            {
                                string substract = Regex.Replace(unit.callsign.Replace("-", ""), "[0-9]", "");
                                descr.alias = substract.Equals("") ? unit.callsign : substract;
                            }

                            descr.istuned = unit.isunittuned();

                            if (!State.currentmodule.Theme.Equals("WWII"))
                            {
                                // Prioritize UHF, then VHF, then FM
                                string selectedFreq = null;

                                // Check for UHF frequencies
                                selectedFreq = unit.altfreq.FirstOrDefault(freq => unit.getmodstr().Contains("UHF"));
                                if (selectedFreq == null)
                                {
                                    // Check for VHF frequencies
                                    selectedFreq = unit.altfreq.FirstOrDefault(freq => unit.getmodstr().Contains("VHF"));
                                }
                                if (selectedFreq == null)
                                {
                                    // Check for FM frequencies
                                    selectedFreq = unit.altfreq.FirstOrDefault(freq => unit.getmodstr().Contains("FM"));
                                }

                                // If no alternate frequency matches, fall back to the main frequency
                                if (selectedFreq != null)
                                {
                                    descr.frq = unit.getmodstr() + " " + Helpers.Common.NormalizeFreqString(selectedFreq);
                                }
                                else
                                {
                                    descr.frq = unit.getmodstr() + " " + unit.getfreqstr();
                                }
                            }
                            else
                            {
                                // For WWII theme, use the main frequency
                                descr.frq = unit.getmodstr() + " " + unit.getfreqstr();
                            }

                            if (AOCS)
                            {
                                descr.bearing = unit.getbearingstr();
                                descr.range = unit.getrangestr();
                                descr.alt = unit.getaltstr();
                                descr.humanname = unit.gethumanname();
                                descr.altfreq = unit.altfreq;

                                foreach (string alt in descr.altfreq)
                                {
                                    altfreqs += Helpers.Common.NormalizeFreqString(alt) + " ";
                                }
                            }

                            units.Add(descr);

                            string lineitem = descr.frq + " " + "[" + descr.alias + "]" + descr.istuned + " " + descr.callsign + " " + descr.bearing + " " + descr.range + " " + descr.alt + " " + altfreqs;

                            unitslist.Add(lineitem);
                        }
                        catch (Exception x)
                        {
                            Log.Write(x.Message, Colors.Inline);
                        }
                    }

                    Log.Write($"Sending {unitslist.Count} Flight units to kneeboard.", Colors.Text);
                }
            }

        }
    }
}
