<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="yes" />
	<xsl:template match="root">
		<root>
			<xsl:copy-of select="/root/xsd:schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema"/>
			<xsl:copy-of select="/root/resheader"/>
			<xsl:for-each select="/root/data">
				<xsl:sort select="@name" />
				<xsl:if test="(@name and ((substring(@name, string-length(@name) - 4) = '.Text') or (substring(@name, string-length(@name) - 11) = '.ToolTipText') or (substring(@name, string-length(@name) - 4) = '.Size') or (substring(@name, string-length(@name) - 8) = '.Location') or (substring(@name, string-length(@name) - 10) = '.ClientSize')))">
					<xsl:copy-of select="."/>
				</xsl:if>
			</xsl:for-each>
		</root>
	</xsl:template>
</xsl:stylesheet>
