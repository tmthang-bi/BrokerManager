//	written	by Tan Ling	Wee	on 2 Dec 2001
//	last updated 10 Apr 2002
//	email :	fuushikaden@yahoo.com
//  Modified (very little) by Sai on 01/02/05
//  email :	sai_freelance@yahoo.com
//  Modified (for VS2005 and latest browsers) by S. Baer on 2007-08-31
//  email : bae1@sevitec.ch
//  Modified (bugfix) by S. Baer on 2008-05-29
//  email : bae1@sevitec.ch
//  Modified (more bugfix, VS2008, FF3, IE7) by S. Baer on 2009-04-22
//  email : <find.out>@sevitec.ch

/* TODO: configure calendar to your needs */
var	fixedX = -1;        // x position (-1 if to appear below control)
var	fixedY = -1;        // y position (-1 if to appear below control)
var showWeekNumber = 0; // 0 - don't show; 1 - show
var showToday = 1;	    // 0 - don't show; 1 - show

/* TODO: translate texts into your language */
var gotoString = "Go To Current Month";
var todayString = "Today is";
var weekString = "Wk";
var scrollLeftMessage = "Click to scroll to previous month. Hold mouse button to scroll automatically.";
var scrollRightMessage = "Click to scroll to next month. Hold mouse button to scroll automatically.";
var selectMonthMessage = "Click to select a month.";
var selectYearMessage = "Click to select a year.";
var selectDateMessage = "Select [date] as date."; // do not replace [date], it will be replaced by date.

var	crossobj, crossMonthObj, crossYearObj, monthSelected, yearSelected, dateSelected, omonthSelected, oyearSelected, odateSelected, monthConstructed, yearConstructed, intervalID1, intervalID2, timeoutID1, timeoutID2, ctlToPlaceValue, ctlNow, dateFormat, nStartingYear;

var startAt = 1; // 0 - sunday ; 1 - monday
var imgDir = ""; // directory for images, will be set in initPicker() method

var	bPageLoaded=false;
var	ie=document.all;

var	today =	new	Date();
var	dateNow	 = today.getDate();
var	monthNow = today.getMonth();
var	yearNow	 = today.getYear();
var	imgsrc = new Array("drop1.gif","drop2.gif","left1.gif","left2.gif","right1.gif","right2.gif");
var	img	= new Array();
var bShow = false;

function initPicker(path) {
    imgDir = path;
    
    for	(var i = 0; i < imgsrc.length; i++) {
        img[i] = new Image();
        img[i].src = imgDir + imgsrc[i]
    }

    document.write ("<div onclick='bShow=true;' id='calendar' class='div-style'><table width="+((showWeekNumber==1)?250:220)+" class='table-style'><tr class='title-background-style' ><td><table width='"+((showWeekNumber==1)?248:218)+"'><tr><td class='title-style'><B><span id='caption'></span></B></td><td align=right style='text-align: right'><a href='javascript:hideCalendar();'><IMG id='closeimage' SRC='"+imgDir+"close.gif' WIDTH='15' HEIGHT='13' BORDER='0' ALT='Close the Calendar'></a></td></tr></table></td></tr><tr><td class='body-style'><span id='content'></span></td></tr>");
    if (showToday==1) {
        document.write ("<tr class='today-style'><td><span id='lblToday'></span></td></tr>");
    }
    document.write ("</table></div><div id='selectMonth' class='div-style'></div><div id='selectYear' class='div-style'></div>");
}

// only allow characters
function checkAllowedKey(e) {
    var k = 8;

    if (window.event) {
        k = window.event.keyCode;
    } else if (e.which) {
        k = e.which;
    }
    // allowed: 0123456789-./
    return ((k >= 48) && (k <= 57)) || (k == 45) || (k == 46) || (k == 47) || (k == 8);
}

/* hides <select> and <applet> objects (for IE only) */
function hideElement(elmID, overDiv) {
  if (ie) {
    for (i = 0; i < document.all.tags(elmID).length; i++ ) {
      obj = document.all.tags( elmID )[i];
      
      if( !obj || !obj.offsetParent) {
        continue;
      }

      // Find the element's offsetTop and offsetLeft relative to the BODY tag.
      objLeft   = obj.offsetLeft;
      objTop    = obj.offsetTop;
      objParent = obj.offsetParent;
      
      while(objParent && objParent.tagName.toUpperCase() != "BODY") {
        objLeft  += objParent.offsetLeft;
        objTop   += objParent.offsetTop;
        objParent = objParent.offsetParent;
      }
  
      objHeight = obj.offsetHeight;
      objWidth = obj.offsetWidth;

      if ((overDiv.offsetLeft + overDiv.offsetWidth ) <= objLeft );
      else if(( overDiv.offsetTop + overDiv.offsetHeight ) <= objTop );
      else if( overDiv.offsetTop >= ( objTop + objHeight ));
      else if( overDiv.offsetLeft >= ( objLeft + objWidth ));
      else {
        obj.style.visibility = "hidden";
      }
    }
  }
}
 
/*
* unhides <select> and <applet> objects (for IE only)
*/
function showElement(elmID)
{
  if (ie) {
    for(i = 0; i < document.all.tags( elmID ).length; i++) {
      obj = document.all.tags( elmID )[i];
      
      if (!obj || !obj.offsetParent) {
        continue;
      }
    
      obj.style.visibility = "";
    }
  }
}


function swapImage(srcImg, destImg) {
    document.getElementById(srcImg).src = imgDir + destImg;
}

function init()	{

    if (yearNow < 1000) {
        yearNow += 1900;
    }
    crossobj = document.getElementById("calendar").style;
    hideCalendar();

    crossMonthObj = document.getElementById("selectMonth").style;
    crossYearObj = document.getElementById("selectYear").style;

    monthConstructed=false;
    yearConstructed=false;

    if (showToday==1) {
        document.getElementById("lblToday").innerHTML =	todayString + " <a class='today-style' onmousemove='window.status=\""+gotoString+"\";' onmouseout='window.status=\"\";' title='"+gotoString+";' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>"+getDayName((today.getDay()-startAt==-1)?6:(today.getDay()-startAt))+", " + dateNow + " " + monthName[monthNow].substring(0,3)	+ "	" +	yearNow	+ "</a>";
    }

    sHTML1= "<span id='spanLeft' class='title-control-normal-style' onmouseover='swapImage(\"changeLeft\",\"left2.gif\");this.className=\"title-control-select-style\";window.status=\""+scrollLeftMessage+"\";' onclick='javascript:decMonth();' onmouseout='clearInterval(intervalID1);swapImage(\"changeLeft\",\"left1.gif\");this.className=\"title-control-normal-style\";window.status=\"\";' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartDecMonth()\",500);' onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1);'>&nbsp<IMG id='changeLeft' SRC='"+imgDir+"left1.gif' width=10 height=11 BORDER=0>&nbsp</span>&nbsp;";
    sHTML1+="<span id='spanRight' class='title-control-normal-style' onmouseover='swapImage(\"changeRight\",\"right2.gif\");this.className=\"title-control-select-style\";window.status=\""+scrollRightMessage+"\";' onmouseout='clearInterval(intervalID1);swapImage(\"changeRight\",\"right1.gif\");this.className=\"title-control-normal-style\";window.status=\"\";' onclick='incMonth();' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartIncMonth()\",500);' onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1);'>&nbsp<IMG id='changeRight' SRC='"+imgDir+"right1.gif' width=10 height=11 BORDER=0>&nbsp</span>&nbsp";
    sHTML1+="<span id='spanMonth' class='title-control-normal-style' onmouseover='swapImage(\"changeMonth\",\"drop2.gif\");this.className=\"title-control-select-style\";window.status=\""+selectMonthMessage+"\";' onmouseout='swapImage(\"changeMonth\",\"drop1.gif\");this.className=\"title-control-normal-style\";window.status=\"\";' onclick='popUpMonth();'></span>&nbsp;";
    sHTML1+="<span id='spanYear'  class='title-control-normal-style' onmouseover='swapImage(\"changeYear\",\"drop2.gif\");this.className=\"title-control-select-style\";window.status=\""+selectYearMessage+"\";' onmouseout='swapImage(\"changeYear\",\"drop1.gif\");this.className=\"title-control-normal-style\";window.status=\"\";' onclick='popUpYear();'></span>&nbsp;";
    document.getElementById("caption").innerHTML  =	sHTML1;
    bPageLoaded=true;
}

function hideCalendar()	{
    if(crossobj){
        crossobj.visibility = "hidden";
        if (crossMonthObj != null) {
            crossMonthObj.visibility = "hidden";
        }
        if (crossYearObj !=	null) {
            crossYearObj.visibility = "hidden";
        };
        showElement('SELECT');
        showElement('APPLET');
    }
}


function padZero(num) {
    return (num	< 10)? '0' + num : num;
}

function constructDate(d,m,y)
{
    sTmp = dateFormat;
    sTmp = sTmp.replace	("dd","<e>");
    sTmp = sTmp.replace	("d","<d>");
    sTmp = sTmp.replace	("<e>",padZero(d));
    sTmp = sTmp.replace	("<d>",d);
    sTmp = sTmp.replace	("mmm","<o>");
    sTmp = sTmp.replace	("mm","<n>");
    sTmp = sTmp.replace	("MM","<n>");
    sTmp = sTmp.replace	("m","<m>");
    sTmp = sTmp.replace	("<m>",m+1);
    sTmp = sTmp.replace	("<n>",padZero(m+1));
    sTmp = sTmp.replace	("<o>",monthName[m]);
    return sTmp.replace ("yyyy",y);
}

function closeCalendar() {
    var	sTmp;
    hideCalendar();
    ctlToPlaceValue.value =	constructDate(dateSelected,monthSelected,yearSelected);
}

/*** Month Pulldown	***/

function StartDecMonth()
{
    intervalID1 = setInterval("decMonth()", 80);
}

function StartIncMonth()
{
    intervalID1 = setInterval("incMonth()", 80);
}

function incMonth () {
    monthSelected++;
    if (monthSelected > 11) {
        monthSelected = 0;
        yearSelected++;
    }
    constructCalendar();
}

function decMonth () {
    monthSelected--;
    if (monthSelected < 0) {
        monthSelected = 11;
        yearSelected--;
    }
    constructCalendar();
}

function constructMonth() {
    popDownYear();
    if (!monthConstructed) {
        sHTML =	"";
        for	(i=0; i<12;	i++) {
            sName =	monthName[i];
            if (i==monthSelected) {
                sName =	"<B>" +	sName +	"</B>";
            }
            sHTML += "<tr><td id='m" + i + "' onclick='monthConstructed=false;monthSelected=" + i + ";constructCalendar();popDownMonth();event.cancelBubble=true;'>&nbsp;" + sName + "&nbsp;</td></tr>";
        }
        document.getElementById("selectMonth").innerHTML = "<table width=70	class='dropdown-style' cellspacing=0 onmouseover='clearTimeout(timeoutID1);' onmouseout='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"popDownMonth()\",100);event.cancelBubble=true;'>" + sHTML +	"</table>";
        monthConstructed = true;
    }
}

function popUpMonth() {
    constructMonth();
    crossMonthObj.visibility = "visible";
    crossMonthObj.left = parseInt(crossobj.left) + 50 + 'px';
    crossMonthObj.top =	parseInt(crossobj.top) + 26 + 'px';
}

function popDownMonth()	{
    crossMonthObj.visibility = "hidden";
}

/*** Year Pulldown ***/

function incYear() {
    for	(i=0; i<7; i++){
        newYear	= (i+nStartingYear)+1;
        if (newYear==yearSelected) {
            txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;";
        } else {
            txtYear =	"&nbsp;" + newYear + "&nbsp;";
        }
        document.getElementById("y"+i).innerHTML = txtYear;
    }
    nStartingYear ++;
    bShow=true;
}

function decYear() {
    for	(i=0; i<7; i++){
        newYear	= (i+nStartingYear)-1
        if (newYear==yearSelected) {
            txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;"
        } else {
            txtYear =	"&nbsp;" + newYear + "&nbsp;";
        }
        document.getElementById("y"+i).innerHTML = txtYear;
    }
    nStartingYear --;
    bShow=true;
}

function selectYear(nYear) {
    yearSelected=parseInt(nYear+nStartingYear);
    yearConstructed=false;
    constructCalendar();
    popDownYear();
}

function constructYear() {
    popDownMonth();
    sHTML =	""
    if (!yearConstructed) {

        sHTML =	"<tr><td align='center' onmouseout='clearInterval(intervalID1);' onmousedown='clearInterval(intervalID1);intervalID1=setInterval(\"decYear()\",30);' onmouseup='clearInterval(intervalID1);'>-</td></tr>";
        j =	0;
        nStartingYear =	yearSelected-3;
        for	(i=(yearSelected-3); i<=(yearSelected+3); i++) {
            sName =	i;
            if (i==yearSelected) {
                sName =	"<B>" +	sName +	"</B>";
            }

            sHTML += "<tr><td id='y" + j + "' onclick='selectYear("+j+");event.cancelBubble=true;'>&nbsp;" + sName + "&nbsp;</td></tr>";
            j ++;
        }

        sHTML += "<tr><td align='center' onmouseout='clearInterval(intervalID2);' onmousedown='clearInterval(intervalID2);intervalID2=setInterval(\"incYear()\",30);' onmouseup='clearInterval(intervalID2);'>+</td></tr>";
        document.getElementById("selectYear").innerHTML	= "<table width=44 class='dropdown-style' onmouseover='clearTimeout(timeoutID2);' onmouseout='clearTimeout(timeoutID2);timeoutID2=setTimeout(\"popDownYear()\",100);' cellspacing=0>"	+ sHTML	+ "</table>";
        yearConstructed	= true;
    }
}

function popDownYear() {
    clearInterval(intervalID1);
    clearTimeout(timeoutID1);
    clearInterval(intervalID2);
    clearTimeout(timeoutID2);
    crossYearObj.visibility= "hidden";
}

function popUpYear() {
    var	leftOffset;
    constructYear();
    crossYearObj.visibility	= "visible";
    leftOffset = parseInt(crossobj.left) + document.getElementById("spanYear").offsetLeft;
    if (ie) {
        leftOffset += 6;
    }
    crossYearObj.left =	leftOffset + 'px';
    crossYearObj.top = parseInt(crossobj.top) +	26 + 'px';
}

/*** calendar ***/


var isoHelper = {
 getWeekNumber: function(date) {
    // 1. Convert input to Y M D
    var Y = date.getFullYear();
    var M = date.getMonth();
    var D = date.getDay();

    // 4. Find the DayOfYearNumber for Y M D
    var DayOfYearNumber = isoHelper.findDayOfYearNumber(date);

     // 5. Find the Jan1Weekday for Y (Monday=1, Sunday=7)
     var Jan1Weekday = isoHelper.findJan1Weekday(date);

     // 6. Find the Weekday for Y M D
     var WeekDay = isoHelper.findDayWeek(date);

     // 7. Find if Y M D falls in YearNumber Y-1, WeekNumber 52 or 53
     var YearNumber = Y;
     var WeekNumber = 0;
     if((DayOfYearNumber <= (8 - Jan1Weekday)) && (Jan1Weekday > 4)) {
        YearNumber = Y - 1
        if((Jan1Weekday == 5) || (Jan1Weekday == 6 && isoHelper.isLeapYear(date))) {
            WeekNumber = 53;
        } else {
            WeekNumber = 52;
        }
     }

     // 8. Find if Y M D falls in YearNumber Y+1, WeekNumber 1
     if(YearNumber == Y) {
        var I;
        if(isoHelper.isLeapYear(date)) {
            I = 366;
        } else {
            I = 365
        }

        if ((I - DayOfYearNumber) < (4 - WeekDay)) {
            YearNumber = Y + 1;
            WeekNumber = 1;
        }
     }

     // 9. Find if Y M D falls in YearNumber Y, WeekNumber 1 through 53
     if(YearNumber == Y) {
        var J;
        J = DayOfYearNumber + (7 - WeekDay) + (Jan1Weekday - 1)
        WeekNumber = J / 7;
        if(Jan1Weekday > 4 ) {
            WeekNumber -= 1;
        }
     }

     return WeekNumber;
 },
 isLeapYear: function(ddate) {
    var Y = ddate.getFullYear();
    if ((Y % 4) == 0 && (Y % 100) != 0) {
        return true;
    }
    if((Y % 400) == 0) {
        return true;
    }
    return false;
 },
 findDayOfYearNumber: function(date) {
    var months = [0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334];
    var day = date.getDate() + months[date.getMonth()];
    // only for march and onwards in leap years:
    if(isoHelper.isLeapYear(date) && date.getMonth() > 1) {
        day++;
    }
    return day;
 },
 findJan1Weekday: function(date) {
    var d = new Date(date.getFullYear(), 0, 1);
    // convert from 0=sun .. 1=mon
    // to 1=mon to 7=sun
    return(isoHelper.findDayWeek(d));
 },
 findDayWeek: function (date) {
    var WeekDay = date.getDay();
    if (startAt == 0) {
        WeekDay = WeekDay + 1;
    }
    if(WeekDay == 0) {
        WeekDay = 7;
    }
    return WeekDay;
 }
}

function constructCalendar() {
    var dateMessage;
    var	startDate =	new	Date (yearSelected,monthSelected,1);
    var	endDate	= new Date (yearSelected,monthSelected+1,1);
    endDate	= new Date(endDate	- (24*60*60*1000));
    numDaysInMonth = endDate.getDate();

    datePointer	= 0;
    dayPointer = startDate.getDay() - startAt;
    
    if (dayPointer<0) {
        dayPointer = 6;
    }

    sHTML =	"<table	border=0 class='body-style'><tr>";

    if (showWeekNumber==1) {
        sHTML += "<td width=27><b>" + weekString + "</b></td><td width=1 rowspan=7 class='weeknumber-div-style'><img src='"+imgDir+"divider.gif' width=1></td>";
    }

    for	(i=0; i<7; i++)	{
        sHTML += "<td width='27' align='right'><B>"+ getDayName(i)+"</B></td>";
    }
    sHTML +="</tr><tr>";
    
    if (showWeekNumber==1) {
        sHTML += "<td align=right>" + isoHelper.getWeekNumber(startDate) + "&nbsp;</td>";
    }

    for	(var i=1; i<=dayPointer;i++) {
        sHTML += "<td>&nbsp;</td>";
    }

    for	(datePointer=1; datePointer<=numDaysInMonth; datePointer++) {
        dayPointer++;
        sHTML += "<td align=right>";

        var sStyle="normal-day-style"; //regular day

        if ((datePointer==dateNow)&&(monthSelected==monthNow)&&(yearSelected==yearNow)) {
            //today 
            sStyle = "current-day-style";
        } else if (dayPointer % 7 == (startAt * -1) +1) {
            //end-of-the-week day
            sStyle = "end-of-weekday-style";
        }

        //selected day
        if ((datePointer==odateSelected) &&	(monthSelected==omonthSelected)	&& (yearSelected==oyearSelected)) {
            sStyle += " selected-day-style";
        }

        sHint = "";
        dateMessage = "onmousemove='window.status=\""+selectDateMessage.replace("[date]",constructDate(datePointer,monthSelected,yearSelected))+"\";' onmouseout='window.status=\"\";' ";
        sHTML += "<a class='"+sStyle+"' "+dateMessage+" title=\"" + sHint + "\" href='javascript:dateSelected="+datePointer+";closeCalendar();'>&nbsp;" + datePointer + "&nbsp;</a>";
        sHTML += "";
        if ((dayPointer+startAt) % 7 == startAt) { 
            sHTML += "</tr><tr>";
            if ((showWeekNumber==1)&&(datePointer<numDaysInMonth)) {
                sHTML += "<td align=right>" + isoHelper.getWeekNumber(new Date(yearSelected, monthSelected, datePointer + 1)) + "&nbsp;</td>";
            }
        }
    }

    document.getElementById("content").innerHTML   = sHTML;
    document.getElementById("spanMonth").innerHTML = "&nbsp;" +	monthName[monthSelected] + "&nbsp;<IMG id='changeMonth' SRC='"+imgDir+"drop1.gif' WIDTH='12' HEIGHT='10' BORDER=0>";
    document.getElementById("spanYear").innerHTML =	"&nbsp;" + yearSelected	+ "&nbsp;<IMG id='changeYear' SRC='"+imgDir+"drop1.gif' WIDTH='12' HEIGHT='10' BORDER=0>";
}

function getDayName(day) {
    if (startAt == 1) {
        day = (day + 1) % 7;
    }
    return dayName[day];
}

function popUpCalendar(ctl,	ctl2, format, sa) {
    
    // adjust close button image which is already rendered
    document.getElementById("closeimage").src = imgDir + 'close.gif';
    
    if (startAt != sa) {
        startAt = sa;
        bPageLoaded = false;
    }
    
    var	leftpos=0;
    var	toppos=0;
        
    if (bPageLoaded) {
        if (crossobj.visibility ==	"hidden") {
            ctlToPlaceValue	= ctl2;
            dateFormat=format;

            formatChar = " ";
            aFormat	= dateFormat.split(formatChar);
            if (aFormat.length < 3) {
                formatChar = "/";
                aFormat	= dateFormat.split(formatChar);
                if (aFormat.length < 3) {
                    formatChar = ".";
                    aFormat	= dateFormat.split(formatChar);
                    if (aFormat.length < 3) {
                        formatChar = "-";
                        aFormat	= dateFormat.split(formatChar);
                        if (aFormat.length < 3) {
                            // invalid date	format
                            formatChar="";
                        }
                    }
                }
            }

            tokensChanged =	0;
            if (formatChar	!= "") {
                // use user's date
                aData =	ctl2.value.split(formatChar);

                for	(i = 0; i < 3; i++) {
                    if ((aFormat[i]=="d") || (aFormat[i]=="dd")) {
                        dateSelected = parseInt(aData[i], 10);
                        tokensChanged ++;
                    } else if ((aFormat[i]=="m") || (aFormat[i]=="mm") || (aFormat[i]=="MM")) {
                        monthSelected =	parseInt(aData[i], 10) - 1;
                        tokensChanged ++;
                    } else if (aFormat[i]=="yyyy") {
                        yearSelected = parseInt(aData[i], 10);
                        tokensChanged ++;
                    } else if (aFormat[i]=="mmm") {
                        for	(j=0; j<12;	j++) {
                            if (aData[i]==monthName[j]) {
                                monthSelected=j;
                                tokensChanged ++;
                            }
                        }
                    }
                }
            }

            if ((tokensChanged!=3)||isNaN(dateSelected)||isNaN(monthSelected)||isNaN(yearSelected)) {
                dateSelected = dateNow;
                monthSelected =	monthNow;
                yearSelected = yearNow;
            }

            odateSelected=dateSelected;
            omonthSelected=monthSelected;
            oyearSelected=yearSelected;

            aTag = ctl;
            if (aTag.offsetParent) {
                do {
                    aTag = aTag.offsetParent;
                    leftpos	+= aTag.offsetLeft;
                    toppos += aTag.offsetTop;
                } while(aTag.tagName.toUpperCase() != "BODY");
            }
        
            crossobj.left =	(fixedX ==-1) ? ctl.offsetLeft + leftpos + 'px' : fixedX + 'px';
            crossobj.top = (fixedY==-1) ?ctl.offsetTop + toppos + ctl.offsetHeight + 2 + 'px' : fixedY + 'px';
            constructCalendar (1, monthSelected, yearSelected);
            crossobj.visibility = "visible";
            
            hideElement( 'SELECT', document.getElementById("calendar") );
            hideElement( 'APPLET', document.getElementById("calendar") );

            bShow = true;
        }
    } else {
        init();
        popUpCalendar(ctl, ctl2, format, sa);
    }
}

document.onkeypress = function hidecal1(evt) {
    evt = (evt) ? evt : ((event) ? event : null);
    if (evt) {
        if (evt.keyCode == 27) {
            hideCalendar();
        }
    }
}

document.onclick = function hidecal2() {
    if (!bShow) {
        hideCalendar();
    }
    bShow = false;
}
