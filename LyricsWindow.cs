using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using Client;

using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace LyricsPlugin
{
    internal partial class LyricsWindow : Form
    {
        public delegate void InvD();

        const String LYRICWIKI_URL = "http://lyrics.wikia.com/api.php";
        new PClient Parent;

        public LyricsWindow()
        {
            InitializeComponent();
        }

        public void UIInit(PClient parent)
        {
            this.Parent = parent;

            parent.SongPlayed += new SongEventHandler(parent_SongPlayed);

            this.Show();
        }

        void DoInvoke(InvD d)
        {
            if (!Visible || IsDisposed) return;

            if (this.InvokeRequired)
            {
                this.Invoke(d);
            }
            else
                d();
        }

        void displayNotFound(String title, String artist)
        {
            this.setWebBrowserHTML(String.Format(@"<html>
                                                    <head>
                                                        <title></title>
                                                    </head>
                                                    <body>
                                                        Unable to find lyrics for <strong>{0}</strong> by <strong>{1}</strong>
                                                    </body>
                                                </html>", title, artist));
        }

        void setWebBrowserHTML(String html)
        {
            DoInvoke(delegate()
            {
                webbrowser.DocumentText = html;
            });
        }

        void parent_SongPlayed(object o, SongEventArgs e)
        {
            String artist = HttpUtility.UrlEncode(e.Song.Artist);
            String title = HttpUtility.UrlEncode(e.Song.Title);

            Thread grab_lyrics = new Thread(new ThreadStart(delegate()
            {
                String sz_api_url = String.Format("{0}?fmt=xml&artist={1}&song={2}", LyricsWindow.LYRICWIKI_URL, artist, title);
                Uri api_url = new Uri(sz_api_url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api_url);
                request.Method = "GET";
            
                XmlDocument xdoc = new XmlDocument();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (System.IO.Stream stream = response.GetResponseStream())
                        xdoc.Load(stream);

                XmlNode page_id_node = xdoc.SelectNodes("/LyricsResult/page_id")[0];
                if (page_id_node == null || page_id_node.InnerText.Length == 0)
                {
                    this.displayNotFound(e.Song.Title, e.Song.Artist);
                    return;
                }
            
                XmlNode url_node = xdoc.SelectNodes("/LyricsResult/url")[0];
                String lyric_url = url_node.InnerText;
            
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(lyric_url);

                HtmlNode lyrics = doc.DocumentNode.SelectSingleNode("//div[@class='lyricbox']");
                if (lyrics == null)
                {
                    this.displayNotFound(e.Song.Title, e.Song.Artist);
                    return;
                }

                foreach (HtmlNode ad in lyrics.SelectNodes("//div[@class='rtMatcher']"))
                    ad.Remove();

                String display_html = String.Format("<h2>{0}</h2><h3>{1}</h3>", e.Song.Title, e.Song.Artist) + lyrics.InnerHtml;
                this.setWebBrowserHTML(display_html);
            }));

            grab_lyrics.Start();
        }
    }
}
