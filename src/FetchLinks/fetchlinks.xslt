<?xml version="1.0" encoding="UTF-8" ?>
<div xsl:version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<link href="$ng:installpath$ngstyles.css" type="text/css" rel="stylesheet" />
	<div class="fetchlinks" />
	$ng:description$
	<p class="ngrelatedlinks" align="right">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="concat('http://services.newsgator.com/subscriber/Related.aspx?relurl=', item/encodedlink)" /></xsl:attribute>
			Related...
		</a>
		<br/>
		<i>Contents retrieved by <a href="http://graemef.com/fetchlinks">FetchLinks</a>
		</i>
	</p>
	<p class="ngpostlinks">
		<a>
			<xsl:attribute name="href"><xsl:value-of select="item/link" /></xsl:attribute>
			<xsl:value-of select="item/link" />
		</a>
		<xsl:if test="string-length(item/commentlink) &gt; 0">
			<xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text> | <xsl:text disable-output-escaping="yes">&amp;nbsp;</xsl:text>
			<a><xsl:attribute name="href"><xsl:value-of select="item/commentlink" /></xsl:attribute>Comments</a>
		</xsl:if>
	</p>
 </div>