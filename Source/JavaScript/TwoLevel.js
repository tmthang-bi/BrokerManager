/*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	TwoLevel v1.02, based on ADxMenu v4
	www.aplus.co.yu/lab/nestedtabs3/
	- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	(c) Copyright 2006 and on, Aleksandar Vacic, www.aplus.co.yu
		This work is licensed under the Creative Commons Attribution License.
		To view a copy of this license, visit http://creativecommons.org/licenses/by/2.0/ or
		send a letter to Creative Commons, 559 Nathan Abbott Way, Stanford, California 94305, USA
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/
var TwoLevel_nCnt = 100;	//	time in milliseconds after which retry should be made.

function TwoLevel_IESetup() {
	var aTmp2, i, j, oLI, oTmp;
	var aTmp = xGetElementsByClassName("twolevel", document, "ul");
	if (aTmp.length) {
		for (i=0;i<aTmp.length;i++) {
			aTmp2 = aTmp[i].getElementsByTagName("li");
			for (j=0;j<aTmp2.length;j++) {
				oLI = aTmp2[j];
				//	if item has submenu, then make it hoverable
				if (oTmp = oLI.getElementsByTagName("ul")) {
					oLI.UL = oTmp[0];	//	direct submenu
					oLI.CTNR = aTmp[i];	//	parent list, the main menu, container
					//	li:hover
					oLI.onmouseenter = function() {
						this.className += " twolevelhover";
						if (this.UL && WCH) WCH.Apply( this.UL, this.CTNR, true );
					};
					//	li:blur
					oLI.onmouseleave = function() {
						this.className = this.className.replace(/twolevelhover/,"");
						if (this.UL && WCH) WCH.Discard( this.UL, this.CTNR );
					};
				}
			}	//for-li.submenu
		}	//for-ul.twolevel
	//	sometimes DOM is just not ready even though IE says it is, thus with this we give him several tries
	//		adjust as needed
	} else if (TwoLevel_nCnt < 2000) {
		setTimeout("TwoLevel_IESetup()", TwoLevel_nCnt);
		TwoLevel_nCnt *= 2.5;	//	it will retry after 100ms, 250ms, 625ms, 1525ms
		TwoLevel_nCnt = parseInt(TwoLevel_nCnt, 10);
	}
}

//	adds support for WCH. if you need WCH, then load WCH.js BEFORE this file
if (typeof(WCH) == "undefined") WCH = null;


/* allows instant "window.onload" (actually DOM.onload) function execution
	credits: Dean Edwards/Matthias Miller/John Resig/Rob Chenny
	http://dean.edwards.name/weblog/2006/06/again/
	http://www.cherny.com/webdev/27/domloaded-updated-again
*/
var TwoLevel_proto = "javascript:void(0)";
if (location.protocol == "https:") TwoLevel_proto = "src=//0";
document.write("<scr"+"ipt id=__ie_onload defer " + TwoLevel_proto + "><\/scr"+"ipt>");
var TwoLevel_script = document.getElementById("__ie_onload");
TwoLevel_script.onreadystatechange = function() {
	if (this.readyState == "complete") {
		TwoLevel_IESetup();
	}
};


/* xGetElementsByClassName()
   Returns an array of elements which are
   descendants of parentEle and have tagName and clsName.
   If parentEle is null or not present, document will be used.
   if tagName is null or not present, "*" will be used.
	credits: Mike Foster, cross-browser.com.
*/
function xGetElementsByClassName(clsName, parentEle, tagName) {
	var elements = null;
	var found = new Array();
	var re = new RegExp('\\b'+clsName+'\\b');
	if (!parentEle) parentEle = document;
	if (!tagName) tagName = '*';
	if (parentEle.getElementsByTagName) {elements = parentEle.getElementsByTagName(tagName);}
	else if (document.all) {elements = document.all.tags(tagName);}
	if (elements) {
		for (var i = 0; i < elements.length; ++i) {
			if (elements[i].className.search(re) != -1) {
				found[found.length] = elements[i];
			}
		}
	}
	return found;
}
