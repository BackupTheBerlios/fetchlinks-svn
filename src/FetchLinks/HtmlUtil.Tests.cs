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
    public abstract partial class HtmlUtil
    {
        [TestFixture]
        public class Tests
        {
            private void RunTest(string test)
            {
                string result = SetBaseUri(test, "http://graemef.com");

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);

                Assert.AreEqual("http://graemef.com", doc.DocumentNode.SelectSingleNode("//head/base").GetAttributeValue("href", null));
            }

            [Test]
            public void TestChangeBaseHref()
            {
                RunTest("<head><base href='boo' /></head><body><h1>Hello</h1>");
            }

            [Test]
            public void TestCaps()
            {
                RunTest("<hEaD><BaSe hREF='boo' /></HeAd><BOdY><h1>Hello</h1>");
            }

            [Test]
            public void TestMissingHead()
            {
                RunTest("<h1>Hello</h1>");
            }

            [Test]
            public void HeadNoBase()
            {
                RunTest("<head/><h1>Hello</h1>");
            }

            [Test]
            public void HeadBaseNoHref()
            {
                RunTest("<head><base/></head><h1>Hello</h1>");
            }

            [Test]
            public void RealXhtml()
            {
                RunTest(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
<head>
  <title>graemef.com | there's nothing quite like whinging in public</title>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
<base href=""http://notreally.com/"" />
<style type=""text/css"" media=""all"">
@import url(misc/drupal.css);
</style>
<link rel=""alternate"" type=""application/atom+xml"" title=""Atom"" href=""?q=atom/feed"" />
<style type=""text/css"">@import url(http://graemef.com/modules/project/project.css);</style>
<link rel=""alternate"" type=""application/rss+xml"" title=""RSS"" href=""?q=node/feed"" />

  <style type=""text/css"" media=""all"">@import ""themes/xtemplate/pushbutton/xtemplate.css"";</style>

</head>");
            }
        }
    }
}
#endif