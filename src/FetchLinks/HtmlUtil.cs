#region Using directives

using System;
using System.Text;
using HtmlAgilityPack;
using System.IO;
using System.Diagnostics;

#endregion

namespace GraemeF.NewsGator
{
    public abstract partial class HtmlUtil
    {
        /// <summary>
        /// See http://blogs.msdn.com/feroze_daud/archive/2004/03/30/104440.aspx
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static String DecodeData(System.Net.WebResponse w)
        {
            //
            // first see if content length header has charset = calue
            //
            String charset = null;
            String ctype = w.Headers["content-type"];
            if (ctype != null)
            {
                int ind = ctype.IndexOf("charset=");
                if (ind != -1)
                {
                    charset = ctype.Substring(ind + 8);
                    Debug.WriteLine("CT: charset=" + charset);
                }
            }

            // save data to a memorystream
            MemoryStream rawdata = new MemoryStream();
            byte[] buffer = new byte[1024];
            Stream rs = w.GetResponseStream();
            int read = rs.Read(buffer, 0, buffer.Length);
            while (read > 0)
            {
                rawdata.Write(buffer, 0, read);
                read = rs.Read(buffer, 0, buffer.Length);
            }

            rs.Close();

            //
            // if ContentType is null, or did not contain charset, we search in body
            //
            if (charset == null)
            {
                MemoryStream ms = rawdata;
                ms.Seek(0, SeekOrigin.Begin);

                StreamReader srr = new StreamReader(ms, Encoding.ASCII);
                String meta = srr.ReadToEnd();

                if (meta != null)
                {
                    int start_ind = meta.IndexOf("charset=");
                    int end_ind = -1;
                    if (start_ind != -1)
                    {
                        end_ind = meta.IndexOf("\"", start_ind);
                        if (end_ind != -1)
                        {
                            int start = start_ind + 8;
                            charset = meta.Substring(start, end_ind - start + 1);
                            charset = charset.TrimEnd(new Char[] { '>', '"' });
                            Debug.WriteLine("META: charset=" + charset);
                        }
                    }
                }
            }

            Encoding e = null;
            if (charset == null)
            {
                e = Encoding.ASCII; //default encoding
            }
            else
            {
                try
                {
                    e = Encoding.GetEncoding(charset);
                }
                catch (Exception ee)
                {
                    Debug.WriteLine("Exception: GetEncoding: " + charset);
                    Debug.WriteLine(ee.ToString());
                    e = Encoding.ASCII;
                }
            }

            rawdata.Seek(0, SeekOrigin.Begin);

            StreamReader sr = new StreamReader(rawdata, e);

            String s = sr.ReadToEnd();

            return s;//.ToLower();
        }

        public static string SetBaseUri(string content, string baseUri)
        {
//            if (content.IndexOf("<head>") >= 0)
//                return content.Replace("<head>", String.Format("<head><base href=\"{0}\" />", response.ResponseUri.AbsoluteUri));
//
//            if (content.IndexOf("<HEAD>") >= 0)
//                return content.Replace("<HEAD>", String.Format("<HEAD><base href=\"{0}\" />", response.ResponseUri.AbsoluteUri));

            // Tidy HTML and rebase
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(content);

            HtmlNode head = doc.DocumentNode.SelectSingleNode("/html/head");

            if (head == null)
                head = doc.DocumentNode.SelectSingleNode("/head");

            if (head == null)
            {
                head = doc.CreateElement("head");
                doc.DocumentNode.PrependChild(head);
            }

            if (head != null)
            {
                HtmlNode baseElement = head.SelectSingleNode("base");
                if (baseElement == null)
                {
                    baseElement = doc.CreateElement("base");
                    head.PrependChild(baseElement);
                }

                baseElement.SetAttributeValue("href", baseUri);
            }

            return doc.DocumentNode.OuterHtml;
        }

    }
}
