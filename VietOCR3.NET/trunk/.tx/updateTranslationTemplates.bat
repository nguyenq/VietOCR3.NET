@echo off
cd ..
set PATH=%PATH%;.\.tx
nxslt2 BulkDialog.resx       .tx\strip_resx.xsl -o .tx\BulkDialog.resx
nxslt2 ChangeCaseDialog.resx .tx\strip_resx.xsl -o .tx\ChangeCaseDialog.resx
nxslt2 DownloadDialog.resx   .tx\strip_resx.xsl -o .tx\DownloadDialog.resx
nxslt2 GUI.resx              .tx\strip_resx.xsl -o .tx\GUI.resx
nxslt2 ImageInfoDialog.resx  .tx\strip_resx.xsl -o .tx\ImageInfoDialog.resx
nxslt2 OptionsDialog.resx    .tx\strip_resx.xsl -o .tx\OptionsDialog.resx
nxslt2 SplitPdfDialog.resx   .tx\strip_resx.xsl -o .tx\SplitPdfDialog.resx
nxslt2 TrackbarDialog.resx   .tx\strip_resx.xsl -o .tx\TrackbarDialog.resx

nxslt2 Controls\TextBoxContextMenuStrip.resx .tx\strip_resx.xsl -o .tx\TextBoxContextMenuStrip.resx

nxslt2 Properties\Resources.resx .tx\strip_Resources_resx.xsl -o .tx\Resources.resx
pause
