namespace GraemeF.NewsGator
{
    using System;
    using System.Xml;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Text;

    using log4net;

    public partial class FetchLinks : NGExtension.INewsGatorExtension
    {


        #region INewsGatorExtension Members

        public void BeginRetrieve()
        {
            //MessageBox.Show("BeginRetrieve");
            // TODO:  Add FetchLinks.BeginRetrieve implementation
        }

        public void EndRetrieve()
        {
            // TODO:  Add FetchLinks.EndRetrieve implementation
        }

        public void PostProcessItem(object reference, object postItem, object appObj)
        {
            // TODO:  Add FetchLinks.PostProcessItem implementation
        }

        private string GetContent(string feedUrl, string url)
        {
            string content;
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);

            if (req is System.Net.HttpWebRequest)
            {
                System.Net.HttpWebRequest httpReq = (System.Net.HttpWebRequest)req;

                httpReq.UserAgent = "NewsGator FetchLinks extension/0.2.0 (http://graemef.com)";
                httpReq.Referer = feedUrl;
            }

            using (System.Net.WebResponse response = req.GetResponse())
            {
                content = HtmlUtil.DecodeData(req.GetResponse());
                return HtmlUtil.SetBaseUri(content, response.ResponseUri.AbsoluteUri);
            }
        }

        private string replace = "<div class=\"fetchlinks\" />";
        public object PreProcessItem(NGExtension.PostInfo postInfo, System.Xml.XmlNode originalItem, out bool createPost)
        {
            if (postInfo.Description.IndexOf(replace) >= 0)
            {
                string content = String.Empty;

                if (postInfo.PostLink != String.Empty && postInfo.PostLink != null)
                {
                    try
                    {
                        content = GetContent(postInfo.FromAddr, postInfo.PostLink);
                    }
                    catch
                    {
                        // content = string.Format("<a href=\"{0}\">Click here</a> to read this item.", new Uri( new Uri(postInfo.FromAddr), postInfo.PostLink, false).AbsoluteUri );
                    }
                }
                //				else
                //					content = "[This item has no content]";

                postInfo.Description = postInfo.Description.Replace(replace, content);
            }

            createPost = true;
            return postInfo;
        }

        #endregion
    }
}
