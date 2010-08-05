if not exist doc mkdir doc
cd doc
copy "..\Debug\HDF5DotNet.xml" comments.xml
MRefBuilder "..\Debug\HDF5DotNet.dll" /out:reflection.org 
XslTransform /xsl:"C:\Program Files\Sandcastle\ProductionTransforms\AddOverloads.xsl" reflection.org /xsl:"C:\Program Files\Sandcastle\ProductionTransforms\AddFriendlyFilenames.xsl" /out:reflection.xml 
XslTransform /xsl:"C:\Program Files\Sandcastle\ProductionTransforms\ReflectionToManifest.xsl"  reflection.xml /out:manifest.xml
if not exist output mkdir output
cd "output"
if not exist html mkdir html
if not exist icons mkdir icons
if not exist scripts mkdir scripts
if not exist styles mkdir styles
if not exist media mkdir media
copy "C:\Program Files\Sandcastle\Presentation\vs2005\icons\*" icons
copy "C:\Program Files\Sandcastle\Presentation\vs2005\scripts\*" scripts
copy "C:\Program Files\Sandcastle\Presentation\vs2005\styles\*" styles
cd .. 
BuildAssembler /config:"c:\Program Files\Sandcastle\Presentation\vs2005\Configuration\sandcastle.config" manifest.xml
XslTransform /xsl:"C:\Program Files\Sandcastle\ProductionTransforms\ReflectionToChmContents.xsl" reflection.xml /arg:html="output\html" /out:"output\Test.hhc"
copy "C:\Program Files\Sandcastle\Presentation\vs2005\Chm\test.hhp" "output\help_proj.hhp"
"C:\Program Files\HTML Help Workshop\hhc.exe" "output\help_proj.hhp"
del output\HDF5DotNet.chm
ren output\Test.chm HDF5DotNet.chm
cd ..
@PAUSE
