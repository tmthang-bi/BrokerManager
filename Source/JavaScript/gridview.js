﻿// JScript File
function CheckAll(me)
{
    var index = me.name.indexOf('_');  
    var prefix = me.name.substr(0,index); 
    for(i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (me.name != o.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    // Must be this way
                    o.checked = !me.checked; 
                    o.click(); 
                }
            }
        } 
    } 
}
function ApplyStyle(me, selectedForeColor, selectedBackColor, foreColor, backColor, bold, checkBoxHeaderId) 
{ 
    var td = me.parentNode; 
    if (td == null) 
        return; 
        
    var tr = td.parentNode;
    if (me.checked)
    { 
       tr.style.fontWeight = 700; // bold
       tr.style.color = selectedForeColor; 
       tr.style.backgroundColor = selectedBackColor; 
    } 
    else 
    { 
       document.getElementById(checkBoxHeaderId).checked = false;
       tr.style.fontWeight = bold; 
       tr.style.color = foreColor; 
       tr.style.backgroundColor = backColor; 
    } 
}

