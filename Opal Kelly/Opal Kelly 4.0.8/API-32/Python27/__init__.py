#-------------------------------------------------------------------------
# __init__.py
#
# The presence of this file turns this directory into a Python package.
#-------------------------------------------------------------------------

import __version__
__version__ = __version__.VERSION_STRING


# Load the package namespace with the core classes and such
from ok import *

