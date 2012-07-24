using System;
using System.Collections.Generic;
using System.Text;
using Client;

namespace LyricsPlugin
{
    public class LyricsPlugin : PPlugin {
        LyricsWindow lyrics;

        public void Stop() { }
        public void Start(PClient parent) {
            lyrics = new LyricsWindow();
            lyrics.UIInit(parent);
        }

        public Proxy.ConfigPage GetTab() { return null; }

        public string Name {
            get { return "Lyrics"; }
        }
    }
}
