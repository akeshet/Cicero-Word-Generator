
	function SplitScreen (nonScrollingRegionId, scrollingRegionId) {

		// store references to the two regions
		this.nonScrollingRegion = document.getElementById(nonScrollingRegionId);
		this.scrollingRegion = document.getElementById(scrollingRegionId);

		// set the scrolling settings
		document.body.style.margin = "0px";
		document.body.style.overflow = "hidden";
		this.scrollingRegion.style.overflow = "auto";

		// fix the size of the scrolling region
		this.resize(null);

		// add an event handler to resize the scrolling region when the window is resized		
		registerEventHandler(window, 'resize', getInstanceDelegate(this, "resize"));

	}

	SplitScreen.prototype.resize = function(e) {
		this.scrollingRegion.style.height = document.body.clientHeight - this.nonScrollingRegion.offsetHeight;
		this.scrollingRegion.style.width = document.body.clientWidth;
	}
