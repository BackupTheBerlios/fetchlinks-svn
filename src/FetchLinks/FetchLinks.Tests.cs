#if TEST

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.IO;
using System.Diagnostics;
using MbUnit.Core.Framework;
using MbUnit.Framework;

#endregion

namespace GraemeF.NewsGator
{
    public partial class FetchLinks : NGExtension.INewsGatorExtension
    {
        [TestFixture]
        public class Tests
        {
            [Test]
            public void graemefdotcom()
            {
                FetchLinks f = new FetchLinks();
                f.GetContent("http://graemef.com", "http://graemef.com");
            }
        }
    }
}
#endif