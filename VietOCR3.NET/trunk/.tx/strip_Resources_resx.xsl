<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="yes" />
	<xsl:template match="root">
		<root>
			<xsl:copy-of select="/root/xsd:schema" xmlns:xsd="http://www.w3.org/2001/XMLSchema"/>
			<xsl:copy-of select="/root/resheader"/>
			<xsl:for-each select="/root/data">
				<xsl:sort select="@name" />
				<xsl:if test="(@name and @xml:space)">
					<xsl:copy-of select="."/>
				</xsl:if>
			</xsl:for-each>
		</root>
	</xsl:template>
</xsl:stylesheet>